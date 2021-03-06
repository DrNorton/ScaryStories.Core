﻿using System.Collections.Generic;

using ScaryStories.Entities.Dto;

namespace ScaryStories.ViewModel.DataContext.Base {
    public delegate void OnCurrentStoryChangedHanlder();

    public interface IStoryManipulator {
        List<StoryDto> Stories { get; set; } 
        StoryDto SelectedStory { get; set; }
       
        void NextStory();
        void PreviousStory();
        void AddToFavorites();
        void DeleteFromFavorites();
        bool CanNext();
        bool CanPrevious();
        void ShareVk();
    }
}