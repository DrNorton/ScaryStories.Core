using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Services.Dto
{
    public class StoryRemoteServiceDto
    {
            private string _header;
            private byte[] _image;
            private string _text;
            private long _likes;
            private bool _isFavorite;
            private int _categoryId;
            private DateTime _createdTime;

          
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
          
            public int CategoryId
            {
                get
                {
                    return _categoryId;
                }
                set
                {
                    _categoryId = value;
                }
            }

            public DateTime CreatedTime {
                get {
                return _createdTime;
            }
                set {
                _createdTime = value;
            }
            }
    }
}
