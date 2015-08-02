using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Catering_management.DAL
{
    public class MenuDAL
    {
        /// <summary>
        /// 添加菜单大类
        /// </summary>
        /// <param name="mName"></param>
        /// <returns></returns>
        public static int InsertCategory(string mName)
        {
            string sql = @"Insert into tb_menu(mName, mType) values (@mName, 0) ";
            return DBHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, new SqlParameter("@mName", mName));
        }

        /// <summary>
        /// 删除菜单大类
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public static int DeleteCategory(string Category)
        {
            string sql = @"Delete from tb_menu where (mName=@Category or 
                        mNum=@Category and mType = 0) 
                        or mType = (select ID from tb_menu where mName=@Category or 
                        mNum=@Category)";
            return DBHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, new SqlParameter("@Category", Category));
        }

        /// <summary>
        /// 获取所有餐品大类
        /// </summary>
        /// <returns></returns>
        public static List<MenuInfo> GetMenuCategory()
        {
            List<MenuInfo> category = new List<MenuInfo>();
            string sql = @"select * from tb_menu where mType=0";
            using (SqlDataReader reader = DBHelper.ExecuteReader(sql, System.Data.CommandType.Text))
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MenuInfo mi = new MenuInfo();
                        mi.ID = reader.GetInt32(0);
                        //mi.mNun = reader.GetString(1);
                        mi.mName = reader.GetString(2);
                        //mi.mType = reader.GetInt32(3);
                        mi.mType = reader.GetInt32(3);
                        category.Add(mi);
                    }
                }
                return category;
            }
        }

        /// <summary>
        /// 获取所有菜
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMenu()
        {
            string sql = @"select * from tb_menu  where mType!=0 ORDER BY mNUm";
            DataTable dt = DBHelper.ExcuteDataTable(sql, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 按搜索条件查询
        /// </summary>
        /// <param name="serchtxt"></param>
        /// <returns></returns>
        public static DataTable GetMenuByNumOrName(string searchtxt, string searchType)
        {
            string sql = @"select * from tb_menu where mType!=0 ";
            if (Convert.ToInt32(searchType) != -1)
                sql += " and mType= @searchType ";
            if (!String.IsNullOrEmpty(searchtxt))
                sql += " and mNum=@searchtxt OR mName=@searchtxt ";
            SqlParameter[] pms=new SqlParameter[]{
                new SqlParameter("@searchtxt",searchtxt),
                new SqlParameter("@searchType",searchType)
            };
            DataTable dt = DBHelper.ExcuteDataTable(sql, CommandType.Text, pms);
            return dt;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static int InsertMenu(MenuInfo mi)
        {
            string sql = "insert into tb_menu(mNum,mName,mType,mPrice)values (@mNum, @mName, @mType, @mPrice)";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@mNum",mi.mNun),
                new SqlParameter("@mName",mi.mName),
                new SqlParameter("@mType",mi.mType),
                new SqlParameter("@mPrice",mi.mPrice)
            };
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static int UpdateMenu(MenuInfo mi)
        {
            string sql = "update tb_menu set mNum=@mNum, mName=@mName, mType=@mType, mPrice=@mPrice where ID=@id";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@mNum",mi.mNun),
                new SqlParameter("@mName",mi.mName),
                new SqlParameter("@mType",mi.mType),
                new SqlParameter("@mPrice",mi.mPrice),
                new SqlParameter("@id",mi.ID)

            };
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteMenu(string id)
        {
            string sql = "delete from tb_menu where mNum='" + id + "'";
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text);
        }

        /// <summary>
        /// 递归加载
        /// </summary>
        public static List<MenuInfo> GetMiByMtype(int mtype)
        {
            List<MenuInfo> miList = new List<MenuInfo>();
            string sql = @"SELECT ID,ISNULL(mNum,'') AS mNum,mName,ISNULL(mPrice,0) AS mPrice FROM dbo.tb_menu WHERE mType=@ID";
            using(SqlDataReader reader=DBHelper.ExecuteReader(sql,CommandType.Text,new SqlParameter("@ID",mtype)))
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        MenuInfo mi = new MenuInfo();
                        mi.ID = reader.GetInt32(0);
                        mi.mNun = reader.GetString(1);
                        mi.mName = reader.GetString(2);
                        mi.mPrice = (float)reader.GetDouble(3);
                        miList.Add(mi);
                    }
                }
            }
            return miList;
        }

        /// <summary>
        /// 获取价格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetPriceByID(int id)
        {
            string sql = "select mPrice from tb_menu where ID=@ID";
            object obj= DBHelper.ExecuteScalar(sql, CommandType.Text, new SqlParameter("@ID", id));
            return obj == null ? string.Empty : obj.ToString();
        }
    }
}
