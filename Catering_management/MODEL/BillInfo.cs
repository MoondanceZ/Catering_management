using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catering_management.MODEL
{
    public class BillInfo
    {
        public int ID { get; set; }
        public string billName { get; set; }
        public string billNum { get; set; }
        public string billTime { get; set; }
        public string billCode { get; set; }
        public Table table { get; set; }
        public string billFood { get; set; }
        public double billMoney { get; set; }
        public string billFlag { get; set; }
    }
}
