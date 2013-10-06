using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Entities.Dto
{
		public class CategoryDto:BaseDto
		{
				public string Name { get; set; }
				public byte[] Image { get;set; }
		}
}
