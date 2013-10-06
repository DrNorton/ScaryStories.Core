using System;
using System.Collections.Generic;
using System.Linq;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;


namespace ScaryStories.Visual.ViewModels
{
    public class CollectionStoriesViewModel : BasePropertyChanging
    {
		private CategoryDto _category;
		private IQueryable<StoryDto> _storiesQuery;
		private StoryDto _selectedStory;
        private List<StoryDto> _stories;
        private ActionCommand _addToFavoritesCommand;
        private ActionCommand _shareVkCommand;
        private FavoriteStoryRepository _favoriteStoryRepository;
 
		public CategoryDto Category {
				get {
					return _category;
				}
				set {
					_category = value;
				}
			}

		public IQueryable<StoryDto> StoriesQuery {
				get {
					return _storiesQuery;
				}
				set {
					_storiesQuery = value;
				}
			}

		public List<StoryDto> Stories
			{
				get {
				    if (_stories == null) {
				        _stories=StoriesQuery.ToList();
				    }
				    return _stories;
				}
			}

		public StoryDto SelectedStory {
		        get {
		            return _selectedStory;
		        }
		        set {
		            _selectedStory = value;
                    base.NotifyPropertyChanged("SelectedStory");
		        }
		    }

        public ActionCommand AddToFavoritesCommand {
            get {
                if (_addToFavoritesCommand == null) {
                    _addToFavoritesCommand=new ActionCommand(AddToFavorites);
                }
                return _addToFavoritesCommand;
            }
        }

        public ActionCommand ShareVkCommand {
            get {
                if (_shareVkCommand == null) {
                    _shareVkCommand=new ActionCommand(ShareVk);
                }
                return _shareVkCommand;
            }
        }

        public CollectionStoriesViewModel(CategoryDto category, IQueryable<StoryDto> stories,FavoriteStoryRepository favoriteResRepository)
		{
			_category = category;
			_storiesQuery = stories;
            _favoriteStoryRepository = favoriteResRepository;
        }

	    public CollectionStoriesViewModel() {
						
		}

        private void ShareVk() {
           
           
        }

        private void AddToFavorites() {
            _favoriteStoryRepository.Save(new FavoriteStoryDto(){Id=Guid.NewGuid(),Story = SelectedStory});
        }

	}
}
