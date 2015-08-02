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

namespace Catering_management.frmBook
{
    public partial class CheckBook : Form
    {
        public CheckBook()
        {
            InitializeComponent();
        }
        string time1, time2;  //用于获取当前时间

        //用于获取dataGridView中的数据
        string billName, billTime, billCode, billFood;
        int id, billNumber, billTable;
        double billMoney;

        private void CheckBook_Load(object sender, EventArgs e)
        {
            this.load();
            this.billCodeLoad();
            this.setCmbx();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string str = null;
            try
            {
                dataGridView1.Rows.Clear();
                int type = Convert.ToInt32(tlspCmbx.ComboBox.SelectedValue);
                if (type == 1)
                    str = time1;
                else
                    str = tlspTxt.Text.Trim();

                DataTable dtBook = BookBLL.GetDtBook(type, str);
                foreach (DataRow dr in dtBook.Rows)
                {
                    string[] newRow ={dr[0].ToString (),dr[1].ToString (),dr[2].ToString (),dr[3].ToString (),dr[4].ToString (),
                                         dr[5].ToString (),dr[6].ToString (),dr[7].ToString (),dr[8].ToString ()};
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

            if (dataGridView1.Rows.Count < 1) return;
            int index = dataGridView1.CurrentRow.Index;  //获取当前记录索引号
            dataGridView1.Rows[index].Selected = true;
            int curNo = Convert.ToInt32(this.dataGridView1.Rows[index].Cells[1].Value);

            try
            {

                if (MessageBox.Show("删除成功！", "成功提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if (BookBLL.DeleteBook(curNo))
                    {
                        MessageBox.Show("删除成功!");
                        this.load();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void load()
        {
            try
            {
                dataGridView1.Rows.Clear();
                try
                {
                    DataTable dtBook = BookBLL.GetDtBook();
                    foreach (DataRow dr in dtBook.Rows)
                    {
                        string[] newRow ={dr[0].ToString (),dr[1].ToString (),dr[2].ToString (),dr[3].ToString (),dr[4].ToString (),
                                         dr[5].ToString (),dr[6].ToString (),dr[7].ToString (),dr[8].ToString ()};
                        dataGridView1.Rows.Add(newRow);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void setCmbx()
        {
            List<BindComboBox> list = new List<BindComboBox>();
            list.Add(new BindComboBox("0", "所有预定"));
            list.Add(new BindComboBox("1", "今日预定"));
            list.Add(new BindComboBox("2", "顾客名称"));
            tlspCmbx.ComboBox.DisplayMember = "Value";
            tlspCmbx.ComboBox.ValueMember = "Key";
            tlspCmbx.ComboBox.DataSource = list;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (BillBLL.IsUseTable(billTable.ToString()) != "" || BillBLL.IsUseTable(billTable.ToString()) == "1")
            {
                MessageBox.Show("该台正在使用中，请换台！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.billCodeLoad();
                BookInfo bi = new BookInfo();
                bi.ID = id.ToString();
                bi.bName = billName;
                bi.bTime = billTime;
                bi.bNumber = billNumber.ToString();
                bi.bCode = billCode;
                bi.bTable = billTable.ToString();
                bi.bFood = billFood;
                bi.bMoney = billMoney.ToString();
                bi.bFlag = "0";
                try
                {
                    if (BookBLL.InsertIntoBill(bi))
                    {
                        MessageBox.Show("开台成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.load();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentRow.Index;
            id = Convert.ToInt32(this.dataGridView1.Rows[index].Cells[0].Value);
            billName = this.dataGridView1.Rows[index].Cells[1].Value.ToString();
            billTime = this.dataGridView1.Rows[index].Cells[2].Value.ToString();
            billNumber = Convert.ToInt32(this.dataGridView1.Rows[index].Cells[3].Value.ToString());
            billTable = Convert.ToInt32(this.dataGridView1.Rows[index].Cells[5].Value.ToString());
            billFood = this.dataGridView1.Rows[index].Cells[6].Value.ToString();
            billMoney = Convert.ToDouble(this.dataGridView1.Rows[index].Cells[7].Value.ToString());
        }

        private void billCodeLoad()
        {
            try
            {
                time1 = DateTime.Now.ToString("yyyy-MM-dd");
                time2 = DateTime.Now.ToString("H:mm");
                lable1.Text = "当前时间：" + time1 + " " + time2;

                //获取账单编号            
                int newBillCode = 0;
                billCode = BillBLL.GetBillCode();
                if (billCode == null)
                {
                    billCode = DateTime.Now.ToString("yyyyMMdd") + "BC" + "100001";
                }
                else
                {
                    newBillCode = Convert.ToInt32(billCode.Substring(10, 6)) + 1;
                    billCode = DateTime.Now.ToString("yyyyMMdd") + "BC" + newBillCode.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
