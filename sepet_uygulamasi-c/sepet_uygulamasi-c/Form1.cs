using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace sepet_uygulamasi_c
{
    public partial class Form1 : Form
    {
        SepetYonetici sepetim = new SepetYonetici();
        int aktifKullaniciId = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UrunleriListele();
            SepetiYenile();

            // ComboBox (cmbSiparis) Ayarları
            cmbSiparis.Items.Clear();
            cmbSiparis.Items.AddRange(new string[] { "Aktif", "Tamamlandi", "Iptal" });
            cmbSiparis.SelectedIndex = 0;
        }

        private void UrunleriListele()
        {
            try
            {
                using (var conn = Veritabani.BaglantiAl())
                {
                    // StokAdedi kısmını sildim, sadece senin kesin olan kolonlarını yazdım
                    string query = "SELECT UrunID as id, UrunAdi as urun_adi, Fiyat as fiyat FROM urunler";
                    MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvÜrünler.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün listesi yüklenemedi: " + ex.Message);
            }
        }

        private void SepetiYenile()
        {
            try
            {
                dgvsepett.DataSource = sepetim.SepetiGetir(aktifKullaniciId); // Yeni adın
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sepet güncellenirken hata: " + ex.Message);
            }
        }

        // button1: SEPETE EKLE
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvÜrünler.CurrentRow != null)
                {
                    // DataGridView'deki "id" isimli kolondan (yani UrunID'den) değeri alıyoruz
                    int urunId = Convert.ToInt32(dgvÜrünler.CurrentRow.Cells["id"].Value);

                    sepetim.SepeteEkle(aktifKullaniciId, urunId, 1);
                    SepetiYenile(); // Sepeti güncelle ki alttaki tabloda görelim
                    MessageBox.Show("Ürün sepete eklendi!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme hatası: " + ex.Message);
            }
        }

        // button6: SEPETTEN ÇIKAR (Eski button3 yerine hangisini atadıysan ona göre güncelle)
        private void button6_Click(object sender, EventArgs e)
        {
            if (dgvsepett.CurrentRow != null)
            {
                int sepetId = Convert.ToInt32(dgvsepett.CurrentRow.Cells["id"].Value);
                sepetim.UrunCikar(sepetId);
                SepetiYenile();
            }
        }

        // button5: SEPETİ TEMİZLE (Eski button2)
        private void button5_Click(object sender, EventArgs e)
        {
            DialogResult sonuc = MessageBox.Show("Sepet temizlensin mi?", "Onay", MessageBoxButtons.YesNo);
            if (sonuc == DialogResult.Yes)
            {
                sepetim.SepetiTemizle(aktifKullaniciId);
                SepetiYenile();
            }
        }

        // button7: SİPARİŞİ ONAYLA (Eski button4)
        private void button7_Click(object sender, EventArgs e)
        {
            SiparisYonetim sy = new SiparisYonetim();
            // Sepetteki toplam tutarı hesaplayan basit bir döngü veya SQL de olabilir
            // Şimdilik test için 100 diyelim ya da dgvsepett'ten hesaplatabilirsin
            decimal toplam = 0;
            foreach (DataGridViewRow row in dgvsepett.Rows)
            {
                toplam += Convert.ToDecimal(row.Cells["Toplam"].Value);
            }

            if (sy.SiparisOlustur(aktifKullaniciId, toplam))
            {
                MessageBox.Show("Sipariş başarıyla 'Aktif' duruma alındı!");
                SepetiYenile();
            }
            else
            {
                MessageBox.Show("Sipariş oluşturulurken bir hata oluştu.");
            }
        }

        private void cmbSiparis_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secilenDurum = cmbSiparis.SelectedItem.ToString();
            SiparisYonetim sy = new SiparisYonetim();
            // Alttaki tabloda artık sepeti değil, o durumdaki siparişleri gösteriyoruz
            dgvsepett.DataSource = sy.SiparisleriGetir(secilenDurum);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgvÜrünler.CurrentRow != null)
                {
                    // DataGridView'deki "id" isimli kolondan (yani UrunID'den) değeri alıyoruz
                    int urunId = Convert.ToInt32(dgvÜrünler.CurrentRow.Cells["id"].Value);

                    sepetim.SepeteEkle(aktifKullaniciId, urunId, 1);
                    SepetiYenile(); // Sepeti güncelle ki alttaki tabloda görelim
                    MessageBox.Show("Ürün sepete eklendi!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme hatası: " + ex.Message);
            }
        }
    }
}