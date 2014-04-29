using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using VkontakteViewModel;
using VkontakteViewModel.Helper;
using VkontakteMangoClient.Helpers;
using GestureEventArgs = Microsoft.Phone.Controls.GestureEventArgs;

namespace VkontakteMangoClient
{
    public partial class PhotosViewPage : PhoneApplicationPage
    {
        public PhotosViewPage()
        {
            InitializeComponent();
            this.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(PhotosViewPage_OrientationChanged);
        }

        void PhotosViewPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            PhotosViewPageViewModel.ChangeOrientation();
            AppBarVisibilityValidate();
        }

        public PhotosViewPageViewModel PhotosViewPageViewModel
        {
            get { return this.DataContext as PhotosViewPageViewModel; }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            PhotosViewPageViewModel.OnNavigatedTo(this,e);
            PhotosViewPageViewModel.ChangeOrientation();

            AppBarVisibilityValidate();
        }

        private void AppBarVisibilityValidate()
        {
            ApplicationBar.IsVisible = PhotosViewPageViewModel.IsPortraitOrientation;
        }

        private static bool flickProgress=false;
        private FlickerArg flickEventArgs;
        private void Gl_OnFlick(object sender, FlickGestureEventArgs e)
        {
            if (!flickProgress)
            {
                flickEventArgs = FlickerExtension.GetFlickerArg(e);
                if (PhotosViewPageViewModel.CanBeNavigated(flickEventArgs))
                {
                    flickProgress = true;
                    imageAnimation.Visibility = Visibility.Visible;
                    Canvas.SetZIndex(imageAnimation, 1000);
                    PhotosViewPageViewModel.SetNextPhotoThumb(flickEventArgs);

                    AnimationStartKeyValue.Value = Application.Current.Host.Content.ActualWidth;
                    if (e.HorizontalVelocity > 0)
                    {
                        AnimationStartKeyValue.Value = -AnimationStartKeyValue.Value;
                    }
                    AnumationEndKeyValue.Value = 0;
                    Storyboard1.Begin();
                }
            }
        }


        private void Storyboard1_OnCompleted(object sender, EventArgs e)
        {
            //imageAnimation.Visibility = Visibility.Collapsed;
            Canvas.SetZIndex(imageAnimation, 1);
            Canvas.SetZIndex(image, 1000);
            Canvas.SetZIndex(listBoxImagePreview, 1001);
            PhotosViewPageViewModel.Flcik(flickEventArgs);
            flickProgress = false;
        }

        private void ApplicationBarIconButtonHome_OnClick(object sender, EventArgs e)
        {
            PhotosViewPageViewModel.GoToHomePage();
        }

        private void ApplicationBarMenuItemPinToStart_Click(object sender, EventArgs e)
        {
            PhotosViewPageViewModel.PinToStart();
        }
    }
}