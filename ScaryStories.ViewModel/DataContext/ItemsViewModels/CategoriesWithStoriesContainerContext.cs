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
    public class CategoriesWithStoriesContainerContext:StoryContainerContext {
        private List<CategoryDto> _categories;
        private CategoryDto _selectedCategory;

        public CategoriesWithStoriesContainerContext(RepositoriesStore store,VkService vkService) 
            :base(store,vkService) {
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

        public override void Run()
        {
            
        }

     

    }
}
