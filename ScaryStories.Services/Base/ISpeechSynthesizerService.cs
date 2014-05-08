using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScaryStories.Services.Base
{
    public interface ISpeechSynthesizerService
    {
        void Speak(string text);
        Action OnEndLoading { get; set; }
        Action<int> OnStartLoading { get; set; }
        Action<int> ReportProgressLoading { get; set; }
        void StopSpeak();
    }

}
