package com.suleyman.sepetuygulamasi.repository;

import com.suleyman.sepetuygulamasi.model.Siparis;
import com.suleyman.sepetuygulamasi.model.SiparisDurumu;
import org.springframework.data.jpa.repository.JpaRepository;

public interface SiparisRepo extends JpaRepository<Siparis, Integer> {
    // Kullanıcının "Sepette" olan aktif siparişini bulmak için C#'ta yazdığımız uzun LINQ sorgusunun Java hali sadece bu satırdır:
    Siparis findByKullaniciIdAndDurum(int kullaniciId, SiparisDurumu durum);
}