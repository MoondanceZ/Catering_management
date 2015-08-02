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

namespace Catering_management.frmCheckout
{
    public partial class Checkout : Form
    {
        public Checkout()
        {
            InitializeComponent();
        }
        string str = null;
        int DIS;

        private void button1_Click(object sender, EventArgs e)
        {
            cmbxTable.Text = string.Empty;
            txtCtm.Text = string.Empty;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                //BillInfo bi = new BillInfo();
                BillInfo bi = BillBLL.GetBillInfo(cmbxTable.Text, txtCtm.Text);

                if (bi != null)
                {
                    Table tb = new Table();
                    tb = bi.table;
                    txtTable.Text = tb.Number;
                    txtCCtm.Text = bi.billName;
                    txtSPay.Text = bi.billMoney.ToString();
                }
                this.hahaha();
                if (txtDis.Text == "无折扣")
                {
                    txtTPay.Text = txtSPay.Text;
                }
                else
                {
                    DIS = Convert.ToInt32(txtDis.Text);
                    txtTPay.Text = Convert.ToString(Convert.ToDouble(txtSPay.Text) * DIS / 10);  //计算打折
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Checkout_Load(object sender, EventArgs e)
        {
            txtTable.Enabled = false;
            getUsingTb();
        }

        private void getUsingTb()
        {
            List<Table> tb = BillBLL.GetUsingTb();
            cmbxTable.DisplayMember = "Number";
            cmbxTable.ValueMember = "ID";
            cmbxTable.DataSource = tb;
        }

        private void hahaha()    //判断是否会员
        {
            MemberInfo mi = new MemberInfo();
            mi = BillBLL.GetMbInfo(txtCCtm.Text);
            if (mi != null)
            {
                txtIsmember.Text = mi.Grade == 1 ? "高级会员" : "普通会员";
                MDiscount dis = new MDiscount();
                dis = mi.MDiscount;
                txtDis.Text = dis.Discount.ToString();
            }
            else
            {
                txtIsmember.Text = "非会员";
                txtDis.Text = "无折扣";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                if (BillBLL.UpdateBill(txtTable.Text))
                {
                    MessageBox.Show("结算成功！", "成功提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                this.clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void clear()
        {
            cmbxTable.Text = string.Empty;
            txtCtm.Text = string.Empty;
            txtTable.Text = string.Empty;
            txtCCtm.Text = string.Empty;
            txtIsmember.Text = string.Empty;
            txtDis.Text = string.Empty;
            txtSPay.Text = string.Empty;
            txtTPay.Text = string.Empty;
        }
    }
}
