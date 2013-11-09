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
using ScaryStories.ViewModel.DataContext.MenuDataContexts;
using ScaryStories.Services;

namespace ScaryStories.Visual.Pages
{
    public partial class VkAuthPage : PhoneApplicationPage {
        public VkAuthDataContext ViewModel;

        public VkAuthPage()
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
                ViewModel = (VkAuthDataContext)dataContext;
                this.DataContext = dataContext;
                dataContext.Run();
                if (ViewModel.VkService.IsAuthorized)
                {
                    ViewModel.VkService.RestoreContext();
                    NavigationService.GoBack();
                }
                else {

                    ViewModel.VkService.OnBack += Back;
                    var uri = ViewModel.VkService.GetUri();
                    webBrowser1.Navigated += webBrowser1_Navigated;
                    webBrowser1.Navigate(uri);
                }
            }


        }

        private void Back() {
      
            NavigationService.GoBack();
        }

        private void VkAuthPage_OnLoaded(object sender, RoutedEventArgs e)
         {
          
            
         }

         void webBrowser1_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
         {
             ViewModel.VkService.ParseUri(e.Uri);
        
         }

    }
}