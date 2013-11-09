using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

using ScaryStories.Entities.Base.Repositories;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Entity;
using ScaryStories.Entities.EntityModels;
using ScaryStories.Entities.Repositories;
using ScaryStories.Services;
using ScaryStories.ViewModel.DataContext.Base;

namespace ScaryStories.ViewModel.DataContext.ItemsViewModels
{
    public class HistoryViewContainerContext:StoryContainerContext {
        private const int historySelectCount = 6;

        public HistoryViewContainerContext(RepositoriesStore store,VkService vkService)
            :base(store,vkService){
                Run();
        }

        public override string DataContextCode {
            get {
                return "HistoryViewContainer";
            }
        }

        public override void Run() {
            if (base.Stories == null) {
                var histories = base.HistoryViewRepository.GetLastHistories(historySelectCount);
                base.Stories = GetStoriesFromHistory(histories).ToList();
            }
            base.Run();
        }

        private IEnumerable<StoryDto> GetStoriesFromHistory(IEnumerable<HistoryViewDto> histories) {
            foreach (var historyViewDto in histories) {
                yield return this.StoryRepository.GetItem(historyViewDto.StoryId);
            }
        }

       

    }
}
