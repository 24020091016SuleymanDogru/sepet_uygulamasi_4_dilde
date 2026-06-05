<?php
// db.php
$host = '127.0.0.1'; // localhost yerine ip yazmak port yönlendirmelerinde daha sağlıklıdır
$port = '3307'; // İşte sihirli dokunuşumuz burası!
$dbname = 'sepet_db';
$username = 'root'; 
$password = ''; 

try {
    // Port bilgisini PDO bağlantı cümlesine ekledik
    $db = new PDO("mysql:host=$host;port=$port;dbname=$dbname;charset=utf8", $username, $password);
    $db->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch(PDOException $e) {
    die("Veritabanı bağlantı hatası: " . $e->getMessage());
}
?>