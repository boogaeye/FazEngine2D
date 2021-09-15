
namespace FazEngine2D.Classes.Addons.Audio
{
    using System.IO;
    using System.Threading;
    using WMPLib;
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Core;
    using FazEngine2D.Extentions;
    using System;

    public sealed class AudioSystem : Addon, IDisposable
    {
        WindowsMediaPlayer soundPlayer;
        public AudioFile AudioFile;
        public double Position { get => soundPlayer.controls.currentPosition; set => soundPlayer.controls.currentPosition = value; }
        public double MaxPosition { get => soundPlayer.currentMedia.duration; }
        public int Volume { get => soundPlayer.settings.volume; set => soundPlayer.settings.volume = value; }
        public AudioSystem()
        {
            StartUpMethod();
        }
        void StartUpMethod()
        {
            
            soundPlayer = new WindowsMediaPlayer();
            this.Log("SoundPlayer Started");

        }
        public override void Dispose()
        {
            base.Dispose();
            Stop();
            soundPlayer = null;
            
            //Thread.Abort();
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
                
                try
                {
                    soundPlayer.URL = AudioFile.GetLocation();
                    soundPlayer.controls.play();
                    if (EngineInstance.EngineDebug)
                    this.Log("Sound Played");
                }
                catch (FileNotFoundException e)
                {
                    this.Error($"FileNotFound 404\n{e.Message}\n{EngineInstance.SaveLoc + @"\Sounds\" + AudioFile.Location}");
                }catch(ThreadAbortException)
                {

                }
            }
            else
            {
                this.Warn("No Audio File Selected");
            }
        }
        public void Stop()
        {
            soundPlayer.controls.stop();
        }

        public override void CallFunctionsBasedOnValue(byte b)
        {
            throw new NotImplementedException();
        }
    }
}
