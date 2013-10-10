using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenNETCF.ORM;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;

namespace ScaryStories.Visual.Services
{
    public class DatabaseService {
        public delegate EventHandler FavoritesListChangeHandler();
        public event FavoritesListChangeHandler OnFavoritesListChange;


        private CategoryRepository _categoryRepository;
        private StoryRepository _storyRepository;

        public DatabaseService(SQLiteDataStore store) {
            _categoryRepository = new CategoryRepository(store);
            _storyRepository = new StoryRepository(store);
        }

        public void DeleteFromFavorites(StoryDto story)
        {
            _storyRepository.RemoveFromFavorites(story);
        }

        public void AddToFavorites(StoryDto story)
        {
            _storyRepository.AddToFavorites(story);
        }
    }
}
