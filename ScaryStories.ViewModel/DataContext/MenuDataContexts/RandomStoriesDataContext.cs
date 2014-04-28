using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;
using ScaryStories.ViewModel.DataContext.ItemsViewModels;

namespace ScaryStories.ViewModel.DataContext.MenuDataContexts
{
    public class RandomStoriesDataContext : StoryContainerContext, IMenuItem
    {
        public RandomStoriesDataContext(RepositoriesStore store,VkService vkService)
             :base(store,vkService){
            _backgroundWorker=new BackgroundWorker();
                 _backgroundWorker.DoWork += _backgroundWorker_DoWork;
                 _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
        }

        void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            base.Stories = (List<StoryDto>)e.Result;
            ProgressBarOff();
        }

        void _backgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
           e.Result= base.StoryRepository.GetRandomStories().ToList();
        }

        private void GetRandomStories() {
            ProgressBarOn();
            _backgroundWorker.RunWorkerAsync();
        }

        public override string DataContextCode
        {
            get {
                return "RandomStoriesListPage";
            }
        }

        public string Header
        {
            get { return "Подборка случайных историй"; }
        }

        public string Description
        {
            get { return "10 cлучайных историй"; }
        }

        public Uri ImagePath
        {
            get { return new Uri("/Content/random.png", UriKind.Relative); }
        }

        public override void Run()
        {
            GetRandomStories();
            base.Run();
        }
    }
}
