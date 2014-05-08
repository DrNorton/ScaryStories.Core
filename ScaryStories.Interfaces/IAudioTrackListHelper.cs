using System.IO;

namespace ScaryStories.Interfaces
{
    public interface IAudioTrackListHelper
    {
        void AddTrack(string name);
        void SaveTrackListToIzolatedStorage();
        void LoadTrackListFromIzolatedStorage();
        string SaveAudioFileToIsoStore(Stream stream,int count);
        void ClearAndDeleteAllFromIzolatedStorage();
    }
}
