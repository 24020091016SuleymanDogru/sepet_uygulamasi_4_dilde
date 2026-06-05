# 🛒 Coklu Dilli Sepet ve Siparis Yonetim Sistemi

Bu proje; bir e-ticaret sisteminin temel tasi olan sepet ve siparis yonetimi mekanizmalarinin farkli yazilim dilleri (**PHP**, **Java**, **ASP.NET C#**) kullanilarak backend ve frontend katmanlarinda nasil implement edildigini gosteren kapsamli bir full-stack portfoy calismasidir.

Ayni relational veritabani mimarisi uzerine kurulan bu sistemler, backend mantiginin farkli ekosistemlerde nasil davrandigini somut bir sekilde gozler onune sermektedir.

---

## 📸 Proje Ekran Goruntuleri

<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/4a92cf22-ab3a-4a39-8d29-4f921ec2eeaf" />
<img width="821" height="575" alt="image" src="https://github.com/user-attachments/assets/56f3eb0e-3420-4ee1-9a57-24e4836c7845" />
<img width="1919" height="1079" alt="image" src="https://github.com/user-attachments/assets/c3721431-4dbe-4a34-b3ba-60df563b9316" />


---

## 🛠️ Kullanilan Teknolojiler ve Yapilar

### 1. Backend & Web Katmanlari
* **PHP:** Nesne yonelimli PDO mimarisi ile veri baglantisi ve dinamik web arayuzu.
* **Java:** Database entegrasyonu ve kurumsal yonetim katmani.
* **ASP.NET & C#:** MVC / API mimarisine uygun, guvenli ve performansli backend yonetimi.

### 2. Veritabani Katmani
* **MySQL / MariaDB:** Iliskisel veritabani yonetim sistemi.
* **Veritabani Kisitlamalari (Constraints):** Veri tutarliligini korumak adina `FOREIGN KEY` ve `ON DELETE CASCADE` iliskileri aktif olarak kullanilmistir.

---

## 📂 Proje Klasor Yapisi

```text
sepet_uygulamasi/
│
├── php_backend/          # PHP Web Projesi Klasoru
│   ├── db.php            # PDO Veritabani Baglantisi (Port: 3307 Ayarli)
│   ├── index.php         # Modern Alisveris Vitrini Arayuzu
│   ├── sepet.php         # Sepet Ozeti ve Siparis Onay Ekrani
│   └── islem.php         # Sepet Ekle/Cikar/Temizle Logics
│
├── java_backend/         # Java Entegrasyon Klasoru
│   └── src/              # Java Kaynak Kodlari
│
├── asp_csharp_backend/   # .NET Core / ASP.NET Klasoru
│   └── Controllers/      # C# Controller Yapilari
│
└── database/
    └── schema.sql        # Ortak Veritabani Tablo Yapilari

```

---

## 🗄️ Veritabani Semasi (Database Schema)

Sistem, tum dillerde asagidaki ortak relational database yapisini kullanmaktadir:

* **users:** Kullanici bilgilerini tutar.
* **products:** Magazadaki urunlerin ad, aciklama, fiyat ve stok bilgilerini barindirir.
* **cart:** Kullanicilar ile urunler arasindaki sepet iliskisini saglar (`FOREIGN KEY` baglantili).
* **orders & order_items:** Onaylanan siparislerin gecmisini ve detaylarini saklar.

---

## 🚀 Kurulum ve Calistirma

### PHP Versiyonu Icin:

1. Proje klasorunu `C:/xampp/htdocs/` dizinine tasiyin.
2. XAMPP Kontrol Panelinden **Apache** ve **MySQL** servislerini baslatin.
3. Tarayicinizdan `http://localhost/php_backend/index.php` adresine gidin.

> **Port Notu:** Eger bilgisayarinizda baska bir MySQL sunucusu (orn. Workbench) `3306` portunu kullaniyorsa, XAMPP otomatik olarak `3307` portundan baslayacaktir. `db.php` icerisindeki port tanimlamasi buna uygun sekilde optimize edilmistir.

### Diger Backend Versiyonlari Icin:

* **Java:** Ilgili IDE (IntelliJ/Eclipse) uzerinden projeyi import ederek veritabani baglantilarini guncelleyin.
* **ASP.NET:** Visual Studio ile solution dosyasini acarak IIS Express veya Kestrel sunucusu uzerinde projeyi ayaga kaldirabilirsiniz.

---

## 👨‍💻 Gelistirici

* **Suleyman Dogru** - Software Engineering Student

