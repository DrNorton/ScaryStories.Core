using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using VkontakteInfrastructure.Interfaces;

namespace VkontakteServiceLayer.Responses
{
    [DataContract]
    public class WallPost : IServiceResult
    {
        [DataMember(Name = "response")]
        public WallPostResults Result { get; set; }

        public bool ResponseIsSuccess() {
            return Result.Id != "";
        }

        public WallPost() {
            Result=new WallPostResults();
        }
    }

    [DataContract]
    public class WallPostResults {
        [DataMember(Name = "post_id")]
        public string Id { get; set; }
    }
}
