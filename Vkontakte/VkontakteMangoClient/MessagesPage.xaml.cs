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
using VkontakteViewModel.ItemsViewModel;

namespace VkontakteMangoClient
{
    public partial class MessagesPage : PhoneApplicationPage
    {
        public MessagesPage()
        {
            InitializeComponent();
        }

        public MessagesPageViewModel ViewModel
        {
            get { return this.DataContext as MessagesPageViewModel; }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.OnNavigatedTo(this,e);
        }

        private void ApplicationBarMenuItemPinToStart_Click(object sender, EventArgs e)
        {
            ViewModel.PinToStart();
        }

        private void ApplicationBarIconButtonHome_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToHome();
        }

        private void ApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            ViewModel.RefreshMessages();
        }

        private void ListBoxMessages_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ViewModel.SelectedItem = (MessageViewModel) e.AddedItems[0];
                ((ListBox) sender).SelectedIndex = -1;
            }
        }
    }
}