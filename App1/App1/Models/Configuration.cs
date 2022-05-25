using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIEva.Models
{
    public class Configuration
    {
        public string name_user { get; set; }
        public string color { get; set; }   
        public string showEva { get; set; }
        public string showEmotions { get; set; }
        public string sound { get; set; }
        public string volume { get; set; }

        public Configuration(string u, string c, string eva, string emotions, string s, string v)
        {
            name_user = u;
            color = c;
            showEva = eva;
            showEmotions = emotions;
            sound = s;
            volume = v;
        }
    }
}