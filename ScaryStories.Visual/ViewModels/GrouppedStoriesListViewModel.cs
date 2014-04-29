using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using ScaryStories.Entities.Dto;
using ScaryStories.Entities.Repositories;
using ScaryStories.Entities.Repositories.Contracts;
using ScaryStories.Visual.Extensions;

namespace ScaryStories.Visual.ViewModels
{
    public class GrouppedStoriesListViewModel : StoriesListViewModel
    {
        private List<AlphaKeyGroup<StoryDto>> _groupedStories = new List<AlphaKeyGroup<StoryDto>>();

        public List<AlphaKeyGroup<StoryDto>> GroupedStories
        {
            get
            {

                return _groupedStories;
            }
            set
            {
                _groupedStories = value;
                base.NotifyOfPropertyChange(() => GroupedStories);
            }
        }

        public GrouppedStoriesListViewModel(IRepositoriesStore repositoriesStore,INavigationService navigationService)
            :base(repositoriesStore,navigationService)
        {
            base.PropertyChanged += GrouppedStoriesListViewModel_PropertyChanged;
        }

        void GrouppedStoriesListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("Stories"))
            {
                if (Stories != null)
                {
                    GroupedStories = AlphaKeyGroup<StoryDto>.CreateGroups(
                        Stories,
                        CultureInfo.CurrentCulture,
                        (StoryDto s) => { return s.Name.ElementAt(0).ToString().ToLower(); },
                        true);
                }

            }
        }

 

        
    }
}
