using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dev1.Entity;
using DevExpress.XtraEditors;

namespace dev1.Formlar
{
    public partial class FrmPersoneller : Form
    {
        public FrmPersoneller()
        {
            InitializeComponent();
        }

        DbisTakipEntities db = new DbisTakipEntities();

        void personeller()
        {
            var degerler = (from x in db.TblPersonel
                            select new
                            {
                                x.ID,
                                x.Ad,
                                x.Soyad,
                                x.Mail,
                                x.Telefon,
                                Departman= x.TblDepartmanlar.Ad,
                                x.Durum
                            }).ToList();
            gridControl1.DataSource = degerler.Where(x => x.Durum == true).ToList();
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            personeller();
            var departmanlar = (from x in db.TblDepartmanlar
                                select new
                                {
                                    x.ID,
                                    x.Ad,                 
                                }).ToList();

            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "Ad";

            lookUpEdit1.Properties.DataSource = departmanlar;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            TblPersonel p = new TblPersonel();
            p.Ad = textEdit2.Text;
            p.Soyad = textEdit4.Text;
            p.Mail = textEdit3.Text;
            p.Gorsel = textEdit6.Text;
            p.Departman = int.Parse(lookUpEdit1.EditValue.ToString());
            db.TblPersonel.Add(p);
            db.SaveChanges();
            XtraMessageBox.Show("Personel başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personeller();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textEdit1.Text))
            {
                XtraMessageBox.Show("Lütfen bir değer giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            var x = int.Parse(textEdit1.Text);
            var deger = db.TblPersonel.Find(x);
            if (deger == null)
            {
                XtraMessageBox.Show("Belirtilen ID'ye sahip personel bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Fonksiyondan çık
            }
            deger.Durum = false;
            db.SaveChanges();
            XtraMessageBox.Show("Personel başarıyla silindi. Silinen personellere silinenler listesinden bakabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            personeller();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textEdit1.Text);
            var deger = db.TblPersonel.Find(x);
            deger.Ad = textEdit2.Text;
            deger.Soyad = textEdit4.Text;
            deger.Mail = textEdit3.Text;
            deger.Gorsel = textEdit6.Text;
            deger.Departman = int.Parse(lookUpEdit1.EditValue.ToString());
            XtraMessageBox.Show("Personel başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            db.SaveChanges();
            personeller();
        }
    }
}
