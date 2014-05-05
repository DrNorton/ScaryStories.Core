using System.Collections.Generic;
using System.Threading.Tasks;
using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;

namespace ScaryStories.Entities.Repositories.Contracts {
    public interface IHistoryViewRepository: IRepository<HistoryViewDetail,HistoryViewDto>
    {
        Task<IEnumerable<HistoryViewDto>> GetLastHistories(int count);
    }
}