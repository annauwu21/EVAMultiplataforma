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

        public bool QuestionVisibility { get; set; }

        public bool ResponseVisibility { get; set; }

        public Chat(string question, string response, string imageSource, bool imageVisible, bool questionVisible, bool responseVisible)
        {
            Question = question;
            Response = response;
            Image = imageSource;
            ImageVisibility = imageVisible;
            QuestionVisibility = questionVisible;
            ResponseVisibility = responseVisible;
        }
    }
}
