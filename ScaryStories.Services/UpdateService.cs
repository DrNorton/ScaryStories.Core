using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services.Base;
using ScaryStories.Services.Dto;
using ScaryStories.Services.StoriesUpdateRemoteService;

namespace ScaryStories.Services
{
    public class UpdateService:IRemoteService {
        private StoriesUpdateServiceClient _serviceClient;
        private DateTime _tempDateTime;
        private CategoryRepository _categoryRepository;
        private StoryRepository _storyRepository;
        private BackgroundWorker _updateWorker;
        private IEnumerable<CategoryDto> _localCategories;
        private int? _currentStoryIndex=0;
        private IEnumerable<int> _newStoriesIds;

        public UpdateService(StoryRepository storyRepository,CategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
            _storyRepository = storyRepository;
            _serviceClient=new StoriesUpdateServiceClient();
            _serviceClient.CheckUpdateCompleted+=_serviceClient_CheckUpdateCompleted;
            _serviceClient.GetNewStoriesCompleted += _serviceClient_GetNewStoriesCompleted;
            _serviceClient.GetNewStoryCompleted+=_serviceClient_GetNewStoryCompleted;
           
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
                
            }
        }

        void _serviceClient_CheckUpdateCompleted(object sender, CheckUpdateCompletedEventArgs e)
        {
            if (OnCheckUpdate != null)
            {
                OnCheckUpdate(new RemoteCheckingUpdateDto() { NewStoriesCount = e.Result.NewStoriesNumber });
            }

        }

        public void StartAsyncUpdate(DateTime lastDateTime)
        {
            _serviceClient.GetNewStoriesAsync(lastDateTime);
        }

        void _serviceClient_GetNewStoriesCompleted(object sender, GetNewStoriesCompletedEventArgs e) {
            if (OnStoriesDownload != null) {
                OnStoriesDownload();
            }
            foreach (var categoryRemote in e.Result) {
                CreateCategoryIfNotExist(categoryRemote);
                _newStoriesIds = categoryRemote.StoriesIds;
                _serviceClient.GetNewStoryAsync(categoryRemote.StoriesIds[(int)_currentStoryIndex]);
                
            }      
        }

        void _serviceClient_GetNewStoryCompleted(object sender, GetNewStoryCompletedEventArgs e)
        {
 	        InsertStory(e.Result);
            var nextId = GetNextStoryid();
            if (nextId != null) {
                
                _serviceClient.GetNewStoryAsync(_newStoriesIds.ElementAt((int)nextId));
            }
           
        }

        private void InsertStory(StoryServiceDto story) {
            var categoryId = LocalCategories.FirstOrDefault(x => x.Name == story.CategoryName).Id;
            _storyRepository.Save(new StoryDto(){CategoryId = categoryId,CreatedTime = DateTime.Now,Image = story.Image,
                    IsFavorite = story.IsFavorite,Likes = story.Likes,Name = story.Header,Text = story.Text});
            if (OnStoryInserted != null) {
                OnStoryInserted();
            }
        }

        private void CreateCategoryIfNotExist(CategoryServiceDto category) {
           var exist=LocalCategories.Any(x => x.Name == category.Name);
            if (!exist) {
                _categoryRepository.Save(new CategoryDto(){CreatedTime = DateTime.Now,Image = category.Image,Name = category.Name});
               RefreshLocalCategories();
            }
        }

        private void RefreshLocalCategories() {
            _localCategories = _categoryRepository.GetAll();
        }

        private int? GetNextStoryid() {
            if (_newStoriesIds.Count() > _currentStoryIndex) {
                return _currentStoryIndex++;
            }
            return null;
        }

        public event OnAction OnStoriesDownload;

        public event OnAction OnStoryInserted;

        public event OnCheckUpdateHandler OnCheckUpdate;


    }
}
