using System.Collections.Generic;
using System.Threading.Tasks;
using OpenNETCF.ORM;
using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories.Contracts;

namespace ScaryStories.Entities.Repositories
{
		public class CategoryRepository:BaseRepository<CategoryDetail,CategoryDto>,ICategoryRepository {

				public CategoryRepository(SQLiteDataStore store)
					:base(store){
						
				}

				public override CategoryDetail UpdateEntry(CategoryDto sourceDto, CategoryDetail targetEntity) {
					targetEntity.Id = sourceDto.Id;
					targetEntity.Image = sourceDto.Image;
					targetEntity.Name = sourceDto.Name;
					return targetEntity;
				}

                public override CategoryDetail CreateEntry(CategoryDto dto)
                {
              
					return new CategoryDetail(dto.Id, dto.Name,dto.Image);
				}


                protected override CategoryDto Convert(CategoryDetail entity)
                {
                    return new CategoryDto(){Id=entity.Id,Image = entity.Image,Name=entity.Name};
                }

                public override Task<IEnumerable<CategoryDto>> Search(string pattern) {
                    return Task <IEnumerable<CategoryDto>> .Factory.StartNew(()=>ConvertColl(_store.Select<CategoryDetail>(x => x.Name.Contains(pattern))));
                }
        }
}
