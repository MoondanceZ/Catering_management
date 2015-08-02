using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Catering_management.frmSafe
{
    public partial class ChangePWD : Form
    {
        public ChangePWD()
        {
            InitializeComponent();
        }

        //private User u;
        //public void setUser(User user)
        //{
        //    this.u = user;
        //}

        private void button1_Click(object sender, EventArgs e)
        {
            //this.checkPWD();
            //if(textBox2.Text!=textBox3.Text)
            //{
            //    MessageBox.Show("两次输入不同，请重新输入！","错误提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //}
            //else if ((textBox2.Text.Length < 4) && (textBox2.Text.Length > 8))
            //{
            //    MessageBox.Show("密码长度不符合，请重新输入！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    string str = "update tb_user set UserPwd = '"+textBox2.Text+"' from tb_user where userName='"+u.a_name+"'";
            //    SqlCommand cmd = DBOprate.getCom(str);
            //  // int i = cmd.ExecuteNonQuery();
            //    //if (i > 0)
            //    //{
            //        MessageBox.Show("修改成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //}
            //        this.Close();

            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       /* private void checkPWD()
        {
            string str = "select * from tb_user where userPwd='" + textBox1.Text + "' and userName='" + u.a_name + "'";
            SqlCommand cmd = DBOprate.getCom(str);
            int i = cmd.ExecuteNonQuery();
            if (i < 0)
            {
                MessageBox.Show("旧密码输入错误！", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }*/
    }
}
