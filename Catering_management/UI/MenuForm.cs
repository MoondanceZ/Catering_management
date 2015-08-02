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

namespace Catering_management.frmMenu
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        int flag = 0; //定义添加修改操作标识
        private void Menu_Load(object sender, EventArgs e)
        {
            this.load();
        }

        private void load()
        {
            dataGridView1.Rows.Clear();
            groupBox1.Enabled = false;
            DataTable dtMenu = MenuBLL.GetMenu();
            foreach (DataRow dr in dtMenu.Rows)
            {
                string[] newRow = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString() };
                dataGridView1.Rows.Add(newRow);
            }

            List<MenuInfo> Category = new List<MenuInfo>();
            Category = MenuBLL.GetMenuCategory();
            cbType.DisplayMember = "mName";
            cbType.ValueMember = "ID";
            cbType.DataSource = Category;

            List<MenuInfo> Category1 = new List<MenuInfo>();
            Category1 = MenuBLL.GetMenuCategory();
            MenuInfo mi = new MenuInfo();
            mi.mName = "-- 全部 --";
            mi.ID = -1;
            Category1.Add(mi);
            Category1.Reverse();
            tlspCbx.ComboBox.DisplayMember = "mName";
            tlspCbx.ComboBox.ValueMember = "ID";
            tlspCbx.ComboBox.DataSource = Category1;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string type = dataGridView1.Rows[i].Cells[3].Value.ToString();
                foreach (var m in Category)
                {
                    if (type == m.ID.ToString())
                    {
                        dataGridView1.Rows[i].Cells[3].Value = m.mName;
                    }
                }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string Num = "";
            int newNum = 0;
            this.closeBtn();
            this.clearText();
            flag = 0;
            DataTable dtMenu = MenuBLL.GetMenu();
            if (dtMenu.Rows.Count == 0)
            {
                txtNum.Text = "M1001";
            }
            else
            {
                Num = Convert.ToString(dtMenu.Rows[dtMenu.Rows.Count - 1]["mNum"]);
                newNum = Convert.ToInt32(Num.Substring(1, 4)) + 1;
                Num = "M" + newNum.ToString();
                txtNum.Text = Num;
            }
        }

        private void cancelEnabled()
        {
            groupBox1.Enabled = false;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            toolStripButton4.Enabled = true;
            toolStripButton5.Enabled = true;
            toolStripButton6.Enabled = true;
            toolStripButton7.Enabled = true;

        }
        private void showBtn()
        {
            groupBox1.Enabled = true;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            toolStripButton6.Enabled = true;
        }
        private void closeBtn()
        {
            groupBox1.Enabled = true;
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = false;
            toolStripButton6.Enabled = false;
        }
        private void clearText()
        {
            txtNum.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPrice.Text = string.Empty;
            cbType.Text = string.Empty;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)  //查询
        {
            dataGridView1.Rows.Clear();
            this.clearText();
            groupBox1.Enabled = true;

            string searchTxt = txtSearch.Text;
            string searchType = tlspCbx.ComboBox.SelectedValue.ToString();

            DataTable dtMenu = MenuBLL.GetMenuByNumOrName(searchTxt, searchType);
            foreach (DataRow dr in dtMenu.Rows)
            {
                string[] newRow = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString() };
                dataGridView1.Rows.Add(newRow);
            }

            List<MenuInfo> Category = new List<MenuInfo>();
            Category = MenuBLL.GetMenuCategory();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string type = dataGridView1.Rows[i].Cells[3].Value.ToString();
                foreach (var m in Category)
                {
                    if (type == m.ID.ToString())
                    {
                        dataGridView1.Rows[i].Cells[3].Value = m.mName;
                    }
                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            txtNum.Enabled = false;
            this.closeBtn();
            flag = 1;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (flag == 0)  //新增
            {
                if (txtNum.Text == "" || txtName.Text == "" || txtPrice.Text == "" || cbType.Text == "")
                {
                    MessageBox.Show("请将信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    try
                    {
                        MenuInfo mi = new MenuInfo();
                        mi.mNun = txtNum.Text;
                        mi.mName = txtName.Text;
                        mi.mType = Convert.ToInt32(cbType.SelectedValue);
                        mi.mPrice = (float)Convert.ToDouble(txtPrice.Text);
                        if (MenuBLL.InsertMenu(mi))
                        {
                            MessageBox.Show("增加菜式成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.showBtn();
                            this.clearText();
                            this.load();
                        }                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else  //编辑
            {
                int index = dataGridView1.CurrentRow.Index; //获取当前记录的索引号
                MenuInfo mi = new MenuInfo();
                mi.ID = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                mi.mNun = txtNum.Text;
                mi.mName = txtName.Text;
                mi.mType = Convert.ToInt32(cbType.SelectedValue);
                mi.mPrice = (float)Convert.ToDouble(txtPrice.Text);

                try
                {
                    if (MenuBLL.UpdateMenu(mi))
                    {
                        MessageBox.Show("编辑成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.showBtn();
                        this.load();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.cancelEnabled();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1) return;
            int index = dataGridView1.CurrentRow.Index;  //获取当前记录索引号
            dataGridView1.Rows[index].Selected = true;
            string curNo = this.dataGridView1.Rows[index].Cells[1].Value.ToString();

            try
            {
                if (MenuBLL.DeleteMenu(curNo))
                {
                    MessageBox.Show("删除成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) //单机单元格
        {
            int index = dataGridView1.CurrentRow.Index;
            txtNum.Text = this.dataGridView1.Rows[index].Cells[1].Value.ToString();
            txtName.Text = this.dataGridView1.Rows[index].Cells[2].Value.ToString();
            cbType.Text = this.dataGridView1.Rows[index].Cells[3].Value.ToString();
            txtPrice.Text = this.dataGridView1.Rows[index].Cells[4].Value.ToString();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            string category = txtSearch.Text.Trim();
            if (String.IsNullOrEmpty(txtSearch.Text.Trim()))
            {
                MessageBox.Show("请输入餐品大类！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (MenuBLL.InsertCategory(category))
                    MessageBox.Show("添加餐品类型成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tlspDeleteCtlg_Click(object sender, EventArgs e)
        {
            string category = txtSearch.Text.Trim();
            if (MessageBox.Show("删除菜单大类会将同时所有属于其的餐品，是否删除？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (MenuBLL.DeleteCategory(category))
                    MessageBox.Show("删除餐品类型成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


    }
}
