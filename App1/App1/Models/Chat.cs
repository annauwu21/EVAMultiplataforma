using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Chat
    {
        
        public string Question { get; set; }
        
        public string Response { get; set; }

        public string Image { get; set; }
        
        public bool ImageVisibility { get; set; }

        public Chat(string question, string response, string mypropiety, bool visible)
        {
            Question = question;
            Response = response;
            Image = mypropiety;
            ImageVisibility = visible;
        }
    }
}
