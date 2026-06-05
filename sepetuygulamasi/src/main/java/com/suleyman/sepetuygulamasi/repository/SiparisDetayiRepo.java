package com.suleyman.sepetuygulamasi.repository;

import com.suleyman.sepetuygulamasi.model.SiparisDetayi;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SiparisDetayiRepo extends JpaRepository<SiparisDetayi, Integer> {
    // Sepette bu üründen zaten var mı diye kontrol edeceğimiz özel metod:
    SiparisDetayi findBySiparisIdAndUrunId(int siparisId, int urunId);
}