using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenNETCF.ORM;

using ScaryStories.Entities.EntityModels;

namespace ScaryStories.Entities.Entity
{
    [Entity(KeyScheme = KeyScheme.Identity)]
    public class StorySourceDetail:IDetail
    {
        private int _id;
        private string _name;
        private string _url;
        private byte[] _image;
        private DateTime _createdTime;
        private StoryDetail[] _stories;

        public StorySourceDetail(int id, string name, string url, byte[] image, DateTime createdTime) {
            _id = id;
            _name = name;
            _url = url;
            _image = image;
            _createdTime = createdTime;
        }

        public StorySourceDetail() {
            
        }

        [Field(IsPrimaryKey = true)]
        public int Id
        {
            get {
                return _id;
            }
            set {
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
        [Field]
        public string Url {
            get {
                return _url;
            }
            set {
                _url = value;
            }
        }

        [Reference(typeof(StoryDetail), "_categoryId",Autofill = true)]
        public StoryDetail[] Stories
        {
            get { return this._stories; }
            set { this._stories = value; }
        }
    }
}
