using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kulkov.Data
{
    public class log
    {
        public int id { get; set; } = -1;
        public string username { get; set; } = string.Empty;
        public string action { get; set; } = string.Empty;
        public DateTime time_stamp { get; set; } = DateTime.Now;
    }
}
