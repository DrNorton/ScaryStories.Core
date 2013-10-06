using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

using ScaryStories.Helpers;

namespace ScaryStories.Entities.EntityModels
{
		
		[InheritanceMapping(Type = typeof(StoryDetail), Code = 1, IsDefault = true)]
		[InheritanceMapping(Type = typeof(CategoryDetail), Code = 2)]
		public abstract class BaseDetail
		{
				

			

		}


}
