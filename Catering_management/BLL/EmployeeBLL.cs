using Catering_management.DAL;
using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Catering_management.BLL
{
    public class EmployeeBLL
    {
        /// <summary>
        /// 查找员工
        /// </summary>
        /// <param name="searchTxt"></param>
        /// <returns></returns>
        public static DataTable GetEmployee(string searchTxt)
        {
            return EmployeeDAL.GetEmployee(searchTxt);
        }

        /// <summary>
        /// 插入员工
        /// </summary>
        /// <param name="ei"></param>
        /// <returns></returns>
        public static bool InsertEmployee(EmployeeInfo ei)
        {
            return EmployeeDAL.InsertEmployee(ei) > 0;
        }

        /// <summary>
        /// 编辑员工
        /// </summary>
        /// <param name="ei"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(EmployeeInfo ei)
        {
            return EmployeeDAL.UpdateEmployee(ei) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int ID)
        {
            return EmployeeDAL.DeleteEmployee(ID) > 0;
        }

          /// <summary>
        /// 报表相关
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDtEmpForXsd()
        {
            return EmployeeDAL.GetDtEmpForXsd();
        }
    }
}
