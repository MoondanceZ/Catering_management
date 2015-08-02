using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Catering_management.DAL
{
    public class MemberDAL
    {
        /// <summary>
        /// 查找会员
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static DataTable GetDtMember(string member, string type)
        {
            string sql = @"select A.ID,A.Num,A.Name,CASE A.Grade WHEN '0' THEN '全部' WHEN '1' THEN '高级会员' ELSE '普通会员' END AS Grade , B.Discount from tb_member A join tb_discount B on A.ID = B.MemberID 
                            where 1=1";
            if (!String.IsNullOrEmpty(member))
                sql += " or A.Num=@member or A.Name=@member ";
            if (Convert.ToInt32(type) != 0)
                sql += " and Grade=@type ";
            sql += " order by Num ";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@member",member),
                new SqlParameter("@type",type)
            };
            DataTable dt = DBHelper.ExcuteDataTable(sql, CommandType.Text, pms);
            return dt;
        }

        /// <summary>
        /// 添加会员
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static int InsertMember(MemberInfo mi)
        {
            MDiscount ds = mi.MDiscount;
            string sql = @"Insert into tb_member(Num,Name,Grade) values (@Num,@Name,@Grade) ";
            sql += "Insert into tb_discount(MemberID, discount) values((select ID from tb_member where Num=@Num),@discount) ";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@Num",mi.Num),
                new SqlParameter("@Name",mi.Name),
                new SqlParameter("@Grade",mi.Grade),
                new SqlParameter("@discount",ds.Discount)
            };
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 编辑会员
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static int UpdateMember(MemberInfo mi)
        {
            MDiscount ds = mi.MDiscount;
            string sql = @"update tb_member set  Name=@Name,Grade=@Grade, Num=@Num where ID=@ID";
            sql += @" update tb_discount set discount=@discount where MemberID=@ID ";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@ID",mi.ID),
                new SqlParameter("@Num",mi.Num),
                new SqlParameter("@Name",mi.Name),
                new SqlParameter("@Grade",mi.Grade),
                new SqlParameter("@discount",ds.Discount)
            };
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static int DeleteMember(string ID)
        {
            string sql = @"Delete from tb_member where ID=@ID";//由触发器删除折扣表
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@ID", ID));
        }
    }
}
