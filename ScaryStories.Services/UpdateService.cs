using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Windows;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Entities.Repositories.Contracts;
using ScaryStories.Services.Base;
using ScaryStories.Services.Dto;
using ScaryStories.Services.StoriesUpdateRemoteService;

namespace ScaryStories.Services
{
    public class UpdateService:IRemoteService {
        private StoriesUpdateServiceClient _serviceClient;
        private DateTime _tempDateTime;
        private ICategoryRepository _categoryRepository;
        private IStoryRepository _storyRepository;
        private IStorySourceRepository _storySourceRepository;
        private BackgroundWorker _updateWorker;
        private IEnumerable<CategoryDto> _localCategories;
        private List<ThreadSyncByCategory> _syncMaster; 
        private IEnumerable<int> _newStoriesIds;
        private IEnumerable<StorySourceDto> _storySources; 
        private object _lockObject=new object();

        public UpdateService(RepositoriesStore store)
        {
            _categoryRepository = store.CategoryRepository;
            _storyRepository = store.StoryRepository;
            _storySourceRepository = store.StorySourceRepository;
            _serviceClient=new StoriesUpdateServiceClient();
            _serviceClient.CheckUpdateCompleted+=_serviceClient_CheckUpdateCompleted;
            _serviceClient.GetNewStoriesCompleted += _serviceClient_GetNewStoriesCompleted;
            _serviceClient.GetNewStoryCompleted+=_serviceClient_GetNewStoryCompleted;
            _serviceClient.GetSourcesCompleted += _serviceClient_GetSourcesCompleted;
            _syncMaster=new List<ThreadSyncByCategory>();
        }

        void _serviceClient_GetSourcesCompleted(object sender, GetSourcesCompletedEventArgs e)
        {
            if (e.Error != null) {
                MessageBox.Show("Нет подключения к интернету, либо служба обновления недоступна");
            }
           var localSources=_storySourceRepository.GetAll().ToList();
            foreach (StorySourceServiceDto sourceDto in e.Result) {
                var localStory=localSources.FirstOrDefault(x => x.Name == sourceDto.Name);
                if (localStory == null) {
                    _storySourceRepository.Insert(new StorySourceDto(){CreatedTime = sourceDto.CreatedTime,Image = sourceDto.Image,Name = sourceDto.Name,Url = sourceDto.Url});
                }

            }
            _serviceClient.GetNewStoriesAsync(_tempDateTime);
        }

        public IEnumerable<CategoryDto> LocalCategories {
            get {
                if (_localCategories == null) {
                    RefreshLocalCategories();
                }
                return _localCategories;
            }
        }

        public void CheckUpdate(DateTime lastDateTime) {
            _tempDateTime = lastDateTime;
            try {
                _serviceClient.CheckUpdateAsync(lastDateTime);
            }
            catch (Exception e) {
                MessageBox.Show("Нет интернета");
              
            }
        }

        void _serviceClient_CheckUpdateCompleted(object sender, CheckUpdateCompletedEventArgs e)
        {
            if (e.Error != null) {
                MessageBox.Show("Не удалось подключиться к интернету, либо служба обновления недоступна");
                return;
            }
            if (OnCheckUpdate != null)
            {
                OnCheckUpdate(new RemoteCheckingUpdateDto() { NewStoriesCount = e.Result.NewStoriesNumber });
            }

        }

        public void StartAsyncUpdate(DateTime lastDateTime) {
            _tempDateTime = lastDateTime;
            _serviceClient.GetSourcesAsync();
           
        }

        void _serviceClient_GetNewStoriesCompleted(object sender, GetNewStoriesCompletedEventArgs e) {
            if (OnStoriesDownload != null) {
                OnStoriesDownload();
            }
            foreach (var categoryRemote in e.Result) {
                var sync = new ThreadSyncByCategory() {
                    CategoryName = categoryRemote.Name,
                    MaxGeneratorCount = categoryRemote.StoriesIds.Count
                };
                _storySources = _storySourceRepository.GetAll().ToList();
                _syncMaster.Add(sync);
                CreateCategoryIfNotExist(categoryRemote);
                _newStoriesIds = categoryRemote.StoriesIds;
                _serviceClient.GetNewStoryAsync(categoryRemote.StoriesIds[(int)sync.GetNextStoryid()]);
                
            }      
        }

        void _serviceClient_GetNewStoryCompleted(object sender, GetNewStoryCompletedEventArgs e)
        {
 	        InsertStory(e.Result);
            var sync=_syncMaster.FirstOrDefault(x => x.CategoryName == e.Result.CategoryName);
            var nextId = sync.GetNextStoryid();
            if (nextId != null) {
                _serviceClient.GetNewStoryAsync(_newStoriesIds.ElementAt((int)nextId));
            }
           
        }

        private void InsertStory(StoryServiceDto story) {
            var categoryId = LocalCategories.FirstOrDefault(x => x.Name == story.CategoryName).Id;
            var storySource = _storySources.FirstOrDefault(x => x.Name == story.SourceName);
            if (storySource != null) {
                var storySourceId = storySource.Id;
                _storyRepository.Insert(new StoryDto(){CategoryId = categoryId,CreatedTime = DateTime.Now,Image = story.Image,
                    IsFavorite = story.IsFavorite,Likes = story.Likes,Name = story.Header,Text = story.Text,SourceId = storySourceId,SourceUrl = story.SourceUrl});
            }
            if (OnStoryInserted != null) {
                OnStoryInserted();
            }
        }

        private void CreateCategoryIfNotExist(CategoryServiceDto category) {
           var exist=LocalCategories.Any(x => x.Name == category.Name);
            if (!exist) {
                _categoryRepository.InsertOrUpdate(new CategoryDto(){CreatedTime = DateTime.Now,Image = category.Image,Name = category.Name});
               RefreshLocalCategories();
            }
        }

        private void RefreshLocalCategories() {
            _localCategories = _categoryRepository.GetAll();
        }

   
        public event Action OnStoriesDownload;

        public event Action OnStoryInserted;

        public event OnCheckUpdateHandler OnCheckUpdate;




        public event Action OnUpdateCompleted;


        public class ThreadSyncByCategory {
            private int _generator;
            public int MaxGeneratorCount { get; set; }
            private object _lockObject=new object();
            public string CategoryName { get; set; }


            public int? GetNextStoryid()
            {
                if (_generator!=MaxGeneratorCount-1)
                {
                    _generator = _generator + 1;
                    return _generator;
                }
                return null;
            }

            
        }
    }
}
