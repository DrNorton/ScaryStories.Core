using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using ScaryStories.Visual.Controls;
using ScaryStories.Visual.Extensions;

namespace ScaryStories.Visual.ViewModels
{
    public class SingleStoryViewModel:BaseScreen
    {
        private readonly IRepositoriesStore _repositoriesStore;
        private readonly INavigationService _navigationService;
        private readonly ISpeechSynthesizerService _speechSynthesizer;
        private Visibility _navigationBarVisibility;
        private StoryDto _currentStory;
        public int CurrentStoryId { get; set; }
        public string StringIds { get; set; }
        private List<int> _ids;

        private UcProgress _progress=new UcProgress();

  



        public SingleStoryViewModel(IRepositoriesStore repositoriesStore, INavigationService navigationService, ISpeechSynthesizerService speechSynthesizer)
        {
            _currentStory=new StoryDto();
            _currentStory.Text = "";
            _repositoriesStore = repositoriesStore;
            _navigationService = navigationService;
            _speechSynthesizer = speechSynthesizer;
            _speechSynthesizer.OnEndLoading += OnGetTrackCompleted;
            _speechSynthesizer.OnStartLoading += StartSpeechLoading;
            _speechSynthesizer.ReportProgressLoading += ReportProgressLoading;
            AddCustomConventions();
        }

        private void StartSpeechLoading(int obj)
        {
            IsLoading = true;
            base.NotifyOfPropertyChange(() => CanNextStory);
            base.NotifyOfPropertyChange(() => CanPrevioslyStory);
            base.NotifyOfPropertyChange(() => CanToFavorite);
            _progress.IsOpen = true;
            _progress.Text = "Начинаем загрузку..";
            _progress.Minimum = 0;
            _progress.Maximum = obj;
            _progress.Value = 0;
        }

        private void ReportProgressLoading(int obj)
        {
            _progress.Value = obj;
            _progress.Text = String.Format("Загружено {0} из {1}", _progress.Value,_progress.Maximum);
        }

        static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<BindableAppBarButton>(
            Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
            Control.IsEnabledProperty, "DataContext", "Click");
        }

        protected async override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            Wait(true);
            if (StringIds != null)
                _ids = StringExtensions.DeserializeListOfIdsToString(StringIds);
            await GetStory(CurrentStoryId);
            Wait(false);
            base.NotifyOfPropertyChange(() => CanNextStory);
            base.NotifyOfPropertyChange(() => CanPrevioslyStory);
            base.NotifyOfPropertyChange(() => CanToFavorite);
            base.OnInitialize();
        }


        private async Task GetStory(int storyId)
        {
           
            CurrentStory=await _repositoriesStore.StoryRepository.GetItem(storyId);
             _repositoriesStore.HistoryViewRepository.Insert(new HistoryViewDto()
            {
                StoryId = CurrentStory.Id,
                ViewTime = DateTime.Now
            });
   
        }

        public async void PrevioslyStory()
        {
            Wait(true);
            var currentIndex = _ids.IndexOf(CurrentStory.Id);
            var previosly = _ids[--currentIndex];
            GetStory(previosly);
            CurrentStoryId = CurrentStory.Id;
            Wait(false);
            base.NotifyOfPropertyChange(() => CanNextStory);
            base.NotifyOfPropertyChange(() => CanPrevioslyStory);
            base.NotifyOfPropertyChange(() => CanToFavorite);
         
        }

        public void SpeechStart()
        {
        
            _speechSynthesizer.Speak(CurrentStory.Text);
        }

        private void OnGetTrackCompleted()
        {
            IsLoading = false;
            base.NotifyOfPropertyChange(() => CanNextStory);
            base.NotifyOfPropertyChange(() => CanPrevioslyStory);
            base.NotifyOfPropertyChange(() => CanToFavorite);
            _progress.IsOpen = false;
            BackgroundAudioPlayer.Instance.Play();
        }

        public bool CanPrevioslyStory
        {
            get
            {
                if (!AppBarEnabled) return false;
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
                if (!AppBarEnabled) return false;
                if (CurrentStory != null)
                {
                    if (CurrentStory.IsFavorite)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public async void NextStory()
        {
            Wait(true);
            var currentIndex = _ids.IndexOf(CurrentStory.Id);
            var nextStory = currentIndex + 1;
            if (nextStory < _ids.Count)
            {
                var nextIndex = _ids[nextStory];
                await GetStory(nextIndex);
                CurrentStoryId = CurrentStory.Id;
            }
            Wait(false);
            base.NotifyOfPropertyChange(() => CanNextStory);
            base.NotifyOfPropertyChange(() => CanPrevioslyStory);
            base.NotifyOfPropertyChange(() => CanToFavorite);
           
        }

        public bool CanNextStory
        {
            get
            {
                if (!AppBarEnabled) return false;
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
