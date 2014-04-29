using System;

namespace VkontakteInfrastructure.Model
{
    public class Photo : IEntity
    {
        public string Pid { get; set; }
        public string Aid { get; set; }
        public string OwnerID { get; set; }
        public string Source { get; set; }
        public string SourceBig { get; set; }
        public string SourceSmall { get; set; }
        public string SourceXbig { get; set; }
        public string SourceXxbig { get; set; }
        public DateTime Created { get; set; }
    }
}