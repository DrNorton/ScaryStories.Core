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
    public partial class AllStoriesList : PhoneApplicationPage
    {
        private string _code;

        public AllStoriesList()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string code = "";
            if (NavigationContext.QueryString.TryGetValue("code", out code))
            {
                _code = code;
                this.DataContext = App.ViewModel.DataContexts.FirstOrDefault(x => x.DataContextCode == code);

            }

        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = (ListBox)sender;
            NavigationService.Navigate(new Uri(String.Format("{0}?id={1}&code={2}", "/Pages/StoryView.xaml", listbox.SelectedIndex, _code), UriKind.Relative));
        }
    }
}