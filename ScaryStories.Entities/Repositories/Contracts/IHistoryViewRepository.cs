using System.Collections.Generic;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;

namespace ScaryStories.Entities.Repositories.Contracts {
    public interface IHistoryViewRepository: IRepository<HistoryViewDetail,HistoryViewDto>
    {
        IEnumerable<HistoryViewDto> GetLastHistories(int count);
    }
}