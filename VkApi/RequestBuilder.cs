using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using VkontakteInfrastructure.Exceptions;
using VkontakteInfrastructure.Interfaces;
using VkontakteInfrastructure.Model;
using VkontakteServiceLayer.Responses;
using VkontakteServiceLayer.Tools;

namespace VkontakteServiceLayer
{
    class RequestBuilder
    {
        private readonly AuthorizationContext context;
        private string apiMethodName;
        private Dictionary<string, string> keyValuePair;
        public RequestBuilder(AuthorizationContext context)
        {
            this.context = context;
            keyValuePair=new Dictionary<string, string>();
            apiMethodName = String.Empty;
        }

        public void SetMethod(string methodName)
        {
            apiMethodName = methodName;
        }

        public void AddParam(string key, string value)
        {
            keyValuePair.Add(key,value);
        }

        public string GetRequestUrl()
        {
            keyValuePair.Add("access_token",context.AccessToken);

            var sb=new StringBuilder();
            var firstKey = true;
            foreach (var key in keyValuePair.Keys)
            {
                if(firstKey)
                {
                    sb.Append("?");
                    firstKey = false;  
                }
                else
                {
                    sb.Append("&");  
                }
                
                sb.Append(key);
                sb.Append("=");
                sb.Append(keyValuePair[key]);
            }

            const string template = "https://api.vkontakte.ru/method/{0}{1}";
            var url = String.Format(template, apiMethodName, sb);
            return url;
        }

        public void SendRequest<T>(Action<T> actionResult, Action<Error> errorAction) where T : IServiceResult, new()
        {
            var client = new WebClient();
            client.OpenReadCompleted += (sender, e) =>
            {
                var response=new JsonSerializer<T>().Deserialize(e.Result);
                if(!response.ResponseIsSuccess())
                {
                    e.Result.Position = 0;
                    var errorResponse=new JsonSerializer<ErrorResponse>().Deserialize(e.Result);
                    if(errorResponse.ResponseIsSuccess())
                    {
                        errorAction.Invoke(errorResponse.ErrorResult);
                    }
                    errorAction.Invoke(new Error() {ErrorMsg ="Unknown error" });
                }
                else
                {
                    actionResult.Invoke(response);
                }
            };

            client.OpenReadAsync(new Uri(GetRequestUrl()));    
        }
    }
}
