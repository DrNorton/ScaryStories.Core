using System.Collections.Generic;

using Microsoft.Expression.Interactivity.Core;

using ScaryStories.Entities.Dto;

namespace ScaryStories.Visual.DataContext.Base {
    public delegate void OnCurrentStoryChangedHanlder();

    public interface IStoryManipulator {
        List<StoryDto> Stories { get; set; } 
        StoryDto SelectedStory { get; set; }
        event OnCurrentStoryChangedHanlder OnCurrentStoryChanged;
        void NextStory();
        void PreviousStory();
        void AddToFavorites();
        void DeleteFromFavorites();
        bool CanNext();
        bool CanPrevious();
       
    }
}