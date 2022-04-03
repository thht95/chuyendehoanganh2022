using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDRL
{
    public partial class FilterBeforeReport : Form
    {
        QLDRLEntities context = new QLDRLEntities();
        public static string type = "";

        public FilterBeforeReport()
        {
            InitializeComponent();
            if (type == "time")
            {
                reloadCbbTime();
                label2.Hide();
                cbbLop.Hide();
            }
            else
            {
                reloadCbbLop();
                label1.Hide();
                cbb.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (type == "time")
            {
                ThongkeSinhvientheoKhoahoc.ngaydanhgia = (DateTime)cbb.SelectedValue;
                ThongkeSinhvientheoKhoahoc form = new ThongkeSinhvientheoKhoahoc();
                form.MdiParent = this.ParentForm;
                form.Show();
            }
            else
            {
                frmReportSVbyLop.lopId = (int) cbbLop.SelectedValue;
                frmReportSVbyLop form = new frmReportSVbyLop();
                form.MdiParent = this.ParentForm;
                form.Show();
            }
        }

        private void reloadCbbTime()
        {
            cbb.DataSource = context.Danhgias.Select(x => x.Ngaydanhgia).Distinct().ToList();
        }

        private void reloadCbbLop()
        {
            cbbLop.DataSource = MainForm.isAdmin ? context.Lops.ToList() : context.Lops.Where(x => x.GVCN_ID == MainForm.id).ToList();
            cbbLop.ValueMember = "ID";
            cbbLop.DisplayMember = "Ten";
        }
    }
}
