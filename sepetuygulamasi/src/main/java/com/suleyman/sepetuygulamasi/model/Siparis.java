package com.suleyman.sepetuygulamasi.model;

import jakarta.persistence.*;
import java.util.List;

@Entity
@Table(name = "siparisler")
public class Siparis {
    public SiparisDurumu getDurum() {
        return durum;
    }

    public void setDurum(SiparisDurumu durum) {
        this.durum = durum;
    }

    public List<SiparisDetayi> getSiparisDetaylari() {
        return siparisDetaylari;
    }

    public void setSiparisDetaylari(List<SiparisDetayi> siparisDetaylari) {
        this.siparisDetaylari = siparisDetaylari;
    }

    public int getKullaniciId() {
        return kullaniciId;
    }

    public void setKullaniciId(int kullaniciId) {
        this.kullaniciId = kullaniciId;
    }

    public int getSiparisId() {
        return siparisId;
    }

    public void setSiparisId(int siparisId) {
        this.siparisId = siparisId;
    }

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int siparisId;

    private int kullaniciId;

    @Enumerated(EnumType.STRING) // ASP'deki hatayı yaşamamak için Enum'ı baştan String olarak ayarlıyoruz
    private SiparisDurumu durum = SiparisDurumu.SEPETTE;

    @OneToMany(cascade = CascadeType.ALL)
    @JoinColumn(name = "siparisId")
    private List<SiparisDetayi> siparisDetaylari;

    // Getter ve Setter metodlarını buraya ekle
}