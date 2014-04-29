using System;
using System.Windows;
using Microsoft.Phone.Controls;
using VkontakteCore;
using VkontakteInfrastructure.Model;
using VkontakteViewModel.Services;

namespace VkontakteViewModel
{
    public class AuthorizationViewModel : BaseViewModel
    {
        const string AppId = "2416563";

        public Uri GetUri()
        {
            var uri = new Uri(AuthorizationHelper.GetAuthorizationUrl(AppId));
            return uri;
        }

        public void ParseUri(Uri uri)
        {
            var result = AuthorizationHelper.ParseNavigatedUrl(uri.ToString());
            if (result.Status == AuthorzationStatus.Success)
            {
                this.GetService<IVkontakteApi>().Initialize(result.Context);
                

                //(Application.Current.RootVisual as PhoneApplicationFrame).Navigate(new Uri("/MainMenu.xaml", UriKind.Relative));
                (Application.Current.RootVisual as PhoneApplicationFrame).GoBack();

                

            }
        }

        public void RestoreContext()
        {
            GetService<IVkontakteApi>().RestoreContext();
            
            
            
        }

        public bool IsAuthorized
        {
            get { return GetService<IVkontakteApi>().Authorized(); }
            
        }
    }
}