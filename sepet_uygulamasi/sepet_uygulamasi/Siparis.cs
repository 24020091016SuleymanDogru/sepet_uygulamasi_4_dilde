using sepet_uygulamasi;
using System.ComponentModel.DataAnnotations;

// Durumlar için bir Enum oluşturuyoruz
public enum SiparisDurumu
{
    Sepette,
    Aktif,
    Tamamlandi,
    Iptal
}

public class Siparis
{
    [Key]
    public int SiparisID { get; set; }
    public int KullaniciID { get; set; }
    public SiparisDurumu Durum { get; set; } = SiparisDurumu.Sepette;

    // İlişkiler
    public Kullanici Kullanici { get; set; }
    public List<SiparisDetayi> SiparisDetaylari { get; set; }
}