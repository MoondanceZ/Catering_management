using Catering_management.DAL;
using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Catering_management.BLL
{
    public class UserBLL
    {
        /// <summary>
        /// 验证用户注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <param name="chkPwd"></param>
        /// <returns></returns>
        public static string CheckRgeister(string userName, string pwd, string chkPwd)
        {
            bool flagRegister = false;
            string ChkStr = null;
            if ((userName.Length >= 4) && (userName.Length <= 8) && (pwd.Length >= 4) && (pwd.Length <= 8) && (pwd == chkPwd))
            {
                flagRegister = true;
            }
            else
            {
                if ((userName.Length < 4) || (userName.Length > 8))
                {
                    ChkStr = "用户名长度不在规定范围之内，请重新输入！";
                }
                if ((pwd.Length < 4) || (pwd.Length > 8))
                {
                    ChkStr = "密码长度不在规定范围之内，请重新输入！";
                }
                if (!(pwd.Equals(chkPwd)))
                {
                    ChkStr = "2次输入的密码不同，请重新输入！";
                }
            }

            if (flagRegister == true)
            {
                if (UserDAL.ChkUserName(userName))
                {
                    ChkStr = "该用户已存在，请重新输入用户名！";
                }
                else
                {
                    if (UserDAL.InsertUser(userName, pwd) > 0)
                        ChkStr = "注册成功";
                }
            }

            return ChkStr;
        }

        /// <summary>
        /// 验证登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="userType"></param>
        /// <param name="su"></param>
        /// <returns></returns>
        public static LoginResult CheckLogin(string userName, string passWord, int userType, out SysUser su)
        {
            su = UserDAL.UserLogin(userName);
            if (su == null)
                return LoginResult.UserNotExists;  //用户不存在
            else
            {
                if (su.Name == userName && su.PassWord == passWord && su.UserType == userType)
                    return LoginResult.Success;  //登录成功
                else if (su.PassWord != passWord)
                    return LoginResult.ErrorPwd;  //密码不对
                else
                    return LoginResult.ErrorUtp;  //用户类型不对
            }           
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUser()
        {
            DataTable dt = UserDAL.GetAllUser();
            return dt;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public static bool UpdateUser(SysUser su)
        {
            int n = UserDAL.UpdateUser(su);
            if (n > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="su"></param>
        /// <returns></returns>
        public static bool DeleteUser(SysUser su)
        {
            int n = UserDAL.DeleteUser(su);
            if (n > 0)
                return true;
            else
                return false;
        }
    }

    public enum LoginResult
    {
        Success,
        ErrorPwd,
        ErrorUtp,
        UserNotExists
    }
}
