<?php
// islem.php
require_once 'db.php';

// Test için varsayılan kullanıcı ID'si. (Sonradan Session'dan alınacak)
$user_id = 1; 

// Hangi işlemin yapılacağını URL'den (?islem=ekle gibi) alıyoruz
$islem = $_GET['islem'] ?? '';

// --- 1. SEPETE ÜRÜN EKLEME ---
if ($islem == 'ekle') {
    $product_id = $_POST['product_id'];
    $quantity = $_POST['quantity'] ?? 1;

    // Önce ürün kullanıcının sepetinde zaten var mı diye kontrol edelim
    $kontrol = $db->prepare("SELECT id, quantity FROM cart WHERE user_id = ? AND product_id = ?");
    $kontrol->execute([$user_id, $product_id]);
    $var_mi = $kontrol->fetch(PDO::FETCH_ASSOC);

    if ($var_mi) {
        // Ürün zaten sepette varsa, yeni bir satır eklemek yerine mevcut miktarı artırıyoruz
        $yeni_miktar = $var_mi['quantity'] + $quantity;
        $guncelle = $db->prepare("UPDATE cart SET quantity = ? WHERE id = ?");
        $guncelle->execute([$yeni_miktar, $var_mi['id']]);
    } else {
        // Ürün sepette yoksa yeni kayıt olarak ekliyoruz
        $ekle = $db->prepare("INSERT INTO cart (user_id, product_id, quantity) VALUES (?, ?, ?)");
        $ekle->execute([$user_id, $product_id, $quantity]);
    }
    
    // İşlem bitince sepet sayfasına geri yönlendir (sepet.php sayfasını oluşturacağız)
    header("Location: sepet.php?durum=basarili");
    exit;
}

// --- 2. SEPETTEN TEKİL ÜRÜN ÇIKARMA ---
elseif ($islem == 'cikar') {
    // Silinecek sepet satırının ID'sini alıyoruz
    $cart_id = $_GET['cart_id'];
    
    // Güvenlik için sadece kendi sepetindeki ürünü silebilmesini sağlıyoruz (user_id = ?)
    $sil = $db->prepare("DELETE FROM cart WHERE id = ? AND user_id = ?");
    $sil->execute([$cart_id, $user_id]);
    
    header("Location: sepet.php?durum=silindi");
    exit;
}

// --- 3. SEPETİ KOMPLE TEMİZLEME ---
elseif ($islem == 'temizle') {
    // Kullanıcının sepetindeki tüm kayıtları siliyoruz
    $temizle = $db->prepare("DELETE FROM cart WHERE user_id = ?");
    $temizle->execute([$user_id]);
    
    header("Location: sepet.php?durum=temizlendi");
    exit;
}

// --- 4. SİPARİŞİ TAMAMLA (TRANSACTION KULLANARAK) ---
elseif ($islem == 'siparis_ver') {
    try {
        // İşlemi (Transaction) başlatıyoruz
        $db->beginTransaction();

        // 1. Sepetteki ürünleri ve ara toplamları çek
        $sepet_sorgu = $db->prepare("
            SELECT c.product_id, c.quantity, p.price, (c.quantity * p.price) as ara_toplam
            FROM cart c
            JOIN products p ON c.product_id = p.id
            WHERE c.user_id = ?
        ");
        $sepet_sorgu->execute([$user_id]);
        $sepet_urunleri = $sepet_sorgu->fetchAll(PDO::FETCH_ASSOC);

        // Sepet boşsa işlemi iptal et
        if (count($sepet_urunleri) == 0) {
            header("Location: sepet.php?durum=bos");
            exit;
        }

        // Toplam tutarı hesapla
        $genel_toplam = 0;
        foreach ($sepet_urunleri as $urun) {
            $genel_toplam += $urun['ara_toplam'];
        }

        // 2. `orders` (Ana sipariş) tablosuna kayıt at
        $siparis_ekle = $db->prepare("INSERT INTO orders (user_id, total_amount, status) VALUES (?, ?, 'aktif')");
        $siparis_ekle->execute([$user_id, $genel_toplam]);
        
        // Eklenen son siparişin ID'sini alıyoruz ki ürünleri bu ID'ye bağlayalım
        $order_id = $db->lastInsertId();

        // 3. `order_items` (Sipariş detayları) tablosuna ürünleri tek tek ekle
        $detay_ekle = $db->prepare("INSERT INTO order_items (order_id, product_id, quantity, price) VALUES (?, ?, ?, ?)");
        foreach ($sepet_urunleri as $urun) {
            $detay_ekle->execute([$order_id, $urun['product_id'], $urun['quantity'], $urun['price']]);
        }

        // 4. Siparişe dönüşen sepeti temizle
        $sepet_temizle = $db->prepare("DELETE FROM cart WHERE user_id = ?");
        $sepet_temizle->execute([$user_id]);

        // Her şey yolundaysa tüm işlemleri veritabanına onayla
        $db->commit();

        header("Location: siparisler.php?durum=basarili");
        exit;

    } catch (Exception $e) {
        // Eğer yukarıdaki adımların birinde hata çıkarsa, hiçbir şeyi kaydetme, başa dön
        $db->rollBack();
        die("Sipariş verilirken bir hata oluştu: " . $e->getMessage());
    }
}
?>