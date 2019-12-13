using System;

namespace Kulkov.Data
{
    public class Post
    {
        public int id_post { get; set; } = -1;
        public string post_name { get; set; } = string.Empty;
        public DateTime date_start { get; set; } = DateTime.Now;
    }
}
