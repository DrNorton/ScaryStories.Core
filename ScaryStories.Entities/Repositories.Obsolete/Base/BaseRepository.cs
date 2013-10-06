using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories.Base;

namespace ScaryStories.Entities.Base.Repositories.Obsolete
{
		public abstract class BaseRepository<entity, Dto> : IRepository<Dto>
				where entity : class,IDetail,new()
				where Dto : BaseDto {
			  protected readonly ScaryStoriesContext _context;

				protected BaseRepository(string connectionString) {
						_context = new ScaryStoriesContext(connectionString);
				}

				public IQueryable<Dto> GetAll() {
					return GetTable().Select(GetConverter());
				}

				public Dto GetItem(Guid id) {
					return GetTable().Where(x => x.Id == id).Select(GetConverter()).FirstOrDefault();
				}

				public void SaveRange(IEnumerable<Dto> items)
				{
						throw new NotImplementedException();
				}

				public void Save(Dto dto) {
					var table = GetTable();
					var updatedOrSavedEntity = table.SingleOrDefault(x => x.Id.Equals(dto.Id));
					if (updatedOrSavedEntity != null) {
						UpdateEntry(dto, updatedOrSavedEntity);
					}
					else {
						updatedOrSavedEntity = CreateEntry(dto);
						table.InsertOnSubmit(updatedOrSavedEntity);
					}
					_context.SubmitChanges();
					dto.Id = updatedOrSavedEntity.Id;
				}

		        public void Delete(Dto dto) {
		            var table = GetTable();
                    var deletedEntity= GetTable().FirstOrDefault(x => x.Id.Equals(dto.Id));
                    table.DeleteOnSubmit(deletedEntity);
                   _context.SubmitChanges();
		        }
    
			 public abstract entity UpdateEntry(Dto sourceDto, entity targetEntity);
			 public abstract entity CreateEntry(Dto dto);
			 protected abstract Table<entity> GetTable();
			 protected abstract Expression<Func<entity, Dto>> GetConverter();


		}

}
