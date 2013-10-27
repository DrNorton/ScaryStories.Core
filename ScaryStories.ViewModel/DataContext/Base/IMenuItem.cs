using System;

namespace ScaryStories.ViewModel.DataContext.Base
{
    public interface IMenuItem:IContext
    {
        string Header { get;}
        string Description { get; }
        Uri ImagePath { get; }
    }
}
