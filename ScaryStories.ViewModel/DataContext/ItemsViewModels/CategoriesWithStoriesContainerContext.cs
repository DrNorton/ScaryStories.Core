using System.Collections.Generic;
using System.Linq;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;

namespace ScaryStories.ViewModel.DataContext.ItemsViewModels
{
    public class CategoriesWithStoriesContainerContext:StoryContainerContext {
        private List<CategoryDto> _categories;
        private CategoryDto _selectedCategory;

        public CategoriesWithStoriesContainerContext(StoryRepository storyRepository,CategoryRepository categoryRepository) 
            :base(storyRepository,categoryRepository){
               GetCategories();
        }

        private void GetCategories()
        {
            Categories =base.CategoryRepository.GetAll().ToList();
        }

        private void LoadStoriesForCurrentCategory()
        {
            base.Stories = base.StoryRepository.GetStoriesByCategory(_selectedCategory.Id).ToList();
        }

        public List<CategoryDto> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                _categories = value;
                base.NotifyPropertyChanged("Categories");
            }
        }

        public CategoryDto SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                LoadStoriesForCurrentCategory();
                base.NotifyPropertyChanged("SelectedCategory");
            }
        }



        public override string DataContextCode
        {
            get {
                return "CategoriesWithStoriesContainer";
            }
        }
    }
}
