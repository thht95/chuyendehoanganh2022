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
    public partial class MainForm : Form
    {
        public static int id = 1;
        public static bool isAdmin = true;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void đổiMậtKhẩuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoiMatKhau doiMatKhau = new DoiMatKhau();
            doiMatKhau.MdiParent = this;
            doiMatKhau.Show();
        }

        private void đăngXuấtToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = Application.OpenForms["Dangnhap"];
            form.Show();
        }

        private void quảnLýLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLLop qLLop = new QLLop();
            qLLop.MdiParent = this;
            qLLop.Show();
        }

        private void quảnLýTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLTaikhoan qLTaikhoan = new QLTaikhoan();
            qLTaikhoan.MdiParent = this;
            qLTaikhoan.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            quảnLýTàiKhoảnToolStripMenuItem.Visible = isAdmin;
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLSinhvien qlSinhvien = new QLSinhvien();
            qlSinhvien.MdiParent = this;
            qlSinhvien.Show();
        }
        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                ThongkeSinhvientheoKhoahoc form = new ThongkeSinhvientheoKhoahoc();
                form.MdiParent = this;
                form.Show();
            }
        }

        private void phiếuĐánhGiáToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLPhieuDanhgia form = new QLPhieuDanhgia();
            form.MdiParent = this;
            form.Show();
        }

        private void danhSáchTiêuChíToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QLBanDanhgia form = new QLBanDanhgia();
            form.MdiParent = this;
            form.Show();
        }

        private void thốngKêSinhViênTheoKhóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterBeforeReport.type = "time";
            FilterBeforeReport form = new FilterBeforeReport();
            form.MdiParent = this;
            form.Show();
        }

        private void thốngKêTheoLớpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FilterBeforeReport.type = "Lop";
            FilterBeforeReport form = new FilterBeforeReport();
            form.MdiParent = this;
            form.Show();
        }
    }
}
