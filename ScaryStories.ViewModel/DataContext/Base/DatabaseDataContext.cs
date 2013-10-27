using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;

namespace ScaryStories.ViewModel.DataContext.Base
{
    public abstract class DatabaseDataContext:BasePropertyChanging,IContext {
        private StoryRepository _storyRepository;
        private CategoryRepository _categoryRepository;

        public DatabaseDataContext(StoryRepository storyRepository,CategoryRepository categoryRepository) {
            _storyRepository = storyRepository;
            _categoryRepository = categoryRepository;
        }

        public abstract string DataContextCode { get; }

        public StoryRepository StoryRepository {
            get {
                return _storyRepository;
            }
        }

        public CategoryRepository CategoryRepository {
            get {
                return _categoryRepository;
            }
       
        }
    }
}
