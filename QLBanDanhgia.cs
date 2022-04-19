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
    public partial class QLBanDanhgia : Form
    {
        QLDRLEntities context = new QLDRLEntities();
        public QLBanDanhgia()
        {
            InitializeComponent(); 
            dgv.AutoGenerateColumns = false;
            reload();
            reloadCbbSinhvien();
            reloadTieuchi();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbbTieuchi.SelectedIndex < 0 || cbbSinhvien.SelectedIndex < 0 || txtSodiem.Text.Trim() == "")
            {
                MessageBox.Show("Thiếu thông tin");
            }
            else
            {
                Danhgia danhgia = new Danhgia();
                danhgia.TieuchiID = Convert.ToInt32(cbbTieuchi.SelectedValue);
                danhgia.SinhvienID = Convert.ToInt32(cbbSinhvien.SelectedValue);
                danhgia.Ngaydanhgia = DateTime.Today;
                danhgia.Diemdanhgia = Convert.ToInt32(txtSodiem.Text);

                context.Danhgias.Add(danhgia);
                context.SaveChanges();
                reload();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
            {
                MessageBox.Show("Chọn đánh giá để sửa");
            }
            else
            {
                if (cbbTieuchi.SelectedIndex < 0 || cbbSinhvien.SelectedIndex < 0 || txtSodiem.Text.Trim() == "")
                {
                    MessageBox.Show("Thiếu thông tin");
                }
                else
                {
                    int id = Convert.ToInt32(lblID.Text);
                    Danhgia danhgia = context.Danhgias.FirstOrDefault(x => x.ID == id);
                    danhgia.TieuchiID = Convert.ToInt32(cbbTieuchi.SelectedValue);
                    danhgia.SinhvienID = Convert.ToInt32(cbbSinhvien.SelectedValue);
                    danhgia.Ngaydanhgia = DateTime.Today;
                    danhgia.Diemdanhgia = Convert.ToInt32(txtSodiem.Text);

                    context.SaveChanges();
                    reload();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
            {
                MessageBox.Show("Chọn đánh giá để xóa");
            }
            else
            {
                int id = Convert.ToInt32(lblID.Text);
                Danhgia danhgia = context.Danhgias.FirstOrDefault(x => x.ID == id);
                context.Danhgias.Remove(danhgia);
                context.SaveChanges();
                reload();
            }
        }

        private void reload()
        {
            if (MainForm.isAdmin)
                dgv.DataSource = context.Danhgias.ToList();
            else
            {
                var lopIds = context.Lops.Where(x => x.GVCN_ID == MainForm.id).Select(x => x.ID).ToList();
                var sinhvienIds = context.Sinhviens.Where(x => lopIds.Contains(x.LopID)).Select(x => x.ID).ToList();
                dgv.DataSource = context.Danhgias.Where(x => sinhvienIds.Contains((int)x.SinhvienID)).ToList();
            }
        }

        public void reloadCbbSinhvien()
        {
            if (MainForm.isAdmin)
            {
                cbbSinhvien.DataSource = context.Sinhviens.ToList();
            }
            else
            {
                var lopIds = context.Lops.Where(x => x.GVCN_ID == MainForm.id).Select(x => x.ID).ToList();
                cbbSinhvien.DataSource = context.Sinhviens.Where(x => lopIds.Contains(x.LopID)).ToList();
            }

            cbbSinhvien.DisplayMember = "Ten";
            cbbSinhvien.ValueMember = "ID";
        }

        public void reloadTieuchi()
        {
            cbbTieuchi.DataSource = context.Tieuchis.ToList();
            cbbTieuchi.DisplayMember = "Noidung";
            cbbTieuchi.ValueMember = "ID";
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                lblID.Text = dgv[0, e.RowIndex].Value.ToString();
                cbbTieuchi.SelectedValue = dgv[1, e.RowIndex].Value;
                cbbSinhvien.SelectedValue = dgv[2, e.RowIndex].Value;
                txtSodiem.Text = dgv[3, e.RowIndex].Value.ToString();
                //MessageBox.Show(dgv[4, e.RowIndex].Value.ToString());

                int svid = Convert.ToInt32(dgv[2, e.RowIndex].Value);
                DateTime date = Convert.ToDateTime(dgv[4, e.RowIndex].Value);
                var data = context.Danhgias.Where(x => x.SinhvienID == svid && x.Ngaydanhgia == date).ToList();
                var sodiem = data.Sum(x => x.Diemdanhgia);
                lblSodiem.Text = sodiem.ToString();
                if (sodiem >= 90)
                    lblDanhgia.Text = "Xuất sắc";
                else if (sodiem >= 80)
                    lblDanhgia.Text = "Tốt";
                else if (sodiem >= 65)
                    lblDanhgia.Text = "Khá";
                else if (sodiem > 50)
                    lblDanhgia.Text = "Trung bình";
                else if (sodiem > 35)
                    lblDanhgia.Text = "Yếu";
                else
                    lblDanhgia.Text = "Kém";

            }
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 1)
                {
                    int id = Convert.ToInt32(e.Value);
                    var tieuchi = context.Tieuchis.FirstOrDefault(x => x.ID == id);
                    if (tieuchi != null)
                        e.Value = tieuchi.Noidung;
                }
                else if (e.ColumnIndex == 2)
                {
                    int id = Convert.ToInt32(e.Value);
                    var sinhvien = context.Sinhviens.FirstOrDefault(x => x.ID == id);
                    if (sinhvien != null)
                        e.Value = sinhvien.Ten;
                }
            }
        }
    }
}