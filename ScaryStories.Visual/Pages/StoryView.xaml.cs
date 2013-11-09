using System;
using System.Linq;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using ScaryStories.Entities.Dto;
using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;

namespace ScaryStories.Visual.Pages
{
    public partial class StoryView : PhoneApplicationPage {
        private IStoryManipulator _context;
        public StoryView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string code = "";
            if (NavigationContext.QueryString.TryGetValue("code", out code))
            {
                IContext context = App.ViewModel.DataContexts.FirstOrDefault(x => x.DataContextCode == code);
                context.Run();
                _context = (IStoryManipulator)context;
                if (_context.SelectedStory == null) {
                    _context.SelectedStory = _context.Stories[0];
                }
                this.DataContext = _context;
                SetButtonsTextAndIcon(_context.SelectedStory.IsFavorite);
                SetButtonEnabled(_context);
            }
        }

        private void SetButtonEnabled(IStoryManipulator context) {
            ApplicationBarIconButton previoslyButton = GetAppBarIconButton("previosly");
            ApplicationBarIconButton nextButton = GetAppBarIconButton("next");
            if (!context.CanPrevious()) {
     
                previoslyButton.IsEnabled = false;
            }
            else {
                ApplicationBarIconButton button = GetAppBarIconButton("previosly");
                previoslyButton.IsEnabled = true;
            }
            if (!context.CanNext()) {
                nextButton.IsEnabled = false;
            }
            else {
                nextButton.IsEnabled = true;
            }
            
        }

        private void SetButtonsTextAndIcon(bool favorite) {
            ApplicationBarIconButton button=GetAppBarIconButton("add");
            if (button == null) {
                button = GetAppBarIconButton("remove");
            }
            if (favorite)
            {
                button.IconUri = new Uri("/Assets/AppBar/appbar.delete.rest.png", UriKind.Relative);
                button.Text = "remove";
            }
            else
            {
                button.IconUri = new Uri("/Assets/AppBar/appbar.favs.rest.png",UriKind.Relative);
                button.Text = "add";
            }
        }

        private ApplicationBarIconButton GetAppBarIconButton(string name)
        {
            foreach (var b in ApplicationBar.Buttons)
                if (((ApplicationBarIconButton)b).Text == name)
                    return (ApplicationBarIconButton)b;

            return null;
        }

        private void ApplicationBarIconButton_OnClick(object sender, EventArgs e) {
            var favorite=((StoryDto)LayoutRoot.DataContext).IsFavorite;
            SetButtonsTextAndIcon(!favorite);
            ApplicationBarIconButton button = GetAppBarIconButton("next");
            button.IsEnabled = false;
        }

        private void SpeechMenuItem_OnClick(object sender, EventArgs e)
        {
            var text = _context.SelectedStory.Text;
            SpeechSynthesizerService service=new SpeechSynthesizerService();
            service.Speak(text);
        }

        private void VkMenuItem_OnClick(object sender, EventArgs e) {
           _context.ShareVk();
        }
    }
}