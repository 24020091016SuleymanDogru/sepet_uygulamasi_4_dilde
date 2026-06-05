using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace sepet_uygulamasi_c
{
    internal class SiparisYonetim
    {
        // 1. Duruma göre siparişleri listeleme (ComboBox için)
        public DataTable SiparisleriGetir(string durum)
        {
            using (var conn = Veritabani.BaglantiAl())
            {
                // Siparişleri ve toplam tutarlarını getirir
                string query = "SELECT SiparisID as id, ToplamTutar as toplam, SiparisTarihi as tarih, Durum as durum " +
                               "FROM siparisler WHERE Durum = @durum";

                MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@durum", durum);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 2. Sepeti Onaylayıp Siparişe Dönüştürme
        public bool SiparisOlustur(int kullaniciId, decimal toplamTutar)
        {
            using (var conn = Veritabani.BaglantiAl())
            {
                conn.Open();
                MySqlTransaction tr = conn.BeginTransaction(); // İşlemi sağlama almak için Transaction kullanıyoruz

                try
                {
                    // A. Siparişler tablosuna ana kaydı ekle
                    string siparisQuery = "INSERT INTO siparisler (KullaniciID, ToplamTutar, Durum) VALUES (@kId, @toplam, 'Aktif'); SELECT LAST_INSERT_ID();";
                    MySqlCommand cmdSiparis = new MySqlCommand(siparisQuery, conn, tr);
                    cmdSiparis.Parameters.AddWithValue("@kId", kullaniciId);
                    cmdSiparis.Parameters.AddWithValue("@toplam", toplamTutar);

                    int yeniSiparisId = Convert.ToInt32(cmdSiparis.ExecuteScalar());

                    // B. Sepetteki ürünleri Sipariş Detaylarına aktar
                    string detayQuery = @"INSERT INTO siparisdetaylari (SiparisID, UrunID, Adet, BirimFiyat) 
                                         SELECT @sId, urun_id, adet, (SELECT Fiyat FROM urunler WHERE UrunID = urun_id) 
                                         FROM sepet WHERE kullanici_id = @kId";

                    MySqlCommand cmdDetay = new MySqlCommand(detayQuery, conn, tr);
                    cmdDetay.Parameters.AddWithValue("@sId", yeniSiparisId);
                    cmdDetay.Parameters.AddWithValue("@kId", kullaniciId);
                    cmdDetay.ExecuteNonQuery();

                    // C. Sepeti temizle
                    string temizleQuery = "DELETE FROM sepet WHERE kullanici_id = @kId";
                    MySqlCommand cmdTemizle = new MySqlCommand(temizleQuery, conn, tr);
                    cmdTemizle.Parameters.AddWithValue("@kId", kullaniciId);
                    cmdTemizle.ExecuteNonQuery();

                    tr.Commit(); // Her şey tamamsa kaydet
                    return true;
                }
                catch (Exception)
                {
                    tr.Rollback(); // Hata varsa geri al
                    return false;
                }
            }
        }

        // 3. Sipariş Durumu Güncelleme (Yönetim kısmı)
        public void DurumGuncelle(int siparisId, string yeniDurum)
        {
            using (var conn = Veritabani.BaglantiAl())
            {
                conn.Open();
                string query = "UPDATE siparisler SET Durum = @durum WHERE SiparisID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@durum", yeniDurum);
                cmd.Parameters.AddWithValue("@id", siparisId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}