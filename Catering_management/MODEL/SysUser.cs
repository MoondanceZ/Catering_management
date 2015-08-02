using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catering_management.MODEL
{
    public class SysUser
    {
        public int Id { get; set; }
        public int UserType { get; set; }
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string funtion { get; set; }
    }
}
