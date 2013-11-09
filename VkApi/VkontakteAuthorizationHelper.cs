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
using VkontakteInfrastructure.Model;

namespace VkontakteCore
{
    public static class AuthorizationHelper
    {
        public static string GetAuthorizationUrl(string applicationId)
        {
            return VkontakteServiceLayer.AuthorizationHelper.GetAuthorizationUrl(applicationId);
        }

         public static AuthorizationResult ParseNavigatedUrl(string url)
         {
             return VkontakteServiceLayer.AuthorizationHelper.ParseNavigatedUrl(url);
         }

         public static AuthorizationResult ParseNavigatedUrl(Uri uri)
         {
             return VkontakteServiceLayer.AuthorizationHelper.ParseNavigatedUrl(uri.ToString());
         }
    }
}
