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
    public partial class QLLop : Form
    {
        QLDRLEntities context = new QLDRLEntities();
        public QLLop()
        {
            InitializeComponent();
            lbl.Visible = cbbGVCN.Visible = MainForm.isAdmin;
            dgv.AutoGenerateColumns = false;
            reload();
            reloadCbb();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtTen.Text.Trim() == "")
            {
                MessageBox.Show("Chưa nhập tên lớp");
            }
            else
            {
                Lop lop = new Lop();
                if(MainForm.isAdmin)
                {
                    lop.GVCN_ID = Convert.ToInt32(cbbGVCN.SelectedValue);
                    MessageBox.Show(lop.GVCN_ID.ToString());
                }
                else
                {
                    lop.GVCN_ID = MainForm.id;
                    MessageBox.Show(lop.GVCN_ID.ToString());
                }
                lop.Ten = txtTen.Text;

                context.Lops.Add(lop);
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
                int id = Convert.ToInt32(lblID.Text);
                Lop lop = context.Lops.FirstOrDefault(x => x.ID == id);
                lop.Ten = txtTen.Text;

                context.SaveChanges();
                reload();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lblID.Text == "")
            {
                MessageBox.Show("Chọn lớp để xóa");
            }
            else
            {
                int id = Convert.ToInt32(lblID.Text);
                Lop lop = context.Lops.FirstOrDefault(x => x.ID == id);
                if (lop.Sinhviens.Count > 0)
                {
                    MessageBox.Show("Lớp có sinh viên, không thể xóa");
                }
                else
                {
                    context.Lops.Remove(lop);
                    context.SaveChanges();
                    reload();
                }
            }
        }
        
        private void reload()
        {
            if (MainForm.isAdmin)
                dgv.DataSource = context.Lops.ToList();
            else 
                dgv.DataSource = context.Lops.Where(x => x.GVCN_ID == MainForm.id).ToList();
        }

        private void reloadCbb()
        {
            cbbGVCN.DataSource = context.CVHTs.ToList();
            cbbGVCN.ValueMember = "ID";
            cbbGVCN.DisplayMember = "Ten";
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

        private void QLLop_Load(object sender, EventArgs e)
        {

        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {

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
                    dgv.DataSource = context.Lops.Where(x => x.GVCN_ID == MainForm.id && (x.Ten.Contains(timkiem))).ToList();
                }
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                lblID.Text = dgv[0, e.RowIndex].Value.ToString();
                txtTen.Text = dgv[1, e.RowIndex].Value.ToString();
            }
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1 && e.ColumnIndex == 2)
            {
                int id = Convert.ToInt32(e.Value);
                var cvht = context.CVHTs.FirstOrDefault(x => x.ID == id);
                e.Value = cvht.Ten;
            }
        }
    }
}
