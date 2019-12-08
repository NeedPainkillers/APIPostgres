using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kulkov.Data
{
    public class Employee
    {
        public int id_emp { get; set; } = -1;
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string patronymic { get; set; } = string.Empty;
        public bool gender { get; set; } = true;
        public DateTime hire_date { get; set; } = DateTime.Now;
        public int id_post { get; set; } = -1;
        public string additionalInfo { get; set; } = string.Empty;
        public Salary salary { get; set; } = new Salary();
        public Post post { get; set; } = new Post();
    }
}
