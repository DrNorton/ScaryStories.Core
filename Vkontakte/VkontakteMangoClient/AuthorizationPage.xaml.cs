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
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        public AuthorizationViewModel ViewModel
        {
            get { return this.DataContext as AuthorizationViewModel; }
        }
        

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = ViewModelLocator.Instance.AuthorizationViewModel;
            
            if (ViewModel.IsAuthorized)
            {
                ViewModel.RestoreContext();
                NavigationService.GoBack();
            }
            else
            {
                ApplicationBar.IsVisible = true;
                var uri = ViewModel.GetUri();
                webBrowser1.Navigated += webBrowser1_Navigated;
                webBrowser1.Navigate(uri);
            }
            
        }

        void webBrowser1_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            ViewModel.ParseUri(e.Uri);

            
        }

        private void ApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            var uri = ViewModel.GetUri();
            webBrowser1.Navigate(uri);
        }
    }
}