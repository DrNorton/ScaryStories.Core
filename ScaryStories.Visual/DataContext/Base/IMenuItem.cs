using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Visual.DataContext.Base
{
    public interface IMenuItem:IContext
    {
        string Header { get;}
        string Description { get; }
        Uri ImagePath { get; }
    }
}
