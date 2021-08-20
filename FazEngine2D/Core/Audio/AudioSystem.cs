
namespace FazEngine2D.Core.Audio
{
    using System.IO;
    using System.Media;
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Classes.Audio;
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
        public void SetAudioFile(AudioFile audioFile)
        {
            AudioFile = audioFile;
            this.Log(AudioFile);
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
                    soundPlayer.Play();
                    Debug.Log("Sound Played");
                }
                catch (FileNotFoundException e)
                {
                    Debug.Error($"FileNotFound 404\n{e.Message}");
                }
            }
            else
            {
                Debug.Warn("No Audio File Selected");
            }
        }
    }
}
