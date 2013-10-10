using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Entities.EntityModels;

namespace ScaryStories.Visual.Services.Events
{
    public class OnStorageChangeEventArgs:EventArgs {
        private List<StoryDetail> _newStoryList;
    }
}
