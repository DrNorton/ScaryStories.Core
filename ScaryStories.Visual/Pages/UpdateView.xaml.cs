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
    public partial class UpdateView : PhoneApplicationPage
    {
        public UpdateView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string code = "";
            if (NavigationContext.QueryString.TryGetValue("code", out code))
            {
                this.DataContext = App.ViewModel.MenuContexts.FirstOrDefault(x => x.DataContextCode == code);
            }

        }
    }
}