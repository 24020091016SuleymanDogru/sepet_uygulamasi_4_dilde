<?php
require_once 'db.php';
$user_id = 1; // Sabit kullanıcı

// Kullanıcının siparişlerini tarihe göre en yeniden eskiye doğru çekiyoruz
$sorgu = $db->prepare("SELECT * FROM orders WHERE user_id = ? ORDER BY created_at DESC");
$sorgu->execute([$user_id]);
$siparisler = $sorgu->fetchAll(PDO::FETCH_ASSOC);
?>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <title>Siparişlerim</title>
</head>
<body>
    <h1>Siparişlerim</h1>
    <a href="index.php">⬅ Ürünlere Dön</a>
    <hr>

    <?php if (count($siparisler) > 0): ?>
        <table border="1" cellpadding="10" cellspacing="0" style="width: 100%; text-align: left;">
            <thead>
                <tr>
                    <th>Sipariş No</th>
                    <th>Tarih</th>
                    <th>Toplam Tutar</th>
                    <th>Durum</th>
                </tr>
            </thead>
            <tbody>
                <?php foreach ($siparisler as $siparis): ?>
                    <tr>
                        <td>#<?= $siparis['id'] ?></td>
                        <td><?= date('d.m.Y H:i', strtotime($siparis['created_at'])) ?></td>
                        <td><?= number_format($siparis['total_amount'], 2) ?> TL</td>
                        <td>
                            <?php 
                            // Duruma göre renkli gösterim
                            if ($siparis['status'] == 'aktif') echo '<span style="color: orange; font-weight: bold;">Aktif / Hazırlanıyor</span>';
                            elseif ($siparis['status'] == 'tamamlandi') echo '<span style="color: green; font-weight: bold;">Tamamlandı</span>';
                            elseif ($siparis['status'] == 'iptal') echo '<span style="color: red; font-weight: bold;">İptal Edildi</span>';
                            ?>
                        </td>
                    </tr>
                <?php endforeach; ?>
            </tbody>
        </table>
    <?php else: ?>
        <p>Henüz bir siparişiniz bulunmamaktadır.</p>
    <?php endif; ?>

</body>
</html>