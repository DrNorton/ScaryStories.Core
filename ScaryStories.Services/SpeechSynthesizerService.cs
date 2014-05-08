using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ScaryStories.Interfaces;
using ScaryStories.Services.Base;
using TranslatorService.Speech;
using System.Threading.Tasks;

namespace ScaryStories.Services
{
 
    public class SpeechSynthesizerService : ISpeechSynthesizerService
    {
        private readonly IAudioTrackListHelper _trackListHelper;
        private List<string> _textParts;
        private int _partIndex;
        private SpeechSynthesizer _speech;

        public Action OnEndLoading { get; set; }
        public Action<int> OnStartLoading { get; set; }
        public Action<int> ReportProgressLoading { get; set; }

        public SpeechSynthesizerService(IAudioTrackListHelper trackListHelper)
        {
            _textParts=new List<string>();
            _partIndex = 0;
            _trackListHelper = trackListHelper;
            _speech = new SpeechSynthesizer("ScaryStoriesApp", "6qYi4vtCuytzHni09LVdqqRpEreVwTrmumY8JVei", "ru-ru");
            _speech.GetSpeakStreamCompleted += speech_GetSpeakStreamCompleted;
        }

        public void StopSpeak()
        {
            
        }

        public void Speak(string text)
        {
            
          _trackListHelper.ClearAndDeleteAllFromIzolatedStorage();
            _partIndex = 0;
            _textParts=new List<string>();
            _textParts=SplitBigTextToManyStrings(text);
            if (OnStartLoading != null) OnStartLoading(_textParts.Count);
            SpeakNextPart();
        }

        private void SpeakNextPart()
        {
            if (_textParts.Count > _partIndex && _textParts[_partIndex]!=null)
            {
                _speech.GetSpeakStreamAsync(_textParts[_partIndex]);
                ReportProgressLoading(_partIndex);
            }
            else
            {
                _trackListHelper.SaveTrackListToIzolatedStorage();
                ReportProgressLoading(_partIndex);
                OnEndLoading();
            }
        }

        void speech_GetSpeakStreamCompleted(object sender, GetSpeakStreamEventArgs e)
        {
            var trackName = _trackListHelper.SaveAudioFileToIsoStore(e.Stream,_partIndex);
            _trackListHelper.AddTrack(trackName);
            _partIndex++;
           SpeakNextPart();
         
        }

  
        private  IEnumerable<List<T>> TextToChunks<T>(IEnumerable<T> items, int chunkSize)
        {
            List<T> chunk = new List<T>(chunkSize);
            foreach (var item in items)
            {
                chunk.Add(item);
                if (chunk.Count == chunkSize)
                {
                    yield return chunk;
                    chunk = new List<T>(chunkSize);
                }
            }
            if (chunk.Any())
                yield return chunk;
        }

        private List<string> SplitBigTextToManyStrings(string text)
        {
            var chunks = TextToChunks(text.ToCharArray(), 500).ToList();
            return chunks.Select(res => new string(res.ToArray())).ToList();
        }  

    }
}
