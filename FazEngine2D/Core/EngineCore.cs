using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using FazEngine2D.Classes;
using FazEngine2D.Classes.Audio;
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
    
    public class ProjectInfo
    {
        public string Name { get; }
        public string SaveLocation { get; }
    }
}