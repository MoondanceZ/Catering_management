using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catering_management.MODEL
{
    public class MemberInfo
    {
        public int ID { get; set; }
        public string Num { get; set; }
        public string Name { get; set; }
        public int Grade { get; set; }
        public MDiscount MDiscount { get; set; }
    }
}
