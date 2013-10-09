using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using OpenNETCF.ORM;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories;

namespace ScaryStories.Entities.Base.Repositories
{
		public abstract class BaseRepository<entity, Dto> : IRepository<Dto>
				where entity : class,IDetail,new()
				where Dto : BaseDto,new() {
		        protected SQLiteDataStore _store;
                public delegate void ChangeHandler(Dto story);
                public event ChangeHandler OnChange;

				protected BaseRepository(SQLiteDataStore store) {
				    _store = store;
				}

				public IEnumerable<Dto> GetAll() {
					return ConvertColl(_store.Select<entity>().ToList());
				}

				public Dto GetItem(Guid id) {
				    return Convert(_store.Select<entity>(id));
                       
				}

				public void SaveRange(IEnumerable<Dto> items)
				{
					throw new NotImplementedException();
				}

				public void Save(Dto dto) {
				
					var updatedOrSavedEntity = _store.Select<entity>(x => x.Id.Equals(dto.Id)).FirstOrDefault();
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
                    _store.Delete<entity>(dto.Id);
		        }
    
			 public abstract entity UpdateEntry(Dto sourceDto, entity targetEntity);
			 public abstract entity CreateEntry(Dto dto);

		     protected IEnumerable<Dto> ConvertColl(IEnumerable<entity> entities) {
                
		        foreach (var entity in entities) {
		            yield return Convert(entity);
		        }
		    }
		     protected abstract Dto Convert(entity entity);


		}

}
