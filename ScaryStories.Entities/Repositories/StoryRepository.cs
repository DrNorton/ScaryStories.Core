﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenNETCF.ORM;
using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories.Contracts;

namespace ScaryStories.Entities.Repositories
{
		public class StoryRepository:BaseRepository<StoryDetail,StoryDto>, IStoryRepository {

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

                 public Task<DateTime> GetLastTimeData() {
                     return Task<DateTime>.Factory.StartNew(()=>_store.GetLastDateTimeInsert("StoryDetail"));
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

				public Task<IEnumerable<StoryDto>> GetStoriesByCategory(int categoryId) {
					return Task<IEnumerable<StoryDto>>.Factory.StartNew(()=>ConvertColl(_store.Select<StoryDetail>(x => x._categoryId == categoryId)));
				}

		        public Task AddToFavorites(StoryDto story)
		        {
		            return Task.Factory.StartNew(() =>
		            {
		                story.IsFavorite = true;
		                this.InsertOrUpdate(story);
		            });
		        }

                public Task RemoveFromFavorites(StoryDto story)
                {
                    return Task.Factory.StartNew(() =>
                    {
                        story.IsFavorite = false;
                        this.InsertOrUpdate(story);
                    });
                }

                public Task<IEnumerable<StoryDto>> GetFavoriteStories()
                {
                    return Task<IEnumerable<StoryDto>>.Factory.StartNew(() => ConvertColl(_store.Select<StoryDetail>(x => x.IsFavorite.Equals(true))));
		        }

                protected override StoryDto Convert(StoryDetail entity)
                {
                    if (entity == null) {
                        return null;
                    }
                    return new StoryDto(){CategoryId = entity._categoryId,Name = entity.Header,
                        Id=entity.Id,Image = entity.Image,IsFavorite = entity.IsFavorite,
                        Likes = entity.Likes,Text=entity.Text};
                }

                public override Task<IEnumerable<StoryDto>> Search(string pattern)
                {
                    return Task<IEnumerable<StoryDto>>.Factory.StartNew(() =>  ConvertColl(_store.Select<StoryDetail>(x => x.Header.ToLower().Contains(pattern.ToLower())))); 
                }

		        public Task<IEnumerable<StoryDto>> GetRandomStories() {
                    return Task <IEnumerable<StoryDto>>.Factory.StartNew(()=>ConvertColl(_store.SelectRandom<StoryDetail>().ToList()));
		        }
            

		    public byte[] GetRandomImage() 
            {
                return (byte[])_store.ExecuteScalar(@"SELECT Image FROM StoryDetail ORDER BY RANDOM() LIMIT 1");
		    }

        }
}
