using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace sepet_uygulamasi.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        // Veritabaný baðlantýmýzý bu sayfaya çaðýrýyoruz (Dependency Injection)
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        // Ön yüze göndereceðimiz ürün listesi
        public List<Urun> Urunler { get; set; } = new List<Urun>();

        // Sayfa yüklendiðinde çalýþacak metot
        public async Task OnGetAsync()
        {
            // Veritabanýndaki tüm ürünleri çekip listeye atýyoruz
            Urunler = await _context.Urunler.ToListAsync();
        }

        public async Task<IActionResult> OnPostSepeteEkleAsync(int urunId)
        {
            // 1. KÖKTEN ÇÖZÜM: Veritabanýndaki ilk kullanýcýyý bul, yoksa otomatik oluþtur!
            var aktifKullanici = await _context.Kullanicilar.FirstOrDefaultAsync();

            if (aktifKullanici == null)
            {
                aktifKullanici = new Kullanici { Ad = "Test Kullanicisi", Email = "test@test.com" };
                _context.Kullanicilar.Add(aktifKullanici);
                await _context.SaveChangesAsync(); // Kullanýcýyý kaydet ki ID'si oluþsun
            }

            int gecerliKullaniciId = aktifKullanici.KullaniciID;

            // 2. Bu kullanýcýnýn 'Sepette' durumunda aktif bir sepeti (sipariþi) var mý bakýyoruz
            var sepet = await _context.Siparisler
                .FirstOrDefaultAsync(s => s.KullaniciID == gecerliKullaniciId && s.Durum == SiparisDurumu.Sepette);

            // Eðer yoksa, ona yeni bir sepet oluþturuyoruz
            if (sepet == null)
            {
                sepet = new Siparis { KullaniciID = gecerliKullaniciId, Durum = SiparisDurumu.Sepette };
                _context.Siparisler.Add(sepet);
                await _context.SaveChangesAsync();
            }

            // 3. Týklanan ürün zaten bu sepette var mý diye bakýyoruz
            var sepetDetay = await _context.SiparisDetaylari
                .FirstOrDefaultAsync(d => d.SiparisID == sepet.SiparisID && d.UrunID == urunId);

            if (sepetDetay != null)
            {
                // Ürün zaten sepetteyse sadece adetini 1 artýrýyoruz
                sepetDetay.Adet++;
            }
            else
            {
                // Ürün sepette yoksa, yepyeni bir satýr olarak ekliyoruz
                _context.SiparisDetaylari.Add(new SiparisDetayi
                {
                    SiparisID = sepet.SiparisID,
                    UrunID = urunId,
                    Adet = 1
                });
            }

            // Yaptýðýmýz tüm deðiþiklikleri veritabanýna kaydediyoruz
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}