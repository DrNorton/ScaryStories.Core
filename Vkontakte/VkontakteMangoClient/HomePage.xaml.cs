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
    public partial class MyProfilePage : PhoneApplicationPage
    {
        public MyProfilePage()
        {
            InitializeComponent();

        }

        public HomePageViewModel ViewModel
        {
            get { return this.DataContext as HomePageViewModel; }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.OnNavigatedTo(this, e);
        }

        
    }
}