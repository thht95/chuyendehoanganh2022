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
    public partial class Dangnhap : Form
    {
        QLDRLEntities context = new QLDRLEntities();

        public Dangnhap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var cvht = context.CVHTs.FirstOrDefault(x => x.Password == txtPassword.Text && x.Username == txtUsername.Text);
            if(cvht != null)
            {
                this.Hide();
                MainForm mainForm = new MainForm();
                MainForm.id = cvht.ID;
                MainForm.isAdmin = cvht.Admin == true;
                mainForm.Show();
                txtPassword.Text = txtUsername.Text = "";
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void Dangnhap_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
