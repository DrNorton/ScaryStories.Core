using System;
using System.Windows.Controls;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using TranslatorService.Speech;
using System.Threading.Tasks;

namespace ScaryStories.Services
{
    public class SpeechSynthesizerService {
        private const string _bingApiKey = @"IN7c847YqJi1ds5p61div7MGkp3jHFoeZMgkb7iaFJ4";
        private const string _languageCode = "ru";
       

        public void Speak(string text) {
         
            SpeechSynthesizer speech = new SpeechSynthesizer("ScaryStoriesApp", "6qYi4vtCuytzHni09LVdqqRpEreVwTrmumY8JVei","ru-ru");
            if (text.Length > 1000) {
                text = text.Substring(0, 999);
            }
            
            speech.GetSpeakStreamCompleted+=speech_GetSpeakStreamCompleted;
            speech.GetSpeakStreamAsync(text);
        }

        

        void speech_GetSpeakStreamCompleted(object sender, GetSpeakStreamEventArgs e)
        {
            var effect = SoundEffect.FromStream(e.Stream);
            FrameworkDispatcher.Update();
            effect.Play();
        }

       

      
    }
}
