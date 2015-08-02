using Catering_management.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Catering_management.BLL
{
    public class UserFunBLL
    {
        /// <summary>
        /// 返回功能列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetFunction()
        {
            DataTable dt = UserFunctionDAL.GetFunction();
            return dt;
        }

        /// <summary>
        /// 返回功能数组
        /// </summary>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static string[] GetfunStr (string fun)
        {
            //fun = fun.Substring(0, fun.Length - 1);
            string[] funStr = fun.Split(',');
            return funStr;
        }
    }
}
