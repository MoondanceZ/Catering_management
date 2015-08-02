using Catering_management.DAL;
using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Catering_management.BLL
{
    public class BookBLL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static bool InsertBook(BookInfo bi)
        {
            return BookDAL.InsertBook(bi) > 0;
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="type"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static System.Data.DataTable GetDtBook(int type, string str)
        {
            if (!String.IsNullOrEmpty(str))
            {
                return BookDAL.GetDtBook(type, str);
            }
            else
               return BookDAL.GetDtBook();
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <returns></returns>
        public static System.Data.DataTable GetDtBook()
        {
            return BookDAL.GetDtBook();
        }

         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteBook(int id)
        {
            return BookDAL.DeleteBook(id) > 0;
        }

            /// <summary>
        /// 开台
        /// </summary>
        /// <param name="bi"></param>
        /// <returns></returns>
        public static bool InsertIntoBill(BookInfo bi)
        {
            return BookDAL.InsertIntoBill(bi) > 0;
        }
    }
}
