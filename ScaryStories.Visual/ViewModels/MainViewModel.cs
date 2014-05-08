using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using OpenNETCF.ORM;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;
using ScaryStories.Services;
using ScaryStories.Visual.Extensions;
using ScaryStories.Visual.Models;
using Action = System.Action;

namespace ScaryStories.Visual.ViewModels
{
    public class MainViewModel:BaseScreen
    {
        private readonly INavigationService _navigationService;
        private readonly IRepositoriesStore _store;
        private List<CategoryDto> _categories;
        private List<StoryDto> _favoriteStories;
        private List<HistoryViewDto> _historyStories;
        private CategoryDto _selectedCategory;
        private HistoryViewDto _selectedHistoryStory;
        private StoryDto _selectedFavouriteStory;
        private MenuItem _selectedMenuItem;

        private Visibility _categoryLoadTextVisibility=Visibility.Visible;
        private Visibility _historyLoadTextVisibility = Visibility.Visible;
        private Visibility _favoritesLoadTextVisibility = Visibility.Visible;
 
        private SettingTemplate _template;

        private List<MenuItem> _menu;

        public MainViewModel(INavigationService navigationService, IRepositoriesStore store,
            ISettingsManager settingsManager)
        {
            _navigationService = navigationService;
            _store = store;
            _template = settingsManager.Listener;
            _categories = new List<CategoryDto>();
            _favoriteStories = new List<StoryDto>();
            HistoryStories = new List<HistoryViewDto>();
            ConfigureMenu();

        }


       
        protected async override void OnViewReady(object view)
        {
            Wait(true);
            await GetCategories();
            await GetFavoriteStories();
            await GetHistoryStories();
            Wait(false);
            base.OnViewReady(view);
        }

        private async Task GetHistoryStories()
        {
            var allHistory=await _store.HistoryViewRepository.GetAll();
            var historyStories = await AddStoryDtoToHistoryViewDto(allHistory);
            HistoryStories=historyStories.ToList();
            HistoryLoadTextVisibility=Visibility.Collapsed;
        }

        private async Task<IEnumerable<HistoryViewDto>> AddStoryDtoToHistoryViewDto(IEnumerable<HistoryViewDto> history)
        {
            var his = history.ToList();
            foreach (var historyViewDto in his)
            {
                historyViewDto.StoryDto =await _store.StoryRepository.GetItem(historyViewDto.StoryId);
            }
            return his;
        } 

        private async Task GetFavoriteStories()
        {
            FavoriteStories = new List<StoryDto>(await _store.StoryRepository.GetFavoriteStories());
            FavoritesLoadTextVisibility=Visibility.Collapsed;
        }

        private async Task GetCategories()
        {
            Categories = new List<CategoryDto>(await _store.CategoryRepository.GetAll());
            CategoryLoadTextVisibility=Visibility.Collapsed;
        }

        private void ConfigureMenu()
        {
            _menu=new List<MenuItem>();
            _menu.Add(new MenuItem() { Header = "Все истории", Description = "Список всех страшных историй",ImagePath = new Uri("/Content/allStories.png", UriKind.Relative),NavigateMenuCommand = new ActionCommand(()=> _navigationService.UriFor<GrouppedStoriesListViewModel>().Navigate())});
            _menu.Add(new MenuItem() { Header = "Подборка случайных историй", Description = "10 cлучайных историй", ImagePath = new Uri("/Content/random.png", UriKind.Relative),NavigateMenuCommand = new ActionCommand(NavigateToRandomStories) });
            _menu.Add(new MenuItem() { Header = "Поиск", Description = "Поиск истории", ImagePath = new Uri(@"/Content/search.png", UriKind.Relative),NavigateMenuCommand = new ActionCommand(()=> _navigationService.UriFor<SearchViewModel>().Navigate()) });
            _menu.Add(new MenuItem() { Header = "Настройки", Description = "Настройки программы", ImagePath = new Uri("/Content/settings.png", UriKind.Relative) ,NavigateMenuCommand = new ActionCommand(()=> _navigationService.UriFor<SettingsViewModel>().Navigate())});
            _menu.Add(new MenuItem() { Header = "Обновление", Description = "Обновление", ImagePath = new Uri("/Content/update.png", UriKind.Relative), NavigateMenuCommand = new ActionCommand(() => _navigationService.UriFor<UpdateViewModel>().Navigate()) });

        }

        private async void NavigateToRandomStories()
        {
            Wait(true);
            var randomHistories = await _store.StoryRepository.GetRandomStories();
            Wait(false);
            _navigationService.UriFor<StoriesListViewModel>().WithParam(x => x.StringIds, StringExtensions.SerializeListOfIdsToString(randomHistories.Select(x => x.Id).ToList())).Navigate();
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

        public HistoryViewDto SelectedHistoryStory
        {
            get { return _selectedHistoryStory; }
            set
            {
                _selectedHistoryStory = value;
                if(value!=null)
                NavigateToSingleStory(value.StoryId);
                base.NotifyOfPropertyChange(() => SelectedHistoryStory);
            }
        }

        public StoryDto SelectedFavouriteStory
        {
            get { return _selectedFavouriteStory; }
            set
            {
                _selectedFavouriteStory = value;
                if (value != null)
                    NavigateToSingleStory(value.Id);
                base.NotifyOfPropertyChange(()=>SelectedFavouriteStory);
            }
        }

        public MenuItem SelectedMenuItem
        {
            get { return _selectedMenuItem; }
            set
            {
                _selectedMenuItem = value;
                value.NavigateMenuCommand.Execute();
                base.NotifyOfPropertyChange(()=>SelectedMenuItem);
            }
        }

        public Visibility CategoryLoadTextVisibility
        {
            get { return _categoryLoadTextVisibility; }
            set
            {
                _categoryLoadTextVisibility = value;
                base.NotifyOfPropertyChange(()=>CategoryLoadTextVisibility);
            }
        }

        public Visibility HistoryLoadTextVisibility
        {
            get { return _historyLoadTextVisibility; }
            set
            {
                _historyLoadTextVisibility = value;
                base.NotifyOfPropertyChange(() => HistoryLoadTextVisibility);
            }
        }

        public Visibility FavoritesLoadTextVisibility
        {
            get { return _favoritesLoadTextVisibility; }
            set
            {
                _favoritesLoadTextVisibility = value;
                base.NotifyOfPropertyChange(() => FavoritesLoadTextVisibility);
            }
        }

        private void NavigateToSingleStory(int id)
        {
            _navigationService.UriFor<SingleStoryViewModel>().WithParam(x => x.CurrentStoryId, id).WithParam(x => x.StringIds, StringExtensions.SerializeListOfIdsToString(FavoriteStories.Select(x => x.Id))).Navigate();
        }

        private void NavigateToCategoryStories(int id)
        {
           _navigationService.UriFor<StoriesListViewModel>().WithParam(x=>x.CategoryId,id).Navigate();
        }
    }
}
