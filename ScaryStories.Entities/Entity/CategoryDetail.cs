using System;
using System.IO;

using OpenNETCF.ORM;

using ScaryStories.Entities.Entity;


namespace ScaryStories.Entities.EntityModels
{
    [Entity(KeyScheme = KeyScheme.Identity)]
	public class CategoryDetail:IDetail {
		    private int _id;
			private string _name;
			private byte[] _image;
			private StoryDetail[] _stories;
             private DateTime _createdTime;

			public CategoryDetail(int id, string name, byte[] image) {
				_id = id;
				_name = name;
				_image = image;
			}

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
			
			[Field]
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
            public DateTime CreatedTime
            {
                get
                {
                    return _createdTime;
                }
                set
                {
                    _createdTime = value;
                }
            }
        
            [Reference(typeof(StoryDetail), "_categoryId",Autofill = true)]
            public StoryDetail[] Stories
			{
				get { return this._stories; }
				set { this._stories=value; }
			}

           

        public CategoryDetail() {
				
			}
		
		}
}
