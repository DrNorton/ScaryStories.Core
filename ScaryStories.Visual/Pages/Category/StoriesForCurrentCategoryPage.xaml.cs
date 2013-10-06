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

namespace ScaryStories.Visual
{
		public partial class StoriesForCurrentCategoryPage : PhoneApplicationPage
		{
				public StoriesForCurrentCategoryPage()
				{
						InitializeComponent();
						DataContext = App.ViewModel;
				}
                private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    var listbox = (ListBox)sender;
                    NavigationService.Navigate(new Uri(String.Format("{0}?id={1}", "/Pages/StoryView.xaml", listbox.SelectedIndex), UriKind.Relative));
                }
		}
}