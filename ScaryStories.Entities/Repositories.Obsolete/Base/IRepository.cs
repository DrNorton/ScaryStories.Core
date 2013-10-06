using System;
using System.Collections.Generic;
using System.Linq;

using ScaryStories.Entities.Dto;

namespace ScaryStories.Entities.Repositories.Obsolete
{
		public interface IRepository<T> where T:BaseDto {
			IQueryable<T> GetAll();
			T GetItem(Guid id);
			void SaveRange(IEnumerable<T> items);
			void Save(T item);
		}
}
