using System.ComponentModel.DataAnnotations;

public class Urun
{
    [Key]
    public int UrunID { get; set; }
    public string UrunAdi { get; set; }
    public decimal Fiyat { get; set; }
}