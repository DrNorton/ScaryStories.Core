using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Entities.Dto
{
		public class StoryDto:BaseDto
		{
				public string Header { get; set; }
				public byte[] Image { get; set; }
				public string Text { get; set; }
				public long Likes { get; set; }
                public bool IsFavorite { get; set; }
				public CategoryDto Category { get; set; }
		}
}
