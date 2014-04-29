using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VkontakteInfrastructure.Model;

namespace VkontakteViewModel.ItemsViewModel
{
    public class MessageViewModel : BaseViewModel
    {
        private readonly Message message;
        private readonly string currentUid;
        private readonly Dictionary<string,User> usersIndex;

        public Visibility IncomeVisibility
        {
            get { return message.IsOut ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility OutVisibility
        {
            get { return message.IsOut ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool IsOut
        {
            get { return message.IsOut; }
        }

        private string newTextMessage;
        public string NewTextMessage
        {
            get { return newTextMessage; }
            set
            {
                newTextMessage = value; 
                OnPropertyChange("NewTextMessage");
                SendMessageEnabled = value.Length > 0;
            }
        }

        private bool sendMessageEnabled;
        public bool SendMessageEnabled
        {
            get { return sendMessageEnabled; }
            set { sendMessageEnabled = value; OnPropertyChange("SendMessageEnabled"); }
        }

        public FontWeight ReadStateFontWeight
        {
            get
            {
                return  message.IsNewMessage? FontWeights.Bold : FontWeights.Normal;
            }
        }


        public MessageViewModel(Message message, IEnumerable<User> usersInProfile, string currentUid)
        {
            this.message = message;
            this.currentUid = currentUid;
            usersIndex=new Dictionary<string, User>();

            foreach (var user in usersInProfile)
            {
                usersIndex[user.Uid] = user;
            }
        }

        public User UserFrom
        {
            get
            {
                return message.IsOut ? usersIndex[currentUid] : usersIndex[this.Uid];
            }
        }

        public User User
        {
            get
            {
                return usersIndex[this.Uid];
            }
        }

        public User UserTo
        {
            get
            {
                return message.IsOut ? usersIndex[this.Uid] : usersIndex[currentUid];
            }
        }

        public string Body { get { return message.Body; } }

        public string Title { get { return String.IsNullOrWhiteSpace(message.Title) ? "..":message.Title; } }

        public DateTime Date { get { return message.Date; } }

        public string Uid { get { return message.Uid; } }

        public string Mid { get { return message.Mid; } }

        public bool IsNewMessage { get { return message.IsNewMessage; } }
    }
}
