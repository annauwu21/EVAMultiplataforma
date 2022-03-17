using System.Collections.Generic;

namespace App1
{
    public class Entity
    {
        public string entity { get; set; }
        public List<int> location { get; set; }
        public string value { get; set; }
        public int confidence { get; set; }
    }
}