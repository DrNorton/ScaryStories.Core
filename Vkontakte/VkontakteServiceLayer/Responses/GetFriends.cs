using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using VkontakteInfrastructure.Interfaces;

namespace VkontakteServiceLayer.Responses
{
    [DataContract]
    public class GetFriends : IServiceResult
    {
        [DataMember(Name = "response")]
        public string[] Result { get; set; }

        public bool ResponseIsSuccess()
        {
            return Result != null;
        }

        public string Error { get; set; }
    }
}
