using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using ScaryStories.ViewModel.DataContext.Base;

using Test.Controls;

namespace ScaryStories.Visual.Pages
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        private string _code = "";
        private IContext _dataContext;

        public SettingsPage()
        {
            InitializeComponent();
         
        }
       
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.TryGetValue("code", out _code))
            {
                _dataContext = App.ViewModel.DataContexts.FirstOrDefault(x => x.DataContextCode == _code);
                _dataContext.Run();
                this.DataContext = _dataContext;
            }

        }
    }
}