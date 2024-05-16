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
    public partial class FrmDepartmanlar : Form
    {
        public FrmDepartmanlar()
        {
            InitializeComponent();
        }

        DbisTakipEntities db = new DbisTakipEntities();
        void Listele()
        {
            
            var degerler = (from x in db.TblDepartmanlar
                            select new
                            {
                                x.ID,
                                x.Ad
                            }).ToList();
            gridControl1.DataSource = degerler;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Listele();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            TblDepartmanlar t = new TblDepartmanlar();
            t.Ad = textEdit2.Text;
            db.TblDepartmanlar.Add(t);
            db.SaveChanges();
            XtraMessageBox.Show("Departman başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Listele();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textEdit1.Text);
            var deger = db.TblDepartmanlar.Find(x);
            db.TblDepartmanlar.Remove(deger);
            db.SaveChanges();
            XtraMessageBox.Show("Departman başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            Listele();

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            int x = int.Parse(textEdit1.Text);
            var deger = db.TblDepartmanlar.Find(x);
            deger.Ad = textEdit2.Text;
            db.SaveChanges();
            XtraMessageBox.Show("Departman başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Listele();
        }
    }
}
