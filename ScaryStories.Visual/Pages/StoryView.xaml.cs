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
    public partial class StoryView : PhoneApplicationPage
    {
        public StoryView()
        {
            InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("id", out msg)) {
                int id = int.Parse(msg);
                LayoutRoot.DataContext = App.ViewModel.Stories[id];
                SetButtonsTextAndIcon(App.ViewModel.Stories[id].IsFavorite);
            }
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e) {
          

        }

        private void SetButtonsTextAndIcon(bool favorite) {
            ApplicationBarIconButton button=GetAppBarIconButton("add");
            if (button == null) {
                button = GetAppBarIconButton("remove");
            }
            if (favorite)
            {
                button.IconUri = new Uri("/Assets/AppBar/appbar.delete.rest.png", UriKind.Relative);
                button.Text = "remove";
            }
            else
            {
                button.IconUri = new Uri("/Assets/AppBar/appbar.favs.rest.png",UriKind.Relative);
                button.Text = "add";
            }
        }

        private ApplicationBarIconButton GetAppBarIconButton(string name)
        {
            foreach (var b in ApplicationBar.Buttons)
                if (((ApplicationBarIconButton)b).Text == name)
                    return (ApplicationBarIconButton)b;

            return null;
        }
    }
}