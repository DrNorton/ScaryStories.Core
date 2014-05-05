using System.Linq;
using System.Threading.Tasks;
using OpenNETCF.ORM;
using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace ScaryStories.Entities.Repositories
{
    public class HistoryViewRepository:BaseRepository<HistoryViewDetail,HistoryViewDto>, IHistoryViewRepository {

        public HistoryViewRepository(SQLiteDataStore store)
            :base(store)
        {
            
        }

        public override Task<IEnumerable<HistoryViewDto>> Search(string pattern)
        {
            throw new NotImplementedException();
        }

        public override HistoryViewDetail UpdateEntry(HistoryViewDto sourceDto, HistoryViewDetail targetEntity) {
            targetEntity.Id = sourceDto.Id;
            targetEntity.StoryId = sourceDto.StoryId;
            targetEntity.ViewTime = sourceDto.ViewTime;
            return targetEntity;
        }

        public override HistoryViewDetail CreateEntry(HistoryViewDto dto) {
            return new HistoryViewDetail() {
                Id = dto.Id,
                StoryId = dto.StoryId,
                ViewTime = dto.ViewTime
            };
        }

        protected override HistoryViewDto Convert(HistoryViewDetail entity) {
            return new HistoryViewDto() { Id = entity.Id, StoryId = entity.StoryId, ViewTime = entity.ViewTime };
        }

        public Task<IEnumerable<HistoryViewDto>> GetLastHistories(int count)
        {
            return Task<IEnumerable<HistoryViewDto>>.Factory.StartNew(() =>
            
                ConvertColl(
                    _store.Fetch<HistoryViewDetail>(
                        count,
                        "",
                        FieldSearchOrder.Ascending,
                        true,
                        typeof (HistoryViewDetail)).ToList())
            );
        } 
    }
}
