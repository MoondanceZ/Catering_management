using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Catering_management.MODEL;
using Catering_management.BLL;

namespace Catering_management
{
    public partial class MainFrm : Form
    {
        SysUser su = new SysUser();
        public MainFrm()
        {
            InitializeComponent();
        }
        public MainFrm(SysUser su)
            : this()
        {
            this.su = su;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            label1.Text = "当前用户：" + su.Name;
            label3.Text = "登录时间：" + DateTime.Now.ToString();

            if (su.UserType == 2)
            {
                label2.Text = "用户权限：员工";
            }
            else
            {
                label2.Text = "用户权限：店长";
            }

            ShowFunBtn();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            frmEmployee.Employee Ep = new frmEmployee.Employee();
            Ep.Show();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            frmMenu.MenuForm Mn = new frmMenu.MenuForm();
            Mn.Show();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmMember.MemberForm Mb = new frmMember.MemberForm();
            Mb.Show();
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            frmBill.Bill Bi = new frmBill.Bill();
            Bi.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmBook.Book Bk = new frmBook.Book();
            Bk.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)   //餐厅现状
        {
            frmNow.Now now = new frmNow.Now();
            now.Show();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)  //顾客结算
        {
            frmCheckout.Checkout Ck = new frmCheckout.Checkout();
            Ck.Show();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)  //营业管理
        {
            frmTurnover.Turnover To = new frmTurnover.Turnover();
            To.Show();
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            frmStock.Stock St = new frmStock.Stock();
            St.Show();
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void 数据备份ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = "";
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.ShowDialog();
                fileName = saveFile.FileName;
                if (fileName == "")
                    return;
                if (BkpAndResBLL.BackupsDB(fileName))
                    MessageBox.Show("数据库备份成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 数据还原ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Filter = "BakUp files (*.bak)|*.bak";
                openFile.ShowDialog();
                string fileName = "";
                fileName = openFile.FileName;
                if (fileName == "")
                    return;
                if (BkpAndResBLL.RestoreDB(fileName))
                    MessageBox.Show("数据库恢复成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void 安全退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 密码修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSafe.ChangePWD Cpwd = new frmSafe.ChangePWD();
            //Cpwd.setUser(u);
            Cpwd.Show();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("感谢您的使用，有任何疑问可以咨询：" + "\n139********");
        }

        private void 用户管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageUser MU = new ManageUser();
            MU.Show();
        }

        /// <summary>
        /// 根据权限显示可用功能
        /// </summary>
        private void ShowFunBtn()
        {
            for (int j = 0; j < toolStrip1.Items.Count; j++)
            {
                toolStrip1.Items[j].Enabled = false;
            }
            string[] funStr = UserFunBLL.GetfunStr(su.funtion);
            for (int i = 0; i < funStr.Length; i++)
            {
                for (int j = 0; j < toolStrip1.Items.Count; j++)
                {
                    if (Convert.ToInt32(toolStrip1.Items[j].Tag) == Convert.ToInt32(funStr[i]))
                    {
                        toolStrip1.Items[j].Enabled = true;
                    }
                }
            }
            toolStripButton8.Enabled = true;
        }
    }
}