using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Navigation;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;

namespace ScaryStories.Visual.ViewModels
{
    public class FavoriteStoriesViewModel:BasePropertyChanging {
        private FavoriteStoryRepository _favoriteStoryRepository;
        private List<StoryDto> _stories; 
        private List<FavoriteStoryDto> _favouriteStoriesList;
        private FavoriteStoryDto _selectedFavouriteStory;
        private StoryDto _selectedStory;
        private ActionCommand _deleteFromFavoritesCommand;

        public FavoriteStoriesViewModel(FavoriteStoryRepository favoriteStoryRepository) {
            _favoriteStoryRepository = favoriteStoryRepository;
            GetFavouriteStoriesList();
        }

        public List<FavoriteStoryDto> FavouriteStoriesList {
            get {
                return _favouriteStoriesList;
            }
            set {
                _favouriteStoriesList = value;
            }
        }

        public FavoriteStoryDto SelectedFavouriteStory {
            get {
                return _selectedFavouriteStory;
            }
            set {
                SelectedStory = value.Story;
                _selectedFavouriteStory = value;
                base.NotifyPropertyChanged("SelectedFavouriteStory");
            }
        }

        public StoryDto SelectedStory
        {
            get
            {
                return _selectedStory;
            }
            set
            {
                _selectedStory = value;
                _selectedFavouriteStory = _favouriteStoriesList.FirstOrDefault(x => x.Story.Equals(value));
                base.NotifyPropertyChanged("SelectedStory");
            }
        }

        public List<StoryDto> Stories {
            get {
                if (_stories == null) {
                    _stories = _favouriteStoriesList.Select(x => x.Story).ToList();
                }
                return _stories;
            }
        }

        public ActionCommand DeleteFromFavoritesCommand {
            get {
                if (_deleteFromFavoritesCommand == null) {
                    _deleteFromFavoritesCommand = new ActionCommand(DeleteFromFavorites);
                }
                return _deleteFromFavoritesCommand;
            }
        }

        private void GetFavouriteStoriesList() {
           FavouriteStoriesList=_favoriteStoryRepository.GetAll().ToList();
        }

        private void DeleteFromFavorites() {
            _favoriteStoryRepository.Delete(SelectedFavouriteStory);
            RefreshRepository();
            var nextStory=Stories.FirstOrDefault();
            if (nextStory != null) {
                SelectedStory = nextStory;
            }
        }

        private void RefreshRepository() {
            FavouriteStoriesList = _favoriteStoryRepository.GetAll().ToList();
            _stories = null;
            base.NotifyPropertyChanged("Stories");
        }

    }
}
