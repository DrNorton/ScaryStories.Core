using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;

namespace ScaryStories.Visual.DataContext.ItemsViewModels
{
    public class FavoriteStoryContainerContext:StoryContainerContext
    {
        private List<StoryDto> _favoriteStories;
        private StoryDto _selectedFavoriteStory;

        public FavoriteStoryContainerContext(StoryRepository storyRepository,CategoryRepository categoryRepository)
            :base(storyRepository,categoryRepository){
            GetFavoriteStories();
        }

        private void GetFavoriteStories()
        {
            FavoriteStories = base.StoryRepository.GetFavoriteStories().ToList();
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
    }
}
