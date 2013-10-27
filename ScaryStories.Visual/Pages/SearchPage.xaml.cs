using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

using ScaryStories.ViewModel.DataContext.Base;

namespace ScaryStories.Visual.Pages
{
    public partial class SearchPage : PhoneApplicationPage
    {
        private string _code = "";

        public SearchPage()
        {
            InitializeComponent();
        }

        private void UIElement_OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key.Equals(Key.Enter))
            {
               ((ISearch)this.DataContext).Search();
                
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

          
            if (NavigationContext.QueryString.TryGetValue("code", out _code))
            {
                this.DataContext = App.ViewModel.MenuContexts.FirstOrDefault(x => x.DataContextCode == _code);
            }

        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
                TextBox box = (TextBox)sender;
                BindingExpression be = box.GetBindingExpression(TextBox.TextProperty);
                be.UpdateSource();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listbox = (ListBox)sender;
            NavigationService.Navigate(new Uri(String.Format("{0}?id={1}&code={2}", "/Pages/StoryView.xaml", listbox.SelectedIndex, _code), UriKind.Relative));
        }

    }
}