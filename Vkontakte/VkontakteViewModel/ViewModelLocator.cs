using VkontakteCore;

namespace VkontakteViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            AuthorizationViewModel=new AuthorizationViewModel();
            FriendsPageViewModel=new FriendsPageViewModel();
            ProfilePageViewModel=new ProfilePageViewModel();
            PhotoPageViewModel=new PhotoAlbumPageViewModel();
            PhotosViewPageViewModel = new PhotosViewPageViewModel();
        }

        private static ViewModelLocator instance;
        public static ViewModelLocator Instance
        {
            get { return instance ?? (instance = new ViewModelLocator()); }
        }

        public AuthorizationViewModel AuthorizationViewModel { get; set; }

        public FriendsPageViewModel FriendsPageViewModel { get; set; }

        public ProfilePageViewModel ProfilePageViewModel { get; set; }

        public PhotoAlbumPageViewModel PhotoPageViewModel { get; set; }

        public PhotosViewPageViewModel PhotosViewPageViewModel { get; set; }  
    }
}