using System;
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

namespace VkontakteInfrastructure.Model
{
    [DataContract(Name = "error")]
    public class Error
    {
        [DataMember(Name = "error_code")]
        public string ErrorCode { get; set; }
        [DataMember(Name = "error_msg")]
        public string ErrorMsg { get; set; }
    }

    [DataContract]
    public class ErrorResponse : IServiceResult
    {
        public bool ResponseIsSuccess()
        {
            return ErrorResult != null;
        }

        public string Error { get; set; }

        [DataMember(Name = "error")]
        public Error ErrorResult { get; set; }
    }
}
