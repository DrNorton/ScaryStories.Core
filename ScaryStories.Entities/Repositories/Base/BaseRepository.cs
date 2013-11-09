using System;
using System.Collections.Generic;
using System.Linq;
using OpenNETCF.ORM;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.Repositories;

namespace ScaryStories.Entities.Base.Repositories
{
		public abstract class BaseRepository<Entity, Dto> : IRepository<Entity,Dto>
				where Entity : class,IDetail,new()
				where Dto : BaseDto,new() {
		        protected SQLiteDataStore _store;
                public delegate void ChangeHandler(Dto story);
                public event ChangeHandler OnChange;

				protected BaseRepository(SQLiteDataStore store) {
				    _store = store;
				}

				public IEnumerable<Dto> GetAll() {
					return ConvertColl(_store.Select<Entity>().ToList());
				}

				public Dto GetItem(int id) {
				    return Convert(_store.Select<Entity>(id));
                       
				}

				public void InsertOrUpdateRange(IEnumerable<Dto> items)
				{
					throw new NotImplementedException();
				}

				public void InsertOrUpdate(Dto dto) {
				
					var updatedOrSavedEntity = _store.Select<Entity>(x => x.Id.Equals(dto.Id)).FirstOrDefault();
					if (updatedOrSavedEntity != null) {
						UpdateEntry(dto, updatedOrSavedEntity);
                        _store.Update(updatedOrSavedEntity);
					    if (OnChange != null) {
					        OnChange(dto);
					    }
					}
					else {
						updatedOrSavedEntity = CreateEntry(dto);
						_store.Insert(updatedOrSavedEntity);
					}
					
					dto.Id = updatedOrSavedEntity.Id;
				}

		        public void Delete(Dto dto) {
                    _store.Delete<Entity>(dto.Id);
		        }


		     public abstract IEnumerable<Dto> Search(string pattern);
			 public abstract Entity UpdateEntry(Dto sourceDto, Entity targetEntity);
			 public abstract Entity CreateEntry(Dto dto);

		     protected IEnumerable<Dto> ConvertColl(IEnumerable<Entity> entities) {
                
		        foreach (var entity in entities) {
		            yield return Convert(entity);
		        }
		    }
		     protected abstract Dto Convert(Entity entity);

             public void Insert(Dto dto)
             {
                 var insertedEntity = CreateEntry(dto);
                 _store.Insert(insertedEntity);
             }
               
        }

}
