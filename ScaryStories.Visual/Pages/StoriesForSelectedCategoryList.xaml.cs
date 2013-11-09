using System;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

using ScaryStories.ViewModel.DataContext.Base;

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
                  IContext dataContext = App.ViewModel.DataContexts.FirstOrDefault(x => x.DataContextCode == _code);
                  this.DataContext = dataContext;
                  dataContext.Run();
                
		      }

		    private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    var listbox = (ListBox)sender;
                    NavigationService.Navigate(new Uri(String.Format("{0}?id={1}&code={2}", "/Pages/StoryView.xaml", listbox.SelectedIndex,_code), UriKind.Relative));
                }
		}
}