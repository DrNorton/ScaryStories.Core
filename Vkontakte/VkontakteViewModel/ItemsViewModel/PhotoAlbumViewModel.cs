using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using VkontakteInfrastructure.Model;

namespace VkontakteViewModel.ItemsViewModel
{
    public class PhotoAlbumViewModel : BaseViewModel
    {
        private readonly PhotoAlbum photoAlbum;

        private readonly PhotoViewModel photoViewModel;
        public PhotoAlbumViewModel(PhotoAlbum photoAlbum, IEnumerable<Photo> photos)
        {
            this.photoAlbum = photoAlbum;


            var photo = photos.FirstOrDefault(i => i.Pid == photoAlbum.ThumbID);
            photoViewModel=new PhotoViewModel(photo);

            //ImageThumb=new ImageViewModel(photoViewModel.SourceSmall);
        }

        //private ImageViewModel imageViewModel;
        //public ImageViewModel ImageThumb
        //{
        //    get { return imageViewModel; }
        //    set { imageViewModel = value; OnPropertyChange("ImageViewModel"); }
        //}


        public string ThumbSource
        {
            get { return photoViewModel.Source; }
        }

        public string AlbumId { get { return photoAlbum.AlbumId;  } }

        public string ThumbID { get { return photoAlbum.ThumbID; } }

        public string OwnerID { get { return photoAlbum.OwnerID; } }

        public string Title { get { return photoAlbum.Title; } }

        public string Description { get { return photoAlbum.Description; } }

        public DateTime Created { get { return photoAlbum.Created; } }

        public DateTime Updated { get { return photoAlbum.Updated; } }

        public string Size { get { return photoAlbum.Size; } }

        public string Privacy { get { return photoAlbum.Privacy; } }
    }
}