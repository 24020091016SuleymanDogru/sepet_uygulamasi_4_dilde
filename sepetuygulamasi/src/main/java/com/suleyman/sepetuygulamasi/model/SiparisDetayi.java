package com.suleyman.sepetuygulamasi.model;

import jakarta.persistence.*;

@Entity
@Table(name = "siparis_detaylari")
public class SiparisDetayi {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int detayId;

    public int getDetayId() {
        return detayId;
    }

    public Urun getUrun() {
        return urun;
    }

    public void setUrun(Urun urun) {
        this.urun = urun;
    }

    public int getAdet() {
        return adet;
    }

    public void setAdet(int adet) {
        this.adet = adet;
    }

    public int getUrunId() {
        return urunId;
    }

    public void setUrunId(int urunId) {
        this.urunId = urunId;
    }

    public int getSiparisId() {
        return siparisId;
    }

    public void setSiparisId(int siparisId) {
        this.siparisId = siparisId;
    }

    public void setDetayId(int detayId) {
        this.detayId = detayId;
    }

    private int siparisId;
    private int urunId;
    private int adet = 1;

    @ManyToOne
    @JoinColumn(name = "urunId", insertable = false, updatable = false)
    private Urun urun;

    // Getter ve Setter metodlarını buraya ekle
}