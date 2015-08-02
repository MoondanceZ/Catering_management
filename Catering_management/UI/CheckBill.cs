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

namespace Catering_management.frmBill
{
    public partial class CheckBill : Form
    {
        public CheckBill()
        {
            InitializeComponent();
        }
        string time1, time2;  //记录当前时间
        private void CheckBill_Load(object sender, EventArgs e)
        {
            this.load();
            this.setCmbx();
            time1 = DateTime.Now.ToString("yyyy-MM-dd");
            time2 = DateTime.Now.ToString("H:mm");
            lable1.Text = "当前时间：" + time1 + " " + time2;
           // this.SetText();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string str = null;
            dataGridView1.Rows.Clear();
            int type = Convert.ToInt32(tlspCmbx.ComboBox.SelectedValue);
            if (type == 1)
                str = time1;
            else
                str = tlspTxt.Text.Trim();
           
            try
            {
                DataTable dtBill = BillBLL.GetDtBill(type, str);
                foreach (DataRow dr in dtBill.Rows)
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
                    if (BillBLL.DeleteBill(curNo))
                    {
                        this.load();
                        MessageBox.Show("删除成功！");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void load()
        {
            dataGridView1.Rows.Clear();
            try
            {
                DataTable dtBill = BillBLL.GetDtBill();
                foreach (DataRow dr in dtBill.Rows)
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

        private void setCmbx()
        {
            List<BindComboBox> list = new List<BindComboBox>();
            list.Add(new BindComboBox("0", "所有开单"));
            list.Add(new BindComboBox("1", "今日开单"));
            list.Add(new BindComboBox("2", "顾客名称"));
            tlspCmbx.ComboBox.DisplayMember = "Value";
            tlspCmbx.ComboBox.ValueMember = "Key";
            tlspCmbx.ComboBox.DataSource = list;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
