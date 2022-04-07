using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Chat
    {
        
        public string Question { get; set; }
        
        public string Response { get; set; }

        public string MyProperty { get; set; } = "https://www.xtrafondos.com/wallpapers/alan-walker-4721.jpg";

        public Chat(string question, string response, string mypropiety)
        {
            Question = question;
            Response = response;
            MyProperty = mypropiety;
        }
    }
}
