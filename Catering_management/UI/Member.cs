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

namespace Catering_management.frmMember
{
    public partial class MemberForm : Form
    {
        public MemberForm()
        {
            InitializeComponent();
        }
        int flag;
        //int grade;
        int disc;  //等级和折扣

        private void Member_Load(object sender, EventArgs e)
        {
            this.load();
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            this.clearText();
            groupBox1.Enabled = true;

            try
            {
                DataTable dtMember = MemberBLL.GetDtMember(toolStripTextBox1.Text.Trim(), tlspCbx.ComboBox.SelectedValue.ToString());
                foreach (DataRow dr in dtMember.Rows)
                {
                    string[] newRow = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString() };
                    dataGridView1.Rows.Add(newRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 添加会员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)  //添加会员
        {
            this.closeBtn();
            this.clearText();
            flag = 0;
            this.AddNum();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)  //编辑会员
        {
            txtNum.Enabled = false;
            this.closeBtn();
            flag = 1;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)  //保存
        {
            MemberInfo mi = new MemberInfo();
            MDiscount ds = new MDiscount();
            mi.Num = txtNum.Text;
            mi.Name = txtName.Text;
            mi.Grade = Convert.ToInt32(cbGarde.SelectedValue);
            ds.Discount = disc;
            mi.MDiscount = ds;
            if (flag == 0)
            {
                if (txtNum.Text == "" || txtName.Text == "" || cbDisc.Text == "" || cbGarde.Text == "")
                {
                    MessageBox.Show("请将信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    try
                    {                                               
                        if(MemberBLL.InsertMember(mi))
                        MessageBox.Show("增加会员成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.showBtn();
                        this.clearText();
                        this.load();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
            {
                mi.ID = memberId;                
                try
                {                                     
                    if (MemberBLL.UpdateMember(mi))
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

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (MemberBLL.DeleteMember(memberId.ToString()))
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
        private void load()
        {
            dataGridView1.Rows.Clear();
            setComboBox();  //设置下拉框
            groupBox1.Enabled = false;
            try
            {
                DataTable dtMember = MemberBLL.GetDtMember(String.Empty, tlspCbx.ComboBox.SelectedValue.ToString());
                foreach (DataRow dr in dtMember.Rows)
                {
                    string[] newRow = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString() };
                    dataGridView1.Rows.Add(newRow);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setComboBox()
        {
            List<BindComboBox> list = new List<BindComboBox>();
            list.Add(new BindComboBox("0", "-- 全部 --"));
            list.Add(new BindComboBox("1", "高级会员"));
            list.Add(new BindComboBox("2", "普通会员"));
            tlspCbx.ComboBox.DisplayMember = "Value";
            tlspCbx.ComboBox.ValueMember = "Key";
            tlspCbx.ComboBox.DataSource = list;


            List<BindComboBox> list1 = new List<BindComboBox>();
            list1.Add(new BindComboBox("1", "高级会员"));
            list1.Add(new BindComboBox("2", "普通会员"));
            cbGarde.DisplayMember = "Value";
            cbGarde.ValueMember = "Key";
            cbGarde.DataSource = list1;
        }

        #region 界面控制
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
        private void closeBtn()  //按键隐藏
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
            cbGarde.Text = string.Empty;
            cbDisc.Text = string.Empty;
        }
        #endregion        

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDisc.Text == "9折")
            {
                disc = 9;
            }
            else if (cbDisc.Text == "8折")
            {
                disc = 8;
            }
            else if (cbDisc.Text == "7折")
            {
                disc = 7;
            }
            else if (cbDisc.Text == "6折")
            {
                disc = 6;
            }
            else if (cbDisc.Text == "5折")
            {
                disc = 5;
            }
            else
            {
                disc = 10;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            memberId = Convert.ToInt32(this.dataGridView1.Rows[index].Cells[0].Value);
            txtNum.Text = this.dataGridView1.Rows[index].Cells[1].Value.ToString();
            txtName.Text = this.dataGridView1.Rows[index].Cells[2].Value.ToString();
            cbGarde.Text = this.dataGridView1.Rows[index].Cells[3].Value.ToString();
            cbDisc.Text = this.dataGridView1.Rows[index].Cells[4].Value.ToString() + "折";
        }

        private void AddNum()
        {
            string Num = "";
            int newNum = 0;
            this.closeBtn();
            this.clearText();
            flag = 0;
            DataTable dtMember = MemberBLL.GetDtMember(String.Empty, "0");
            if (dtMember.Rows.Count == 0)
            {
                txtNum.Text = "V1001";
            }
            else
            {
                Num = Convert.ToString(dtMember.Rows[dtMember.Rows.Count - 1]["Num"]);
                newNum = Convert.ToInt32(Num.Substring(1, 4)) + 1;
                Num = "V" + newNum.ToString();
                txtNum.Text = Num;
            }
        }

        public int memberId;
    }
}
