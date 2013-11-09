using System;
using System.Collections.Generic;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;

namespace ScaryStories.Entities.Repositories.Contracts {
    public interface IStoryRepository:IRepository<StoryDetail,StoryDto> {
        DateTime GetLastTimeData();
        IEnumerable<StoryDto> GetStoriesByCategory(int categoryId);
        void AddToFavorites(StoryDto story);
        void RemoveFromFavorites(StoryDto story);
        IEnumerable<StoryDto> GetFavoriteStories();
        IEnumerable<StoryDto> GetRandomStories();
        byte[] GetRandomImage();
    }
}