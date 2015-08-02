using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catering_management.MODEL
{
    /// <summary>
    /// 用户绑定ComBox
    /// </summary>
    public class BindComboBox
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public BindComboBox(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
