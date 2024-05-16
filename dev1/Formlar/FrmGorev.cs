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
    public partial class FrmGorev : Form
    {
        public FrmGorev()
        {
            InitializeComponent();
        }

        DbisTakipEntities db = new DbisTakipEntities();
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            TblGorevler t = new TblGorevler();
            t.Aciklama = textEdit2.Text;
            t.Durum = true;
            t.GorevAlan = int.Parse(lookUpEdit1.EditValue.ToString());
            t.Tarih = DateTime.Parse(lookUpEdit2.Text);
            t.GorevVeren = int.Parse(textEdit1.Text);
            db.TblGorevler.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Görev başarıyla eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmGorev_Load(object sender, EventArgs e)
        {
            var degerler = (from x in db.TblPersonel
                                select new
                                {
                                    x.ID,
                                    AdSoyad = x.Ad + " " + x.Soyad
                                }).ToList();

            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AdSoyad";

            lookUpEdit1.Properties.DataSource = degerler;
        }
    }
}
