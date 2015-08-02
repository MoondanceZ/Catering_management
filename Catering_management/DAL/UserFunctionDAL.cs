using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Catering_management.DAL
{
    public class UserFunctionDAL
    {
        /// <summary>
        /// 返回功能列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetFunction()
        {
            string sql = @"SELECT ID, (CASE funName 
					WHEN 'ResStatus' THEN '餐厅现状'
                    WHEN 'Open' THEN '顾客开台'
					WHEN 'Order' THEN '顾客结算'
					WHEN 'Book' THEN '顾客预约' 
					WHEN 'Stock' THEN '进货管理' 
					WHEN 'Member' THEN '会员管理' 
					WHEN 'Menu' THEN '菜单管理'
					WHEN 'Turnover' THEN '营业管理'
					WHEN 'Emply' THEN '员工管理'
					WHEN 'Safe' THEN '安全管理'                     
                    END) AS funName
                    FROM tb_function";
            DataTable dt = DBHelper.ExcuteDataTable(sql, CommandType.Text);
            return dt;
        }
    }
}
