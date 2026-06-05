<?php
require_once 'db.php';
$user_id = 1; // Test kullanıcımız

// Sepet verilerini ürün bilgileriyle birleştirerek çekiyoruz
$sorgu = $db->prepare("
    SELECT c.id as cart_id, c.quantity, p.name, p.price, (c.quantity * p.price) as ara_toplam
    FROM cart c
    JOIN products p ON c.product_id = p.id
    WHERE c.user_id = ?
");
$sorgu->execute([$user_id]);
$sepet_urunleri = $sorgu->fetchAll(PDO::FETCH_ASSOC);

$genel_toplam = 0;
?>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Sepetim - Teknoloji Mağazası</title>
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
            color: #333;
        }

        /* Navbar */
        .navbar {
            background-color: #2c3e50;
            color: white;
            padding: 15px 50px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
        }
        .navbar a {
            color: white;
            text-decoration: none;
            font-weight: bold;
        }

        .container {
            max-width: 1000px;
            margin: 40px auto;
            padding: 0 20px;
        }

        h1 {
            color: #2c3e50;
            border-bottom: 2px solid #3498db;
            padding-bottom: 10px;
            margin-bottom: 30px;
        }

        /* Sepet Tablosu Stil */
        .cart-table {
            width: 100%;
            border-collapse: collapse;
            background: white;
            border-radius: 10px;
            overflow: hidden;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            margin-bottom: 30px;
        }
        .cart-table th {
            background-color: #34495e;
            color: white;
            text-align: left;
            padding: 15px;
        }
        .cart-table td {
            padding: 15px;
            border-bottom: 1px solid #eee;
        }
        .cart-table tr:last-child td {
            border-bottom: none;
        }

        /* Fiyat Renkleri */
        .price { font-weight: bold; color: #2c3e50; }
        .subtotal { font-weight: bold; color: #27ae60; }

        /* İşlem Butonları */
        .btn {
            padding: 8px 15px;
            border-radius: 5px;
            text-decoration: none;
            font-weight: bold;
            display: inline-block;
            transition: 0.3s;
            cursor: pointer;
            border: none;
        }
        .btn-remove {
            color: #e74c3c;
            font-size: 0.9rem;
        }
        .btn-remove:hover {
            text-decoration: underline;
        }
        
        /* Sepet Özeti ve Ana Butonlar */
        .cart-summary {
            background: white;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            text-align: right;
        }
        .total-price {
            font-size: 1.5rem;
            margin-bottom: 20px;
        }
        .total-price span {
            color: #27ae60;
            font-weight: bold;
        }

        .actions {
            display: flex;
            justify-content: flex-end;
            gap: 15px;
        }
        .btn-clear {
            background-color: #95a5a6;
            color: white;
        }
        .btn-clear:hover { background-color: #7f8c8d; }
        
        .btn-order {
            background-color: #27ae60;
            color: white;
            padding: 12px 30px;
            font-size: 1.1rem;
        }
        .btn-order:hover { background-color: #219150; }

        /* Boş Sepet Uyarısı */
        .empty-cart {
            text-align: center;
            padding: 50px;
            background: white;
            border-radius: 10px;
        }
    </style>
</head>
<body>

    <div class="navbar">
        <h2>⚡ Teknoloji Mağazası</h2>
        <a href="index.php">⬅ Ürünlere Dön</a>
    </div>

    <div class="container">
        <h1>Sepetim</h1>

        <?php if (count($sepet_urunleri) > 0): ?>
            <table class="cart-table">
                <thead>
                    <tr>
                        <th>Ürün Adı</th>
                        <th>Birim Fiyat</th>
                        <th>Adet</th>
                        <th>Toplam</th>
                        <th>İşlem</th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach ($sepet_urunleri as $item): ?>
                        <?php $genel_toplam += $item['ara_toplam']; ?>
                        <tr>
                            <td><strong><?= htmlspecialchars($item['name']) ?></strong></td>
                            <td class="price"><?= number_format($item['price'], 2) ?> ₺</td>
                            <td><?= $item['quantity'] ?> Adet</td>
                            <td class="subtotal"><?= number_format($item['ara_toplam'], 2) ?> ₺</td>
                            <td>
                                <a href="islem.php?islem=cikar&cart_id=<?= $item['cart_id'] ?>" class="btn-remove">Kaldır</a>
                            </td>
                        </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>

            <div class="cart-summary">
                <div class="total-price">Genel Toplam: <span><?= number_format($genel_toplam, 2) ?> ₺</span></div>
                <div class="actions">
                    <a href="islem.php?islem=temizle" class="btn btn-clear" onclick="return confirm('Sepeti boşaltmak istediğine emin misin?')">🗑 Sepeti Temizle</a>
                    <a href="islem.php?islem=siparis_ver" class="btn btn-order">✅ Siparişi Onayla</a>
                </div>
            </div>

        <?php else: ?>
            <div class="empty-cart">
                <h3>Sepetiniz şu an boş.</h3>
                <p>Hemen ürünlerimize göz atıp alışverişe başlayabilirsiniz!</p>
                <br>
                <a href="index.php" class="btn btn-order">Alışverişe Başla</a>
            </div>
        <?php endif; ?>
    </div>

</body>
</html>