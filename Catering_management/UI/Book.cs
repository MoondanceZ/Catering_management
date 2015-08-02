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
    public partial class Book : Form
    {
        public Book()
        {
            InitializeComponent();
        }
        double money;   //计算总的钱数
        string lbText;  //获取所有餐品
  
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtFoods.Text == "")
            {
                MessageBox.Show("请选择餐品！");
            }
            else
            {
                listBox1.Items.Add(txtFoods.Text);
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
            if (txtCtm.Text == "" || txtBDt.Text == "" || txtPQty.Text == "" || txtTel.Text == "")
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

                BookInfo bi = new BookInfo();
                bi.bName = txtCtm.Text;
                bi.bTable = txtable.Text;
                bi.bNumber = txtPQty.Text;
                bi.bTime = txtBDt.Text;
                bi.bTelephone = txtTel.Text;
                bi.bFood = lbText;
                bi.bMoney = money.ToString();
                bi.bFlag = "0";
                try
                {
                    if (BookBLL.InsertBook(bi))
                    {
                        MessageBox.Show("增加预订成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.clearBox();
                        this.clearList();
                        txtBDt.Text = DateTime.Now.ToString("yyyy-MM-dd H:mm");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void Book_Load(object sender, EventArgs e)
        {
            txtBDt.Text = DateTime.Now.ToString("yyyy-MM-dd H:mm");
            this.loadMenu(0, treeView1.Nodes);
        }

        private void loadMenu(int pid, TreeNodeCollection td)
        {
            List<MenuInfo> miList = MenuBLL.GetMiByMtype(pid);
            foreach (var mi in miList)
            {
                if(pid!=0)
                {
                    TreeNode tNode = td.Add(mi.mName + ": " + mi.mPrice);
                    tNode.Tag = mi.ID;
                    loadMenu(mi.ID, tNode.Nodes);
                }
                else
                {
                    TreeNode tNode = td.Add(mi.mName);
                    tNode.Tag = mi.ID;
                    loadMenu(mi.ID, tNode.Nodes);
                }
            }
        }

        private void clearBox()  //清空顾客信息
        {
            txtCtm.Text = string.Empty;
            txtBDt.Text = string.Empty;
            txtPQty.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtable.Text = string.Empty;
        }
        private void clearList()  //清空预订餐品信息
        {
            money = 0;
            listBox1.Items.Clear();
            txtPrice.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmBook.CheckBook Cb = new CheckBook();
            Cb.Show();
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                int mId = (int)e.Node.Tag;
                price = MenuBLL.GetPriceByID(mId);
                // txtPrice.Text = price;
                txtFoods.Text = e.Node.Text;
            }
        }

        private string price;
    }
}
