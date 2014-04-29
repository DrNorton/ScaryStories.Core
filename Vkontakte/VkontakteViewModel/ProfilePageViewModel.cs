using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VkontakteCore;
using VkontakteInfrastructure.Model;
using VkontakteViewModel.ItemsViewModel;
using VkontakteViewModel.Resources;
using VkontakteViewModel.Services;

namespace VkontakteViewModel
{
    public class ProfilePageViewModel : BaseViewModel
    {
        public ProfilePageViewModel()
        {
            OpenMessageConversationCommand=new DelegateCommand(MessageConverClick);
            OpenPhotosCommand=new DelegateCommand(obj=>GoToPhotoPage());
            OpenFriendsCommand=new DelegateCommand(obj=> { });
        }

        public void MessageConverClick(object obj)
        {
            GetService<ISimpleNavigationService>().NavigateToMessageConversationPage(UserViewModel.Uid);
        }

        private UserViewModel userViewModel;
        public UserViewModel UserViewModel
        {
            get { return userViewModel; }
            set { userViewModel = value; OnPropertyChange("UserViewModel"); }
        }

        public override void OnNavigatedTo(PhoneApplicationPage page, NavigationEventArgs e)
        {
            base.OnNavigatedTo(page,e);

            var uid = GetStateOrUrlParam("uid");
            userViewModel=new UserViewModel(new User() { Uid = uid});
            GetService<IVkontakteApi>().GetUserProfile(uid, GetUserProfileComplete, GetUserProfileError);
        }

        public ICommand OpenMessageConversationCommand { get; set; }

        public ICommand OpenPhotosCommand { get; set; }

        public ICommand OpenFriendsCommand { get; set; }

        public void GetUserProfileComplete(User user)
        {
            userViewModel=new UserViewModel(user);
            OnPropertyChange("UserViewModel");
        }

        public void GetUserProfileError(Error error)
        {
            GetService<ICommonErrorHandler>().HandleError(error);
        }

        public void PinToStart()
        {
            var shellTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("/ProfilePage.xaml?pinToStart=1&uid=" + userViewModel.Uid));

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
                BackTitle = AppResource.Profile,
                
            };
            ShellTile.Create(new Uri("/ProfilePage.xaml?pinToStart=1&uid=" + userViewModel.Uid, UriKind.Relative), standardTileData);
        }

        public void GoToHome()
        {
            GetService<ISimpleNavigationService>().NavigateToHomePage();
        }

        public void GoToPhotoPage()
        {
            GetService<ISimpleNavigationService>().NavigateToPhotoAlbumPage(UserViewModel.Uid);
        }
    }
}