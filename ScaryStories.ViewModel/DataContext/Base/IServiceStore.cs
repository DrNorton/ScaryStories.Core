using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScaryStories.Services;

namespace ScaryStories.ViewModel.DataContext.Base
{
    public interface IServiceStore
    {
        UpdateService UpdateService { get; }
        RandomImageService RandomImageService { get; }
    }
}
