using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
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
                _backgroundWorker=new BackgroundWorker();
                _backgroundWorker.DoWork += _backgroundWorker_DoWork;
                _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            GetCategories();
        }

        void _backgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e) {
            base.Stories = (List<StoryDto>)e.Result;
            ProgressBarOff();
        }

        void _backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            e.Result = base.StoryRepository.GetStoriesByCategory(_selectedCategory.Id).ToList();
        }

        private async void GetCategories() {
            Task<List<CategoryDto>> task = Task<List<CategoryDto>>.Factory.StartNew(() => base.CategoryRepository.GetAll().ToList());
            Categories = task.Result;
        }

        private void LoadStoriesForCurrentCategory()
        {
            ProgressBarOn();
            _backgroundWorker.RunWorkerAsync();
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
