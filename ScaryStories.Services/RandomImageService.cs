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
        private readonly RepositoriesStore _store;


        public RandomImageService(RepositoriesStore store)
        {
            _store = store; 
        }

        public  byte[] GetRandomImage()
        {
            //var task = _store.GetRandomImage();
            //task.Wait();
            //return task.Result;
            return null;
        }
    }
}
