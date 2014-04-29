using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories;
using ScaryStories.Visual.Extensions;

namespace ScaryStories.Visual.ViewModels
{
    public class SingleStoryViewModel:Screen
    {
        private readonly IRepositoriesStore _repositoriesStore;
        private readonly INavigationService _navigationService;
        private StoryDto _currentStory;
        public int CurrentStoryId { get; set; }
        public string StringIds { get; set; }
        private List<int> _ids;
       

        public SingleStoryViewModel(IRepositoriesStore repositoriesStore,INavigationService navigationService)
        {
            _repositoriesStore = repositoriesStore;
            _navigationService = navigationService;
            AddCustomConventions();
        }

        static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<BindableAppBarButton>(
            Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
            Control.IsEnabledProperty, "DataContext", "Click");
        }

        protected override void OnInitialize()
        {
            _ids = StringExtensions.DeserializeListOfIdsToString(StringIds);
            GetStory(CurrentStoryId);
            base.OnInitialize();
        }

        private void GetStory(int storyId)
        {
            CurrentStory=_repositoriesStore.StoryRepository.GetItem(storyId);
        }

        public void PrevioslyStory()
        {
            var currentIndex = _ids.IndexOf(CurrentStory.Id);
            var previosly = _ids[--currentIndex];
            CurrentStory = _repositoriesStore.StoryRepository.GetItem(previosly);
            CurrentStoryId = CurrentStory.Id;
            base.NotifyOfPropertyChange(() => CanNextStory);
            base.NotifyOfPropertyChange(() => CanPrevioslyStory);
        }

        public bool CanPrevioslyStory
        {
            get
            {
                var currentIndex = _ids.IndexOf(CurrentStoryId);
                if (currentIndex-1 > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public void ToFavorite()
        {
            _repositoriesStore.StoryRepository.AddToFavorites(CurrentStory);
        }

        public void NextStory()
        {
            var currentIndex = _ids.IndexOf(CurrentStory.Id);
            var nextStory = currentIndex + 1;
            if (nextStory < _ids.Count)
            {
                var nextIndex = _ids[nextStory];
                CurrentStory = _repositoriesStore.StoryRepository.GetItem(nextIndex);
                CurrentStoryId = CurrentStory.Id;
            }
            base.NotifyOfPropertyChange(()=>CanNextStory);
            base.NotifyOfPropertyChange(() => CanPrevioslyStory);
        }

        public bool CanNextStory
        {
            get
            {
                var currentIndex = _ids.IndexOf(CurrentStoryId);
                if (_ids.Count() > currentIndex+1)
                {
                    return true;
                }
                return false;
            }
        }

        public StoryDto CurrentStory
        {
            get { return _currentStory; }
            set
            {
                _currentStory = value;
                base.NotifyOfPropertyChange(()=>CurrentStory);
            }
        }
    }
}
