using sepet_uygulamasi;
using System.ComponentModel.DataAnnotations;

public class Kullanici
{
    [Key]
    public int KullaniciID { get; set; }
    public string Ad { get; set; }
    public string Email { get; set; }

    // İlişki (Bir kullanıcının birden fazla siparişi olabilir)
    public List<Siparis> Siparisler { get; set; }
}