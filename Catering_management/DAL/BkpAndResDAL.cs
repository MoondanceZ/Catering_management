using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Catering_management.DAL
{
    public class BkpAndResDAL
    {
        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int BackupsDB(string fileName)
        {
            string sql = @"BACKUP DATABASE Catering  TO DISK ='"+fileName+".bak'";
            return DBHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text);
        }

        /// <summary>
        /// 还原数据库
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int RestoreDB(string fileName)
        {
            string sql = @"backup log Catering to disk='" + fileName + "'use master RESTORE DATABASE Catering  from disk='" + fileName + "'";
            return DBHelper.ExecuteNonQuery(sql, System.Data.CommandType.Text);
        }
    }
}
