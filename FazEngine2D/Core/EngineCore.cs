using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using FazEngine2D.Classes;
using FazEngine2D.Extentions;
using FazEngine2D.Core.Audio;
using System.Windows.Input;
using System.Diagnostics;

namespace FazEngine2D.Core
{
    public class Engine
    {
        static void Main(string[] args) => new EngineInstance().EngineStartUp();
        
    }
    public class EngineInstance
    {
        public static List<GameWindow> Windows = new List<GameWindow>();
        public static string SaveLoc;
        public void EngineStartUp()
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("FazEngine Started...");
            Debug.Log(Directory.GetCurrentDirectory());
            Console.BackgroundColor = ConsoleColor.Black;
            new GameWindow();
            Windows[0].GameObject.AddAddon(new AudioSystem());
            Windows[0].GameObject.AddAddon(new Bruh());
            Debug.Log(Windows[0].activeGameObjects.Count);
            ProjectStartUp(new Test().SaveLocation);
            ((AudioSystem)Windows[0].GameObject.GetAddon<AudioSystem>()).SetAudioFile(new AudioFile() { Location = "gunWeak.wav" });
            ((AudioSystem)Windows[0].GameObject.GetAddon<AudioSystem>()).Play();
            Windows[0].Form.ShowDialog();
            Console.ReadLine();
        }
        
        public void ProjectStartUp(string saveLocation)
        {
            if (!Directory.Exists(saveLocation))
            {
                Directory.CreateDirectory(saveLocation);
            }
            if (!Directory.Exists(saveLocation + @"\Sounds"))
            {
                Directory.CreateDirectory(saveLocation + @"\Sounds");
            }
            SaveLoc = saveLocation;
        }
    }
    public sealed class Debug
    {
        public static void Log(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            if (debug == null) { Console.BackgroundColor = ConsoleColor.Red; return; }
            Console.WriteLine(debug.ToString());
        }
        public static void Warn(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            if (debug == null) { Console.BackgroundColor = ConsoleColor.Red; return; }
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(debug.ToString());
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void Error(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            if (debug == null) { Console.BackgroundColor = ConsoleColor.Red; return; }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(debug.ToString());
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
    public class Test : ProjectInfo
    {
        public override string Name => "Bruh";

        public override string SaveLocation => "Test";
    }
    public abstract class ProjectInfo
    {
        public abstract string Name { get; }
        public abstract string SaveLocation { get; }
    }
}
namespace FazEngine2D.Core.Audio
{
    using System.Media;
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