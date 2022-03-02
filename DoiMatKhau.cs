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
    public partial class DoiMatKhau : Form
    {
        QLDRLEntities context = new QLDRLEntities();

        public DoiMatKhau()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtMkMoi.Text != txtXacnhan.Text)
            {
                MessageBox.Show("Mật khẩu mới và xác nhận mật khẩu chưa trùng");
            }
            else
            {
                int cvhtId = MainForm.id;
                var cvht = context.CVHTs.FirstOrDefault(x => x.ID == cvhtId && x.Password == txtMKcu.Text);
                if (cvht == null)
                {
                    MessageBox.Show("Mật khẩu cũ không chính xác");
                }
                else
                {
                    cvht.Password = txtMkMoi.Text;
                    context.SaveChanges();
                    MessageBox.Show("Thành công");
                }
            }
        }
    }
}
