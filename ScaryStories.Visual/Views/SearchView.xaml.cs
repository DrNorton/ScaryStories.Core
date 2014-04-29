using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ScaryStories.Visual.ViewModels;

namespace ScaryStories.Visual.Views
{
    public partial class SearchView : PhoneApplicationPage
    {
        public SearchView()
        {
            InitializeComponent();
        }

        private void UIElement_OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
                Search();
            }
        }

        private void Search()
        {
            var context = this.DataContext as SearchViewModel;
            context.Search();
        }



        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox)sender;
            BindingExpression be = box.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
        }

        private void PhoneTextBox_OnActionIconTapped(object sender, EventArgs e)
        {
            Search();
        }
    }
}