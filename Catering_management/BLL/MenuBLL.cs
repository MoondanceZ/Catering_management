using Catering_management.DAL;
using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Catering_management.BLL
{
    public class MenuBLL
    {
        /// <summary>
        /// 插入菜单大类
        /// </summary>
        /// <param name="mName"></param>
        /// <returns></returns>
        public static bool InsertCategory(string Category)
        {
            return(MenuDAL.InsertCategory(Category)>0);            
        }

        /// <summary>
        /// 删除菜单大类
        /// </summary>
        /// <param name="Category"></param>
        /// <returns></returns>
        public static bool DeleteCategory(string Category)
        {
            return (MenuDAL.DeleteCategory(Category) > 0);
        }

        /// <summary>
        /// 返回餐品大类
        /// </summary>
        /// <returns></returns>
        public static List<MenuInfo> GetMenuCategory()
        {
            return MenuDAL.GetMenuCategory();
        }
        /// <summary>
        /// 获取餐品
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMenu()
        {
            DataTable dt = MenuDAL.GetMenu();
            return dt;
        }

        /// <summary>
        /// 按搜索条件查询
        /// </summary>
        /// <param name="serchtxt"></param>
        /// <returns></returns>
        public static DataTable GetMenuByNumOrName(string searchtxt, string searchType)
        {
            DataTable dt = MenuDAL.GetMenuByNumOrName(searchtxt, searchType);
            return dt;
        }

         /// <summary>
        /// 新增
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static bool InsertMenu(MenuInfo mi)
        {
            return MenuDAL.InsertMenu(mi) > 0;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static bool UpdateMenu(MenuInfo mi)
        {
            return MenuDAL.UpdateMenu(mi) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteMenu(string id)
        {
            return MenuDAL.DeleteMenu(id) > 0;
        }

        /// <summary>
        /// 递归加载
        /// </summary>
        public static List<MenuInfo> GetMiByMtype(int mtype)
        {
            List<MenuInfo> miList = MenuDAL.GetMiByMtype(mtype);
            return miList;
        }

         /// <summary>
        /// 获取价格
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetPriceByID(int id)
        {
            return MenuDAL.GetPriceByID(id);

        }
    }
}
