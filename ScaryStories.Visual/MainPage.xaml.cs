using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using ScaryStories.ViewModel.DataContext.Base;


namespace ScaryStories.Visual
{
		public partial class MainPage : PhoneApplicationPage
		{
				// Constructor
				public MainPage()
				{
						InitializeComponent();

						// Set the data context of the listbox control to the sample data
						DataContext = App.ViewModel;
				    CategorySection.DataContext =
				        App.ViewModel.DataContexts.FirstOrDefault(x => x.DataContextCode == "CategoriesWithStoriesContainer");
                    FavoriteSection.DataContext = App.ViewModel.DataContexts.FirstOrDefault(x => x.DataContextCode == "FavoriteStoryContainer");
				    MenuSection.DataContext = App.ViewModel;
						this.Loaded += new RoutedEventHandler(MainPage_Loaded);
				}

				// Load data for the ViewModel Items
				private void MainPage_Loaded(object sender, RoutedEventArgs e)
				{
					
				}

                private void Selector_OnCategorySelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    NavigationService.Navigate(
                        new Uri(
                            "/Pages/StoriesForSelectedCategoryList.xaml",
                            UriKind.Relative));
                }

                private void Selector_OnFavoriteSelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    var listbox = (ListBox)sender;
                    NavigationService.Navigate(new Uri(String.Format("{0}?id={1}&code={2}", "/Pages/StoryView.xaml", listbox.SelectedIndex, "FavoriteStoryContainer"), UriKind.Relative));
                }

                private void Selector_OnMenuSelectionChanged(object sender, SelectionChangedEventArgs e) {
                    var listbox = (ListBox)sender;
                    var menuItemCode = ((IMenuItem)(listbox.SelectedValue)).DataContextCode;
                    NavigationService.Navigate(
                        new Uri(String.Format("/Pages/{0}.xaml?code={1}", menuItemCode,menuItemCode), UriKind.Relative));
                            

                 
                }

		}
}