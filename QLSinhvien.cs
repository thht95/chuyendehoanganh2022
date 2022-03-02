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
    public partial class QLSinhvien : Form
    {
        QLDRLEntities context = new QLDRLEntities();
        public QLSinhvien()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
            reload();
            reloadCbb();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập tên sinh viên");
            }
            else
            {
                Sinhvien sinhvien = new Sinhvien();
                sinhvien.Ten = txtTen.Text;
                sinhvien.Ngaysinh = dtp.Value;
                sinhvien.LopID = Convert.ToInt32(cbbLop.SelectedValue);
                sinhvien.Gioitinh = rbtnNam.Checked;

                context.Sinhviens.Add(sinhvien);
                context.SaveChanges();
                reload();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
            {
                MessageBox.Show("Chọn sinh viên để sửa");
            }
            else
            {
                int id = Convert.ToInt32(lblID.Text);
                Sinhvien sinhvien = context.Sinhviens.FirstOrDefault(x => x.ID == id);
                sinhvien.Ten = txtTen.Text;
                sinhvien.Ngaysinh = dtp.Value;
                sinhvien.LopID = Convert.ToInt32(cbbLop.SelectedValue);
                sinhvien.Gioitinh = rbtnNam.Checked;

                context.SaveChanges();
                reload();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
            {
                MessageBox.Show("Chọn sinh viên để xóa");
            }
            else
            {
                int id = Convert.ToInt32(lblID.Text);
                Sinhvien sinhvien = context.Sinhviens.FirstOrDefault(x => x.ID == id);
                if (sinhvien.Danhgias.Count > 0)
                {
                    MessageBox.Show("Sinh viên có bản đánh giá, không thể xóa");
                }
                else
                {
                    context.Sinhviens.Remove(sinhvien);
                    context.SaveChanges();
                    reload();
                }
            }
        }

        private void reload()
        {
            if (MainForm.isAdmin)
                dgv.DataSource = context.Sinhviens.ToList();
            else
            {
                var lopIds = context.Lops.Where(x => x.GVCN_ID == MainForm.id).Select(x => x.ID).ToList();
                dgv.DataSource = context.Sinhviens.Where(x => lopIds.Contains(x.LopID)).ToList();
            }
        }
        private void reloadCbb()
        {
            cbbLop.DataSource = MainForm.isAdmin ? context.Lops.ToList() : context.Lops.Where(x => x.GVCN_ID == MainForm.id).ToList();
            cbbLop.ValueMember = "ID";
            cbbLop.DisplayMember = "Ten";
        }

        private void txtTimkiem_Enter(object sender, EventArgs e)
        {
            if (txtTimkiem.Text.Trim() == "Tìm kiếm")
            {
                txtTimkiem.Text = "";
                txtTimkiem.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            }
        }

        private void txtTimkiem_Leave(object sender, EventArgs e)
        {
            if (txtTimkiem.Text.Trim() == "")
            {
                txtTimkiem.Text = "Tìm kiếm";
                txtTimkiem.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Italic, GraphicsUnit.Point, ((byte)(0)));
            }
        }

        private void txtTimkiem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var timkiem = txtTimkiem.Text.Trim();
                if (timkiem == "")
                {
                    reload();
                }
                else
                {
                    if (MainForm.isAdmin)
                        dgv.DataSource = context.Sinhviens.Where(x => x.Ten.Contains(timkiem)).ToList();
                    else
                    {
                        var lopIds = context.Lops.Where(x => x.GVCN_ID == MainForm.id).Select(x => x.ID).ToList();
                        dgv.DataSource = context.Sinhviens.Where(x => lopIds.Contains(x.LopID)).Where(x => x.Ten.Contains(timkiem)).ToList();
                    }
                }
            }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                lblID.Text = dgv[0, e.RowIndex].Value.ToString();
                txtTen.Text = dgv[1, e.RowIndex].Value.ToString();
                dtp.Value = dgv[2, e.RowIndex].Value == null ? new DateTime() : Convert.ToDateTime(dgv[2, e.RowIndex].Value);
                if (dgv[3, e.RowIndex].Value == null || Convert.ToBoolean(dgv[3, e.RowIndex].Value))
                    rbtnNam.Checked = true;
                else
                    rbtnNu.Checked = true;
                cbbLop.SelectedValue = (dgv[4, e.RowIndex].Value);
            }
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 4)
                {
                    int id = Convert.ToInt32(e.Value);
                    var lop = context.Lops.FirstOrDefault(x => x.ID == id);
                    if (lop != null)
                        e.Value = lop.Ten;
                }
                
                else if (e.ColumnIndex == 3)
                {
                    if (e.Value != null && Convert.ToBoolean(e.Value))
                    {
                        e.Value = "Nam";
                    }
                    else
                    {
                        e.Value = "Nữ";
                    }
                }

                else if (e.ColumnIndex == 2)
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("d");
                }
            }
        }
    }
}
