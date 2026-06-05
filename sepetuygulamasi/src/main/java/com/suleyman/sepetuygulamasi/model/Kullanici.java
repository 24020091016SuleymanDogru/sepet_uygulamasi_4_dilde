package com.suleyman.sepetuygulamasi.model;

import jakarta.persistence.*;

@Entity
@Table(name = "kullanicilar")
public class Kullanici {
    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getAd() {
        return ad;
    }

    public void setAd(String ad) {
        this.ad = ad;
    }

    public int getKullaniciId() {
        return kullaniciId;
    }

    public void setKullaniciId(int kullaniciId) {
        this.kullaniciId = kullaniciId;
    }

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private int kullaniciId;

    private String ad;
    private String email;

    // IDE'nin menüsünden sağ tıklayıp "Generate -> Getter and Setter" yaparak metodları eklemeyi unutma!
}