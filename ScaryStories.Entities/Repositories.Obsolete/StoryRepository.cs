using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;

namespace ScaryStories.Entities.Repositories.Obsolete
{
		public class StoryRepository:BaseRepository<StoryDetail,StoryDto> {

				public StoryRepository(string connectionString)
					:base(connectionString)
				{
					
				}

				public override StoryDetail UpdateEntry(StoryDto sourceDto, StoryDetail targetEntity) {
					targetEntity.Id = sourceDto.Id;
					targetEntity.Image = sourceDto.Image;
					targetEntity.Header = sourceDto.Header;
					targetEntity.Likes = sourceDto.Likes;
					targetEntity.Text = sourceDto.Text;
					targetEntity._categoryId = sourceDto.Category.Id;
				    targetEntity.IsFavorite = sourceDto.IsFavorite;
					return targetEntity;
				}

				public override StoryDetail CreateEntry(StoryDto dto) {
					return new StoryDetail() {
						Id = dto.Id,
						Category = base._context.Categories.FirstOrDefault(x => x.Id == dto.Category.Id),
						_categoryId = dto.Category.Id,
						Header = dto.Header,
						Image = dto.Image,
						Likes = dto.Likes,
						Text = dto.Text,
                        IsFavorite = dto.IsFavorite
					};
				}

				protected override System.Data.Linq.Table<StoryDetail> GetTable() {
					return base._context.Stories;
				}

				protected override System.Linq.Expressions.Expression<Func<StoryDetail, StoryDto>> GetConverter() {
					return c => new StoryDto() {
						Id = c.Id,
						Category = new CategoryDto() {
							Id = c.Category.Id,
							Image = c.Category.Image,
							Name = c.Category.Name
						},
						Header = c.Header,
						Image = c.Image,
						Likes = c.Likes,
						Text = c.Text,
                        IsFavorite = c.IsFavorite
					};
				}

				public IQueryable<StoryDto> GetStoriesByCategory(Guid categoryId) {
					return _context.Stories.Where(x => x.Category.Id == categoryId).Select(GetConverter());
				}

		        public void AddToFavorites(StoryDto story) {
		           story.IsFavorite = true;
		           this.Save(story);
		         }

                public void RemoveFromFavorites(StoryDto story)
                 {
                     story.IsFavorite = false;
                     this.Save(story);
                 }

		        public IQueryable<StoryDto> GetFavoritesStories() {
		            return GetTable().Where(x => x.IsFavorite.Equals(true)).Select(GetConverter());
		        }
		}
}
