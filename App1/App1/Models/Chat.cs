using System;
using System.Collections.Generic;
using System.Drawing;
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

        public Color Bubble1Color { get; set; }
        public Color Bubble1Border { get; set; }

        public Chat(string question, string response, string imageSource, bool imageVisible, bool questionVisible, bool responseVisible, Color b1C, Color b1B)
        {
            Question = question;
            Response = response;
            Image = imageSource;
            ImageVisibility = imageVisible;
            QuestionVisibility = questionVisible;
            ResponseVisibility = responseVisible;
            Bubble1Color = b1C;
            Bubble1Border = b1B;
        }

        public Chat(Color b1C, Color b1B)
        {
            Bubble1Color = b1C;
            Bubble1Border = b1B;
        }
    }
}
