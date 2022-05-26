using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIEva.Models
{
    public class User
    {
        public string name_user { get; set; }
        public string pass { get; set; }

        public User (string u, string p)
        {
            name_user = u;
            pass = p;
        }
    }
}