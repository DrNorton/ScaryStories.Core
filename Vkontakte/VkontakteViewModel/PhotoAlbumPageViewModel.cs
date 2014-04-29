using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VkontakteCore;
using VkontakteInfrastructure.Model;
using VkontakteViewModel.ItemsViewModel;
using VkontakteViewModel.Resources;
using VkontakteViewModel.Services;

namespace VkontakteViewModel
{
    public class PhotoAlbumPageViewModel : BaseViewModel
    {
        public PhotoAlbumPageViewModel()
        {
            if (DesignerProperties.IsInDesignTool)
            {
                var user = new User()
                {
                    Uid = "1",
                    FirstName = "Имя",
                    LastName = "Фамилия",
                    Nickname = "Ник",
                    PhotoMedium = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg"
                };
                UserViewModel = new UserViewModel(user);

                var album = new PhotoAlbum()
                {
                    AlbumId = "1",
                    Created = new DateTime(2000, 10, 10),
                    Description = "Description",
                    Title = "Title Title Title Title Title Title Title",
                    OwnerID = user.Uid,
                    ThumbID = "3",
                    Size = "10"
                };

                var list = new List<Photo>()
                {
                    new Photo()
                    {
                        Aid = album.AlbumId,
                        Created = new DateTime(2000, 10, 10),
                        OwnerID = user.Uid,
                        Pid = "3",
                        Source = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                        SourceBig = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                        SourceSmall = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                        SourceXbig = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                        SourceXxbig = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                    }
                };

                PhotoAlbums = new List<PhotoAlbumViewModel>()
                {
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                    new PhotoAlbumViewModel(album, list),
                };
            }
        }

        private UserViewModel userViewModel;
        public UserViewModel UserViewModel
        {
            get { return userViewModel; }
            set { userViewModel = value; OnPropertyChange("UserViewModel"); }
        }

        private List<PhotoAlbumViewModel> photoAlbums;
        public List<PhotoAlbumViewModel> PhotoAlbums
        {
            get { return photoAlbums; }
            set { photoAlbums = value; OnPropertyChange("PhotoAlbums"); }
        }

        private PhotoAlbumViewModel selectedPhotoAlbum;
        public PhotoAlbumViewModel SelectedPhotoAlbum
        {
            get { return selectedPhotoAlbum; }
            set
            {
                selectedPhotoAlbum = value;
                OnPropertyChange("SelectedPhotoAlbum");
                if (value != null)
                {
                    GetService<ISimpleNavigationService>().NavigateToPhotosViewPage(UserViewModel.Uid, value.AlbumId);
                }
            }
        }

        public override void OnNavigatedTo(PhoneApplicationPage page, System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(page, e);

            var uid = GetStateOrUrlParam("uid");

            GetService<IVkontakteApi>().GetUserProfile(uid, GetUserProfileComplete, GetUserProfileError);
        }

        public void GetUserProfileComplete(User user)
        {
            userViewModel = new UserViewModel(user);
            OnPropertyChange("UserViewModel");

            GetService<IVkontakteApi>().GetPhotoAlbums(user.Uid, (albums) =>
            {
                GetService<IVkontakteApi>().GetPhotos(user.Uid,albums.Select(i => i.ThumbID).ToList(), 
                (photos)=>
                {
                    photoAlbums = albums.Select(i => new PhotoAlbumViewModel(i, photos)).ToList();
                    OnPropertyChange("PhotoAlbums");
                },
                (error)=> {GetService<ICommonErrorHandler>().HandleError(error); });
            }, 
            (error) => { GetService<ICommonErrorHandler>().HandleError(error); });
        }

        public void GetUserProfileError(Error error)
        {
            GetService<ICommonErrorHandler>().HandleError(error);
        }

        public void PinToStart()
        {
            var url = "/PhotoAlbumPage.xaml?pinToStart=1&uid=" + userViewModel.Uid;
            var shellTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(url));

            if (shellTile != null)
            {
                shellTile.Delete();
            }

            var standardTileData = new StandardTileData()
            {

                BackgroundImage = new Uri(userViewModel.PhotoMedium, UriKind.Absolute),
                BackBackgroundImage = new Uri(userViewModel.PhotoMedium, UriKind.Absolute),
                Title = userViewModel.FirstName,
                BackContent = userViewModel.LastName,
                BackTitle = AppResource.PhotoAlbums,
                Count = PhotoAlbums.Count
            };
            ShellTile.Create(new Uri(url, UriKind.Relative), standardTileData);
        }

        public void GoToHomePage()
        {
            GetService<ISimpleNavigationService>().NavigateToHomePage();
        }
    }
}