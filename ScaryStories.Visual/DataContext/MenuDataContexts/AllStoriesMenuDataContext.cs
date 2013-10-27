using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Entities.Repositories;
using ScaryStories.Visual.DataContext.Base;
using ScaryStories.Visual.DataContext.ItemsViewModels;

namespace ScaryStories.Visual.DataContext.MenuDataContexts
{
    public class AllStoriesMenuDataContext:StoryContainerContext,IMenuItem
    {
        public AllStoriesMenuDataContext(CategoryRepository categoryRepository,StoryRepository storyRepository)
            :base(storyRepository,categoryRepository){
            LoadAllStories();
        }

        private void LoadAllStories() {
            this.Stories = base.StoryRepository.GetAll().ToList();
        }



        public string Header
        {
            get {
                return "Все истории";
            }
        }

        public string Description
        {
            get {
                return "Список всех страшных историй";
            }
        }

        public Uri ImagePath
        {
            get {
                return new Uri("/Content/allStories.png", UriKind.Relative);
            }
        }

        public string DataContextCode
        {
            get {
                return "AllStoriesList";
            }
        }
    }
}
