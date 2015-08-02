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

namespace Catering_management.frmStock
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }
        string time;
        int flag = 0;

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            time = dtInstroe.Value.ToString("yyyy-MM-dd");
            string srhStr=null;
            this.clearText();
            groupBox1.Enabled = true;
            try
            {
                if(Convert.ToInt32(tlspCbx.ComboBox.SelectedValue)==0)
                    srhStr=null;
                else if(Convert.ToInt32(tlspCbx.ComboBox.SelectedValue)==1)
                    srhStr=time;
                else
                  srhStr= tlspSearch.Text;                                

                DataTable dtGoods = StockBLL.GetGoods(srhStr, Convert.ToInt32(tlspCbx.ComboBox.SelectedValue));
                foreach (DataRow dr in dtGoods.Rows)
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

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.clearText();
            this.closeBtn();
            txtStockName.Enabled = true;
            flag = 0;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.closeBtn();
            flag = 1;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            txtStockName.Enabled = false;
            time = dtInstroe.Value.ToString("yyyy-MM-dd H:mm");
            Goods goods = new Goods();
            goods.sNum = txtQty.Text;
            goods.sName = txtStockName.Text;
            goods.Price = Convert.ToDouble(txtPrice.Text);
            goods.sTime = time;
            if (flag == 0)
            {
                if (txtStockName.Text == "" || txtQty.Text == "" || txtPrice.Text == "")
                {
                    MessageBox.Show("请将信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    try
                    {
                        if (StockBLL.InserGoods(goods))
                        {
                            MessageBox.Show("增加货品成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                goods.ID = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                
                
                try
                {                    
                    if (StockBLL.UpdateGoods(goods))
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
            int curNo =Convert.ToInt32(this.dataGridView1.Rows[index].Cells[0].Value);
            
            try
            {
                if (StockBLL.DeleteGoods(curNo))             //执行delete语句*/
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

        #region 按键设置
        private void clearText()
        {
            txtStockName.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtPrice.Text = string.Empty;
        }

        private void closeBtn()  //按键隐藏
        {
            groupBox1.Enabled = true;
            toolStripButton1.Enabled = false;
            toolStripButton2.Enabled = false;
            toolStripButton3.Enabled = false;
            toolStripButton6.Enabled = false;
        }

        private void showBtn()
        {
            groupBox1.Enabled = true;
            toolStripButton1.Enabled = true;
            toolStripButton2.Enabled = true;
            toolStripButton3.Enabled = true;
            toolStripButton6.Enabled = true;
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
        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            txtStockName.Text = this.dataGridView1.Rows[index].Cells[2].Value.ToString();
            txtQty.Text = this.dataGridView1.Rows[index].Cells[3].Value.ToString();
            txtPrice.Text = this.dataGridView1.Rows[index].Cells[4].Value.ToString();
            dtInstroe.Text = this.dataGridView1.Rows[index].Cells[1].Value.ToString();
        }

        private void load()
        {
            dataGridView1.Rows.Clear();
            //string srhStr=tlspSearch.Text;
            setComboBox();
            time = dtInstroe.Value.ToString("yyyy-MM-dd");

            DataTable dtGoods = StockBLL.GetGoods(string.Empty, Convert.ToInt32(tlspCbx.ComboBox.SelectedValue));
            foreach (DataRow dr in dtGoods.Rows)
            {
                string[] newRow = { dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString() };
                dataGridView1.Rows.Add(newRow);
            }
        }

        private void setComboBox()
        {
            List<BindComboBox> list = new List<BindComboBox>();
            list.Add(new BindComboBox("0", "-- 全部 --"));
            list.Add(new BindComboBox("1", "进货时间"));
            list.Add(new BindComboBox("2", "货物名称"));
            tlspCbx.ComboBox.DisplayMember = "Value";
            tlspCbx.ComboBox.ValueMember = "Key";
            tlspCbx.ComboBox.DataSource = list;

        }

        private void Stock_Load(object sender, EventArgs e)
        {
            load();
        }

    }
}
