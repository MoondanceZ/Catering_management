using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catering_management.MODEL
{
    public class Table
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public int IsUse { get; set; }
        public int BillID { get; set; }
    }
}
