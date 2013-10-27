using System;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace ScaryStories.Visual.Pages
{
		public partial class StoriesForSelectedCategoryList : PhoneApplicationPage {
            private string _code = "CategoriesWithStoriesContainer";
				public StoriesForSelectedCategoryList()
				{
						InitializeComponent();
                      
				}

		      protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e) {
                  base.OnNavigatedTo(e);
                  this.DataContext = App.ViewModel.DataContexts.FirstOrDefault(x => x.DataContextCode == _code);
		      }

		    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    var listbox = (ListBox)sender;
                    NavigationService.Navigate(new Uri(String.Format("{0}?id={1}&code={2}", "/Pages/StoryView.xaml", listbox.SelectedIndex,_code), UriKind.Relative));
                }
		}
}