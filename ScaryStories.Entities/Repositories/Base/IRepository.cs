using System;
using System.Collections.Generic;

using ScaryStories.Entities.Dto;

namespace ScaryStories.Entities.Repositories
{
		public interface IRepository<T> where T:BaseDto {
			IEnumerable<T> GetAll();
			T GetItem(Guid id);
			void SaveRange(IEnumerable<T> items);
			void Save(T item);
            IEnumerable<T> Search(string pattern);
            
		}
}
