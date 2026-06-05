using System.ComponentModel.DataAnnotations;

public class SiparisDetayi
{
    [Key]
    public int DetayID { get; set; }
    public int SiparisID { get; set; }
    public int UrunID { get; set; }
    public int Adet { get; set; } = 1;

    // İlişkiler
    public Siparis Siparis { get; set; }
    public Urun Urun { get; set; }
}