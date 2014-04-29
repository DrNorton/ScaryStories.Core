using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Visual.Extensions;

namespace ScaryStories.Visual.ViewModels
{
    public class StoriesListViewModel:Screen
    {
         private readonly IRepositoriesStore _store;
        private readonly INavigationService _navigationService;

       
        private StoryDto _selectedStory;
        private List<StoryDto> _stories; 
        private BackgroundWorker _backgroundWorker;

        public int CategoryId { get; set; }

        public StoriesListViewModel(IRepositoriesStore store, INavigationService navigationService)
        {
            _store = store;
            _navigationService = navigationService;
            _stories=new List<StoryDto>();
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            _backgroundWorker.RunWorkerAsync();
        }

        void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
       {
           Stories = (List<StoryDto>)e.Result;
       }

        void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var stories=new List<StoryDto>();
            if (CategoryId == 0)
            {
               stories = _store.StoryRepository.GetAll().ToList();
            }
            else
            {
                stories = _store.StoryRepository.GetStoriesByCategory(CategoryId).ToList();
            }
            e.Result =stories;
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
                    NavigateToStoryView(value.Id);
                }
             
                base.NotifyOfPropertyChange(()=>SelectedStory);
            }
        }

        private void NavigateToStoryView(int id)
        {
            _navigationService.UriFor<SingleStoryViewModel>().WithParam(x=>x.CurrentStoryId,id).WithParam(x=>x.StringIds,StringExtensions.SerializeListOfIdsToString(Stories.Select(x=>x.Id))).Navigate();
        }
    }
}
