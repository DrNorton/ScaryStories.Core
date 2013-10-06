using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using ScaryStories.Entities.Entity;


namespace ScaryStories.Entities.EntityModels
{
		[Table]
		public class CategoryDetail:IDetail {
				private Guid _id;
			private string _name;
			private byte[] _image;
			private EntitySet<StoryDetail> _stories;

			public CategoryDetail(Guid id, string name, byte[] image) {
				_id = id;
				_name = name;
				_stories =new EntitySet<StoryDetail>();
				_image = image;
			}

			[Column(IsPrimaryKey = true, DbType = "uniqueidentifier", CanBeNull = false)]
			public Guid Id
			{
					get
					{
							return _id;
					}
					set
					{
							_id = value;
					}
			}
			
				[Column]
			public string Name
				{
						get
						{
								return _name;
						}
						set
						{
								_name = value;
						}
				}
				[Column]
			public byte[] Image
				{
						get
						{
								return _image;
						}
						set
						{
								_image = value;
						}
				}

			[Association(Storage = "_stories", OtherKey = "_categoryId", ThisKey = "Id")]
			public EntitySet<StoryDetail> Stories
			{
					get { return this._stories; }
					set { this._stories.Assign(value); }
			}

			public CategoryDetail() {
				
			}
		
		}
}
