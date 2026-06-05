package com.suleyman.sepetuygulamasi.model;

import jakarta.persistence.*;

@Entity
@Table(name = "urunler")
public class Urun {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int urunId;

    public int getUrunId() {
        return urunId;
    }

    public double getFiyat() {
        return fiyat;
    }

    public void setFiyat(double fiyat) {
        this.fiyat = fiyat;
    }

    public String getUrunAdi() {
        return urunAdi;
    }

    public void setUrunAdi(String urunAdi) {
        this.urunAdi = urunAdi;
    }

    public void setUrunId(int urunId) {
        this.urunId = urunId;
    }

    private String urunAdi;
    private double fiyat;

    // Getter ve Setter metodlarını buraya ekle
}