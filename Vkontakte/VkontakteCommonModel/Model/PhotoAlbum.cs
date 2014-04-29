using System;
using System.Runtime.Serialization;

namespace VkontakteInfrastructure.Model
{
    public class PhotoAlbum : IEntity
    {
        public string AlbumId { get; set; }

        public string ThumbID { get; set; }

        public string OwnerID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public string Size { get; set; }

        public string Privacy { get; set; }
    }
}