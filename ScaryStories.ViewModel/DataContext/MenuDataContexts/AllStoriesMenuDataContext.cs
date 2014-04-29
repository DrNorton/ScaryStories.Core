using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;
using ScaryStories.ViewModel.DataContext.ItemsViewModels;
using ScaryStories.ViewModel.Extensions;

namespace ScaryStories.ViewModel.DataContext.MenuDataContexts
{
    public class AllStoriesMenuDataContext:StoryContainerContext,IMenuItem {
        private List<AlphaKeyGroup<StoryDto>> _groupedStories=new List<AlphaKeyGroup<StoryDto>>();
      

        public AllStoriesMenuDataContext(RepositoriesStore store,VkService vkService)
            :base(store,vkService){
           _backgroundWorker=new BackgroundWorker();
           _backgroundWorker.DoWork += _backgroundWorker_DoWork;
           _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
        }

        void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            var tuple=(Tuple<List<AlphaKeyGroup<StoryDto>>, List<StoryDto>>)e.Result;
            GroupedStories = tuple.Item1;
            Stories = tuple.Item2;
            ProgressBarOff();
        }

        void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var stories = base.StoryRepository.GetAll().ToList();
            var groupedStories = AlphaKeyGroup<StoryDto>.CreateGroups(
               stories,
               CultureInfo.CurrentCulture,
               (StoryDto s) => { return s.Name.ElementAt(0).ToString().ToLower(); },
               true);
            e.Result = new Tuple<List<AlphaKeyGroup<StoryDto>>, List<StoryDto>>(groupedStories, stories);
        
        }

      
        private void CreateGroupedStoryList() {
            
        }

      

        public string Header
        {
            get {
                return "Все истории";
            }
        }

        public string Description
        {
            get {
                return "Список всех страшных историй";
            }
        }

        public Uri ImagePath
        {
            get {
                return new Uri("/Content/allStories.png", UriKind.Relative);
            }
        }

        public string DataContextCode
        {
            get {
                return "AllStoriesList";
            }
        }

     

        public override void Run()
        {
            if (base.Stories == null) {
                ProgressBarOn();
                _backgroundWorker.RunWorkerAsync();
            }
            base.Run();
        }
    }
}
