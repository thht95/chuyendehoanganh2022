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
    public partial class QLTaikhoan : Form
    {
        QLDRLEntities context = new QLDRLEntities();

        public QLTaikhoan()
        {
            InitializeComponent();
            reload();
            dgv.AutoGenerateColumns = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkData())
            {
                CVHT cvht = new CVHT();
                cvht.Ten = txtTen.Text;
                cvht.SDT = txtSDT.Text;
                cvht.Username = txtUsername.Text;
                cvht.Password = txtPassword.Text;
                cvht.Diachi = txtDiachi.Text;
                cvht.Gioitinh = rbtnNam.Checked;
                cvht.Admin = rbtnIsAdmin.Checked;

                context.CVHTs.Add(cvht);
                context.SaveChanges();
                reload();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
            {
                MessageBox.Show("Chọn lớp để sửa");
            }
            else
            {
                if (checkData())
                {
                    int id = Convert.ToInt32(lblID.Text);
                    CVHT cvht = context.CVHTs.FirstOrDefault(x => x.ID == id);
                    cvht.Ten = txtTen.Text;
                    cvht.Admin = rbtnIsAdmin.Checked;
                    cvht.Gioitinh = rbtnNam.Checked;
                    cvht.Username = txtUsername.Text;
                    cvht.Password = txtPassword.Text;
                    cvht.Diachi = txtDiachi.Text;

                    context.SaveChanges();
                    reload();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
            {
                MessageBox.Show("Chọn tài khoản để xóa");
            }
            else
            {
                int id = Convert.ToInt32(lblID.Text);
                CVHT cvht = context.CVHTs.FirstOrDefault(x => x.ID == id);
                if (cvht.Lops.Count > 0)
                {
                    MessageBox.Show("Cố vấn học tập này có lớp đang quản lý. Không thể xóa");
                }
                else
                {
                    context.CVHTs.Remove(cvht);
                    context.SaveChanges();
                    reload();
                }
            }
        }

        private bool checkData()
        {
            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "" || txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Username, Password, Tên không được phép để trống");
                return false;
            }
            return true;
        }
        private void reload()
        {
            dgv.DataSource = context.CVHTs.ToList();
        }

        private void QLTaikhoan_Load(object sender, EventArgs e)
        {

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                lblID.Text = dgv[0, e.RowIndex].Value.ToString();
                txtTen.Text = dgv[1, e.RowIndex].Value == null ? "" : dgv[1, e.RowIndex].Value.ToString();
            }
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }
    }
}
