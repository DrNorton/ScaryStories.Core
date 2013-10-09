using OpenNETCF.ORM;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;

namespace ScaryStories.Entities.Repositories
{
		public class CategoryRepository:BaseRepository<CategoryDetail,CategoryDto> {

				public CategoryRepository(SQLiteDataStore store)
					:base(store){
						
				}

				public override CategoryDetail UpdateEntry(CategoryDto sourceDto, CategoryDetail targetEntity) {
					targetEntity.Id = sourceDto.Id;
					targetEntity.Image = sourceDto.Image;
					targetEntity.Name = sourceDto.Name;
					return targetEntity;
				}

				public override CategoryDetail CreateEntry(CategoryDto dto) {
					return new CategoryDetail(dto.Id, dto.Name,dto.Image);
				}


                protected override CategoryDto Convert(CategoryDetail entity)
                {
                    return new CategoryDto(){Id=entity.Id,Image = entity.Image,Name=entity.Name};
                }
        }
}
