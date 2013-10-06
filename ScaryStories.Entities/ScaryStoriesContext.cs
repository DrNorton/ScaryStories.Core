using System.Data.Linq;

using ScaryStories.Entities.Entity;
using ScaryStories.Entities.EntityModels;

namespace ScaryStories.Entities
{
		public class ScaryStoriesContext: DataContext
		{
			public Table<StoryDetail> Stories;
			public Table<CategoryDetail> Categories;
				
			public ScaryStoriesContext(string connectionString)
					:base(connectionString){
				
			}
		}
}
