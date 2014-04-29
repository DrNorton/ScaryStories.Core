using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using VkontakteCore;
using VkontakteInfrastructure.Model;
using VkontakteViewModel.ItemsViewModel;
using VkontakteViewModel.Services;

namespace VkontakteViewModel
{
    public class HomePageViewModel : BaseViewModel
    {
        private UserViewModel userViewModel;
        public UserViewModel UserViewModel
        {
            get { return userViewModel; }
        }

        public HomePageViewModel()
        {
            if(DesignerProperties.IsInDesignTool)
            {
                userViewModel =
                    new UserViewModel(new User()
                    {
                        FirstName = "Имя",
                        LastName = "Фамилия",
                        Nickname = "Ник",
                        PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg"
                    });
            }

            PhotosViewCommand=new DelegateCommand(PhotoViewCommand);
            MessagesViewCommand = new DelegateCommand((obj) => GetService<ISimpleNavigationService>().NavigateToMessagesPage());
            FriendsViewCommand = new DelegateCommand(obj => GetService<ISimpleNavigationService>().NavigateToFriendsPage());

        }

        public ICommand PhotosViewCommand { get; set; }

        private void PhotoViewCommand(object parameter)
        {
            GetService<ISimpleNavigationService>().NavigateToPhotoAlbumPage(GetService<IVkontakteApi>().GetCurrentUid());
        }

        public ICommand MessagesViewCommand { get; set; }

        public ICommand FriendsViewCommand { get; set; }


        public override void OnNavigatedTo(PhoneApplicationPage page, NavigationEventArgs e)
        {
            base.OnNavigatedTo(page,e);
            if(userViewModel==null)
            {
                GetService<IVkontakteApi>().GetCurrentUserProfile(GetUser, (error)=>
                {
                    GetService<ICommonErrorHandler>().HandleError(error);
                });
            }
        }

        private void GetUser(User user)
        {
            userViewModel=new UserViewModel(user);
            OnPropertyChange("UserViewModel");
        }
    }
}
