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
    public partial class FrmPersonelistatistik : Form
    {
        public FrmPersonelistatistik()
        {
            InitializeComponent();
        }

        DbisTakipEntities db = new DbisTakipEntities();
        private void FrmPersonelistatistik_Load(object sender, EventArgs e)
        {
            labelControl11.Text = db.TblDepartmanlar.Count().ToString();
            labelControl9.Text = db.TblFirmalar.Count().ToString();
            labelControl3.Text = db.TblPersonel.Count().ToString();
            labelControl17.Text = db.TblGorevler.Count(x => x.Durum==true).ToString();
            labelControl23.Text = db.TblGorevler.Count(x => x.Durum == false).ToString();
            labelControl5.Text = db.TblGorevler.Select(x => x.Aciklama).FirstOrDefault();
            labelControl15.Text = db.TblFirmalar.Select(x => x.il).Distinct().Count().ToString();
            labelControl7.Text = db.TblFirmalar.Select(x => x.Sektor).Distinct().Count().ToString();
            DateTime bugun = DateTime.Today;
            labelControl21.Text = db.TblGorevler.Count(x => x.Tarih == bugun).ToString();
            var d1 = db.TblGorevler.GroupBy(x => x.GorevAlan).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            labelControl13.Text = db.TblPersonel.Where(x => x.ID == d1).Select(y => y.Ad + " " + y.Soyad).FirstOrDefault().ToString();
            labelControl19.Text = db.TblDepartmanlar.Where(x => x.ID == db.TblPersonel.Where(t => t.ID == d1).Select(z => z.Departman).FirstOrDefault()).Select(y => y.Ad).FirstOrDefault().ToString();
        }
    }
}
