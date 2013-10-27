using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Entities.Repositories;
using ScaryStories.Services.Base;

namespace ScaryStories.Services
{
    public class RandomImageService:IRandomImageService {
        private StoryRepository _storyRepository;
        private CategoryRepository _categoryRepository;

        public RandomImageService(StoryRepository storyRepository) {
            _storyRepository = storyRepository;
           
        }

        public byte[] GetRandomImage() {
            return _storyRepository.GetRandomImage();
        }
    }
}
