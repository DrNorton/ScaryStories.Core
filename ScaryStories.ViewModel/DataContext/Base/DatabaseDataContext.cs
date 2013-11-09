using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories;
using ScaryStories.Entities.Repositories.Contracts;
using ScaryStories.Helpers;

namespace ScaryStories.ViewModel.DataContext.Base
{
    public abstract class DatabaseDataContext:BaseTemplate,IContext {
        private IStoryRepository _storyRepository;
        private ICategoryRepository _categoryRepository;
        private IHistoryViewRepository _historyViewRepository;

        public ICategoryRepository CategoryRepository {
            get {
                return _categoryRepository;
            }
        }

        public IStoryRepository StoryRepository {
            get {
                return _storyRepository;
            }
        }

        public IHistoryViewRepository HistoryViewRepository {
            get {
                return _historyViewRepository;
            }
        }

        

        public DatabaseDataContext(RepositoriesStore repositories)
        {
            _storyRepository = repositories.StoryRepository;
            _categoryRepository = repositories.CategoryRepository;
            _historyViewRepository = repositories.HistoryViewRepository;
        }

        public abstract string DataContextCode { get; }
        public abstract  void Run();

    }
}
