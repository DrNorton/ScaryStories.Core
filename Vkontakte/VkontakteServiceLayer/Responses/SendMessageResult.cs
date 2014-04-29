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
using VkontakteInfrastructure.Interfaces;

namespace VkontakteServiceLayer.Responses
{
    public class SendMessageResult : IServiceResult
    {
        public int Response { get; set; }
        public bool ResponseIsSuccess()
        {
            return Response != 0;
        }
    }
}
