using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using OpenNETCF.ORM;

namespace ScaryStories.Entities.Entity
{
    [Entity(KeyScheme = KeyScheme.Identity)]
    public class HistoryViewDetail:IDetail {
        private int _id;
        private int _storyId;
        private DateTime _viewTime;

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
        public int StoryId {
            get {
                return _storyId;
            }
            set {
                _storyId = value;
            }
        }
        [Field]
        public DateTime ViewTime {
            get {
                return _viewTime;
            }
            set {
                _viewTime = value;
            }
        }

        public HistoryViewDetail(int id, int storyId, DateTime viewTime) {
            _id = id;
            _storyId = storyId;
            _viewTime = viewTime;
        }

        public HistoryViewDetail() {
            
        }
    }
}
