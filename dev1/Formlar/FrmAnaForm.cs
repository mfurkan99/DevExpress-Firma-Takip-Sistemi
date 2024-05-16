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
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }

        DbisTakipEntities db = new DbisTakipEntities();
        private void FrmAnaForm_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = (from x in db.TblGorevler
                                       select new
                                       {
                                           x.Aciklama,
                                           Personel = x.TblPersonel.Ad + " " + x.TblPersonel.Soyad,
                                           x.Durum
                                       }).Where(y => y.Durum == true).ToList();
            gridView1.Columns["Durum"].Visible = false;

            DateTime bugun = DateTime.Parse(DateTime.Now.ToShortDateString());

            gridControl2.DataSource = (from x in db.TblGorevDetaylar
                                       select new
                                       {
                                           Görev = x.TblGorevler.Aciklama,
                                           x.Aciklama,
                                           x.Tarih
                                       }).Where(x => x.Tarih == bugun).ToList();
        }
    }
}
