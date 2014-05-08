using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Phone.BackgroundAudio;
using ScaryStories.Interfaces;


namespace AudioPlaybackAgent
{
    public class AudioTrackListHelper : IAudioTrackListHelper
    {
        private List<string> _playList=new List<string>();
        private const string PlayListName = "playList.xml";
        private int currentTrackIndex = 0;

        public void AddTrack(string name)
        {
            _playList.Add(name);
        }

        public AudioTrack GetCurrentTrack()
        {
            return new AudioTrack(new Uri(_playList[currentTrackIndex],UriKind.Relative),String.Format("Часть {0}",currentTrackIndex),"","",null);
        }

        public AudioTrack GetNextTrack()
        {

            currentTrackIndex++;
            if (_playList.Count > currentTrackIndex)
            {
                return GetCurrentTrack();
            }
            return null;
        }

        public void ClearAndDeleteAllFromIzolatedStorage()
        {
            _playList.Clear();
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();

            var files = isolatedStorageFile.GetFileNames().Where(x => x.Contains("audio")).ToList();
            foreach (var file in files)
            {
                if(!String.IsNullOrEmpty(file))
                isolatedStorageFile.DeleteFile(file);
            }
        }

        public AudioTrack GetPrevioslyTrack()
        {
            currentTrackIndex--;
            if (currentTrackIndex < 0)
            {
                return null;
            }
            return GetCurrentTrack();
        }

        public void SaveTrackListToIzolatedStorage()
        {
            var xml = SerializeTrackList();
            CopyToIsolatedStorage(xml,PlayListName);
        }

        public void LoadTrackListFromIzolatedStorage()
        {
            DeserializeTrackList();
        }

        private string SerializeTrackList()
        {
            var serializer = new XmlSerializer(typeof(List<string>));
            var str = new StringBuilder();
            using (var xml=XmlWriter.Create(str))
            {
                serializer.Serialize(xml,_playList);
            }
            return str.ToString();
        }

        private void DeserializeTrackList()
        {
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
             
                if (myIsolatedStorage.FileExists(PlayListName))
                {
                    using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(PlayListName, FileMode.Open))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof (List<string>));
                        _playList = (List<string>) serializer.Deserialize(stream);
                    }
                }
            }
        }

        private Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream,Encoding.Unicode);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private void CopyToIsolatedStorage(string xml, string storeFileName)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists(storeFileName))
                {
                    storage.DeleteFile(storeFileName);
                }

                using (IsolatedStorageFileStream file = storage.CreateFile(storeFileName))
                    {
                        using (var reader=GenerateStreamFromString(xml))
                        {
                            const int chunkSize = 4096;
                            byte[] bytes = new byte[chunkSize];
                            int byteCount;

                            while ((byteCount = reader.Read(bytes, 0, chunkSize)) > 0)
                            {
                                file.Write(bytes, 0, byteCount);
                            }
                        }
                       
                    }
                
            }
        }

        public string SaveAudioFileToIsoStore(Stream stream,int count)
        {
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
           
            using (IsolatedStorageFileStream isolatedStorageFileStream = isolatedStorageFile.CreateFile(String.Format("audio{0}", count)))
                {
                    int chunkSize = 1024;
                    byte[] bytes = new byte[chunkSize];
                    int byteCount;

                    while ((byteCount = stream.Read(bytes, 0, chunkSize)) > 0)
                    {
                        isolatedStorageFileStream.Write(bytes, 0, byteCount);
                    }
               
                }


            return String.Format("audio{0}", count);
        }
    }
}
