using System;
using System.Collections.Generic;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;

namespace ScaryStories.Entities.Repositories
{
    public interface IRepository<Detail, Dto> 
         where Detail : class,IDetail, new()
         where Dto : BaseDto, new()
      {
			IEnumerable<Dto> GetAll();
			Dto GetItem(int id);
			void InsertOrUpdateRange(IEnumerable<Dto> items);
			void InsertOrUpdate(Dto dto);
		    void Insert(Dto dto);
            IEnumerable<Dto> Search(string pattern);
		    void Delete(Dto dto);
            Detail UpdateEntry(Dto sourceDto, Detail targetEntity);
            Detail CreateEntry(Dto dto);
            
		}
}
