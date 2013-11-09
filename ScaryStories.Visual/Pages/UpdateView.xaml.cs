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
                IContext dataContext = App.ViewModel.DataContexts.FirstOrDefault(x => x.DataContextCode == code);
                this.DataContext = dataContext;
                dataContext.Run();
            }

        }
    }
}