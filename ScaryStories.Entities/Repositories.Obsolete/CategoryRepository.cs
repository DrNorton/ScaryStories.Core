using System;
using System.Data.Linq;
using System.Linq;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;

namespace ScaryStories.Entities.Repositories.Obsolete
{
		public class CategoryRepository:BaseRepository<CategoryDetail,CategoryDto> {

				public CategoryRepository(string connectionString)
					:base(connectionString){
						
				}

				protected override System.Data.Linq.Table<CategoryDetail> GetTable() {
					return base._context.Categories;
				}

				protected override System.Linq.Expressions.Expression<Func<CategoryDetail, CategoryDto>> GetConverter() {
					return c => new CategoryDto() {
						Id = c.Id,
						Image = c.Image,
						Name = c.Name
					};
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


				
		}
}
