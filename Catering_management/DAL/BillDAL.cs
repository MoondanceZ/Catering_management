using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Catering_management.DAL
{
    public class BillDAL
    {
        /// <summary>
        /// 获取billCode
        /// </summary>
        /// <returns></returns>
        public static string GetBillCode()
        {
            string sql = " SELECT TOP 1 billCode FROM tb_bill ORDER BY billCode DESC";

            object obj = DBHelper.ExecuteScalar(sql, System.Data.CommandType.Text);
            return obj == null ? "" : obj.ToString();

        }

        /// <summary>
        /// 返回桌状态
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string IsUseTable(string num)
        {
            string sql = " select IsUse from tb_table where Num=@num";
            object obj = DBHelper.ExecuteScalar(sql, System.Data.CommandType.Text, new SqlParameter("@num", num));
            return obj == null ? "" : obj.ToString();
        }

        /// <summary>
        /// 获取未在使用的餐桌
        /// </summary>
        /// <returns></returns>
        public static List<Table> GetUsingTb()
        {
            List<Table> listTb = new List<Table>();
            string sql = "select * from tb_table where IsUse = 1 ";
            using (SqlDataReader reader = DBHelper.ExecuteReader(sql, CommandType.Text))
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        Table tb = new Table();
                        tb.ID = reader.GetInt32(0);
                        tb.Number = reader.GetString(1);
                        tb.IsUse = reader.GetInt32(2);
                        tb.BillID = reader.GetInt32(3);
                        listTb.Add(tb);
                    }
                }
                else
                    listTb = null;
            }
            return listTb;
        }

        /// <summary>
        /// 插入开单
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static int InsertBill(BillInfo bi)
        {
            Table tb = bi.table;
            string sql = @"INSERT INTO dbo.tb_bill
        ( billName ,
          billTime ,
          billNumber ,
          billCode ,
          billTable ,
          billFood ,
          billMoney ,
          billFlag
        )
VALUES  ( @billName ,
          @billTime ,
          @billNumber ,
          @billCode ,
          @billTable ,
          @billFood ,
          @billMoney ,
          @billFlag
        )";
            sql += @" set identity_insert tb_table ON 
INSERT INTO dbo.tb_table
        (ID, Num, BillID, IsUse )
VALUES  (@ID, @Num, 
         Convert(int, (select ID from tb_bill where billCode=@billCode)),
          @IsUse  
          )";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@billName",bi.billName),
                new SqlParameter("@billTime",bi.billTime),
                new SqlParameter("@billNumber",bi.billNum),
                new SqlParameter("@billCode",bi.billCode),
                new SqlParameter("@billTable",tb.ID),
                new SqlParameter("@billFood",bi.billFood),
                new SqlParameter("@billMoney",bi.billMoney),
                new SqlParameter("@billFlag",bi.billFlag),
                new  SqlParameter("@ID",tb.ID),
                new  SqlParameter("@Num",tb.Number),
                new  SqlParameter("@IsUse",tb.IsUse)
            };

            return DBHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms);
        }

        /// <summary>
        /// 获取桌ID
        /// </summary>
        /// <returns></returns>
        public static string GetTableID()
        {
            string sql = @"SELECT TOP 1 id FROM dbo.tb_table ORDER BY ID desc";
            object obj = DBHelper.ExecuteScalar(sql, CommandType.Text);
            return obj == null ? "1" : obj.ToString();
        }

        /// <summary>
        /// 查找用户开单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDtBill()
        {
            string str = @"SELECT tb_bill.ID, billName, billTime, billNumber, billCode, tb_table.Num, billFood, billMoney, CASE billFlag WHEN'0'THEN '未付款' ELSE '已付款' END AS billFlag 
FROM    tb_bill JOIN dbo.tb_table ON dbo.tb_table.BillID=dbo.tb_bill.ID ORDER BY billTime DESC";
            return DBHelper.ExcuteDataTable(str, CommandType.Text);
        }

        /// <summary>
        /// 查找用户开单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDtBill(int type, string str)
        {
            string sql = null;
            if (type == 0)
            {
                sql = @"SELECT tb_bill.ID, billName, billTime, billNumber, billCode, tb_table.Num, billFood, billMoney, CASE billFlag WHEN'0'THEN '未付款' ELSE '已付款' END AS billFlag 
FROM    tb_bill JOIN dbo.tb_table ON dbo.tb_table.BillID=dbo.tb_bill.ID ORDER BY billTime DESC";
            }
            else if (type == 1)
                sql = @"SELECT tb_bill.ID, billName, billTime, billNumber, billCode, tb_table.Num, billFood, billMoney, CASE billFlag WHEN'0'THEN '未付款' ELSE '已付款' END AS billFlag 
FROM    tb_bill JOIN dbo.tb_table ON dbo.tb_table.BillID=dbo.tb_bill.ID where convert(varchar(20),billTime,120) like '" + str + "%'  ORDER BY billTime DESC";
            else
                sql = @"SELECT tb_bill.ID, billName, billTime, billNumber, billCode, tb_table.Num, billFood, billMoney, CASE billFlag WHEN'0'THEN '未付款' ELSE '已付款' END AS billFlag 
FROM    tb_bill JOIN dbo.tb_table ON dbo.tb_table.BillID=dbo.tb_bill.ID where tb_bill.billName='" + str + "' ORDER BY billTime DESC";
            return DBHelper.ExcuteDataTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteBill(int id)
        {
            string sql = @"delete from tb_bill where id=@id";
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("id", id));
        }

        /// <summary>
        /// 获取开单信息  用于结算
        /// </summary>
        /// <param name="table"></param>
        /// <param name="ctm"></param>
        /// <returns></returns>
        public static BillInfo GetBillInfo(string table, string ctm)
        {
            BillInfo bi = new BillInfo();
            Table tb = new Table();
            string sql = @"select tb_table.Num,billName,billMoney from tb_bill JOIN dbo.tb_table ON dbo.tb_bill.billTable=dbo.tb_table.ID 
where billFlag='0'  and tb_table.Num='" + table + "'";
            if (!string.IsNullOrEmpty(ctm))
                sql += " and billName='" + ctm + "' ";
            using (SqlDataReader reader = DBHelper.ExecuteReader(sql, CommandType.Text))
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        tb.Number = reader.GetString(0);
                        bi.table = tb;
                        bi.billName = reader.GetString(1);
                        bi.billMoney = reader.GetDouble(2);
                    }
                }
                else
                    bi = null;
            }
            return bi;
        }

        /// <summary>
        /// 返回顾客信息
        /// </summary>
        /// <param name="ctm"></param>
        /// <returns></returns>
        public static MemberInfo GetMbInfo(string ctm)
        {
            MemberInfo mi = new MemberInfo();
            string sql = "select Name,Grade,tb_discount.Discount from tb_member JOIN dbo.tb_discount ON dbo.tb_discount.MemberID = dbo.tb_member.ID where Name='" + ctm + "'";
            using (SqlDataReader reader = DBHelper.ExecuteReader(sql, CommandType.Text))
            {
                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        MDiscount md = new MDiscount();
                        md.Discount = reader.GetInt32(2);
                        mi.MDiscount = md;
                        mi.Name = reader.GetString(0);
                        mi.Grade = reader.GetInt32(1);

                    }
                }
                else
                    mi = null;
            }
            return mi;
        }

        /// <summary>
        /// 更新表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int UpdateBill(string str)
        {
            string sql = "update tb_bill set  billFlag='1' where billTable ='" + str + "'";
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text);
        }

        /// <summary>
        /// 获取时间段开单
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static DataTable GetBillByTime(string t1, string t2)
        {
            string str = "select billName,billTime,billCode,billNumber,billMoney from tb_bill where convert(varchar(20),billTime,120)  between '" + t1 + "' and '" + t2 + "' and billFlag='1' ";
            return DBHelper.ExcuteDataTable(str, CommandType.Text);
        }
    }
}
