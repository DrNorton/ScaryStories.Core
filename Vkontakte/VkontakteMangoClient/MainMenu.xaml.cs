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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace VkontakteMangoClient
{
    public partial class MainMenu : PhoneApplicationPage
    {
        public MainMenu()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainMenu_Loaded);
        }

        

        void MainMenu_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(e.NavigationMode==NavigationMode.New)
            {
                NavigationService.Navigate(new Uri("/HomePage.xaml", UriKind.Relative));    
            }
            else
            {
                //NavigationService.GoBack();
                
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}