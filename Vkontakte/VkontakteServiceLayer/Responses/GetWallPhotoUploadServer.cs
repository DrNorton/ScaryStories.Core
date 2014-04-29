using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using VkontakteInfrastructure.Interfaces;

namespace VkontakteServiceLayer.Responses
{
    [DataContract]
    public class GetWallPhotoUploadServer:IServiceResult {
        [DataMember(Name = "response")]
        public WallUploadServerResult Result { get; set; }

        bool IServiceResult.ResponseIsSuccess() {
            return Result!= null;
        }

        public GetWallPhotoUploadServer() {
                Result=new WallUploadServerResult();
        }
       
    }
    [DataContract]
    public class WallUploadServerResult {
        [DataMember(Name = "upload_url")]
        public string Url { get; set; }
        [DataMember(Name = "aid")]
        public string Aid { get; set; }
        [DataMember(Name = "mid")]
        public string Mid { get; set; }   
    }
}
