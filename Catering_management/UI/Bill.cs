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
using System.Text.RegularExpressions;

namespace Catering_management.frmBill
{
    public partial class Bill : Form
    {
        public Bill()
        {
            InitializeComponent();
        }
        string price;
        double money;   //计算总的钱数
        string lbText;  //获取所有餐品
        private void Bill_Load(object sender, EventArgs e)
        {
            this.billCodeLoad();
            this.SetText();
            this.loadMenu(0, treeView1.Nodes);
        }

        private void loadMenu(int pid, TreeNodeCollection treeNodeCollection)
        {
            List<MenuInfo> miList = MenuBLL.GetMiByMtype(pid);
            foreach (var mi in miList)
            {
                if (pid != 0)
                {
                    TreeNode tNode = treeNodeCollection.Add(mi.mName + ": " + mi.mPrice);
                    tNode.Tag = mi.ID;
                    loadMenu(mi.ID, tNode.Nodes);
                }
                else
                {
                    TreeNode tNode = treeNodeCollection.Add(mi.mName);
                    tNode.Tag = mi.ID;
                    loadMenu(mi.ID, tNode.Nodes);
                }

            }
        }

        private void clearBox()  //清空顾客信息
        {
            txtCtName.Text = string.Empty;
            txtSdt.Text = string.Empty;
            txtPqty.Text = string.Empty;
            txtDrjsht.Text = string.Empty;
            cmbTable.Text = string.Empty;
        }
        private void clearList()  //清空预订餐品信息
        {
            money = 0;
            listBox1.Items.Clear();
            txtPrice.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtFood.Text == "")
            {
                MessageBox.Show("请选择餐品！");
            }
            else
            {
                listBox1.Items.Add(txtFood.Text);
                money = Convert.ToDouble(price) + money;
                txtPrice.Text = Convert.ToString(money);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.clearList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (ChkEnterInfo() == false)
                return;
            if (txtCtName.Text == "" || txtSdt.Text == "" || txtPqty.Text == "" || txtDrjsht.Text == "")
            {
                MessageBox.Show("请将信息填写完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {                
                for (int i = 0; i < this.listBox1.Items.Count; i++)
                {
                    lbText += this.listBox1.Items[i].ToString() + "\r\n";
                }
                BillInfo bill = new BillInfo();
                Table tb = new Table();
                tb.ID = Convert.ToInt32(BillBLL.GetTableID()) + 1;
                tb.Number = cmbTable.Text;
                tb.IsUse = 1;
                bill.billName = txtCtName.Text;
                bill.billNum = txtPqty.Text;
                bill.billTime = txtSdt.Text;
                bill.billFood = lbText;
                bill.billCode = txtDrjsht.Text;
                bill.billMoney = money;
                bill.billFlag = "0";
                bill.table = tb;
                try
                {
                    if (BillBLL.InsertBill(bill))
                    {
                        MessageBox.Show("开单成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.clearBox();
                        this.clearList();
                        this.billCodeLoad();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool ChkEnterInfo()
        {
            bool result = true;
            if(!Regex.IsMatch(txtPqty.Text,@"^[0-9]*$",RegexOptions.IgnoreCase))
            {
                MessageBox.Show("人数请输入数字！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                result = false;
            }            
            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckBill Cb = new CheckBill();
            Cb.Show();
        }
        private void billCodeLoad()  //自动生成账单编号
        {
            string billCode = BillBLL.GetBillCode();
            try
            {
                txtSdt.Text = DateTime.Now.ToString("yyyy-MM-dd H:mm");

                if (billCode == null)
                {
                    txtDrjsht.Text = DateTime.Now.ToString("yyyyMMdd") + "BC" + "100001";
                }
                else
                {
                    int newBillCode = Convert.ToInt32(billCode.Substring(10, 6)) + 1;
                    billCode = DateTime.Now.ToString("yyyyMMdd") + "BC" + newBillCode.ToString();
                    txtDrjsht.Text = billCode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void SetText()  //设置dataGridView1列名
        {

        }

        private void isTable()
        {            
            if (BillBLL.IsUseTable(cmbTable.Text)!=""||BillBLL.IsUseTable(cmbTable.Text)=="1")
            {
                MessageBox.Show("该台正在使用中，请换台！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // MessageBox.Show(comboBox2.Text);
            this.isTable();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                int mId = (int)e.Node.Tag;
                price = MenuBLL.GetPriceByID(mId);
                // txtPrice.Text = price;
                txtFood.Text = e.Node.Text;
            }
        }

    }
}
