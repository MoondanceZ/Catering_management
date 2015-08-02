using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Catering_management.BLL;

namespace Catering_management.frmNow
{
    public partial class Now : Form
    {
        public Now()
        {
            InitializeComponent();
        }
        string time1, time2;  //记录当前时间

        private void Now_Load(object sender, EventArgs e)
        {
            time1 = DateTime.Now.ToString("yyyy-MM-dd");
            time2 = DateTime.Now.ToString("H:mm");
            DataTable dtBook = BookBLL.GetDtBook(1, time1);
            foreach (DataRow dr in dtBook.Rows)
            {
                string[] newRow ={dr[0].ToString (),dr[1].ToString (),dr[2].ToString (),dr[3].ToString (),dr[4].ToString (),
                                         dr[5].ToString (),dr[6].ToString (),dr[7].ToString (),dr[8].ToString ()};
                dataGridView2.Rows.Add(newRow);
            }

            DataTable dtBill = BillBLL.GetDtBill(1, time1);
            foreach (DataRow dr in dtBill.Rows)
            {
                string[] newRow ={dr[0].ToString (),dr[1].ToString (),dr[2].ToString (),dr[3].ToString (),dr[4].ToString (),
                                         dr[5].ToString (),dr[6].ToString (),dr[7].ToString (),dr[8].ToString ()};
                dataGridView1.Rows.Add(newRow);
            }
                                
        }
    }
}
