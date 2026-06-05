package com.suleyman.sepetuygulamasi.controller;

import com.suleyman.sepetuygulamasi.model.*;
import com.suleyman.sepetuygulamasi.repository.*;
import jakarta.annotation.PostConstruct;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestParam;

import java.util.List;

@Controller
public class WebController {

    // Veritabanı bağlantılarımızı (C#'taki _context gibi) içeri alıyoruz
    @Autowired private KullaniciRepo kullaniciRepo;
    @Autowired private UrunRepo urunRepo;
    @Autowired private SiparisRepo siparisRepo;
    @Autowired private SiparisDetayiRepo detayRepo;

    // KIYAK KISIM: Veritabanı boşsa MySQL ile uğraşma diye ürünleri otomatik ekler
    @PostConstruct
    public void testVerileriniYukle() {
        if (urunRepo.count() == 0) {
            Urun u1 = new Urun(); u1.setUrunAdi("Oyuncu Klavyesi"); u1.setFiyat(1250.00); urunRepo.save(u1);
            Urun u2 = new Urun(); u2.setUrunAdi("Kablosuz Mouse"); u2.setFiyat(450.50); urunRepo.save(u2);
            Urun u3 = new Urun(); u3.setUrunAdi("Oyuncu Kulaklığı"); u3.setFiyat(1899.99); urunRepo.save(u3);
            Urun u4 = new Urun(); u4.setUrunAdi("27 inç Monitör"); u4.setFiyat(5400.00); urunRepo.save(u4);
        }
    }

    // ANA SAYFAYI GETİR (C#'taki OnGetAsync)
    @GetMapping("/")
    public String anaSayfa(Model model) {
        List<Urun> urunler = urunRepo.findAll();
        model.addAttribute("urunler", urunler);
        return "index"; // Birazdan oluşturacağımız index.html sayfasını açar
    }

    // SEPETE ÜRÜN EKLE (C#'taki OnPostSepeteEkleAsync)
    @PostMapping("/sepete-ekle")
    public String sepeteEkle(@RequestParam int urunId) {
        // 1. Kullanıcıyı bul veya hiç yoksa "Süleyman" adına otomatik oluştur
        Kullanici aktifKullanici = kullaniciRepo.findAll().stream().findFirst().orElse(null);
        if (aktifKullanici == null) {
            aktifKullanici = new Kullanici();
            aktifKullanici.setAd("Süleyman");
            aktifKullanici.setEmail("iletisim@suleyman.com");
            kullaniciRepo.save(aktifKullanici);
        }

        int gecerliKullaniciId = aktifKullanici.getKullaniciId();

        // 2. Bu kullanıcının 'Sepette' olan siparişini bul, yoksa yeni aç
        Siparis sepet = siparisRepo.findByKullaniciIdAndDurum(gecerliKullaniciId, SiparisDurumu.SEPETTE);
        if (sepet == null) {
            sepet = new Siparis();
            sepet.setKullaniciId(gecerliKullaniciId);
            sepet.setDurum(SiparisDurumu.SEPETTE);
            siparisRepo.save(sepet);
        }

        // 3. Ürün sepette zaten var mı kontrol et, varsa adedi artır, yoksa yeni ekle
        SiparisDetayi detay = detayRepo.findBySiparisIdAndUrunId(sepet.getSiparisId(), urunId);
        if (detay != null) {
            detay.setAdet(detay.getAdet() + 1);
            detayRepo.save(detay);
        } else {
            detay = new SiparisDetayi();
            detay.setSiparisId(sepet.getSiparisId());
            detay.setUrunId(urunId);
            detay.setAdet(1);
            detayRepo.save(detay);
        }

        return "redirect:/"; // İşlem bitince sayfayı yenile
    }

    // SEPET SAYFASINI GETİR
    @GetMapping("/sepet")
    public String sepetSayfasi(Model model) {
        Kullanici aktifKullanici = kullaniciRepo.findAll().stream().findFirst().orElse(null);

        if (aktifKullanici != null) {
            Siparis aktifSepet = siparisRepo.findByKullaniciIdAndDurum(aktifKullanici.getKullaniciId(), SiparisDurumu.SEPETTE);
            model.addAttribute("aktifSepet", aktifSepet);

            // Ara toplam hesaplama
            double genelToplam = 0;
            if (aktifSepet != null && aktifSepet.getSiparisDetaylari() != null) {
                for (SiparisDetayi detay : aktifSepet.getSiparisDetaylari()) {
                    genelToplam += detay.getAdet() * detay.getUrun().getFiyat();
                }
            }
            model.addAttribute("genelToplam", genelToplam);
        }
        return "sepet"; // sepet.html sayfasını açacak
    }

    // SEPETTEN TEK ÜRÜN ÇIKAR
    @PostMapping("/urun-cikar")
    public String urunCikar(@RequestParam int detayId) {
        detayRepo.deleteById(detayId);
        return "redirect:/sepet"; // Silince sepet sayfasını yenile
    }

    // SEPETİ KOMPLE TEMİZLE
    @PostMapping("/sepeti-temizle")
    public String sepetiTemizle(@RequestParam int siparisId) {
        siparisRepo.deleteById(siparisId);
        return "redirect:/sepet";
    }
}