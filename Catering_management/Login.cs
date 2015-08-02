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
using Catering_management.MODEL;

namespace Catering_management
{
    public partial class Login : Form
    {
        private int userType = 0;
        
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string passWord = txtUserPwd.Text;
            this.ChkUserType();
            SysUser su = new SysUser();
            LoginResult loginResult = UserBLL.CheckLogin(userName, passWord, userType, out su);
            switch(loginResult)
            {
                case LoginResult.Success:
                    //MessageBox.Show("登录成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainFrm mf = new MainFrm(su);
                    mf.Show();
                    this.Hide();
                    break;
                case LoginResult.ErrorPwd:
                    MessageBox.Show("密码错误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case LoginResult.ErrorUtp:
                    MessageBox.Show("用户类型不对！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    MessageBox.Show("用户不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
            #region 旧版本
            //{
            //    try
            //    {
            //        if (textBox1.Text == "" || textBox2.Text == "")
            //        {
            //            MessageBox.Show("用户名或密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //        }
            //        else
            //        {
            //            string name = textBox1.Text.Trim();
            //            string pwd = textBox2.Text.Trim();
            //            //this.radioButton1.Checked = 1;
            //            SqlConnection conn = DBOprate.getConn();
            //            conn.Open();
            //            SqlCommand cmd = new SqlCommand("select * from tb_user where UserName='" + name + "' and UserPwd='" + pwd + "'and UserType='" + userType + "' ", conn);
            //            SqlDataReader sdr = cmd.ExecuteReader();  //结果集放到sdr对象中
            //            Object[] objs = new Object[sdr.FieldCount];
            //            sdr.Read();
            //            if (sdr.HasRows) //如果有数据
            //            {

            //                sdr.GetValues(objs);
            //                //MessageBox.Show(objs[0].ToString());                
            //                DBOprate.close(conn);
            //                MessageBox.Show("登录成功！" + "\n欢迎进入餐饮管理系统！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                User u = new User();
            //                // u.a_id = objs[1].ToString();
            //                u.a_name = objs[1].ToString();
            //                MainFrm MF = new MainFrm();
            //                MF.setUser(u);
            //                MF.MyTemp2 = userType;
            //                MF.Show();
            //                // MessageBox.Show(Convert .ToString(MF.MyTemp2 ));
            //                this.Hide();
            //            }
            //            else
            //            {
            //                //textBox1.Text = "";
            //                //textBox2.Text = "";
            //                MessageBox.Show("用户名和密码有误或权限不对！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //} 
            #endregion
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Register RS = new Register();
            RS.Show();
        }

        private void ChkUserType()
        {
            if (rdbCommon.Checked)
                userType = 2;
            else if (rdbBoss.Checked)
                userType = 1;
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txtUserName.Text = "admin";
            txtUserPwd.Text = "admin";
            rdbBoss.Checked = true;
        }

    }
}
