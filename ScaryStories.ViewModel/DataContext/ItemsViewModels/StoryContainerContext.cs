using System.Collections.Generic;
using System.Linq;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;
using ScaryStories.ViewModel.DataContext.Base;

namespace ScaryStories.ViewModel.DataContext.ItemsViewModels
{
    public abstract class StoryContainerContext:DatabaseDataContext,IStoryManipulator
    {
        
        private List<StoryDto> _stories;
        private StoryDto _selectedStory;
        private bool _nextEnabled;

        private ActionCommand _deleteFromFavoritesCommand;
        private ActionCommand _addToFavoritesCommand;
        private ActionCommand _previoslyStoryCommand;
        private ActionCommand _nextStoryCommand;

        public StoryContainerContext(StoryRepository storyRepository, CategoryRepository categoryRepository)
               :base(storyRepository,categoryRepository){
            
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
                if (OnCurrentStoryChanged != null) {
                    OnCurrentStoryChanged();
                }
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


        public event OnCurrentStoryChangedHanlder OnCurrentStoryChanged;
    }
}
