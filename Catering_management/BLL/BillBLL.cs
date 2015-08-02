using Catering_management.DAL;
using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Catering_management.BLL
{
    public class BillBLL
    {
        /// <summary>
        /// 获取billCode
        /// </summary>
        /// <returns></returns>
        public static string GetBillCode()
        {
            return BillDAL.GetBillCode();
        }

        /// <summary>
        /// 返回桌状态
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string IsUseTable(string num)
        {
            return BillDAL.IsUseTable(num);
        }

        /// <summary>
        /// 返回所以未使用餐桌
        /// </summary>
        /// <returns></returns>
        public static List<Table> GetUsingTb()
        {
            return BillDAL.GetUsingTb();
        }

        /// <summary>
        /// 插入开单
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static bool InsertBill(BillInfo bi)
        {
            return BillDAL.InsertBill(bi) > 0;
        }

          /// <summary>
        /// 获取桌ID
        /// </summary>
        /// <returns></returns>
        public static string GetTableID()
        {
            return BillDAL.GetTableID();
        }

        /// <summary>
        /// 查找用户开单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDtBill()
        {
            return BillDAL.GetDtBill();
        }

        /// <summary>
        /// 查找用户开单
        /// </summary>
        /// <param name="type"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DataTable GetDtBill(int type, string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return BillDAL.GetDtBill(type, str);
            }
            else
                return BillDAL.GetDtBill();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteBill(int id)
        {
            return BillDAL.DeleteBill(id) > 0;
        }

        /// <summary>
        /// 获取开单信息  用于结算
        /// </summary>
        /// <param name="table"></param>
        /// <param name="ctm"></param>
        /// <returns></returns>
        public static BillInfo GetBillInfo(string table, string ctm)
        {
            return BillDAL.GetBillInfo(table, ctm);
        }

             /// <summary>
        /// 返回顾客信息
        /// </summary>
        /// <param name="ctm"></param>
        /// <returns></returns>
        public static MemberInfo GetMbInfo(string ctm)
        {
            return BillDAL.GetMbInfo(ctm);
        }

        /// <summary>
        /// 更新表
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool UpdateBill(string str)
        {
            return BillDAL.UpdateBill(str) > 0;
        }

         /// <summary>
        /// 获取时间段开单
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public static DataTable GetBillByTime(string t1,string t2)
        {
            return BillDAL.GetBillByTime(t1, t2);
        }
    }
}
