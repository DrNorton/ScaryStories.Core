using System.Runtime.Serialization;
using VkontakteInfrastructure.Interfaces;

namespace VkontakteServiceLayer.Responses
{
    [DataContract]
    public class GetProfiles : IServiceResult
    {
        [DataMember(Name = "response")]
        public UserServiceItem[] Result { get; set; }

        public bool ResponseIsSuccess()
        {
            return Result != null;
        }

        public string Error { get; set; }
    }
}