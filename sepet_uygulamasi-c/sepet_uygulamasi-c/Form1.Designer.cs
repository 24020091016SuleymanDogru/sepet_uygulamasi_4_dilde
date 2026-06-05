namespace sepet_uygulamasi_c
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvÜrünler = new System.Windows.Forms.DataGridView();
            this.dgvsepett = new System.Windows.Forms.DataGridView();
            this.cmbSiparis = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvÜrünler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsepett)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvÜrünler
            // 
            this.dgvÜrünler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvÜrünler.Location = new System.Drawing.Point(12, 12);
            this.dgvÜrünler.Name = "dgvÜrünler";
            this.dgvÜrünler.Size = new System.Drawing.Size(795, 158);
            this.dgvÜrünler.TabIndex = 0;
            // 
            // dgvsepett
            // 
            this.dgvsepett.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsepett.Location = new System.Drawing.Point(12, 201);
            this.dgvsepett.Name = "dgvsepett";
            this.dgvsepett.Size = new System.Drawing.Size(549, 348);
            this.dgvsepett.TabIndex = 1;
            // 
            // cmbSiparis
            // 
            this.cmbSiparis.FormattingEnabled = true;
            this.cmbSiparis.Location = new System.Drawing.Point(595, 229);
            this.cmbSiparis.Name = "cmbSiparis";
            this.cmbSiparis.Size = new System.Drawing.Size(121, 21);
            this.cmbSiparis.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(595, 289);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Sepete Ekle";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(595, 483);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(121, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "Sepeti Temizle";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(595, 425);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(121, 23);
            this.button6.TabIndex = 5;
            this.button6.Text = "Sepetten Çıkar";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(595, 361);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(121, 23);
            this.button7.TabIndex = 6;
            this.button7.Text = "Sepeti Onayla";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(819, 561);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbSiparis);
            this.Controls.Add(this.dgvsepett);
            this.Controls.Add(this.dgvÜrünler);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvÜrünler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsepett)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvurunler;
        private System.Windows.Forms.DataGridView dgvsepet;
        private System.Windows.Forms.ComboBox cmbSiparisDurum;
        private System.Windows.Forms.Button sepete;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dgvÜrünler;
        private System.Windows.Forms.DataGridView dgvsepett;
        private System.Windows.Forms.ComboBox cmbSiparis;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
    }
}

