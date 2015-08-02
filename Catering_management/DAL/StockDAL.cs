using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Catering_management.DAL
{
    public class StockDAL
    {
        /// <summary>
        /// 根据时间或货物名搜索
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type">0:全部，1：时间，2：货物</param>
        /// <returns></returns>
        public static DataTable GetGoods(string str, int type)
        {
            DataTable dt = new DataTable();
            if(type==0)
            {
                string sql = @"select ID,Convert(VARCHAR(50), sTime,23),sName,sNum,sMoney from tb_stock";
                dt = DBHelper.ExcuteDataTable(sql, CommandType.Text);   
            }

            else if (type == 1)
            {
                string sql = @"select  ID,Convert(VARCHAR(50), sTime,23),sName,sNum,sMoney from tb_stock  where convert(varchar(20),sTime,120) like '%" + str + "%'";
                dt = DBHelper.ExcuteDataTable(sql, CommandType.Text);                
            }
            else
            {
                string sql = @"select  ID,Convert(VARCHAR(50), sTime,23),sName,sNum,sMoney from tb_stock where sName='" + str + "'";
                dt = DBHelper.ExcuteDataTable(sql, CommandType.Text);   
            }
            return dt;
        }

        /// <summary>
        /// 新增货物
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public static int InserGoods(Goods goods)
        {
            string sql = @"insert into tb_stock(sTime,sName,sNum,sMoney)values (@sTime,@sName,@sNum,@sMoney)";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@sTime",goods.sTime),
                new SqlParameter("@sName",goods.sName),
                new SqlParameter("@sNum",goods.sNum),
                new SqlParameter("sMoney",goods.Price)
            };
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 编辑货物
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public static int UpdateGoods(Goods goods)
        {
            string sql = "update tb_stock set  sMoney=@sMoney,sNum=@sNum where sName=@sName where ID=@ID";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@ID",goods.ID),
                new SqlParameter("@sTime",goods.sTime),
                new SqlParameter("@sName",goods.sName),
                new SqlParameter("@sNum",goods.sNum),
                new SqlParameter("sMoney",goods.Price)
            };
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteGoods(int ID)
        {
            string sql= "delete from tb_stock where ID=@ID";
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@ID", ID.ToString()));
        }

        /// <summary>
        /// 报表
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static DataTable GetDtStkForXsd(string t1, string t2)
        {
            string sql = "SELECT sTime,sName,sNum,sMoney FROM dbo.tb_stock WHERE sTime BETWEEN '" + t1 + "'and '" + t2 + "'";
            return DBHelper.ExcuteDataTable(sql, CommandType.Text);
        }
    }
}
