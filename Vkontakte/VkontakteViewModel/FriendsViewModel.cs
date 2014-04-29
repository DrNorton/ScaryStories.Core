using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using VkontakteCore;
using VkontakteInfrastructure.Model;
using VkontakteViewModel.ItemsViewModel;
using VkontakteViewModel.Services;

namespace VkontakteViewModel
{
    public class FriendsPageViewModel : BaseViewModel
    {
        private List<UserViewModel> friendsCollection;
        public List<UserViewModel> ListFriends
        {
            get { return friendsCollection??new List<UserViewModel>();; }
        }

        public FriendsPageViewModel()
        {
             if(DesignerProperties.IsInDesignTool)
             {
                 friendsCollection=new List<UserViewModel>()
                 {
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg"}),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                     new UserViewModel(new User() { FirstName = "Имя", LastName = "Фамилия", Nickname = "Ник", PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg" }),
                 };
             }
        }

        public override void OnNavigatedTo(PhoneApplicationPage page, System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(page, e);

            if (friendsCollection == null)
            {
                GetService<IVkontakteApi>().GetFriends(GetFriendsAction, error =>
                {
                    GetService<ICommonErrorHandler>().HandleError(error);
                });
            }
        }
        

        private void GetFriendsAction(List<User> friends)
        {
            friendsCollection = friends.Select(i => new UserViewModel(i)).ToList();
            OnPropertyChange("ListFriends");
        }

        public void SelectFriend(UserViewModel userViewModel)
        {
            GetService<ISimpleNavigationService>().NavigateToProfilePage(userViewModel.Uid);
        }
    }
}