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
    public class StoriesListViewModel:BaseScreen
    {
         private readonly IRepositoriesStore _store;
        private readonly INavigationService _navigationService;

       
        private StoryDto _selectedStory;
        private List<StoryDto> _stories; 
    

        public int CategoryId { get; set; }

        public string StringIds { get; set; }

        public StoriesListViewModel(IRepositoriesStore store, INavigationService navigationService)
        {
            _store = store;
            _navigationService = navigationService;
            _stories=new List<StoryDto>();
           
       
        }

        protected override void OnViewReady(object view)
        {
            Wait(true);
            GetStories();
            Wait(false);
            base.OnViewReady(view);
        }

        private async void GetStories()
        {
            
            var stories = new List<StoryDto>();
            if (!String.IsNullOrEmpty(StringIds))
            {
                var idList = StringExtensions.DeserializeListOfIdsToString(StringIds);
                foreach (var id in idList)
                {
                    stories.Add(await _store.StoryRepository.GetItem(id));
                }
            }
            else
            {
                if (CategoryId == 0)
                {
                    stories = (await _store.StoryRepository.GetAll()).ToList();
                }
                else
                {
                    stories = (await _store.StoryRepository.GetStoriesByCategory(CategoryId)).ToList();
                }
            }
           
            Stories = stories;

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
