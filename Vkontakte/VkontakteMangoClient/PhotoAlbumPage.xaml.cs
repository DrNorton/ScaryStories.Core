using System;
using System.Collections.Generic;
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

namespace VkontakteMangoClient
{
    public partial class PhotoAblumPage : PhoneApplicationPage
    {
        public PhotoAblumPage()
        {
            InitializeComponent();
        }

        protected PhotoAlbumPageViewModel ViewModel
        {
            get { return DataContext as PhotoAlbumPageViewModel; }
        }
        
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.OnNavigatedTo(this,e);
        }

        private void ApplicationBarIconButtonHome_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToHomePage();
        }

        private void ApplicationBarMenuItemPinToStart_Click(object sender, EventArgs e)
        {
            ViewModel.PinToStart();
        }
    }
}