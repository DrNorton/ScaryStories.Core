using System;
using OpenNETCF.ORM;
using ScaryStories.Entities.Entity;

namespace ScaryStories.Entities.EntityModels
{
         [Entity(KeyScheme = KeyScheme.Identity)]
		public class StoryDetail:IDetail{
				private int _id;

                [Field(IsPrimaryKey = true)]
				public int Id
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
                [Field]
		        public  bool IsFavorite { get; set; }

                [Field]
                 public int _categoryId { get; set; }

		

                [Field]
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
                [Field]
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
                [Field]
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
                [Field]
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
               
                
                
             

             public StoryDetail(int id, string header, byte[] image, string text, long likes, int categoryId,bool isFavorite) {
				_id = id;
				_header = header;
				_image = image;
				_text = text;
				_likes = likes;
				_categoryId = categoryId;
                 IsFavorite = isFavorite;
             }

			public StoryDetail() {
				
			}
		}
}
