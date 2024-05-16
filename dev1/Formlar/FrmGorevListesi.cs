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

namespace dev1.Formlar
{
    public partial class FrmGorevListesi : Form
    {
        public FrmGorevListesi()
        {
            InitializeComponent();
        }

        DbisTakipEntities db = new DbisTakipEntities();
        private void FrmGorevListesi_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = (from x in db.TblGorevler
                                       select new
                                       {
                                           x.ID,
                                           x.Aciklama
                                       }).ToList();


            

            labelControl3.Text = db.TblGorevler.Where(x => x.Durum == true).Count().ToString();
            labelControl5.Text = db.TblGorevler.Where(x => x.Durum == false).Count().ToString();
            labelControl2.Text = db.TblDepartmanlar.Count().ToString();

            chartControl1.Series["Durum"].Points.AddPoint("Aktif Görevler", int.Parse(labelControl3.Text));
            chartControl1.Series["Durum"].Points.AddPoint("Pasif Görevler", int.Parse(labelControl5.Text));

        }
    }
}
