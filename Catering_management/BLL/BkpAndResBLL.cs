using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catering_management.DAL;

namespace Catering_management.BLL
{
    public class BkpAndResBLL
    {
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool BackupsDB(string fileName)
        {
            int n = BkpAndResDAL.BackupsDB(fileName);
            if (n == -1)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool RestoreDB(string fileName)
        {
            int n = BkpAndResDAL.RestoreDB(fileName);
            if (n == -1)
                return true;
            else
                return false;
        }
    }
}
