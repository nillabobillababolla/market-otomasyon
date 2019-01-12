﻿using Market.BLL.Repository;
using Market.DAL;
using Market.Models.Entities;
using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Windows.Forms;

namespace Market.WFA
{
    public partial class CRUD : Form
    {
        public CRUD()
        {
            InitializeComponent();
        }
        Kategori seciliKategori;
        Kategori lstSeciliKat;
        Urun lstSeciliUrun;
        private void CRUD_Load(object sender, EventArgs e)
        {
            VerileriDoldur();
            lstUrunler.DataSource = null;
        }

        private void VerileriDoldur()
        {

            seciliKategori = cmbKategoriler.SelectedItem as Kategori;
            lstUrunler.DataSource = null;
            lstKategori.DataSource = new KategoriRepo().GetAll();
            cmbKategoriler.DataSource = new KategoriRepo().GetAll();
            lstUrunler.DataSource = new UrunRepo().GetAll(x => x.KategoriId == seciliKategori.Id);
        }

        private void btnKatEkle_Click(object sender, EventArgs e)
        {
            try
            {
                var kategori = new Kategori
                {
                    KategoriAdi = txtKategori.Text,
                    Aciklama = txtKategoriAciklama.Text
                };

                if (new KategoriRepo().Insert(kategori) > 0)
                {
                    MessageBox.Show("Kategori eklendi.");
                }
                else
                {
                    MessageBox.Show("Kategori Ekleme hatasi.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            VerileriDoldur();
        }

        private void cmbKategoriler_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            seciliKategori = cmbKategoriler.SelectedItem as Kategori;
            if (seciliKategori == null) return;

            var urunler = new UrunRepo().GetAll(x => x.KategoriId == seciliKategori.Id);
            lstUrunler.DataSource = urunler.ToList();
        }
        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            if (seciliKategori == null) return;

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
                    KoliBarkod = Convert.ToString(rnd.Next(1000000, 9999999)),
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

        private void kategoriSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstSeciliKat = lstKategori.SelectedItem as Kategori;
            if (lstSeciliKat == null) return;

            var cevap = MessageBox.Show("Secili Kategoriyi silmek istiyor musunuz?", "Kategori silme",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap != DialogResult.Yes) return;
            try
            {
                var silinecekKat = new KategoriRepo();
                silinecekKat.Delete(lstSeciliKat);

                VerileriDoldur();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Silmek istediginiz kayit baska bir tabloda kullanildigi icin silemezsiniz");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void urunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstSeciliUrun = lstUrunler.SelectedItem as Urun;
            if (lstUrunler.SelectedItem == null) return;

            var cevap = MessageBox.Show("Secili Ürünü silmek istiyor musunuz?", "Urun silme",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (cevap != DialogResult.Yes) return;
            try
            {
                var silinecekUrun = new UrunRepo();
                silinecekUrun.Delete(lstSeciliUrun);
                VerileriDoldur();


            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Silmek istediginiz kayit baska bir tabloda kullanildigi icin silemezsiniz");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            lstSeciliUrun = lstUrunler.SelectedItem as Urun;
            var guncellenecekUrun = new UrunRepo().GetById(lstSeciliUrun.Id);           
            if (lstSeciliUrun == null) return;          
            try
            {                 
                lstSeciliUrun.KategoriId = seciliKategori.Id;
                lstSeciliUrun.UrunAdi = txtUrunAdi.Text;
                lstSeciliUrun.Aciklama = txtUrunAciklama.Text;
                lstSeciliUrun.KoliIciAdet = Convert.ToInt32(txtKoliIciAdet.Text);
                lstSeciliUrun.Kdv = Convert.ToDecimal(txtKdv.Text);
                lstSeciliUrun.KoliBarkod = lstSeciliUrun.KoliBarkod;
                lstSeciliUrun.UrunBarkod = lstSeciliUrun.UrunBarkod;
                lstSeciliUrun.AlisFiyat = Convert.ToDecimal(txtAlisFiyati.Text);
                if (new UrunRepo().Update()> 0)
                {
                    MessageBox.Show("Güncelleme Başarılı.");
                    VerileriDoldur();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
