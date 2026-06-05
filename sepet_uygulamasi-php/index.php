<?php
require_once 'db.php';

// Tüm ürünleri veritabanından çekiyoruz
$sorgu = $db->query("SELECT * FROM products");
$urunler = $sorgu->fetchAll(PDO::FETCH_ASSOC);
?>

<!DOCTYPE html>
<html lang="tr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Teknoloji Mağazası</title>
    <style>
        /* Genel Ayarlar */
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f4f9;
            margin: 0;
            padding: 0;
            color: #333;
        }

        /* Üst Menü (Header) */
        .navbar {
            background-color: #2c3e50;
            color: white;
            padding: 15px 50px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
        }
        .navbar h2 {
            margin: 0;
            font-size: 1.5rem;
            letter-spacing: 1px;
        }
        .cart-btn {
            background-color: #e74c3c;
            color: white;
            text-decoration: none;
            padding: 10px 20px;
            border-radius: 5px;
            font-weight: bold;
            transition: background-color 0.3s;
        }
        .cart-btn:hover {
            background-color: #c0392b;
        }

        /* İçerik Konteyneri */
        .container {
            max-width: 1200px;
            margin: 40px auto;
            padding: 0 20px;
        }

        /* Ürün Kartları Izgarası (Grid) */
        .product-grid {
            display: grid;
            grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
            gap: 25px;
        }

        /* Tekil Ürün Kartı */
        .product-card {
            background: white;
            border-radius: 10px;
            padding: 25px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
            text-align: center;
            transition: transform 0.3s, box-shadow 0.3s;
            display: flex;
            flex-direction: column;
            justify-content: space-between;
        }
        .product-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 20px rgba(0,0,0,0.1);
        }
        .product-title {
            font-size: 1.3rem;
            font-weight: bold;
            color: #2c3e50;
            margin-bottom: 10px;
        }
        .product-desc {
            font-size: 0.95rem;
            color: #7f8c8d;
            margin-bottom: 20px;
            min-height: 40px;
        }
        .product-price {
            font-size: 1.8rem;
            color: #27ae60;
            font-weight: bold;
            margin-bottom: 20px;
        }

        /* Form ve Butonlar */
        .add-to-cart-form {
            display: flex;
            justify-content: center;
            align-items: center;
            gap: 10px;
        }
        .add-to-cart-form input[type="number"] {
            width: 60px;
            padding: 10px;
            border: 1px solid #bdc3c7;
            border-radius: 5px;
            font-size: 1rem;
            text-align: center;
        }
        .add-to-cart-form button {
            background-color: #3498db;
            color: white;
            border: none;
            padding: 10px 15px;
            border-radius: 5px;
            cursor: pointer;
            font-weight: bold;
            font-size: 1rem;
            transition: background-color 0.3s;
            flex-grow: 1;
        }
        .add-to-cart-form button:hover {
            background-color: #2980b9;
        }
    </style>
</head>
<body>

    <div class="navbar">
        <h2>⚡ Teknoloji Mağazası</h2>
        <a href="sepet.php" class="cart-btn">🛒 Sepetime Git</a>
    </div>

    <div class="container">
        <div class="product-grid">
            <?php foreach ($urunler as $urun): ?>
                <div class="product-card">
                    <div>
                        <div class="product-title"><?= htmlspecialchars($urun['name']) ?></div>
                        <div class="product-desc"><?= htmlspecialchars($urun['description']) ?></div>
                        <div class="product-price"><?= number_format($urun['price'], 2) ?> ₺</div>
                    </div>
                    
                    <form class="add-to-cart-form" action="islem.php?islem=ekle" method="POST">
                        <input type="hidden" name="product_id" value="<?= $urun['id'] ?>">
                        <input type="number" name="quantity" value="1" min="1">
                        <button type="submit">Sepete Ekle</button>
                    </form>
                </div>
            <?php endforeach; ?>
        </div>
    </div>

</body>
</html>