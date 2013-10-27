using System.Collections.Generic;
 using System.Linq;

 using OpenNETCF.ORM;

using ScaryStories.Entities.Repositories;
 using ScaryStories.Helpers;
 using ScaryStories.Visual.DataContext.Base;
 using ScaryStories.Visual.DataContext.ItemsViewModels;
 using ScaryStories.Visual.DataContext.MenuDataContexts;

namespace ScaryStories.Visual
{
	public class MainViewModel:BasePropertyChanging {
	    private List<IContext> _dataContexts;
	    

	    public MainViewModel(SQLiteDataStore store) {
	        var storyRepository = new StoryRepository(store);
	        var categoryRepository = new CategoryRepository(store);
		    DataContexts=new List<IContext>();
            InitializationDataContexts(storyRepository,categoryRepository);
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

	    private void InitializationDataContexts(StoryRepository storyRepository,CategoryRepository categoryRepository) {
	        DataContexts.Add(new CategoriesWithStoriesContainerContext(storyRepository,categoryRepository));
            DataContexts.Add(new FavoriteStoryContainerContext(storyRepository,categoryRepository));
            InitializationMenuContexts(storyRepository,categoryRepository);
            var ds = MenuContexts;
	    }

        private void InitializationMenuContexts(StoryRepository storyRepository, CategoryRepository categoryRepository)
        {
	        DataContexts.Add(new AllStoriesMenuDataContext(categoryRepository,storyRepository));
            DataContexts.Add(new UpdateMenuDataContext(storyRepository,categoryRepository));
            DataContexts.Add(new SearchMenuDataContext(storyRepository,categoryRepository));
        }
	}
}