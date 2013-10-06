 using System;
 using System.Collections.Generic;
 using System.Linq;

 using ScaryStories.Entities.Dto;
 using ScaryStories.Entities.Repositories;
 using ScaryStories.Helpers;


namespace ScaryStories.Visual
{
	public class MainViewModel:BasePropertyChanging {
	    private List<CategoryDto> _categories;
        private List<StoryDto> _stories;
	    private List<StoryDto> _favoritesStories; 

	    private CategoryRepository _categoryRepository;
	    private StoryRepository _storyRepository;
	  
        private ActionCommand _deleteFromFavoritesCommand;
	    private ActionCommand _addToFavoritesCommand;
	    private ActionCommand _previoslyStoryCommand;
	    private ActionCommand _nextStoryCommand;

	    private StoryDto _selectedStory;
	    private CategoryDto _selectedCategory;
	    private StoryDto _selectedFavoriteStory;
	    

	    public MainViewModel(string connectionString) {
			_categoryRepository=new CategoryRepository(connectionString);
            _storyRepository=new StoryRepository(connectionString);
	        _categories = _categoryRepository.GetAll().ToList();
            _selectedCategory=new CategoryDto();
            _selectedStory=new StoryDto();
	        _favoritesStories = _storyRepository.GetFavoritesStories().ToList();
            SelectedFavoriteStory=new StoryDto();

	    }

	    private void LoadStoriesForCurrentCategory() {
	        Stories = _storyRepository.GetStoriesByCategory(_selectedCategory.Id).ToList();
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

	    public List<StoryDto> FavoritesStory {
	        get {
	            return _favoritesStories;
	        }
	        set {
	            _favoritesStories = value;
	        }
	    }

	    public StoryDto SelectedFavoriteStory {
	        get {

	            return _selectedFavoriteStory;
	        }
	        set {
	            _selectedFavoriteStory = value;
	        }
	    }
	}
}