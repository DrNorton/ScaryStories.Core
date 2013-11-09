using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using OpenNETCF.ORM;

using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;
using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;
using ScaryStories.ViewModel.DataContext.ItemsViewModels;
using ScaryStories.ViewModel.DataContext.MenuDataContexts;
using ScaryStories.ViewModel.Extensions;

namespace ScaryStories.ViewModel
{
    public class MainViewModel : BaseTemplate, IServiceStore {
        public Action OnRequestNavigate;

	    private List<IContext> _dataContexts;
	    private byte[] _wallpaperImage;
        private RandomImageService _randomImageService;
        private UpdateService _updateService;
        private SettingsManager _settingsManager;
        private VkService _vkService;

	    public MainViewModel(SQLiteDataStore store,SettingsManager settings,VkService vkService) {
	        var repositoryStore = new RepositoriesStore(store);
		    DataContexts=new List<IContext>();
            _randomImageService = new RandomImageService(repositoryStore);
            _updateService = new UpdateService(repositoryStore);
	        _settingsManager = settings;
	        VkService = vkService;
            InitializationDataContexts(repositoryStore,settings,VkService);
	    }

       

        public List<IContext> DataContexts {
	        get {
	            return _dataContexts;
	        }
	        set {
	            _dataContexts = value;
	        }
	    }

	    public List<IMenuItem> MenuContexts {
	        get {
                   return _dataContexts.Where(x => typeof(IMenuItem).IsAssignableFrom(x.GetType())==true).Cast<IMenuItem>().ToList();
	        }
	        

	    }

	    public byte[] WallpaperImage {
	        get {
	            return _wallpaperImage;
	        }
	        set {
	            _wallpaperImage = value;
                base.NotifyPropertyChanged("WallpaperImage");
	        }
	    }

        private void InitializationDataContexts(RepositoriesStore repositories, SettingsManager settingsManager,VkService vkService) {
            var template = _settingsManager.Listener;
            this.Template =template;
            DataContexts.Add(new CategoriesWithStoriesContainerContext(repositories, vkService) { Template = template });
            DataContexts.Add(new FavoriteStoryContainerContext(repositories, vkService) { Template = template });
            DataContexts.Add(new HistoryViewContainerContext(repositories, vkService) { Template = template });
            DataContexts.Add(new VkAuthDataContext(VkService) { });
            InitializationMenuContexts(repositories, settingsManager, template, vkService);
          
	    }

        private void InitializationMenuContexts(RepositoriesStore repositories,SettingsManager settingsManager,SettingTemplate template,VkService vkService)
        {
            DataContexts.Add(new AllStoriesMenuDataContext(repositories, vkService) { Template = template });
            DataContexts.Add(new UpdateMenuDataContext(repositories, _updateService) { Template = template});
            DataContexts.Add(new SearchMenuDataContext(repositories,vkService) { Template = template});
            DataContexts.Add(new RandomStoriesDataContext(repositories, vkService) { Template = template });
            DataContexts.Add(new SettingsMenuDataContext(settingsManager) { Template = template });
            
        }

        public UpdateService UpdateService
        {
            get {
                return _updateService;
            }
        }

        public RandomImageService RandomImageService
        {
            get {
                return _randomImageService;
            }
        }

        public VkService VkService {
            get {
                return _vkService;
            }
            set {
                _vkService = value;
            }
        }
    }
}