
namespace FazEngine2D.Classes.Addons.Audio
{
    using System.IO;
    using System.Media;
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Core;
    using FazEngine2D.Extentions;
    public class AudioSystem : Addon
    {
        SoundPlayer soundPlayer;
        public AudioFile AudioFile;
        public AudioSystem()
        {
            StartUpMethod();
        }
        void StartUpMethod()
        {
            soundPlayer = new SoundPlayer();
            this.Log("SoundPlayer Started");
        }
        public new void Dispose()
        {
            soundPlayer.Stop();
            soundPlayer = null;
        }
        public void SetAudioFile(AudioFile audioFile)
        {
            AudioFile = audioFile;
        }
        public void Play()
        {
            if (AudioFile != null)
            {
                if (EngineInstance.SaveLoc == null)
                {
                    Debug.Warn("Project Info is not defined please define it to continue using file locations");
                    return;
                }
                soundPlayer.SoundLocation = EngineInstance.SaveLoc + @"\Sounds\" + AudioFile.Location;
                try
                {
                    soundPlayer.Stop();
                    soundPlayer.Play();
                    if (EngineInstance.EngineDebug)
                    Debug.Log("Sound Played");
                }
                catch (FileNotFoundException e)
                {
                    Debug.Error($"FileNotFound 404\n{e.Message}\n{EngineInstance.SaveLoc + @"\Sounds\" + AudioFile.Location}");
                }
            }
            else
            {
                Debug.Warn("No Audio File Selected");
            }
        }
        public void Stop()
        {
            soundPlayer.Stop();
        }
        public void PlayAsLoop()
        {
            if (AudioFile != null)
            {
                if (EngineInstance.SaveLoc == null)
                {
                    Debug.Warn("Project Info is not defined please define it to continue using file locations");
                    return;
                }
                soundPlayer.SoundLocation = EngineInstance.SaveLoc + @"\Sounds\" + AudioFile.Location;
                try
                {
                    soundPlayer.Stop();
                    soundPlayer.PlayLooping();
                    if (EngineInstance.EngineDebug)
                    Debug.Log("Sound Played Looped");
                }
                catch (FileNotFoundException e)
                {
                    Debug.Error($"FileNotFound 404\n{e.Message}\n{EngineInstance.SaveLoc + @"\Sounds\" + AudioFile.Location}");
                }
            }
            else
            {
                Debug.Warn("No Audio File Selected");
            }
        }
    }
}
