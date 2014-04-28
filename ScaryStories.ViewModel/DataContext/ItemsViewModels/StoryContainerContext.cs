using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;
using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;
using System.ComponentModel;

namespace ScaryStories.ViewModel.DataContext.ItemsViewModels
{
    public abstract class StoryContainerContext:DatabaseDataContext,IStoryManipulator
    {
        private List<StoryDto> _stories;
        private StoryDto _selectedStory;
        private bool _nextEnabled;
        private VkService _vkService;

        protected BackgroundWorker _backgroundWorker;
        private double _opacity;
        private bool _progressBarIsDeterminate;
        private Visibility _progressBarVisibility;

        private ActionCommand _deleteFromFavoritesCommand;
        private ActionCommand _addToFavoritesCommand;
        private ActionCommand _previoslyStoryCommand;
        private ActionCommand _nextStoryCommand;

        


        public StoryContainerContext(RepositoriesStore store,VkService vkontakteService)
            :base(store) {
            _vkService = vkontakteService;
        }

        public override string DataContextCode
        {
            get {
                return "StoryContainer";
            }
        }

        public List<StoryDto> Stories
        {
            get {
                return _stories;
            }
            set {
                _stories = value;
                base.NotifyPropertyChanged("Stories");
            }
        }

        public StoryDto SelectedStory
        {
            get {
                return _selectedStory;
            }
            set {
                _selectedStory = value;
                SaveStoryToHistoryView();
                base.NotifyPropertyChanged("SelectedStory");
            }
        }


        public void NextStory()
        {
            if (CanNext())
            {
                var currentIndex = Stories.IndexOf(SelectedStory);
                SelectedStory = Stories.ElementAt(currentIndex + 1);
            }
        }

        public void PreviousStory()
        {
            if (CanPrevious())
            {
                var currentIndex = Stories.IndexOf(SelectedStory);
                SelectedStory = Stories.ElementAt(currentIndex - 1);
            }
        }

        public void AddToFavorites()
        {
            base.StoryRepository.AddToFavorites(SelectedStory);
        }

        public void DeleteFromFavorites()
        {
            base.StoryRepository.RemoveFromFavorites(SelectedStory);
        }

        public ActionCommand DeleteFromFavoritesCommand
        {
            get
            {
                if (_deleteFromFavoritesCommand == null)
                {
                    _deleteFromFavoritesCommand = new ActionCommand(DeleteFromFavorites);
                }
                return _deleteFromFavoritesCommand;
            }
        }

        public ActionCommand AddToFavoritesCommand
        {
            get
            {
                if (_addToFavoritesCommand == null)
                {
                    return new ActionCommand(AddToFavorites);
                }
                return _addToFavoritesCommand;
            }
        }

        public ActionCommand NextStoryCommand
        {
            get
            {
                if (_nextStoryCommand == null)
                {
                    _nextStoryCommand = new ActionCommand(NextStory);
                }
                return _nextStoryCommand;
            }
        }

        public ActionCommand PrevioslyStoryCommand
        {
            get
            {
                if (_previoslyStoryCommand == null)
                {
                    _previoslyStoryCommand = new ActionCommand(PreviousStory);
                }
                return _previoslyStoryCommand;
            }
        }

        public override  void Run() {
            if (_vkService.ClientOnAutorization) {
                _vkService.ClientOnAutorization = false;
                ShareVk();

            }
        }

        public void ShareVk() {
            _vkService.SentToWall(SelectedStory.Text);
            
        }

        public bool NextEnabled {
            get {
                return _nextEnabled;
            }
            set {
                _nextEnabled = value;
                base.NotifyPropertyChanged("NextEnabled");
            }
        }

        public bool CanNext()
        {
            var currentIndex = Stories.IndexOf(SelectedStory);
            if (currentIndex + 1 < Stories.Count)
            {
                return true;
            }
            return false;
        }

        public bool CanPrevious()
        {
            var currentIndex = Stories.IndexOf(SelectedStory);
            if (currentIndex != 0) {
                return true;
            }
            return false;

        }

        protected void ProgressBarOn()
        {
            Opacity = 0.1;
            ProgressBarIsDeterminate = true;
            ProgressBarVisibility = Visibility.Visible;
        }

        protected void ProgressBarOff()
        {
            Opacity = 1;
            ProgressBarIsDeterminate = false;
            ProgressBarVisibility = Visibility.Collapsed;
        }

        private void SaveStoryToHistoryView() {
            if (this.HistoryViewRepository != null) {
                this.HistoryViewRepository.Insert(new HistoryViewDto(){StoryId = SelectedStory.Id,ViewTime = DateTime.Now});
            }
        }

        public double Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                _opacity = value;
                base.NotifyPropertyChanged("Opacity");
            }
        }

        public bool ProgressBarIsDeterminate
        {
            get
            {
                return _progressBarIsDeterminate;
            }
            set
            {
                _progressBarIsDeterminate = value;
                base.NotifyPropertyChanged("ProgressBarIsDeterminate");
            }
        }

        public Visibility ProgressBarVisibility
        {
            get
            {
                return _progressBarVisibility;
            }
            set
            {
                _progressBarVisibility = value;
                base.NotifyPropertyChanged("ProgressBarVisibility");
            }
        }
       
    }
}
