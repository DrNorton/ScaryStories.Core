using System;
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

namespace VkontakteViewModel.Services
{
    public class SimpleNavigationService : ISimpleNavigationService
    {
        private PhoneApplicationFrame phoneApplicationFrame
        {
            get { return (Application.Current.RootVisual as PhoneApplicationFrame); }
        }

        public void NavigatToAuthorizationPage()
        {
            NavigateTo("/AuthorizationPage.xaml");
        }

        public void NavigateToProfilePage(string uid)
        {
            NavigateTo("/ProfilePage.xaml?uid=" + uid);
        }

        public void NavigateToHomePage()
        {
            NavigateTo("/HomePage.xaml");
        }

        public void NavigateToPhotoAlbumPage(string uid)
        {
            NavigateTo("/PhotoAlbumPage.xaml?uid={0}",uid);
        }

        public void NavigateToPhotosViewPage(string uid, string albumUid)
        {
            NavigateTo(String.Format("/PhotosViewPage.xaml?uid={0}&aid={1}",uid,albumUid));
        }

        public void NavigateToMessageConversationPage(string uid)
        {
            NavigateTo(String.Format("/MessageConversationPage.xaml?uid={0}",uid));
        }

        public void NavigateToMessagesPage()
        {
            NavigateTo("/MessagesPage.xaml");
        }

        public void NavigateToFriendsPage()
        {
            NavigateTo("/FriendsPage.xaml");
        }


        private void NavigateTo(string urlTemplate, params string[] values)
        {
            var url = String.Format(urlTemplate, values);
            phoneApplicationFrame.Navigate(new Uri(url, UriKind.Relative));
        }
    }
}
