using System;


namespace ScaryStories.Entities.Dto
{
		public class BaseDto
		{
			public int Id { get; set; }
            public DateTime CreatedTime { get; set; }
            public string Name { get; set; }
		}
}
