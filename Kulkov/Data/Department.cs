using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kulkov.Data
{
    public class Department
    {
        public int id_dept { get; set; } = -1;
        public string dept_name { get; set; } = string.Empty;
        public int id_loc { get; set; } = -1;
        public int id_head { get; set; } = -1;
        public Location location { get; set; } = new Location();
        public List<Employee> employees { get; set; } = new List<Employee>;
    }
}
