using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ScaryStories.Services.Base;
using TranslatorService.Speech;
using System.Threading.Tasks;

namespace ScaryStories.Services
{
    public class SpeechSynthesizerService {
        private const string _bingApiKey = @"IN7c847YqJi1ds5p61div7MGkp3jHFoeZMgkb7iaFJ4";
        private const string _languageCode = "ru";
        public Action<string> OnSaveToIsolatedCompleted;
       

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
          
                SaveToIsoStore(e.Stream);
            
       
        }

        private void SaveToIsoStore(Stream stream)
        {
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            if (!isolatedStorageFile.FileExists("temp5"))
            {
                using (IsolatedStorageFileStream isolatedStorageFileStream = isolatedStorageFile.CreateFile("temp5"))
                {
                    int chunkSize = 1024;
                    byte[] bytes = new byte[chunkSize];
                    int byteCount;

                    while ((byteCount = stream.Read(bytes, 0, chunkSize)) > 0)
                    {
                        isolatedStorageFileStream.Write(bytes, 0, byteCount);
                    }
                    if (OnSaveToIsolatedCompleted != null)
                    {
                        OnSaveToIsolatedCompleted("temp5");
                    }
                }



            }
        }


    }
}
