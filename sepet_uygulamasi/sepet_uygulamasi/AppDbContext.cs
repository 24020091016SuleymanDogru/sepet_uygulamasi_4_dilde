using Microsoft.EntityFrameworkCore;

namespace sepet_uygulamasi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Urun> Urunler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<SiparisDetayi> SiparisDetaylari { get; set; }

        // --- ŞU KISMI EKLİYORUZ ---
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Durum enum'ını veritabanına string (yazı) olarak kaydetmesi gerektiğini belirtiyoruz
            modelBuilder.Entity<Siparis>()
                .Property(s => s.Durum)
                .HasConversion<string>();
        }
        // --------------------------
    }
}