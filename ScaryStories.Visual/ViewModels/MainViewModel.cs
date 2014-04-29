using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using OpenNETCF.ORM;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;
using ScaryStories.Services;
using ScaryStories.Visual.Extensions;
using ScaryStories.Visual.Models;

namespace ScaryStories.Visual.ViewModels
{
    public class MainViewModel:Screen
    {
        private readonly INavigationService _navigationService;
        private readonly IRepositoriesStore _store;
        private List<CategoryDto> _categories;
        private List<StoryDto> _favoriteStories;
        private List<HistoryViewDto> _historyStories;
        private CategoryDto _selectedCategory;
 
        private SettingTemplate _template;

        private List<MenuItem> _menu; 
      
        public MainViewModel(INavigationService navigationService,IRepositoriesStore store,ISettingsManager settingsManager)
        {
            _navigationService = navigationService;
            _store = store;
            _template = settingsManager.Listener;
            _categories=new List<CategoryDto>();
            _favoriteStories=new List<StoryDto>();
            HistoryStories = new List<HistoryViewDto>();
            ConfigureMenu();
        }

        protected override void OnInitialize()
        {
            GetCategories();
            GetFavoriteStories();
            GetHistoryStories();
            base.OnInitialize();
        }

        private void GetHistoryStories()
        {
            HistoryStories=new List<HistoryViewDto>(_store.HistoryViewRepository.GetAll().ToList());
        }

        private void GetFavoriteStories()
        {
            FavoriteStories = new List<StoryDto>(_store.StoryRepository.GetAll());
        }

        private void GetCategories()
        {
            Categories = new List<CategoryDto>(_store.CategoryRepository.GetAll());
        }

        private void ConfigureMenu()
        {
            _menu=new List<MenuItem>();
            _menu.Add(new MenuItem() { Header = "Все истории", Description = "Список всех страшных историй",ImagePath = new Uri("/Content/allStories.png", UriKind.Relative),NavigateMenuCommand = new ActionCommand(()=> _navigationService.UriFor<GrouppedStoriesListViewModel>().Navigate())});
            _menu.Add(new MenuItem() { Header = "Подборка случайных историй", Description = "10 cлучайных историй", ImagePath = new Uri("/Content/random.png", UriKind.Relative) });
            _menu.Add(new MenuItem() { Header = "Поиск", Description = "Поиск истории", ImagePath = new Uri(@"/Content/search.png", UriKind.Relative),NavigateMenuCommand = new ActionCommand(()=> _navigationService.UriFor<SearchViewModel>().Navigate()) });
            _menu.Add(new MenuItem() { Header = "Настройки", Description = "Настройки программы", ImagePath = new Uri("/Content/settings.png", UriKind.Relative) ,NavigateMenuCommand = new ActionCommand(()=> _navigationService.UriFor<SettingsViewModel>().Navigate())});
            _menu.Add(new MenuItem() { Header = "Обновление", Description = "Обновление", ImagePath = new Uri("/Content/update.png", UriKind.Relative) });

        }

    
        public List<MenuItem> Menu
        {
            get { return _menu; }
            set { _menu = value; }
        }

        public SettingTemplate Template
        {
            get { return _template; }
        }

        public List<CategoryDto> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                base.NotifyOfPropertyChange(()=>Categories);
            }
        }

        public List<StoryDto> FavoriteStories
        {
            get { return _favoriteStories; }
            set
            {
                _favoriteStories = value;
                base.NotifyOfPropertyChange(()=>FavoriteStories);
            }
        }

        public List<HistoryViewDto> HistoryStories
        {
            get { return _historyStories; }
            set
            {
                _historyStories = value;
                base.NotifyOfPropertyChange(()=>HistoryStories);
            }
        }

        public CategoryDto SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                if (value != null)
                {
                    NavigateToCategoryStories(value.Id);
                }
                base.NotifyOfPropertyChange(()=>SelectedCategory);
            }
        }

        private void NavigateToCategoryStories(int id)
        {
           _navigationService.UriFor<StoriesListViewModel>().WithParam(x=>x.CategoryId,id).Navigate();
        }
    }
}
