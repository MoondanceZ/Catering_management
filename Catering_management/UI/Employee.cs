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
using System.Text.RegularExpressions;

namespace Catering_management.frmEmployee
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }

        int flag = 0; //定义添加修改操作标识
        private void Employee_Load(object sender, EventArgs e)
        {
            this.load();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)//添加
        {
            this.closeBtn();
            this.clearText();
            flag = 0;
            this.AddNum();
        }

        #region 按键控制
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
            txtSalary.Text = string.Empty;
            txtAddr.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtPs.Text = string.Empty;
            txtManid.Text = string.Empty;
        }
        #endregion

        private void toolStripButton4_Click(object sender, EventArgs e) //保存
        {           
            EmployeeInfo ei = new EmployeeInfo();
            ei.Num = txtNum.Text;
            ei.Name = txtName.Text;
            ei.Position = txtPs.Text;
            ei.Salary = Convert.ToDouble(txtSalary.Text);
            ei.Sex = cmbSex.SelectedText;
            ei.Telephone = txtTel.Text;
            ei.Manid = txtManid.Text;
            ei.Addr = txtAddr.Text;
            if (flag == 0)
            {
                if (CheckInfo()==false)
                {
                    //MessageBox.Show("请将信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    try
                    {
                        if (EmployeeBLL.InsertEmployee(ei))
                        {
                            MessageBox.Show("增加员工成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            else
            {
                int index = dataGridView1.CurrentRow.Index; //获取当前记录的索引号
                int Id = Convert.ToInt16(dataGridView1.Rows[index].Cells[0].Value);
                ei.ID = Id;
                try
                {
                    if (EmployeeBLL.UpdateEmployee(ei))
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

        /// <summary>
        /// 用正则表达式验证信息- -！，写着玩，练练手
        /// </summary>
        private bool CheckInfo()
        {
            bool result = false;
            //验证身份证号
            if (!Regex.IsMatch(txtManid.Text, @"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("请输入正确的身份证号码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = false;
            }
            else if (!Regex.IsMatch(txtTel.Text, @"^\d{11}$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("请输入11位数字电话号码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = false;
            }
            else if (!Regex.IsMatch(txtSalary.Text, @"^\+?[1-9][0-9]*$", RegexOptions.IgnoreCase))
            {
                MessageBox.Show("工资请输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = false;
            }
            else
                result = true;
            return result;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            txtNum.Enabled = false;
            this.closeBtn();
            flag = 1;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            this.clearText();
            groupBox1.Enabled = true;

            DataTable dtEmployee = EmployeeBLL.GetEmployee(tlspTxt.Text.Trim());
            foreach (DataRow dr in dtEmployee.Rows)
            {
                string[] newRow ={dr[0].ToString(),dr[1].ToString(),dr[2].ToString(),
                      dr[3].ToString(),dr[4].ToString(),dr[5].ToString(),dr[6].ToString(),dr[7].ToString(),dr[8].ToString(),};
                dataGridView1.Rows.Add(newRow);
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            txtNum.Text = this.dataGridView1.Rows[index].Cells[1].Value.ToString();
            txtName.Text = this.dataGridView1.Rows[index].Cells[2].Value.ToString();
            cmbSex.Text = this.dataGridView1.Rows[index].Cells[3].Value.ToString();
            txtSalary.Text = this.dataGridView1.Rows[index].Cells[4].Value.ToString();
            txtPs.Text = this.dataGridView1.Rows[index].Cells[5].Value.ToString();
            txtTel.Text = this.dataGridView1.Rows[index].Cells[6].Value.ToString();
            txtManid.Text = this.dataGridView1.Rows[index].Cells[7].Value.ToString();
            txtAddr.Text = this.dataGridView1.Rows[index].Cells[8].Value.ToString();
        }
        private void load()
        {
            dataGridView1.Rows.Clear();
            groupBox1.Enabled = false;
            try
            {
                DataTable dtEmployee = EmployeeBLL.GetEmployee(string.Empty);
                foreach (DataRow dr in dtEmployee.Rows)
                {
                    string[] newRow ={dr[0].ToString(),dr[1].ToString(),dr[2].ToString(),
                      dr[3].ToString(),dr[4].ToString(),dr[5].ToString(),dr[6].ToString(),dr[7].ToString(),dr[8].ToString(),};
                    dataGridView1.Rows.Add(newRow);
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

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.cancelEnabled();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1) return;
            int index = dataGridView1.CurrentRow.Index;  //获取当前记录索引号
            dataGridView1.Rows[index].Selected = true;
            int curNo = Convert.ToInt32(this.dataGridView1.Rows[index].Cells[0].Value);

            try
            {
                //执行delete语句
                if (MessageBox.Show("是否删除！", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (EmployeeBLL.DeleteEmployee(curNo))
                        MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void AddNum()
        {
            string Num = "";
            int newNum = 0;
            this.closeBtn();
            this.clearText();
            flag = 0;
            DataTable dtEmployee = EmployeeBLL.GetEmployee(string.Empty);
            if (dtEmployee.Rows.Count == 0)
            {
                txtNum.Text = "E1001";
            }
            else
            {
                Num = Convert.ToString(dtEmployee.Rows[dtEmployee.Rows.Count - 1]["Num"]);
                newNum = Convert.ToInt32(Num.Substring(1, 4)) + 1;
                Num = "E" + newNum.ToString();
                txtNum.Text = Num;
            }
        }
    }
}
