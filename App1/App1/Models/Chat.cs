using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    public class Chat
    {
        
        public string Question { get; set; }
        
        public string Response { get; set; }

        public Chat(string question, string response)
        {
            Question = question;
            Response = response;
        }
    }
}
