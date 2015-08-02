using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Catering_management.DAL
{
    public class BookDAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static int InsertBook(BookInfo bi)
        {
            string sql = @" INSERT INTO dbo.tb_book
		          ( bName ,
		            bTime ,
		            bNumber ,
		            bTelephone ,
		            bTable ,
		            bFood ,
		            bMoney ,
		            bFlag
		          )
		  VALUES  ( @bName ,
		            @bTime ,
		            @bNumber ,
		            @bTelephone ,
		            @bTable ,
		            @bFood ,
		            @bMoney ,
		            @bFlag
		          )";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@bName",bi.bName),
                new SqlParameter("@bTime",bi.bTime),
                new SqlParameter("@bNumber",bi.bNumber),
                new SqlParameter("@bTelephone",bi.bTelephone),
                new SqlParameter("@bTable",bi.bTable),
                new SqlParameter("@bFood",bi.bFood),
                new SqlParameter("@bMoney",bi.bMoney),
                new SqlParameter("@bFlag",bi.bFlag)
            };
            return DBHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms);
        }

        public static System.Data.DataTable GetDtBook()
        {
            string sql = @"SELECT ID, bName, bTime, bNumber, bTelephone, bTable, bFood, bMoney,CASE bFlag WHEN '0' THEN '未开台' ELSE '已开台' END AS billFlag
FROM      tb_book ORDER BY bTime DESC";
            return DBHelper.ExcuteDataTable(sql, System.Data.CommandType.Text);
        }

        public static System.Data.DataTable GetDtBook(int type, string str)
        {
            string sql = null;
            if (type == 0)
            {
                sql = @"SELECT ID, bName, bTime, bNumber, bTelephone, bTable, bFood, bMoney,CASE bFlag WHEN '0' THEN '未开台' ELSE '已开台' END AS billFlag
FROM      tb_book ORDER BY bTime DESC";
            }
            else if (type == 1)
                sql = @"SELECT ID, bName, bTime, bNumber, bTelephone, bTable, bFood, bMoney,CASE bFlag WHEN '0' THEN '未开台' ELSE '已开台' END AS billFlag
FROM      tb_book where convert(varchar(20),bTime,120) like '" + str + "%'  ORDER BY bTime DESC";
            else
                sql = @"SELECT ID, bName, bTime, bNumber, bTelephone, bTable, bFood, bMoney,CASE bFlag WHEN '0' THEN '未开台' ELSE '已开台' END AS billFlag
FROM      tb_book where tb_bill.bName='" + str + "' ORDER BY bTime DESC";
            return DBHelper.ExcuteDataTable(sql, CommandType.Text);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteBook(int id)
        {
            string sql = "delete from tb_book where bName='" + id + "'";
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text);
        }

        /// <summary>
        /// 开台
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static int InsertIntoBill(BookInfo bi)
        {
            string sql = @"INSERT INTO dbo.tb_book 
        ( bName ,
          bTime ,
          bNumber ,
          bTelephone ,
          bTable ,
          bFood ,
          bMoney ,
          bFlag
        )
VALUES  ( 
            @bName ,
          @bTime ,
          @bNumber ,
          @bTelephone ,
          @bTable ,
          @bFood ,
          @bMoney ,
          @bFlag
        )";

            sql += "  update tb_book set bFlag= '1' where ID=@ID";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@ID",bi.ID),
                new SqlParameter("@bName",bi.bName),
                new SqlParameter("@bTime",bi.bTime),
                new SqlParameter("@bNumber",bi.bNumber),
                new SqlParameter("@bTelephone",bi.bTelephone),
                new SqlParameter("@bTable",bi.bTable),
                new SqlParameter("@bFood",bi.bFood),
                new SqlParameter("@bMoney",bi.bMoney),
                new SqlParameter("@bFlag",bi.bFlag)
            };
            return DBHelper.ExecuteNonQuery((sql), CommandType.Text, pms);
        }
    }
}
