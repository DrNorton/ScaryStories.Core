﻿using System;
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
		public partial class MainPage : PhoneApplicationPage
		{
				// Constructor
				public MainPage()
				{
						InitializeComponent();

						// Set the data context of the listbox control to the sample data
						DataContext = App.ViewModel;
						this.Loaded += new RoutedEventHandler(MainPage_Loaded);
				}

				// Load data for the ViewModel Items
				private void MainPage_Loaded(object sender, RoutedEventArgs e)
				{
					
				}

				private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
					var listbox = (ListBox)sender;
					NavigationService.Navigate(
                        new Uri(String.Format("/Pages/Category/StoriesForCurrentCategoryPage.xaml?index={0}", listbox.SelectedIndex), UriKind.Relative));
				}

                private void SelectorFavourites_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
                {
                    var listbox = (ListBox)sender;
                    NavigationService.Navigate(new Uri(String.Format("{0}?id={1}", "/Pages/StoryView.xaml", listbox.SelectedIndex), UriKind.Relative));
                }

		}
}