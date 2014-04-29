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
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VkontakteCore;
using VkontakteInfrastructure.Model;
using VkontakteViewModel.ItemsViewModel;
using VkontakteViewModel.Resources;
using VkontakteViewModel.Services;

namespace VkontakteViewModel
{
    public class MessagesPageViewModel : BaseViewModel
    {
        private List<MessageViewModel> messages;
        public List<MessageViewModel> Messages
        {
            get { return messages; }
        }

        public MessageViewModel SelectedItem
        {
            set
            {
                this.GetService<ISimpleNavigationService>().NavigateToMessageConversationPage(value.Uid);
            }
        }

        public MessagesPageViewModel()
        {
            if (DesignerProperties.IsInDesignTool)
            {
                var user = new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg", Uid = "12345"};
                var users = new[] {user};
                var message = new MessageViewModel(new Message()
                {
                    Body = "Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. Message body. ",
                    Uid = user.Uid,
                    Title = "title of message",
                    Date = new DateTime(2010,05,05,04,04,04),
                    IsNewMessage = false,
                    Mid = "0123"
                    },users, "0123");
                messages = new List<MessageViewModel>()
                 {
                     message,
                     message,
                     message,
                     message,
                     message,
                     message,
                     message,
                     message,
                     message,
                     message,
                     message,
                     message,
                     message,
                 };
            }
        }

        private void GetRefreshMessages()
        {
            this.GetService<IVkontakteApi>().GetMessages(result =>
            {
                var uids = result.Select(i => i.Uid).ToList();
                var currentUid = GetService<IVkontakteApi>().GetCurrentUid();
                uids.Add(currentUid);

                GetService<IVkontakteApi>().GetUserProfiles(uids,
                    getUserProfileResult =>
                    {
                        messages = result.Select(i => new MessageViewModel(i, getUserProfileResult, currentUid)).ToList();
                        OnPropertyChange("Messages");
                    },
                    errorResult =>
                    {
                        GetService<ICommonErrorHandler>().HandleError(errorResult);
                    });

            },
            errorResult =>
            {
                GetService<ICommonErrorHandler>().HandleError(errorResult);
            });
        }

        public void RefreshMessages()
        {
            GetRefreshMessages();
        }

        public override void OnNavigatedTo(PhoneApplicationPage page, System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(page, e);
            GetRefreshMessages();
        }

        public void PinToStart()
        {
            GetService<IVkontakteApi>().GetCurrentUserProfile(user=>
            {
                var userViewModel = new UserViewModel(user);

                var shellTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("/MessagesPage.xaml?pinToStart=1&uid=" + userViewModel.Uid));

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
                    BackTitle = AppResource.Messages,
                    Count = 10
                };
                ShellTile.Create(new Uri("/MessagesPage.xaml?pinToStart=1&uid=" + userViewModel.Uid, UriKind.Relative), standardTileData);
            },
            error=>
            {
                GetService<ICommonErrorHandler>().HandleError(error);
            });

            
        }

        public void GoToHome()
        {
            GetService<ISimpleNavigationService>().NavigateToHomePage();
        }
    }
}
