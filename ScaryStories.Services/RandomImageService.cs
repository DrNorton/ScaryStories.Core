using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Entities.Repositories;
using ScaryStories.Entities.Repositories.Contracts;
using ScaryStories.Services.Base;

namespace ScaryStories.Services
{
    public class RandomImageService:IRandomImageService {
        private IStoryRepository _storyRepository;
        

        public RandomImageService(RepositoriesStore store)
        {
            _storyRepository = store.StoryRepository;
           
        }

        public byte[] GetRandomImage() {
            return _storyRepository.GetRandomImage();
        }
    }
}
