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
using Microsoft.Phone.Shell;
using VkontakteViewModel;
using VkontakteViewModel.ItemsViewModel;

namespace VkontakteMangoClient
{
    public partial class MessageConversation : PhoneApplicationPage
    {
        public MessageConversation()
        {
            InitializeComponent();

            OrientationChanged += new EventHandler<OrientationChangedEventArgs>(MessageConversation_OrientationChanged);
        }

        void MessageConversation_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            ViewModel.OrientationChanged(e);
        }

        public MessageConversationPageViewModel ViewModel { get { return this.DataContext as MessageConversationPageViewModel; }}

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.OnNavigatedTo(this,e);
            ViewModel.UpdateCompleteAction = UpdateComplete;
            ViewModel.NewMessageTextUpdateAction = NewMessageTextUpdateAction;
        }

        protected void NewMessageTextUpdateAction(string newText)
        {
            this.Dispatcher.BeginInvoke(() =>
            {

                (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = newText.Length > 0;
            });
        }

        protected void UpdateComplete()
        {
            if (ListBoxMessages.Items.Count > 0)
            {
                ListBoxMessages.UpdateLayout();
                ListBoxMessages.ScrollIntoView(ListBoxMessages.Items.Last());
            }
        }


        private void ApplicationBarIconButtonHome_OnClick(object sender, EventArgs e)
        {
            ViewModel.GoToHome();
        }

        private void ApplicationBarMenuItemPinToStart_Click(object sender, EventArgs e)
        {
            ViewModel.PinToStart();
        }

        private void ApplicationBarIconButton_OnClick(object sender, EventArgs e)
        {
            ViewModel.Refresh();
        }

        private void ApplicationBarIconButtonAddMessage_OnClick(object sender, EventArgs e)
        {
            ViewModel.SendMessage();
        }


        private void ListBoxMessages_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                ((ListBox) sender).SelectedIndex = -1;
            }
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = (sender as PhoneTextBox);
            (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).IsEnabled = textBox.Text.Length > 0;
        }
    }
}