using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace sepet_uygulamasi.Pages
{
    public class SepetModel : PageModel
    {
        private readonly AppDbContext _context;

        public SepetModel(AppDbContext context)
        {
            _context = context;
        }

        // Ön yüze taþýyacaðýmýz sepet verisi
        public Siparis AktifSepet { get; set; }

        public async Task OnGetAsync()
        {
            // Yine veritabanýndaki ilk kullanýcýyý çekiyoruz
            var aktifKullanici = await _context.Kullanicilar.FirstOrDefaultAsync();

            if (aktifKullanici != null)
            {
                // Kullanýcýnýn sepetini, içindeki detaylarý ve detaylarýn içindeki Urun bilgilerini getiriyoruz (Include komutu baðlama iþi yapar)
                AktifSepet = await _context.Siparisler
                    .Include(s => s.SiparisDetaylari)
                        .ThenInclude(d => d.Urun)
                    .FirstOrDefaultAsync(s => s.KullaniciID == aktifKullanici.KullaniciID && s.Durum == SiparisDurumu.Sepette);
            }
        }

        // 1. Tek Bir Ürünü Sepetten Çýkarma Metodu
        public async Task<IActionResult> OnPostUrunCikarAsync(int detayId)
        {
            // Silinecek ürün detayýný buluyoruz
            var detay = await _context.SiparisDetaylari.FindAsync(detayId);

            if (detay != null)
            {
                _context.SiparisDetaylari.Remove(detay); // Tablodan sil
                await _context.SaveChangesAsync();       // Deðiþikliði veritabanýna kaydet
            }

            return RedirectToPage(); // Sayfayý yenile
        }

        // 2. Sepeti Komple Temizleme Metodu
        public async Task<IActionResult> OnPostSepetiTemizleAsync(int siparisId)
        {
            // Aktif sepeti buluyoruz
            var sepet = await _context.Siparisler.FindAsync(siparisId);

            if (sepet != null)
            {
                _context.Siparisler.Remove(sepet); // Sepeti sil
                await _context.SaveChangesAsync();

                // Not: Veritabanýný kurarken ON DELETE CASCADE özelliðini açtýðýmýz için, 
                // ana sepeti sildiðimizde içindeki tüm SiparisDetaylari satýrlarý MySQL tarafýndan otomatik silinir!
            }

            return RedirectToPage();
        }
    }
}