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
    public partial class QLPhieuDanhgia : Form
    {
        QLDRLEntities context = new QLDRLEntities();

        public QLPhieuDanhgia()
        {
            InitializeComponent();
            dgv.AutoGenerateColumns = false;
            reload();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtNoidung.Text.Trim() == "" || txtSodiem.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập nội dung và số điểm");
            }
            else
            {
                Tieuchi tieuchi = new Tieuchi();
                tieuchi.Noidung = txtNoidung.Text;
                tieuchi.Sodiem = Convert.ToInt32(txtSodiem.Text);

                context.Tieuchis.Add(tieuchi);
                context.SaveChanges();
                reload();
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "" )
            {
                MessageBox.Show("Chọn tiêu chí để sửa");
            }
            else
            {
                if (txtNoidung.Text.Trim() == "" || txtSodiem.Text.Trim() == "")
                {
                    MessageBox.Show("Chưa nhập nội dung và số điểm");
                }
                else
                {
                    int id = Convert.ToInt32(lblID.Text);
                    Tieuchi tieuchi = context.Tieuchis.FirstOrDefault(x => x.ID == id);
                    tieuchi.Noidung = txtNoidung.Text;
                    tieuchi.Sodiem = Convert.ToInt32(txtSodiem.Text);

                    context.SaveChanges();
                    reload();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
            {
                MessageBox.Show("Chọn tiêu chí để xóa");
            }
            else
            {
                int id = Convert.ToInt32(lblID.Text);
                Tieuchi tieuchi = context.Tieuchis.FirstOrDefault(x => x.ID == id);
                if (tieuchi.Danhgias.Count > 0)
                {
                    MessageBox.Show("Tiêu chí này có bản đánh giá, không thể xóa");
                }
                else
                {
                    context.Tieuchis.Remove(tieuchi);
                    context.SaveChanges();
                    reload();
                }
            }
        }

        private void reload()
        {
            dgv.DataSource = context.Tieuchis.ToList();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                lblID.Text = dgv[0, e.RowIndex].Value.ToString();
                txtNoidung.Text = dgv[1, e.RowIndex].Value.ToString();
                txtSodiem.Text = dgv[2, e.RowIndex].Value.ToString();
            }
        }
    }
}
