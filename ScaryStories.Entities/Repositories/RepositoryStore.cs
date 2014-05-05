
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenNETCF.ORM;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories.Contracts;

namespace ScaryStories.Entities.Repositories
{
    public interface IRepositoriesStore {
   
        ICategoryRepository CategoryRepository { get; set; }
        IStoryRepository StoryRepository { get; set; }
        IHistoryViewRepository HistoryViewRepository { get; set; }
        IStorySourceRepository StorySourceRepository { get; set; }
    }

    public class RepositoriesStore : IRepositoriesStore {
        private ICategoryRepository _categoryRepository;
        private IStoryRepository _storyRepository;
        private IHistoryViewRepository _historyViewRepository;

        public ICategoryRepository CategoryRepository
        {
            get { return _categoryRepository; }
            set { _categoryRepository = value; }
        }

        public IStoryRepository StoryRepository
        {
            get { return _storyRepository; }
            set { _storyRepository = value; }
        }

        public IHistoryViewRepository HistoryViewRepository
        {
            get { return _historyViewRepository; }
            set { _historyViewRepository = value; }
        }

        public IStorySourceRepository StorySourceRepository
        {
            get { return _storySourceRepository; }
            set { _storySourceRepository = value; }
        }

        private IStorySourceRepository _storySourceRepository;

        public RepositoriesStore(SQLiteDataStore store)
        {
            _categoryRepository = new CategoryRepository(store);
            _storyRepository = new StoryRepository(store);
            _historyViewRepository = new HistoryViewRepository(store);
            _storySourceRepository = new StorySourceRepository(store);

        }

       
     
    }
}
