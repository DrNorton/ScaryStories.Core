using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VkontakteServiceLayer.Responses
{
    public interface IServiceResult
    {
        bool ResponseIsSuccess();
        string Error { get; set; }
    }
}
