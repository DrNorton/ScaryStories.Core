﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Services.Dto;
using ScaryStories.Services.StoriesUpdateRemoteService;

namespace ScaryStories.Services.Base
{
    public delegate void OnCheckUpdateHandler(RemoteCheckingUpdateDto dto);
  

    public interface IRemoteService {
       void CheckUpdate(DateTime lastDateTime);
       void StartAsyncUpdate(DateTime lastDateTime);

       event Action OnStoriesDownload;
       event Action OnStoryInserted;
        event Action OnUpdateCompleted;
       event OnCheckUpdateHandler OnCheckUpdate;
    }
}
