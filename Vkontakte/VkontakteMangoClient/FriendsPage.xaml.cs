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
using Microsoft.Phone.Shell;
using VkontakteViewModel;
using VkontakteViewModel.ItemsViewModel;

namespace VkontakteMangoClient
{
    public partial class FriendsPage : PhoneApplicationPage
    {
        public FriendsPage()
        {
            InitializeComponent();

            this.DataContext = ViewModelLocator.Instance.FriendsPageViewModel;
        }

        public FriendsPageViewModel ViewModel
        {
            get { return this.DataContext as FriendsPageViewModel; }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            (DataContext as BaseViewModel).OnNavigatedTo(this, e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            (DataContext as BaseViewModel).OnNavigatedFrom(this, e);
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ViewModel.SelectFriend(e.AddedItems[0] as UserViewModel);
   
                
            }
        }
    }
}