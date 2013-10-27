using System;
using System.Linq;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

using ScaryStories.Entities.Dto;
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

            string msg = "";
            int id=0;

            if (NavigationContext.QueryString.TryGetValue("id", out msg)) {
               id = int.Parse(msg);
             
            }
            string code = "";
            if (NavigationContext.QueryString.TryGetValue("code", out code))
            {
                _context = (IStoryManipulator)App.ViewModel.DataContexts.FirstOrDefault(x => x.DataContextCode == code);
                _context.OnCurrentStoryChanged += context_OnCurrentStoryChanged;
                this.DataContext = _context;
                LayoutRoot.DataContext = _context.Stories[id];
                SetButtonsTextAndIcon(_context.Stories[id].IsFavorite);
                SetButtonEnabled(_context);
            }
            

        }

        void context_OnCurrentStoryChanged()
        {
            SetButtonsTextAndIcon(_context.SelectedStory.IsFavorite);
            SetButtonEnabled(_context);
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e) {
          

        }

        private void SetButtonEnabled(IStoryManipulator context) {
            if (!context.CanPrevious()) {
                ApplicationBarIconButton button = GetAppBarIconButton("previosly");
                button.IsEnabled = false;
            }
            if (!context.CanNext())
            {
                ApplicationBarIconButton button = GetAppBarIconButton("next");
                button.IsEnabled = false;
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
    }
}