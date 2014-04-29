using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;

namespace ScaryStories.Visual.ViewModels
{
    public class SearchViewModel:Screen
    {
        private readonly IRepositoriesStore _store;
        private readonly INavigationService _navigationService;
        private Visibility _progressBarVisibility = Visibility.Collapsed;
        private ActionCommand _doFindCommand;
        private string _searchPattern = "";
        private BackgroundWorker _searchWorker;
        private List<StoryDto> _stories;
        private StoryDto _selectedStory;

        public SearchViewModel(IRepositoriesStore store,INavigationService navigationService)
        {
            _store = store;
            _navigationService = navigationService;
            _searchWorker = new BackgroundWorker();
            _searchWorker.DoWork += new DoWorkEventHandler(_searchWorker_DoWork);
            _searchWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(_searchWorker_Completed);
            
        }

        public void Search()
        {
            ProgressBarVisibility = Visibility.Visible;
            _searchWorker.RunWorkerAsync();

        }

        private void _searchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result =_store.StoryRepository.Search(_searchPattern).ToList();
        }

        private void _searchWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBarVisibility = Visibility.Collapsed;
            this.Stories = (List<StoryDto>)e.Result;
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
                base.NotifyOfPropertyChange(()=>ProgressBarVisibility);
            }
        }

        public string SearchPattern
        {
            get
            {
                return _searchPattern;
            }
            set
            {
                _searchPattern = value;
                base.NotifyOfPropertyChange(()=>SearchPattern);
            }
        }

        public List<StoryDto> Stories
        {
            get { return _stories; }
            set
            {
                _stories = value;
                base.NotifyOfPropertyChange(()=>Stories);
            }
        }

        public StoryDto SelectedStory
        {
            get { return _selectedStory; }
            set
            {
                _selectedStory = value;
                if (value != null)
                {
                    NavigateToViewStory(value.Id);
                }
               
                base.NotifyOfPropertyChange(()=>SelectedStory);
            }
        }

        private void NavigateToViewStory(int id)
        {
            _navigationService.UriFor<SingleStoryViewModel>().WithParam(x=>x.CurrentStoryId,id).Navigate();
        }
    }
}
