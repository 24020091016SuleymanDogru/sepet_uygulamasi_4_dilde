using MySql.Data.MySqlClient;
using System.Data;

public class SepetYonetici
{
    public DataTable SepetiGetir(int kullaniciId)
    {
        using (var conn = Veritabani.BaglantiAl())
        {
            // "Beklemede" as durum diyerek sanal bir kolon ekledik
            string query = @"SELECT s.id, u.UrunAdi as urun_adi, u.Fiyat as fiyat, s.adet, 
                         (u.Fiyat * s.adet) as Toplam, 'Sepette' as durum 
                         FROM sepet s 
                         JOIN urunler u ON s.urun_id = u.UrunID 
                         WHERE s.kullanici_id = @kId";

            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            da.SelectCommand.Parameters.AddWithValue("@kId", kullaniciId);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    public void SepeteEkle(int kullaniciId, int urunId, int adet)
    {
        using (var conn = Veritabani.BaglantiAl())
        {
            conn.Open();
            string query = @"INSERT INTO sepet (kullanici_id, urun_id, adet) 
                         VALUES (@kId, @uId, @adet) 
                         ON DUPLICATE KEY UPDATE adet = adet + @adet";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@kId", kullaniciId);
            cmd.Parameters.AddWithValue("@uId", urunId);
            cmd.Parameters.AddWithValue("@adet", adet);
            cmd.ExecuteNonQuery();
        }
    }

    public void UrunCikar(int sepetId)
    {
        using (var conn = Veritabani.BaglantiAl())
        {
            conn.Open();
            string query = "DELETE FROM sepet WHERE id = @id";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", sepetId);
            cmd.ExecuteNonQuery();
        }
    }

    public void SepetiTemizle(int kullaniciId)
    {
        using (var conn = Veritabani.BaglantiAl())
        {
            conn.Open();
            string query = "DELETE FROM sepet WHERE kullanici_id = @kId";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@kId", kullaniciId);
            cmd.ExecuteNonQuery();
        }
    }
}