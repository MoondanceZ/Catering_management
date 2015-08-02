using Catering_management.DAL;
using Catering_management.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Catering_management.BLL
{
    public class MemberBLL
    {
        
        /// <summary>
        /// 查找会员
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public static DataTable GetDtMember(string member, string type)
        {            
            return MemberDAL.GetDtMember(member, type);
        }

        /// <summary>
        /// 添加会员
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static bool InsertMember(MemberInfo mi)
        {
            return MemberDAL.InsertMember(mi) > 0;
        }

        /// <summary>
        /// 编辑会员
        /// </summary>
        /// <param name="mi"></param>
        /// <returns></returns>
        public static bool UpdateMember(MemberInfo mi)
        {
            return MemberDAL.UpdateMember(mi) > 0;
        }

        /// <summary>
        /// 删除会员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static bool DeleteMember(string ID)
        {
            return MemberDAL.DeleteMember(ID) > 0;
        }
    }
}
