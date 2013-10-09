 using System;
 using System.Collections.Generic;
 using System.Linq;

 using OpenNETCF.ORM;

 using ScaryStories.Entities.Dto;
 using ScaryStories.Entities.Repositories;
 using ScaryStories.Helpers;
 using ScaryStories.Visual.ViewModels;

namespace ScaryStories.Visual
{
	public class MainViewModel:BasePropertyChanging {
	    private List<CategoryDto> _categories;
        private List<StoryDto> _stories;
	    private List<StoryDto> _favoriteStories; 
        

	    private CategoryRepository _categoryRepository;
	    private StoryRepository _storyRepository;
	  
        private ActionCommand _deleteFromFavoritesCommand;
	    private ActionCommand _addToFavoritesCommand;
	    private ActionCommand _previoslyStoryCommand;
	    private ActionCommand _nextStoryCommand;

	    private StoryDto _selectedStory;
	    private CategoryDto _selectedCategory;
	    private StoryDto _selectedFavoriteStory;

	    private MenuViewModel _menuViewModel;
	    private MenuItem _selectedMenuItem;

	    public MainViewModel(SQLiteDataStore store) {
			_categoryRepository=new CategoryRepository(store);
            _storyRepository=new StoryRepository(store);
            _storyRepository.OnChange += _storyRepository_OnChange;
            _selectedCategory=new CategoryDto();
            _selectedStory=new StoryDto();
            _selectedFavoriteStory=new StoryDto();
            _menuViewModel = new MenuViewModel();
            SelectedMenuItem=new MenuItem();
            GetCategories();
            GetFavoritesStory();
	    }

	    private void GetFavoritesStory() {
            FavoriteStories = _storyRepository.GetFavoritesStories().ToList();
	    }

	    private void GetCategories() {
            Categories = _categoryRepository.GetAll().ToList();
	    }
	    
        void _storyRepository_OnChange(StoryDto story) {
            GetFavoritesStory();
            SelectedStory = story;
          
        }

	    private void LoadStoriesForCurrentCategory() {
	        Stories = _storyRepository.GetStoriesByCategory(_selectedCategory.Id).ToList();
	    }

	    private void ShowFavoritesDetails() {
	        Stories = _favoriteStories;
	    } 

        private void DeleteFromFavorites() {
          
            _storyRepository.RemoveFromFavorites(SelectedStory);
        }

	    private void AddToFavorites() {
            _storyRepository.AddToFavorites(SelectedStory);
	    }

	    private void NextStory() {
	        var currentIndex = _stories.IndexOf(SelectedStory);
	        if (currentIndex+1 < _stories.Count) {
	            SelectedStory = _stories.ElementAt(currentIndex + 1);
	        }
	    }

	    private void PrevioslyStory(){
	        var currentIndex = _stories.IndexOf(SelectedStory);
	        if (currentIndex != 0) {
	            SelectedStory = _stories.ElementAt(currentIndex - 1);
	        }
	    }

	    private bool IsCanPrevios() {
            var currentIndex = _stories.IndexOf(SelectedStory);
            if (currentIndex == 0) {
                return false;
            }
	        return true;
	    }

        public ActionCommand DeleteFromFavoritesCommand
        {
            get
            {
                if (_deleteFromFavoritesCommand == null)
                {
                    _deleteFromFavoritesCommand = new ActionCommand(DeleteFromFavorites);
                }
                return _deleteFromFavoritesCommand;
            }
        }

        public ActionCommand AddToFavoritesCommand
        {
            get
            {
                if (_addToFavoritesCommand == null)
                {
                    return new ActionCommand(AddToFavorites);
                }
                return _addToFavoritesCommand;
            }
        }

        public ActionCommand NextStoryCommand
        {
            get
            {
                if (_nextStoryCommand == null) {
                    _nextStoryCommand=new ActionCommand(NextStory);
                }
                return _nextStoryCommand;
            }
        }

        public ActionCommand PrevioslyStoryCommand
        {
            get
            {
                if (_previoslyStoryCommand == null) {
                    _previoslyStoryCommand=new ActionCommand(PrevioslyStory);
                }
                return _previoslyStoryCommand;
            }
        }


	    public List<CategoryDto> Categories {
	        get {
	            return _categories;
	        }
	        set {
	            _categories = value;
                base.NotifyPropertyChanged("Categories");
	        }
	    }

	    public CategoryDto SelectedCategory {
	        get {
	            return _selectedCategory;
	        }
	        set {
	            _selectedCategory = value;
                LoadStoriesForCurrentCategory();
                base.NotifyPropertyChanged("SelectedCategory");
	        }
	    }

	    public List<StoryDto> Stories {
	        get {
	            return _stories;
	        }
	        set {
	            _stories = value;
                base.NotifyPropertyChanged("Stories");
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

	    public List<StoryDto> FavoriteStories {
	        get {
	            return _favoriteStories;
	        }
	        set {
	            _favoriteStories = value;
                base.NotifyPropertyChanged("FavoriteStories");
	        }
	    }

	    public StoryDto SelectedFavoriteStory {
	        get {

	            return _selectedFavoriteStory;
	        }
	        set {
                ShowFavoritesDetails();
	            _selectedFavoriteStory = value;
	        }
	    }


	    public MenuViewModel MenuViewModel {
	        get {
	            return _menuViewModel;
	        }
	    }

	    public MenuItem SelectedMenuItem {
	        get {
	            return _selectedMenuItem;
	        }
	        set {
	            _selectedMenuItem = value;
                base.NotifyPropertyChanged("SelectedMenuItem");
	        }
	    }
	}
}