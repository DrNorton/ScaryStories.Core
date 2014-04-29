using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Caliburn.Micro;
using Microsoft.Expression.Interactivity.Core;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services.Base;
using ScaryStories.Services.Dto;

namespace ScaryStories.Visual.ViewModels
{
    public class UpdateViewModel:Screen
    {
        private ActionCommand _updateCommand;
        private IRemoteService _service;
        private readonly IRepositoriesStore _repositoriesStore;
        private DateTime _lastStoryDatetime;
        private string _statusText;
        private Visibility _progressBarVisibility = Visibility.Collapsed;
        private Visibility _insertedStoriesCountVisibility = Visibility.Collapsed;
        private bool _updateButtonEnabled = false;
        private long _newStoriesCount;
        private bool _isIndeterminate = true;
        private int _currentImportStoryNumber = 0;
        private object lockObject = new object();
      

        public UpdateViewModel(IRemoteService service,IRepositoriesStore repositoriesStore)
        {
            _service = service;
            _repositoriesStore = repositoriesStore;
            _lastStoryDatetime = _repositoriesStore.StoryRepository.GetLastTimeData();
            _service = service;
            _service.OnCheckUpdate += _service_OnCheckUpdate;
            _service.OnStoryInserted += _service_OnStoryInserted;
            _service.OnStoriesDownload += _service_OnStoriesDownload;
            _service.OnUpdateCompleted += _service_OnUpdateCompleted;
            CheckUpdate();
        }


        void _service_OnUpdateCompleted()
        {
            ProgressBarVisibility = Visibility.Collapsed;
            InsertedStoriesCountVisibility = Visibility.Collapsed;
            UpdateButtonEnabled = false;
        }

        void _service_OnCheckUpdate(RemoteCheckingUpdateDto dto)
        {
            ProgressBarVisibility = Visibility.Collapsed;
            IsIndeterminate = false;
            NewStoriesCount = dto.NewStoriesCount;

            if (dto.NewStoriesCount != 0)
            {
                LogToStatusText(String.Format("Новых историй:{0}", dto.NewStoriesCount));
                UpdateButtonEnabled = true;
            }
            else
            {
                LogToStatusText(String.Format("Нет обновлений", dto.NewStoriesCount));
            }
        }

        private void LogToStatusText(string text)
        {
            StatusText = text + "\n";
        }

        private void _service_OnStoryInserted()
        {
            lock (lockObject)
            {
                CurrentImportStoryNumber = CurrentImportStoryNumber + 1;
            }
        }

        private void _service_OnStoriesDownload()
        {
            IsIndeterminate = false;
            UpdateButtonEnabled = true;

        }

        public ActionCommand UpdateCommand
        {
            get
            {
                if (_updateCommand == null)
                {
                    _updateCommand = new ActionCommand(Update);
                }
                return _updateCommand;
            }
        }

        public void Update()
        {
            LogToStatusText("Начинаем загрузку..");
            _service.StartAsyncUpdate(_lastStoryDatetime);
            ProgressBarVisibility = Visibility.Visible;
            InsertedStoriesCountVisibility = Visibility.Visible;
            UpdateButtonEnabled = false;

        }

        public void CheckUpdate()
        {
            ProgressBarVisibility = Visibility.Visible;
            IsIndeterminate = true;
            LogToStatusText("Подождите. Идет проверка доступных обновлений");
            _lastStoryDatetime = _repositoriesStore.StoryRepository.GetLastTimeData();
            try
            {
                _service.CheckUpdate(_lastStoryDatetime);
            }
            catch (Exception e)
            {
                MessageBox.Show("Нет подключения к интернету, либо сервис обновления не доступен");

            }
        }

        public Visibility ProgressBarVisibility
        {
            get
            {
                return _progressBarVisibility;
            }
            set
            {
                _progressBarVisibility = value;
       
                base.NotifyOfPropertyChange("ProgressBarVisibility");
            }
        }

        public bool IsIndeterminate
        {
            get
            {
                return _isIndeterminate;
            }
            set
            {
                _isIndeterminate = value;
                base.NotifyOfPropertyChange("IsIndeterminate");
            }
        }

        public long NewStoriesCount
        {
            get
            {
                return _newStoriesCount;
            }
            set
            {
                _newStoriesCount = value;
                base.NotifyOfPropertyChange("NewStoriesCount");
            }
        }

        public int CurrentImportStoryNumber
        {
            get
            {
                return _currentImportStoryNumber;
            }
            set
            {
                _currentImportStoryNumber = value;
                base.NotifyOfPropertyChange("CurrentImportStoryNumber");
            }
        }

        public Visibility InsertedStoriesCountVisibility
        {
            get
            {
                return _insertedStoriesCountVisibility;
            }
            set
            {
                _insertedStoriesCountVisibility = value;
                base.NotifyOfPropertyChange("InsertedStoriesCountVisibility");
            }
        }

        public bool UpdateButtonEnabled
        {
            get
            {
                return _updateButtonEnabled;
            }
            set
            {
                _updateButtonEnabled = value;
                base.NotifyOfPropertyChange("UpdateButtonEnabled");
            }
        }

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                base.NotifyOfPropertyChange(()=>StatusText);
            }
        }
    }
}
