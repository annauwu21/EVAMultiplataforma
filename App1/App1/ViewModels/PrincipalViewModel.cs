using App1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App1.ViewModels
{
    public class PrincipalViewModel
    {
        public ObservableCollection<Chat> Chats { get; set; }

        public PrincipalViewModel(List<Chat> chatsList)
        {
            ObservableCollection<Chat> Chats = new ObservableCollection<Chat>();

            for (int i=0;i<chatsList.Count;i++)
            {
                Chats.Add(chatsList[i]);

            }
           

        }

    }
}
