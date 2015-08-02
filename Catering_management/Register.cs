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

namespace Catering_management
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        //bool flagRegister;  //定义标识符，确认账号注册
        private void button1_Click(object sender, EventArgs e)
        {
            string ChkRegStr = UserBLL.CheckRgeister(txtUserName.Text, txtPwd.Text, txtChkPwd.Text);
            MessageBox.Show(ChkRegStr, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
