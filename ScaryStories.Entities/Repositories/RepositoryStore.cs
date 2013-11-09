
using OpenNETCF.ORM;

using ScaryStories.Entities.Repositories.Contracts;

namespace ScaryStories.Entities.Repositories
{
    public interface IRepositoriesStore {
        ICategoryRepository CategoryRepository { get; }
        IStoryRepository StoryRepository { get; }
        IHistoryViewRepository HistoryViewRepository { get; }
    }

    public class RepositoriesStore : IRepositoriesStore {
        private ICategoryRepository _categoryRepository;
        private IStoryRepository _storyRepository;
        private IHistoryViewRepository _historyViewRepository;

        public RepositoriesStore(SQLiteDataStore store)
        {
            _categoryRepository = new CategoryRepository(store);
            _storyRepository = new StoryRepository(store);
            _historyViewRepository = new HistoryViewRepository(store);
        }

       
      
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
    }
}
