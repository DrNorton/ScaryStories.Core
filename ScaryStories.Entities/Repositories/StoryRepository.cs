using System;
using System.Collections.Generic;

using OpenNETCF.ORM;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;

namespace ScaryStories.Entities.Repositories
{
		public class StoryRepository:BaseRepository<StoryDetail,StoryDto> {

				public StoryRepository(SQLiteDataStore store)
					:base(store)
				{
					
				}

				public override StoryDetail UpdateEntry(StoryDto sourceDto, StoryDetail targetEntity) {
					targetEntity.Id = sourceDto.Id;
					targetEntity.Image = sourceDto.Image;
					targetEntity.Header = sourceDto.Name;
					targetEntity.Likes = sourceDto.Likes;
					targetEntity.Text = sourceDto.Text;
				    targetEntity._categoryId = sourceDto.CategoryId;
				    targetEntity.IsFavorite = sourceDto.IsFavorite;
				    targetEntity.CreatedTime = sourceDto.CreatedTime;
					return targetEntity;
				}

                 public DateTime GetLastTimeData() {
                     return _store.GetLastDateTimeInsert("StoryDetail");
                 }

				public override StoryDetail CreateEntry(StoryDto dto) {
					return new StoryDetail() {
						Id = dto.Id,
                        _categoryId = dto.CategoryId,
						Header = dto.Name,
						Image = dto.Image,
						Likes = dto.Likes,
						Text = dto.Text,
                        IsFavorite = dto.IsFavorite,
                        CreatedTime = dto.CreatedTime
					};
				}


				public IEnumerable<StoryDto> GetStoriesByCategory(int categoryId) {
					return ConvertColl(_store.Select<StoryDetail>(x => x._categoryId == categoryId));
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

                public IEnumerable<StoryDto> GetFavoriteStories()
                {
                    return ConvertColl(_store.Select<StoryDetail>(x => x.IsFavorite.Equals(true)));
		        }

                protected override StoryDto Convert(StoryDetail entity)
                {
                    return new StoryDto(){CategoryId = entity._categoryId,Name = entity.Header,
                        Id=entity.Id,Image = entity.Image,IsFavorite = entity.IsFavorite,
                        Likes = entity.Likes,Text=entity.Text};
                }

                public override IEnumerable<StoryDto> Search(string pattern) {
                    return ConvertColl(_store.Select<StoryDetail>(x => x.Header.ToLower().Contains(pattern.ToLower())));
                }

		    public byte[] GetRandomImage() 
            {
                return (byte[])_store.ExecuteScalar(@"SELECT Image FROM StoryDetail ORDER BY RANDOM() LIMIT 1");
		    }

        }
}
