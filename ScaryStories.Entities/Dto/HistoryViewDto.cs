using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Entities.Dto
{
    public class HistoryViewDto:BaseDto
    {
        public int StoryId { get; set; }
        public DateTime ViewTime { get; set; }

        public StoryDto StoryDto { get; set; }
    }
}
