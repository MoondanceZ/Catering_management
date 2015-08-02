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
using Microsoft.Reporting.WinForms;

namespace Catering_management.frmTurnover
{
    public partial class Turnover : Form
    {
        public Turnover()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string time1 = dateTimePicker1.Value.ToString("yyyy-MM-dd H:mm");
            string time2 = dateTimePicker2.Value.ToString("yyyy-MM-dd H:mm");

            DataTable dtBill = BillBLL.GetBillByTime(time1, time2);
            DataTable dtStock = StockBLL.GetDtStkForXsd(time1, time2);
            DataTable dtEmp = EmployeeBLL.GetDtEmpForXsd();
            ReportDataSource rds1 = new ReportDataSource("TurnoverDataset", dtBill);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds1);
            reportViewer1.RefreshReport();

            ReportDataSource rds2 = new ReportDataSource("StockDataset", dtStock);
            reportViewer2.LocalReport.DataSources.Clear();
            reportViewer2.LocalReport.DataSources.Add(rds2);
            reportViewer2.RefreshReport();

            ReportDataSource rds3 = new ReportDataSource("SalaryDataset", dtEmp);
            reportViewer3.LocalReport.DataSources.Clear();
            reportViewer3.LocalReport.DataSources.Add(rds3);
            reportViewer3.RefreshReport();

            var sumBill = dtBill.AsEnumerable().Sum(s => s.Field<double>("billMoney"));
            txtBill.Text = sumBill.ToString();
            var sumEmp = dtEmp.AsEnumerable().Sum(s => s.Field<double>("Salary"));
            txtSalary.Text = sumEmp.ToString();
            var sumStock = dtStock.AsEnumerable().Sum(s => s.Field<double>("sMoney"));
            txtStock.Text = sumStock.ToString();
            txtSum.Text = (sumBill - sumEmp - sumStock).ToString();

        }

        private void Turnover_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddDays(-(DateTime.Today.Day - 1));
            //string time1 = dateTimePicker1.Value.AddDays(-600).ToString("yyyy-MM-dd H:mm");
            //string time2 = dateTimePicker1.Value.ToString("yyyy-MM-dd H:mm");
            //this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            ////this.reportViewer1.RefreshReport();
            //DataTable dtBill = BillBLL.GetBillByTime(time1, time2);
            //ReportDataSource rds1 = new ReportDataSource("TurnoverDataset", dtBill);            
            //reportViewer1.LocalReport.DataSources.Clear();
            //reportViewer1.LocalReport.DataSources.Add(rds1);
            //reportViewer1.RefreshReport();
            //this.reportViewer1.RefreshReport();
            //this.reportViewer2.RefreshReport();
            //this.reportViewer3.RefreshReport();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
