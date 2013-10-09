using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ScaryStories.Visual.Pages
{
    public partial class StoriesWithSortingPage : PhoneApplicationPage
    {
        public StoriesWithSortingPage()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = (ListBox)sender;
            NavigationService.Navigate(new Uri(String.Format("{0}?id={1}", "/Pages/StoryView.xaml", listbox.SelectedIndex), UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("id", out msg))
            {
                int id = int.Parse(msg);
                LayoutRoot.DataContext = App.ViewModel.Stories[id];
              
            }
        }
    }
}