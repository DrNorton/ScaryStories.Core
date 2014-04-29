using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VkontakteInfrastructure.Interfaces;
using VkontakteInfrastructure.Model;
using VkontakteServiceLayer.Responses;

namespace VkontakteServiceLayer.Tools
{
    public class JsonSerializer<T> where T : IServiceResult, new()
    {
        public JsonSerializer()
        {
            
        }

        public T Deserialize(Stream stream)
        {
            try {
                var test = new StreamReader(stream).ReadToEnd();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(test);
            }
            catch (Exception exp)
            {
                return new T();
            }
        }

        public T Deserialize(string str)
        {
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
            }
            catch (Exception exp)
            {
                return new T();
            }
        }
    }

    

}
