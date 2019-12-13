using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kulkov.Data
{
    public class Salary
    {
        public int id_emp { get; set; } = -1;
        public int salary { get; set; } = -1;
        public int fee { get; set; } = -1;
        public DateTime time_update { get; set; } = DateTime.Now;
    }
}
