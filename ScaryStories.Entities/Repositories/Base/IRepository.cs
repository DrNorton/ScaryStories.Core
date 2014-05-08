using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;

namespace ScaryStories.Entities.Repositories
{
    public interface IRepository<Detail, Dto> 
         where Detail : class,IDetail, new()
         where Dto : BaseDto, new()
      {
			Task<IEnumerable<Dto>> GetAll();
			Task<Dto> GetItem(int id);
			Task InsertOrUpdateRange(IEnumerable<Dto> items);
			void InsertOrUpdate(Dto dto);
		    void Insert(Dto dto);
            Task<IEnumerable<Dto>> Search(string pattern);
		    Task Delete(Dto dto);
            Detail UpdateEntry(Dto sourceDto, Detail targetEntity);
            Detail CreateEntry(Dto dto);
            
		}
}
