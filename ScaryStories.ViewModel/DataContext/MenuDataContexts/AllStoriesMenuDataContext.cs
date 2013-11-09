using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;
using ScaryStories.ViewModel.DataContext.ItemsViewModels;
using ScaryStories.ViewModel.Extensions;

namespace ScaryStories.ViewModel.DataContext.MenuDataContexts
{
    public class AllStoriesMenuDataContext:StoryContainerContext,IMenuItem {
        private List<AlphaKeyGroup<StoryDto>> _groupedStories;

        public AllStoriesMenuDataContext(RepositoriesStore store,VkService vkService)
            :base(store,vkService){
           
        }

        private void LoadAllStories() {
            this.Stories = base.StoryRepository.GetAll().ToList();
            GroupedStories = AlphaKeyGroup<StoryDto>.CreateGroups(
                this.Stories,
                CultureInfo.CurrentCulture,
                (StoryDto s) => { return s.Name.ElementAt(0).ToString().ToLower(); },
                true);
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

        public List<AlphaKeyGroup<StoryDto>> GroupedStories
        {
            get {

                return _groupedStories;
            }
            set {
                _groupedStories = value;
                base.NotifyPropertyChanged("GroupedStories");
            }
        }

      

        public override void Run()
        {
            if (base.Stories == null) {
                LoadAllStories();
            }
            base.Run();
        }
    }
}
