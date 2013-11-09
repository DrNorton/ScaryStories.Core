using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;

namespace ScaryStories.ViewModel.DataContext.ItemsViewModels
{
    public class FavoriteStoryContainerContext:StoryContainerContext
    {
        private List<StoryDto> _favoriteStories;
        private StoryDto _selectedFavoriteStory;

        public FavoriteStoryContainerContext(RepositoriesStore store,VkService vkService)
            : base(store, vkService)
        {
            GetFavoriteStories();
        }

        private void GetFavoriteStories()
        {
            FavoriteStories = base.StoryRepository.GetFavoriteStories().ToList();
            int ds = 16;
        }

        private void ShowFavoritesDetails()
        {
            Stories = _favoriteStories;
        } 

        public List<StoryDto> FavoriteStories
        {
            get
            {
                return _favoriteStories;
            }
            set
            {
                _favoriteStories = value;
                base.NotifyPropertyChanged("FavoriteStories");
            }
        }

        public StoryDto SelectedFavoriteStory
        {
            get
            {

                return _selectedFavoriteStory;
            }
            set
            {
                ShowFavoritesDetails();
                _selectedFavoriteStory = value;
            }
        }

        public override string DataContextCode
        {
            get {
                return "FavoriteStoryContainer";
            }
        }

        public override void Run()
        {
            if (base.Stories == null) {
                GetFavoriteStories();
            }
        }

     
    }
}
