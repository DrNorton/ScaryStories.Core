using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Helpers;
using ScaryStories.ViewModel.DataContext.Base;
using ScaryStories.ViewModel.DataContext.ItemsViewModels;

namespace ScaryStories.ViewModel.DataContext.MenuDataContexts
{
    public class SearchMenuDataContext : StoryContainerContext, IMenuItem, ISearch
    {
        private Visibility _progressBarVisibility=Visibility.Collapsed;
        private ActionCommand _doFindCommand;
        private string _searchPattern="";
        private BackgroundWorker _searchWorker;

        public SearchMenuDataContext(StoryRepository storyRepository,CategoryRepository categoryRepository)
           :base(storyRepository,categoryRepository){
                 _searchWorker=new BackgroundWorker();
         _searchWorker.DoWork+=new DoWorkEventHandler(_searchWorker_DoWork);
            _searchWorker.RunWorkerCompleted+=new RunWorkerCompletedEventHandler(_searchWorker_Completed);
        }

        public void Search() {
            ProgressBarVisibility = Visibility.Visible;
            _searchWorker.RunWorkerAsync();
            
        }

        private void _searchWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result= StoryRepository.Search(_searchPattern).ToList();
        }

        private void _searchWorker_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressBarVisibility = Visibility.Collapsed;
            this.Stories = (List<StoryDto>)e.Result;
        }



        public override string DataContextCode
        {
            get { return "SearchPage"; }
        }

        public string Header
        {
            get { return "Поиск"; }
        }

        public string Description
        {
            get { return "Поиск истории"; }
        }

        public Uri ImagePath
        {
            get { return new Uri(@"..\Content\search.png", UriKind.Relative); }
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

        public string SearchPattern {
            get {
                return _searchPattern;
            }
            set {
                _searchPattern = value;
                base.NotifyPropertyChanged("SearchPattern");
            }
        }
    }
}
