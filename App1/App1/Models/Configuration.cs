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
    }
}