using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using AudioPlaybackAgent;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using Microsoft.Phone.BackgroundAudio;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services;
using ScaryStories.Services.Base;
using ScaryStories.Visual.Extensions;

namespace ScaryStories.Visual.ViewModels
{
    public class SingleStoryViewModel:BaseScreen
    {
        private readonly IRepositoriesStore _repositoriesStore;
        private readonly INavigationService _navigationService;
        private readonly SpeechSynthesizerService _speechSynthesizer;
        private Visibility _navigationBarVisibility;
        private StoryDto _currentStory;
        public int CurrentStoryId { get; set; }
        public string StringIds { get; set; }
        private List<int> _ids;
       

        public SingleStoryViewModel(IRepositoriesStore repositoriesStore,INavigationService navigationService)
        {
            _repositoriesStore = repositoriesStore;
            _navigationService = navigationService;
            _speechSynthesizer = new SpeechSynthesizerService();
            
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
            Wait(true);
            if (StringIds != null)
            _ids = StringExtensions.DeserializeListOfIdsToString(StringIds);
            GetStory(CurrentStoryId);
            Wait(false);
            base.OnInitialize();
        }

        private async void GetStory(int storyId)
        {
            Wait(true);
            CurrentStory=await _repositoriesStore.StoryRepository.GetItem(storyId);
            await _repositoriesStore.HistoryViewRepository.Insert(new HistoryViewDto()
            {
                StoryId = CurrentStory.Id,
                ViewTime = DateTime.Now
            });
            Wait(false);
        }

        public async void PrevioslyStory()
        {
            var currentIndex = _ids.IndexOf(CurrentStory.Id);
            var previosly = _ids[--currentIndex];
            GetStory(previosly);
            CurrentStoryId = CurrentStory.Id;
            base.NotifyOfPropertyChange(() => CanNextStory);
            base.NotifyOfPropertyChange(() => CanPrevioslyStory);
            base.NotifyOfPropertyChange(() => CanToFavorite);
        }

        public void SpeechStart()
        {
            _speechSynthesizer.OnSaveToIsolatedCompleted += OnCompleted;
            _speechSynthesizer.Speak(CurrentStory.Text);
        }

        private void OnCompleted(string obj)
        {
            AudioPlayer.PlayList.Add(new AudioTrack(new Uri("temp5", UriKind.Relative), "Lullabies", "Bob Acri", "Bob Acri", null));
            BackgroundAudioPlayer.Instance.Play();
        }

        public bool CanPrevioslyStory
        {
            get
            {
                if (_ids == null)
                {
                    NavigationBarVisibility = Visibility.Collapsed;
                    return false;
                }
                NavigationBarVisibility = Visibility.Visible;
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
            CurrentStory.IsFavorite = true;
            _repositoriesStore.StoryRepository.AddToFavorites(CurrentStory);
            MessageBox.Show("История добавлена в избранное");
        }

        public bool CanToFavorite
        {
            get
            {
                if (CurrentStory.IsFavorite)
                {
                    return false;
                }
                return true;
            }
        }

        public async void NextStory()
        {
            var currentIndex = _ids.IndexOf(CurrentStory.Id);
            var nextStory = currentIndex + 1;
            if (nextStory < _ids.Count)
            {
                var nextIndex = _ids[nextStory];
                GetStory(nextIndex);
                CurrentStoryId = CurrentStory.Id;
            }
            base.NotifyOfPropertyChange(()=>CanNextStory);
            base.NotifyOfPropertyChange(() => CanPrevioslyStory);
            base.NotifyOfPropertyChange(() => CanToFavorite);
        }

        public bool CanNextStory
        {
            get
            {
                if (_ids == null)
                {
                    NavigationBarVisibility = Visibility.Collapsed;
                    return false;
                }
                NavigationBarVisibility = Visibility.Visible;
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

        public Visibility NavigationBarVisibility
        {
            get { return _navigationBarVisibility; }
            set
            {
                _navigationBarVisibility = value;
                base.NotifyOfPropertyChange(()=>NavigationBarVisibility);
            }
        }
    }
}
