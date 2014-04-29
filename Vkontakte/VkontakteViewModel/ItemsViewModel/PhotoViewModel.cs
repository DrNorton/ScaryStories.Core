using System;
using System.Windows.Media.Imaging;
using VkontakteInfrastructure.Model;
using VkontakteViewModel.ItemsViewModel;

namespace VkontakteViewModel
{
    public class PhotoViewModel : BaseViewModel
    {
        private readonly Photo photo;

        public PhotoViewModel(Photo photo)
        {
            this.photo = photo ?? new Photo() ;
        }

        public string SourceMaxSize
        {
            get
            {
                if (SourceXxbig != null) return SourceXxbig;
                if (SourceXbig != null) return SourceXbig;
                if (SourceBig != null) return SourceBig;
                if (SourceSmall != null) return SourceSmall;
                return Source;
            }
        }

        public string Pid { get { return photo.Pid; } }
        public string Aid { get { return photo.Aid; } }

        public string Source { get { return photo.Source; } }
        public string SourceBig { get { return photo.SourceBig; } }
        public string SourceSmall { get { return photo.SourceSmall; } }
        public string SourceXbig { get { return photo.SourceXbig; } }
        public string SourceXxbig { get { return photo.SourceXxbig; } }
        public DateTime Created { get { return photo.Created; } }
    }
}