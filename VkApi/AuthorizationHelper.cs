using System;
using System.Collections.Generic;
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
using VkontakteInfrastructure.Model;

namespace VkontakteServiceLayer
{
    public class AuthorizationHelper
    {
        private const string RedirectUrl = "http://api.vkontakte.ru/blank.html";

        public static string GetAuthorizationUrl(string applicationId)
        {
            //const string url = "http://api.vkontakte.ru/oauth/authorize?client_id={0}&scope=notify,friends,photos,audio,video,docs,notes,pages,offers,questions,wall,messages&redirect_uri={1}&display=page&response_type=token";
            const string url = "http://api.vkontakte.ru/oauth/authorize?client_id={0}&scope=notify,friends,photos,audio,video,docs,notes,pages,offers,questions,wall,messages&redirect_uri={1}&display=touch&response_type=token";
            return String.Format(url, applicationId, RedirectUrl);
        }

        public static AuthorizationResult ParseNavigatedUrl(string url)
        {
            if (url.StartsWith(RedirectUrl) && url.Length>RedirectUrl.Length)
            {
                var tokenData = GetToken(url);
                var tokenArray = GetTokenArray(tokenData);

                var authorizationResult = GetAuthResult(tokenArray);
                return authorizationResult;

            }

            return new AuthorizationResult() { Status = AuthorzationStatus.Unknown };
        }

        private static AuthorizationResult GetAuthResult(Dictionary<string, string> tokenArray)
        {
            var authorizationResult=new AuthorizationResult();
            
            if (tokenArray.ContainsKey("error"))
            {
                authorizationResult.Status = AuthorzationStatus.Error;
                authorizationResult.Description = tokenArray[tokenArray["error"]];
            }
            else
            {
                authorizationResult.Status = AuthorzationStatus.Success;
                authorizationResult.Description = "success";
                authorizationResult.Context = new AuthorizationContext()
                {
                    AccessToken = tokenArray["access_token"],
                    CurrentUserId = tokenArray["user_id"],
                };
            }
            return authorizationResult;
        }

        private static string GetToken(string url)
        {
            return url.Substring(RedirectUrl.Length+1);
        }

        private static Dictionary<string,string> GetTokenArray(string tokenData)
        {
            var result = new Dictionary<string, string>();
            var keyValuePairs=tokenData.Split(new[] {'&'}, StringSplitOptions.RemoveEmptyEntries);
            
            var splitedKeyValuePairs=keyValuePairs.Select(i => i.Split(new[] {'='}, StringSplitOptions.RemoveEmptyEntries)).ToList();
            
            splitedKeyValuePairs.ForEach(j=>result.Add(j[0], j[1]));

            return result;
        }
    }
}
