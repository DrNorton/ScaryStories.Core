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
    public partial class ProfilePage : PhoneApplicationPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(ProfilePage_Loaded);
        }

        void ProfilePage_Loaded(object sender, RoutedEventArgs e)
        {


        }

        private ProfilePageViewModel ViewModel
        {
            get { return this.DataContext as ProfilePageViewModel; }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.OnNavigatedTo(this, e);
        }

        private void ApplicationBarMenuItemPinToStart_Click(object sender, EventArgs e)
        {
            ViewModel.PinToStart();
        }

        private void ApplicationBarIconButtonHome_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToHome();
        }
    }
}