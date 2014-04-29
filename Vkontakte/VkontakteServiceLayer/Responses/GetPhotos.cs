using System.Runtime.Serialization;
using Newtonsoft.Json;
using VkontakteInfrastructure.Interfaces;

namespace VkontakteServiceLayer.Responses
{
    [DataContract]
    public class Photo
    {
        [DataMember(Name = "pid")]
        public string Pid { get; set; }
        [DataMember(Name = "aid")]
        public string Aid { get; set; }
        [DataMember(Name = "owner_id")]
        public string OwnerID { get; set; }
        [DataMember(Name = "src")]
        public string Source { get; set; }
        [DataMember(Name = "src_big")]
        public string SourceBig { get; set; }
        [DataMember(Name = "src_small")]
        public string SourceSmall { get; set; }
        [DataMember(Name = "src_xbig")]
        public string SourceXbig { get; set; }
        [DataMember(Name = "src_xxbig")]
        public string SourceXxbig { get; set; }
        [DataMember(Name = "created")]
        public string Created { get; set; }
    }

    [DataContract]
    public class GetPhotos : IServiceResult
    {
        [DataMember(Name = "response")]
        public Photo[] Photos { get; set; }

        public bool ResponseIsSuccess()
        {
            return Photos != null;
        }
    }
}