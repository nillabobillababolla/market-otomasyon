﻿using Market.BLL.Repository;
using Market.Models.Entities;
using System;
using System.Windows.Forms;

namespace Market.WFA
{
    public partial class SatisDialog : Form
    {
        public MalKabul malkabulForm;
        public SatisDialog()
        {
            InitializeComponent();
            VerileriDoldur();
        }
        Kategori seciliKategori;
        private void VerileriDoldur()
        {          
            cmbKategoriler.DataSource = new KategoriRepo().GetAll();           
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            seciliKategori = cmbKategoriler.SelectedItem as Kategori;
            if (seciliKategori == null) return;
            if (txtYeniBarkod.Text == null) return;
            try
            {
                
                Random rnd = new Random();
                var urun = new Urun
                {
                    KategoriId = seciliKategori.Id,
                    UrunAdi = txtUrunAdi.Text,
                    Aciklama = txtUrunAciklama.Text,
                    KoliIciAdet = Convert.ToInt32(txtKoliIciAdet.Text),
                    Kdv = Convert.ToDecimal(txtKdv.Text),
                    KoliBarkod = txtYeniBarkod.Text,
                    UrunBarkod = Convert.ToString(rnd.Next(10000000, 99999999)),
                    AlisFiyat = Convert.ToDecimal(txtAlisFiyati.Text),
                };

                if (new UrunRepo().Insert(urun) > 0)
                {
                    MessageBox.Show($@"{urun.UrunAdi} ürünü eklendi.");
                }
                else
                {
                    MessageBox.Show("Urun Ekleme hatasi.");
                }
                VerileriDoldur();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SatisDialog_Load(object sender, EventArgs e)
        {
            txtYeniBarkod.Text = malkabulForm.txtBarkod.Text;
            Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
            pbBarkod.Image = barcode.Draw(txtYeniBarkod.Text, 100, 2);
        }

     
    }
}
