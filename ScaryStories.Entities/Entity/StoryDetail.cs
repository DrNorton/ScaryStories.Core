using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using ScaryStories.Entities.Entity;

namespace ScaryStories.Entities.EntityModels
{
		[Table]
		public class StoryDetail:IDetail{
				private Guid _id;

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

				private string _header;
				private byte[] _image;
				private string _text;
				private long _likes;
		        private bool _isFavorite;
				private EntityRef<CategoryDetail> _category;
				[Column]
				internal Guid _categoryId;
				[Column]
				public string Header
				{
						get
						{
								return _header;
						}
						set
						{
								_header = value;
						}
				}
                [Column(DbType = "varbinary(8000)")]
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
				[Column]
				public long Likes
				{
						get
						{
								return _likes;
						}
						set
						{
								_likes = value;
						}
				}
                [Column]
				public string Text
				{
						get
						{
								return _text;
						}
						set
						{
								_text = value;
						}
				}
                [Column]
                public bool IsFavorite
                {
                    get
                    {
                        return _isFavorite;
                    }
                    set
                    {
                        _isFavorite = value;
                    }
                }


				[Association(Storage = "_category", ThisKey = "_categoryId", OtherKey = "Id", IsForeignKey = true)]
				public CategoryDetail Category
				{
						get
						{
								return _category.Entity;
						}
						set
						{
								_category.Entity = value;
								if (value != null)
								{
										_categoryId = value.Id;
								}
						}
				}

		    

		    public StoryDetail(Guid id, string header, byte[] image, string text, long likes, Guid categoryId,bool isFavorite) {
				_id = id;
				_header = header;
				_image = image;
				_text = text;
				_likes = likes;
				_categoryId = categoryId;
		        _isFavorite = isFavorite;
		    }

			public StoryDetail() {
				
			}
		}
}
