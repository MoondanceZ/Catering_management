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
    public partial class ManageUser : Form
    {
        public ManageUser()
        {
            InitializeComponent();
        }
        SysUser su = new SysUser();
        List<string> suFun = new List<string>();

        private void ManageUser_Load(object sender, EventArgs e)
        {

            this.load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            InitialDgvAuthority();
            //string uType;
            int index = dgvUser.CurrentRow.Index;
            su.Name = this.dgvUser.Rows[index].Cells[1].Value.ToString();
            su.PassWord = this.dgvUser.Rows[index].Cells[2].Value.ToString();
            su.Id = Convert.ToInt32(this.dgvUser.Rows[index].Cells[0].Value.ToString());
            su.UserType = this.dgvUser.Rows[index].Cells[3].Value.ToString() == "店长" ? 1 : 2;
            su.funtion = this.dgvUser.Rows[index].Cells[4].Value.ToString();
            suFun = su.funtion.Split(',').ToArray().ToList();
            if (su.UserType == 1)
            {
                txtAuthority.Text = "店长";
            }
            else
            {
                txtAuthority.Text = "店员";
            }
            txtUserName.Text = su.Name;
            txtPwd.Text = su.PassWord;
            ShowFun(suFun);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            su.Name = txtUserName.Text;
            su.PassWord = txtPwd.Text;
            su.funtion = GetUpdateFun();

            if (UserBLL.UpdateUser(su))
            {
                MessageBox.Show("修改成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.load();
            }
            else
                MessageBox.Show("修改失败！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if(UserBLL.DeleteUser(su))
            {
                MessageBox.Show("删除成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.load();
            }
            else
                MessageBox.Show("删除失败！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information); 
        }
        
        /// <summary>
        /// 载入
        /// </summary>
        public void load()
        {
            DataTable dtUser = UserBLL.GetAllUser();
            txtAuthority.Enabled = false;
            dgvUser.DataSource = dtUser;
            dgvUser.Columns[0].Visible = false;
            dgvUser.Columns[1].HeaderText = "用户名称";
            dgvUser.Columns[2].HeaderText = "用户密码";
            dgvUser.Columns[3].HeaderText = "用户类型";
            dgvUser.Columns[4].Visible = false;

            DataTable dtFun = UserFunBLL.GetFunction();
            foreach (DataRow dr in dtFun.Rows)
            {
                string[] newRow = { "false", dr["ID"].ToString(), dr["funName"].ToString() };
                dgvAuthority.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// 初始化功能列表
        /// </summary>
        private void InitialDgvAuthority()
        {
            for (int i = 0; i < dgvAuthority.Rows.Count; i++)
            {
                dgvAuthority.Rows[i].Cells[0].Value = false;
            }
        }

        /// <summary>
        /// 获取选定的功能
        /// </summary>
        /// <returns></returns>
        private string GetUpdateFun()
        {
            string funStr = null;
            for (int i = 0; i < dgvAuthority.Rows.Count; i++)
            {
                int n = i + 1;
                if ((bool)dgvAuthority.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    funStr += (n.ToString() + ',');
                }
            }
            return funStr.Substring(0, funStr.Length - 1);
        }

        /// <summary>
        /// 显示拥有权限
        /// </summary>
        /// <param name="funID"></param>
        private void ShowFun(List<string> funID)
        {
            foreach (string id in funID)
            {
                int idToNUm = Convert.ToInt32(id);
                dgvAuthority.Rows[idToNUm - 1].Cells[0].Value = true;
            }
        }
    }
}
