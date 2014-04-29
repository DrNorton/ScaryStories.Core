using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using VkontakteCore;
using VkontakteInfrastructure.Model;
using VkontakteViewModel.Helper;
using VkontakteViewModel.ItemsViewModel;
using VkontakteViewModel.Resources;
using VkontakteViewModel.Services;

namespace VkontakteViewModel
{
    public class PhotosViewPageViewModel :BaseViewModel
    {
        public PhotosViewPageViewModel()
        {
            if (DesignerProperties.IsInDesignTool)
            {
                var photo = new Photo()
                {
                    Created = new DateTime(2000, 10, 10),
                    Pid = "3",
                    Source = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                    SourceBig = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                    SourceSmall = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                    SourceXbig = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                    SourceXxbig = "http://cs615.vkontakte.ru/u457829/a_d81aa14a.jpg",
                };
                Photos = new Collection<PhotoViewModel>()
                {
                    new PhotoViewModel(photo),
                    new PhotoViewModel(photo),
                    new PhotoViewModel(photo),
                    new PhotoViewModel(photo),
                    new PhotoViewModel(photo),
                    new PhotoViewModel(photo),
                    new PhotoViewModel(photo),
                    new PhotoViewModel(photo),
                };

                SelectedPhotoViewModel = Photos.First();
            }
        }

        private IList<PhotoViewModel> photos;
        public IList<PhotoViewModel> Photos
        {
            get { return photos; }
            set
            {
                photos = value; 
                OnPropertyChange("Photos");
            }
        }

        private PhotoViewModel selectedPhotoViewModel;
        public PhotoViewModel SelectedPhotoViewModel
        {
            get { return selectedPhotoViewModel; }
            set { 
                selectedPhotoViewModel = value;
                OnPropertyChange("SelectedPhotoViewModel"); 
            }
        }


        private UserViewModel userViewModel;
        public UserViewModel UserViewModel
        {
            get { return userViewModel; }
            set { userViewModel = value; OnPropertyChange("UserViewModel"); }
        }

        public override void OnNavigatedTo(PhoneApplicationPage page, System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(page, e);

            var uid = GetStateOrUrlParam("uid");
            var aid = GetStateOrUrlParam("aid");
            var selectedPhotoIdParam = GetStateOrUrlParamNullable("selectedPhotoId");
            

            GetService<IVkontakteApi>().GetUserProfile(uid,user=>
            {
                UserViewModel=new UserViewModel(user);
            },
            error=> { });

            GetService<IVkontakteApi>().GetPhotosByAlbum(aid,uid, resultPhotos=>
            {
                Photos = resultPhotos.Select(i => new PhotoViewModel(i)).ToList();
                OnPropertyChange("Photos");

                var photo = Photos.FirstOrDefault();
                if (selectedPhotoIdParam != null)
                {
                    photo = Photos.FirstOrDefault(i => i.Pid == selectedPhotoIdParam);
                }

                SelectedPhotoViewModel = photo??Photos.FirstOrDefault();

                
            }, 
            error=>
            {
                GetService<ICommonErrorHandler>().HandleError(error);
            });
        }


        public void ChangeOrientation()
        {
            previewListBoxVisible = IsPortraitOrientation;
            AppBarVisible = IsPortraitOrientation;
            OnPropertyChange("PreviewListBoxVisible");
        }

        private bool previewListBoxVisible=true;
        public Visibility PreviewListBoxVisible
        {
            get { return previewListBoxVisible?Visibility.Visible:Visibility.Collapsed; }
            set { previewListBoxVisible = value == Visibility.Visible; OnPropertyChange("PreviewListBoxVisible"); }
        }


        public void Flcik(FlickerArg e)
        {
            var nextPhoto = GetNextPhoto(e);
            if (nextPhoto != null)
            {
                SelectedPhotoViewModel = nextPhoto;
            }
        }

        private string nextPhotoThumbSource;
        public string NextPhotoThumbSource
        {
            get { return nextPhotoThumbSource; }
            set { nextPhotoThumbSource = value; OnPropertyChange("NextPhotoThumbSource"); }
        }

        private bool appBarVisible;
        public bool AppBarVisible
        {
            get { return appBarVisible; }
            set { appBarVisible = value; OnPropertyChange("AppBarVisible"); }
        }

        public bool CanBeNavigated(FlickerArg flickEventArgs)
        {
            if (Math.Abs(flickEventArgs.HorizontalVelocity) < Math.Abs(flickEventArgs.VerticalVelocity))
            {
                return false;
            }

            var index = GetIndexOfSelectedPhoto();
            if (flickEventArgs.HorizontalVelocity > 0 && index<1)
            {
                return false;
            }

            if (flickEventArgs.HorizontalVelocity < 0 && index >= (Photos.Count-1))
            {
                return false;
            }

            return true;
            //var index = GetIndexOfSelectedPhoto();
            //if (index > 0 && Photos.Count > 0)
            //{
            //    return Photos[index - 1];
            //}
        }

        public void SetNextPhotoThumb(FlickerArg e)
        {
            var nextPhoto = GetNextPhoto(e);
            if (nextPhoto != null)
            {
                NextPhotoThumbSource = nextPhoto.Source;
            }
        }

        private PhotoViewModel GetNextPhoto(FlickerArg e)
        {
            if (e.Direction == System.Windows.Controls.Orientation.Horizontal)
            {
                if (e.HorizontalVelocity < 0)
                {
                    return ShowNextPhoto();
                }
                else
                {
                    return ShowPreviousPhoto();
                }
            }
            return null;
        }

        private PhotoViewModel ShowPreviousPhoto()
        {
            var index = GetIndexOfSelectedPhoto();
            if (index > 0 && Photos.Count>0)
            {
                return Photos[index - 1];
            }
            return null;
        }

        private PhotoViewModel ShowNextPhoto()
        {
            var index = GetIndexOfSelectedPhoto();
            if (index < (Photos.Count - 1) && Photos.Count > 0)
            {
                return Photos[index + 1];
            }
            return null;
        }


        private int GetIndexOfSelectedPhoto()
        {
            for (int i = 0; i < Photos.Count(); i++)
            {
                if (Photos[i] == SelectedPhotoViewModel)
                {
                    return i;
                }
            }
            return -1;
        }

        public void GoToHomePage()
        {
            GetService<ISimpleNavigationService>().NavigateToHomePage();
        }

        public void PinToStart()
        {
            var uid = GetStateOrUrlParam("uid");
            var aid = GetStateOrUrlParam("aid");
            const string urlTemplate = "/PhotosViewPage.xaml?pinToStart=1&uid={0}&aid={1}&selectedPhotoId={2}";
            var url = String.Format(urlTemplate, uid, aid, SelectedPhotoViewModel.Pid);

            var shellTile = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains(url));
            if (shellTile != null)
            {
                shellTile.Delete();
            }
            var standardTileData = new StandardTileData()
            {
                BackgroundImage = new Uri(SelectedPhotoViewModel.Source, UriKind.Absolute),
                Title = userViewModel.FirstName,

                BackBackgroundImage = new Uri(userViewModel.PhotoMedium, UriKind.Absolute),
                BackContent = userViewModel.LastName,
                BackTitle = AppResource.Photos
            };
            ShellTile.Create(new Uri(url, UriKind.Relative), standardTileData);
        }
    }
}