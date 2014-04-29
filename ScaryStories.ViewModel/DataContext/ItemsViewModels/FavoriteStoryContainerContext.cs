using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;

namespace ScaryStories.ViewModel.DataContext.ItemsViewModels
{
    public class FavoriteStoryContainerContext:StoryContainerContext
    {
        public FavoriteStoryContainerContext(RepositoriesStore store,VkService vkService)
            : base(store, vkService)
        {
            
        }

        public override string DataContextCode
        {
            get {
                return "FavoriteStoryContainer";
            }
        }

        public override void Run()
        {
           
        }

     
    }
}
