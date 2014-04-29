using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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

            const string template = "https://api.vk.com/method/{0}{1}";
            var url = String.Format(template, apiMethodName, sb);
            return url;
        }

        public void SendRequest<T>(Action<T> actionResult, Action<Error> errorAction) where T : IServiceResult, new()
        {
            var client = new WebClient();
            client.UploadStringCompleted += (sender, e) =>
            {
             
                var response=new JsonSerializer<T>().Deserialize(e.Result);
                if(!response.ResponseIsSuccess())
                {
                
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
            var uri=GetRequestUrl();
            var split = uri.Split(new char[] { '?' });
            client.UploadStringAsync(new Uri(split[0]), "POST","?"+split[1]);
          
          //  client.OpenReadAsync(ur);    
        }

        public void SendImage<T>(Action<T> actionResult, Action<Error> errorAction,string photoUrl,byte[] image) where T : IServiceResult, new() {
            var client = new WebClient();
            client.OpenWriteCompleted += (sender, e) =>
            {
                Stream outputStream = e.Result;
                var bytes=Encoding.UTF8.GetBytes((string)e.UserState);
                outputStream.Write(bytes, 0, bytes.Length);
                outputStream.Flush();
                var buffer = new byte[512];
                var count=outputStream.Read(buffer, 0, buffer.Length);

                var response = new JsonSerializer<T>().Deserialize(e.Result);
                if (!response.ResponseIsSuccess())
                {

                    var errorResponse = new JsonSerializer<ErrorResponse>().Deserialize(e.Result);
                    if (errorResponse.ResponseIsSuccess())
                    {
                        errorAction.Invoke(errorResponse.ErrorResult);
                    }
                    errorAction.Invoke(new Error() { ErrorMsg = "Unknown error" });
                }
                else
                {
                    actionResult.Invoke(response);
                }
            };

           // var tag = Encoding.UTF8.GetBytes("photo=");
           // var buffer = new byte[tag.Length + image.Length];
           //System.Buffer.BlockCopy(tag, 0, buffer, 0, tag.Length);
           //System.Buffer.BlockCopy(image, 0, buffer, tag.Length, image.Length);
           //var nameValueCollection = new NameValueCollection();
           //nameValueCollection.Add("ImageBytes", Convert.ToBase64String(data));
         //   UseHttpClient(new Uri(photoUrl),"","",image);
            // client.OpenWriteAsync(new Uri(photoUrl), "POST","photo="+Convert.ToBase64String(image));
        }

      
       
    }
}
