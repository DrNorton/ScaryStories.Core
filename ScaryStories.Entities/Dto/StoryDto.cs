﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Entities.Dto
{
		public class StoryDto:BaseDto
		{
                public DateTime CreatedTime { get; set; }
                public string Name { get; set; }
				public byte[] Image { get; set; }
				public string Text { get; set; }
				public long Likes { get; set; }
                public bool IsFavorite { get; set; }
				public int CategoryId { get; set; }
                public string SourceUrl { get; set; }
                public int SourceId { get; set; }

		}
}
