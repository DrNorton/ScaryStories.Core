using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;

namespace ScaryStories.Entities.Repositories.Contracts {
    public interface IStoryRepository:IRepository<StoryDetail,StoryDto> {
        Task<DateTime> GetLastTimeData();
        Task<IEnumerable<StoryDto>> GetStoriesByCategory(int categoryId);
        Task AddToFavorites(StoryDto story);
        Task RemoveFromFavorites(StoryDto story);
        Task<IEnumerable<StoryDto>> GetFavoriteStories();
        Task<IEnumerable<StoryDto>> GetRandomStories();
        byte[] GetRandomImage();
    }
}