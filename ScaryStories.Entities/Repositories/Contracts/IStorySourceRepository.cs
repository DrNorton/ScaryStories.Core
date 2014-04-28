using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;

namespace ScaryStories.Entities.Repositories.Contracts
{
    public interface IStorySourceRepository : IRepository<StorySourceDetail, StorySourceDto>
    {
    }
}
