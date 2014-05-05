﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using AudioPlaybackAgent;
using Caliburn.Micro;
using Microsoft.Phone.Controls;
using OpenNETCF.ORM;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services;
using ScaryStories.Services.Dto;
using ScaryStories.Visual.Extensions;
using ScaryStories.Visual.ViewModels;

namespace ScaryStories.Visual
{
    public class BootStrapper : PhoneBootstrapper
    {
        private PhoneContainer _container;
        private const string DBConnectionString = "ScaryStories.db";

        public BootStrapper()
        {

        }

        public PhoneContainer Container
        {
            get { return _container; }
        }

        protected override void Configure()
        {
            _container = new PhoneContainer();
            _container.RegisterPhoneServices(RootFrame);
            _container.PerRequest<MainViewModel>();
            _container.PerRequest<GrouppedStoriesListViewModel>();
            _container.PerRequest<StoriesListViewModel>();
            _container.PerRequest<SingleStoryViewModel>();
            _container.PerRequest<SettingsViewModel>();
            _container.PerRequest<SearchViewModel>();
            _container.PerRequest<UpdateViewModel>();
            var audioPlayer = new AudioPlayer();

            var sqlStore=ConfigureDatabase();
            _container.RegisterInstance(typeof(IRepositoriesStore),null,new RepositoriesStore(sqlStore));
            _container.RegisterInstance(typeof(IVkService), null, ConfigureVk());
            var settingsManager = ConfigureSettingsManager();
            _container.RegisterInstance(typeof(ISettingsManager),null,settingsManager);
            
        }

       

        private  SettingsManager ConfigureSettingsManager()
        {
            var settingsManager = new SettingsManager(IsolatedStorageSettings.ApplicationSettings);
            settingsManager.Load();
            return settingsManager;
        }

        private VkService ConfigureVk()
        {
            var vkService = new VkService();
            vkService.OnLoginDemand += NavigateToAuthorization;
            return vkService;
        }

        private SQLiteDataStore ConfigureDatabase()
        {
            if (!CheckDatabaseExists(DBConnectionString))
            {
                ImportDatabaseToIsolatedStorage();
            }
            var store = new SQLiteDataStore(DBConnectionString);
            store.AddType<CategoryDetail>();
            store.AddType<StoryDetail>();
            store.AddType<HistoryViewDetail>();
            store.AddType<StorySourceDetail>();
            store.Insert(new StoryDetail(0, "ывывывыв" + DateTime.Now, null, "cывывывheck" + DateTime.Now, 0, 2, false));
            return store;
        }

        private void NavigateToAuthorization(Action<string> obj)
        {
            RootFrame.Navigate(new Uri(String.Format("/Pages/{0}.xaml?code={1}", "VkAuthPage", "VkAuth"), UriKind.Relative));
        }


        private bool CheckDatabaseExists(string name)
        {
            IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
            return fileStorage.FileExists(name);

        }

        private void ImportDatabaseToIsolatedStorage()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var dbStream = assembly.GetManifestResourceStream("ScaryStories.Visual.StoryDb.ScaryStories.db"))
            {
                IsolatedStorageFile fileStorage = IsolatedStorageFile.GetUserStoreForApplication();
                using (var isolatedStorageWriter = new IsolatedStorageFileStream("ScaryStories.db", FileMode.OpenOrCreate, fileStorage))
                {
                    dbStream.CopyTo(isolatedStorageWriter);
                    dbStream.Close();
                    isolatedStorageWriter.Close();
                }
            }
        }

        public void GoTo(string uri)
        {
            RootFrame.Navigate(new Uri(uri, UriKind.RelativeOrAbsolute));
        }



        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

        protected override void OnLaunch(object sender, Microsoft.Phone.Shell.LaunchingEventArgs e)
        {
            // FlurryWP8SDK.Api.StartSession(ConstantsStorage.FlurryApiKey);
        }

        protected override void OnActivate(object sender, Microsoft.Phone.Shell.ActivatedEventArgs e)
        {
            //FlurryWP8SDK.Api.StartSession(ConstantsStorage.FlurryApiKey);
        }

        protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
        {
            return new TransitionFrame();
        }

        protected override void OnClose(object sender, Microsoft.Phone.Shell.ClosingEventArgs e)
        {
            //FlurryWP8SDK.Api.EndSession();
        }

        protected override void OnUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Произошло необработанное исключение; перейти в отладчик
                System.Diagnostics.Debugger.Break();
            }

            MessageBox.Show(e.ExceptionObject.Message, "Неожиданная ошибка", MessageBoxButton.OK);
        }
    }
}
