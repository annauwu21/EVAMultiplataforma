using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIEva.Models
{
    public class History
    {
        public string name_user { get; set; }
        public string question { get; set; }
        public string response { get; set; }
        public string type { get; set; }
        public string timedate { get; set; }

        public History(string u, string q, string r, string t, string d)
        {
            name_user = u;
            question = q;
            response = r;
            type = t;
            timedate = d;
        }
    }
}