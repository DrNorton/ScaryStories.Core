using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
           
        }

        private void GetRandomStories() {
            base.Stories=base.StoryRepository.GetRandomStories().ToList();
        }

        public override string DataContextCode
        {
            get {
                return "StoryView";
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
