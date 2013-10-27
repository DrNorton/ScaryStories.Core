using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Services.Dto
{
    public class CategoryRemoteServiceDto
    {
       
            private string _name;
            private byte[] _image;
            private IEnumerable<StoryRemoteServiceDto> _stories;
             private DateTime _createdTime;

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
            public IEnumerable<StoryRemoteServiceDto> Stories
            {
                get
                {
                    return _stories;
                }
                set
                {
                    _stories = value;
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
