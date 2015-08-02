using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Catering_management.MODEL;

namespace Catering_management.DAL
{
    public class EmployeeDAL
    {
        /// <summary>
        /// 查找员工
        /// </summary>
        /// <param name="searchTxt"></param>
        /// <returns></returns>
        public static DataTable GetEmployee(string searchTxt)
        {
            string sql = @"select * from tb_employee where 1=1 ";
            if (!String.IsNullOrEmpty(searchTxt))
                sql += " Num=@searchTxt or Name=@searchTxt";
            sql += "order by Num";
            return DBHelper.ExcuteDataTable(sql, CommandType.Text, new SqlParameter("@searchTxt", searchTxt));
        }

        /// <summary>
        /// 插入员工
        /// </summary>
        /// <param name="ei"></param>
        /// <returns></returns>
        public static int InsertEmployee(EmployeeInfo ei)
        {
            string sql = @"insert into tb_employee(
Num,Name,Sex,Salary,Position,Telephone,Manid,Addr)values(@Num,@Name,@Sex,@Salary,@Position,@Telephone,@Manid,@Addr)";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@Num",ei.Num),
                new SqlParameter("@Name",ei.Name),
                new SqlParameter("@Sex",ei.Sex),
                new SqlParameter("@Salary",ei.Salary),
                new SqlParameter("@Position",ei.Position),
                new SqlParameter("@Telephone",ei.Telephone),
                new SqlParameter("@Manid",ei.Manid),
                new SqlParameter("@Addr",ei.Addr)
            };
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 编辑员工
        /// </summary>
        /// <param name="ei"></param>
        /// <returns></returns>
        public static int UpdateEmployee(EmployeeInfo ei)
        {
            string sql = @"update tb_employee set Num=@Num, Name=@Name, Sex=@Sex, @Salary=@Salary, Position=@Position,TelePhone=@Telephone
                ,Manid=Manid,Addr=@Addr where ID=@ID
                ";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@ID",ei.ID.ToString()),
                new SqlParameter("@Num",ei.Num),
                new SqlParameter("@Name",ei.Name),
                new SqlParameter("@Sex",ei.Sex),
                new SqlParameter("@Salary",ei.Salary),
                new SqlParameter("@Position",ei.Position),
                new SqlParameter("@Telephone",ei.Telephone),
                new SqlParameter("@Manid",ei.Manid),
                new SqlParameter("@Addr",ei.Addr)
            };
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteEmployee(int ID)
        {
            string sql = "delete from tb_employee where ID=@ID";
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("ID", ID.ToString()));
        }

        /// <summary>
        /// 报表相关
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDtEmpForXsd()
        {
            string sql = " SELECT Num,Name,Salary,Sex,Position,Telephone FROM dbo.tb_employee";
            return DBHelper.ExcuteDataTable(sql, CommandType.Text);
        }               
    }
}
