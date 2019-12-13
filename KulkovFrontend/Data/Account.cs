using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kulkov.Data
{
    public class Account
    {
        public int user_id { get; set; } = -1;
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string created_on { get; set; } = string.Empty;
        public string last_login { get; set; } = string.Empty;
    }
}
