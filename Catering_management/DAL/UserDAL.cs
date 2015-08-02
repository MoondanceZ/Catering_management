using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Catering_management.DAL
{
    public class UserDAL
    {
        /// <summary>
        /// 验证用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool ChkUserName(string userName)
        {
            string sql = @"select count(*) from tb_user where UserName=@userName";
            SqlParameter[] pms=new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            int n = (int)DBHelper.ExecuteScalar(sql, System.Data.CommandType.Text, pms);
            if (n > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 用户注册，插入用户数据
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static int InsertUser(string userName, string passWord)
        {
            string sql = @"insert into tb_user(UserName,UserPwd,UserType) values(@userName, @passWord, 2)";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@userName", userName),
                new SqlParameter("@passWord", passWord)
            };

            return DBHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text, pms);
        }

        /// <summary>
        /// 验证用户登录  返回一用户对象
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public static SysUser UserLogin(string userName)
        {
            SysUser su = null;
            string sql = @"select ID, UserType, UserName, UserPwd, UserFun from tb_user where UserName=@userName";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@userName",userName)
            };
            using (SqlDataReader sda = DBHelper.ExecuteReader(sql, System.Data.CommandType.Text, pms))
            {
                if(sda.HasRows)
                    if(sda.Read())
                    {
                        su = new SysUser();
                        su.Id = sda.GetInt32(0);
                        su.UserType = sda.GetInt32(1);
                        su.Name = sda.GetString(2);
                        su.PassWord = sda.GetString(3);
                        su.funtion = sda.GetString(4);
                    }
            }
            return su;
        }

        /// <summary>
        /// 返回所有用户
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUser()
        {
            string sql = @"SELECT id,UserName, UserPwd,CASE WHEN UserType=1 THEN '店长' ELSE '员工' END AS UserType,UserFun FROM tb_user";
            DataTable dt = DBHelper.ExcuteDataTable(sql, CommandType.Text);
            return dt;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public static int UpdateUser(SysUser su)
        {
            string sql = @"UPDATE dbo.tb_user SET UserName=@userName, UserPwd=@passWord, UserFun=@userFun WHERE id=@ID";
            SqlParameter[] pms = new SqlParameter[]{
                new SqlParameter("@userName", su.Name),
                new SqlParameter("@passWord", su.PassWord),
                new SqlParameter("@userFun", su.funtion),
                new SqlParameter("@ID", su.Id)
            };
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, pms);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public static int DeleteUser(SysUser su)
        {
            string sql = @"DELETE from tb_user where id=@id";
            return DBHelper.ExecuteNonQuery(sql, CommandType.Text, new SqlParameter("@ID", su.Id));
        }
    }
}
