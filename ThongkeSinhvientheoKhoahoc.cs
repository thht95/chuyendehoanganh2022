using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDRL
{
    public partial class ThongkeSinhvientheoKhoahoc : Form
    {
        public static DateTime ngaydanhgia = DateTime.Now;
        public ThongkeSinhvientheoKhoahoc()
        {
            InitializeComponent();

            //SqlConnection con = new SqlConnection("data source=.;initial catalog=QLDRL;integrated security=True");
            //var cmd = con.CreateCommand();
            //cmd.CommandText = "allSinhvienByNgayDanhgia";
            //cmd.Parameters.AddWithValue("@Ngaydanhgia", ngaydanhgia);
            //cmd.CommandType = CommandType.StoredProcedure;
            //DataTable dt = new DataTable();
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dt);

            rptSinhvien rp = new rptSinhvien();
            rp.SetParameterValue("@Ngaydanhgia", ngaydanhgia);
            crystalReportViewer1.ReportSource = rp;
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        } 
    }
}
