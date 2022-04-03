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
    public partial class frmReportSVbyLop : Form
    {
        public static int lopId = 0;

        public frmReportSVbyLop()
        {
            InitializeComponent();
            rptSinhvienByLop rp = new rptSinhvienByLop();
            rp.SetParameterValue("@LopID", lopId);
            crystalReportViewer1.ReportSource = rp;
        }
    }
}
