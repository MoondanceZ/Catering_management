using Catering_management.DAL;
using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Catering_management.BLL
{
    public class StockBLL
    {
        /// <summary>
        /// 根据时间或货物名搜索
        /// </summary>
        /// <param name="str"></param>
        /// <param name="type">0：全部，1：时间，2：货物</param>
        /// <returns></returns>
        public static DataTable GetGoods(string str, int type)
        {
            return StockDAL.GetGoods(str, type);
        }

        /// <summary>
        /// 新增货物
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public static bool InserGoods(Goods goods)
        {
            return StockDAL.InserGoods(goods) > 0;
        }

        /// <summary>
        /// 编辑货物
        /// </summary>
        /// <param name="goods"></param>
        /// <returns></returns>
        public static bool UpdateGoods(Goods goods)
        {
            return StockDAL.UpdateGoods(goods) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool DeleteGoods(int ID)
        {
            return StockDAL.DeleteGoods(ID) > 0;
        }

        /// <summary>
        /// 报表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetDtStkForXsd(string t1, string t2)
        {
            return StockDAL.GetDtStkForXsd(t1, t2);
        }
    }
}
