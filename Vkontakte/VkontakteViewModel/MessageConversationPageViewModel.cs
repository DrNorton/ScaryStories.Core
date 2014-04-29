using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Shell;
using VkontakteCore;
using VkontakteInfrastructure.Model;
using VkontakteViewModel.ItemsViewModel;
using VkontakteViewModel.Resources;
using VkontakteViewModel.Services;

namespace VkontakteViewModel
{
    public class MessageConversationPageViewModel : BaseViewModel
    {
        public MessageConversationPageViewModel()
        {
            if (DesignerProperties.IsInDesignTool)
            {
                FillDesignTimeData();
            }
        }

        private void FillDesignTimeData()
        {
            var user = new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg", Uid = "12345" };
            var users = new[] { user };
            var message = new MessageViewModel(new Message()
            {
                Body = "Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. ",
                Uid = user.Uid,
                Title = "title of message",
                Date = new DateTime(2010, 05, 05, 04, 04, 04),
                IsNewMessage = false,
                Mid = "0123"
            }, users, "0123");

            var messageOut = new MessageViewModel(new Message()
            {
                Body = "Message",
                Uid = user.Uid,
                Title = "title",
                Date = new DateTime(2010, 05, 05, 04, 04, 04),
                IsNewMessage = false,
                Mid = "0123",
                IsOut = true,
            }, users, "12345");
            messages = new List<MessageViewModel>()
                 {
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                     messageOut,
                     message,
                 };
             
        }

        private string sendMessageEnabled;
        public string SendMessageEnabled
        {
            get { return sendMessageEnabled; }
            set
            {
                sendMessageEnabled = value;
                OnPropertyChange("SendMessageEnabled");
            }
        }

        private UserViewModel userViewModel;

        public UserViewModel UserViewModel
        {
            get { return userViewModel; }
            set { userViewModel = value; OnPropertyChange("UserViewModel"); }
        }

        private List<MessageViewModel> messages;

        private string newMessageText;

        public string NewMessageText
        {
            get { return newMessageText; }
            set
            {
                newMessageText = value; 
                OnPropertyChange("NewMessageText");
                NewMessageTextUpdateAction.Invoke(value);
            }
        }

        public List<MessageViewModel> Messages
        {
            get { return messages; }
            set { messages = value; OnPropertyChange("Messages"); }
        }

        public int ScreenWidth
        {
            get
            {
                return base.IsPortraitOrientation ? 440 : 600;
            }
        }

        public void OrientationChanged(Microsoft.Phone.Controls.OrientationChangedEventArgs e)
        {
            OnPropertyChange("ScreenWidth");
        }

        public void SendMessage()
        {
            var uid = GetStateOrUrlParam("uid");
            var textmessage = NewMessageText;
            this.GetService<IVkontakteApi>().SendMessage(uid, textmessage, ()=>
            {
                NewMessageText = String.Empty;
                UpdateMessages();
            }, errorResponse => this.GetService<ICommonErrorHandler>().HandleError(errorResponse));
        }

        public Action UpdateCompleteAction { get; set; }
        public Action<String> NewMessageTextUpdateAction { get; set; }

        public bool SendMessageButtonIsEnabled
        {
            get { return newMessageText.Length > 0; }
        }


        public override void OnNavigatedTo(Microsoft.Phone.Controls.PhoneApplicationPage page, System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(page, e);

            UpdateMessages();
        }

        
        

        private void UpdateMessages()
        {
            var uid = GetStateOrUrlParam("uid");
            var currentUid = GetService<IVkontakteApi>().GetCurrentUid();


            this.GetService<IVkontakteApi>().GetUserProfile(uid, (user) =>
            {
                UserViewModel = new UserViewModel(user);
                GetService<IVkontakteApi>().GetMessageConversation(uid, resultMessages =>
                {
                    var uids = resultMessages.Select(i => i.Uid).ToList();
                    uids.Add(currentUid);

                    GetService<IVkontakteApi>().GetUserProfiles(uids, profiles =>
                    {
                        var messageViewModels = resultMessages.OrderBy(i => i.Date).Select(
                            i => new MessageViewModel(i, profiles, currentUid)).
                            ToList();

                        Messages = messageViewModels;
                        if(UpdateCompleteAction!=null)
                            UpdateCompleteAction.Invoke();
                    },
                        error =>
                        {

                        });
                },
                error =>
                {
                    GetService<ICommonErrorHandler>().HandleError(error);
                });
            },
            error =>
            {
                GetService<ICommonErrorHandler>().HandleError(error);
            });
        }


        public void Refresh()
        {
            UpdateMessages();
        }

        public void GoToHome()
        {
            GetService<ISimpleNavigationService>().NavigateToHomePage();
        }

        public void PinToStart()
        {
            var uid = GetStateOrUrlParam("uid");
            GetService<IVkontakteApi>().GetUserProfile(uid,user =>
            {
                var userViewModel = new UserViewModel(user);

                var shellTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("/MessageConversationPage.xaml?pinToStart=1&uid=" + userViewModel.Uid));

                if (shellTile != null)
                {
                    shellTile.Delete();
                }

                var standardTileData = new StandardTileData()
                {

                    BackgroundImage = new Uri(userViewModel.PhotoMedium, UriKind.Absolute),
                    BackBackgroundImage = new Uri(userViewModel.PhotoMedium, UriKind.Absolute),

                    Title = userViewModel.FirstName,
                    BackContent = userViewModel.LastName,
                    BackTitle = AppResource.Conversation,
                    Count = 10
                };
                ShellTile.Create(new Uri("/MessageConversationPage.xaml?pinToStart=1&uid=" + userViewModel.Uid, UriKind.Relative), standardTileData);
            },
            error =>
            {
                GetService<ICommonErrorHandler>().HandleError(error);
            });
        }
    }
}
