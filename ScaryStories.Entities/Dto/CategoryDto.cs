using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Entities.Dto
{
		public class CategoryDto:BaseDto
		{
                public DateTime CreatedTime { get; set; }
                public string Name { get; set; }
				public byte[] Image { get;set; }
		}
}
