using System.ComponentModel;
using System.Windows;

using Microsoft.Expression.Interactivity.Core;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services;
using ScaryStories.Services.Base;
using ScaryStories.Services.Dto;
using ScaryStories.Visual.DataContext.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ScaryStories.Visual.DataContext.MenuDataContexts
{
    public class UpdateMenuDataContext : DatabaseDataContext,IMenuItem
    {
        private ActionCommand _updateCommand;
        private IRemoteService _service;
        private DateTime _lastStoryDatetime;
        private RemoteCheckingUpdateDto _checkingInfo;
        private string _statusText;
        private Visibility _progressBarVisibility=Visibility.Collapsed;
        private BackgroundWorker _threadImporter;
        

        public UpdateMenuDataContext(StoryRepository storyRepository, CategoryRepository categoryRepository)
          :base(storyRepository,categoryRepository){
            _lastStoryDatetime = base.StoryRepository.GetLastTimeData();
            _service = new UpdateService();
            _service.OnCheckUpdate += _service_OnCheckUpdate;
            _service.CheckUpdate(_lastStoryDatetime);
            _service.OnRequestUpdateComplete+=new OnRequestUpdateCompleteHandler(RequestUpdateComplete);
            _threadImporter=new BackgroundWorker();
            _threadImporter.DoWork += _threadImporter_DoWork;
            _threadImporter.RunWorkerCompleted += _threadImporter_RunWorkerCompleted;
        }

        void _threadImporter_DoWork(object sender, DoWorkEventArgs e) {
            var remoteCategories = (List<CategoryRemoteServiceDto>)e.Argument;
            var categories = base.CategoryRepository.GetAll();
            foreach (var remoteCategory in remoteCategories)
            {
                var localCategory = categories.FirstOrDefault(x => x.Name == remoteCategory.Name);
                if (localCategory == null)
                {
                    var localCategoryId = CreateLocalCategory(remoteCategory);
                    CreateLocalStories(remoteCategory.Stories, localCategoryId);
                }
                else
                {
                    CreateLocalStories(remoteCategory.Stories, localCategory.Id);
                }
            }
           
        }

        void _threadImporter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StatusText = "";
            LogToStatusText("Обновление закончено.");
            ProgressBarVisibility = Visibility.Collapsed;
        }

      

        private void LogToStatusText(string text) {
            StatusText += text+"\n";
        }

        void _service_OnCheckUpdate(RemoteCheckingUpdateDto result) {
            CheckingInfo = result;
           if (_checkingInfo != null) {
                    if (_checkingInfo.NewStoriesCount == 0) {
                        LogToStatusText("Нет историй для обновлений");
                    }
                    else {
                        LogToStatusText(String.Format("Всего историй для обновления: {0}", _checkingInfo.NewStoriesCount));
                    }
            }
        }

        public string Header
        {
            get { return "Обновление"; }
        }

        public string Description
        {
            get {
                return "Обновление";
            }
        }

        public Uri ImagePath
        {
            get {
                return new Uri("/Content/update.png", UriKind.Relative);
            }
        }

        public ActionCommand UpdateCommand {
            get {
                if (_updateCommand == null) {
                    _updateCommand=new ActionCommand(Update);
                }
                return _updateCommand;
            }
        }

        public void Update() {
            LogToStatusText("Идёт обновление..");
            LogToStatusText("Начинаем загрузку..");

           ProgressBarVisibility=Visibility.Visible;
            _service.Update(_lastStoryDatetime);
        }

        private void RequestUpdateComplete(List<CategoryRemoteServiceDto> remoteCategories) {
            LogToStatusText("Загрузка закончена. Начинаем импортирование...");
           _threadImporter.RunWorkerAsync(remoteCategories);
        }

        private int CreateLocalCategory(CategoryRemoteServiceDto dto) {
            var category = new CategoryDto() { CreatedTime = DateTime.Now, Image = dto.Image, Name = dto.Name };
            base.CategoryRepository.Save(category);
            return category.Id;
        }

        private void CreateLocalStories(IEnumerable<StoryRemoteServiceDto> remoteStories,int localCategoryId  ) {
            foreach (var remoteStory in remoteStories) {
                this.StoryRepository.Save(new StoryDto(){CategoryId = localCategoryId,CreatedTime = DateTime.Now,Name = remoteStory.Header,Image = remoteStory.Image,
                    IsFavorite = remoteStory.IsFavorite,Likes = remoteStory.Likes,Text = remoteStory.Text});
            }
        }

        public override string DataContextCode
        {
            get { return "UpdateView"; }
        }

        public RemoteCheckingUpdateDto CheckingInfo {
            get {
                return _checkingInfo;
            }
            set {
                _checkingInfo = value;
                base.NotifyPropertyChanged("CheckingInfo");
                base.NotifyPropertyChanged("CheckingUpdateText");
            }
        }

       

        public string StatusText {
            get {
                return _statusText;
            }
            set {
                _statusText = value;
                base.NotifyPropertyChanged("StatusText");
            }
        }

        public Visibility ProgressBarVisibility {
            get {
                return _progressBarVisibility;
            }
            set {
                _progressBarVisibility = value;
                base.NotifyPropertyChanged("ProgressBarVisibility");
            }
        }
    }
}
