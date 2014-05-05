using System.Threading.Tasks;
using System.Windows.Media.Animation;

using OpenNETCF.ORM;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Entities.Repositories
{
    public class StorySourceRepository : BaseRepository<StorySourceDetail,StorySourceDto>, IStorySourceRepository
    {
        public StorySourceRepository(SQLiteDataStore store)
            :base(store)
        {
            
        }
        public override Task<IEnumerable<StorySourceDto>> Search(string pattern)
        {
            throw new NotImplementedException();
        }

        public override StorySourceDetail UpdateEntry(StorySourceDto sourceDto, StorySourceDetail targetEntity) {
            targetEntity.Id = sourceDto.Id;
            targetEntity.Image = sourceDto.Image;
            targetEntity.Name = sourceDto.Name;
            targetEntity.Url = sourceDto.Url;
            targetEntity.CreatedTime = sourceDto.CreatedTime;
            return targetEntity;
        }

        public override StorySourceDetail CreateEntry(StorySourceDto dto)
        {
            return new StorySourceDetail(dto.Id,dto.Name,dto.Url,dto.Image,dto.CreatedTime);
        }

        protected override StorySourceDto Convert(StorySourceDetail entity)
        {
            return new StorySourceDto(){Id = entity.Id,CreatedTime = entity.CreatedTime,Image =entity.Image,Name=entity.Name,Url = entity.Url};
        }
    }
}
