using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using FazEngine2D.Classes;
using FazEngine2D.Classes.Addons;
using FazEngine2D.Classes.Audio;
using FazEngine2D.Extentions;
using FazEngine2D.Core.Audio;
using System.Windows.Input;
using System.Diagnostics;

namespace FazEngine2D.Core
{
    public sealed class EngineInstance
    {
        public static List<GameWindow> Windows = new List<GameWindow>();
        public static EngineInstance Instance;
        public static string SaveLoc;
        public static string Name;
        public static bool EngineDebug = false;
        public void EngineStartUp(ProjectInfo pi)
        {
            Instance = this;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("FazEngine Started...");
            Debug.Log(Directory.GetCurrentDirectory());
            Debug.Log("Waiting For Project...");
            Console.BackgroundColor = ConsoleColor.Black;
            ProjectStructure(pi);
            Console.ReadLine();
        }

        void ProjectStructure(ProjectInfo pi)
        {
            Debug.Log("Starting Project Structuring");
            Name = pi.Name;
            Debug.Log($"Got {Name}");
            SaveLoc = $@"{Name}\{pi.SavLoc}";
            Debug.Log($"Got saving location {SaveLoc}");
            ProjectStartUp(pi);
        }
        
        void ProjectStartUp(ProjectInfo pi)
        {
            if (Name == null)
            {
                Debug.Warn("Project Could Not Be Set Up...");
                return;
            }
            Directory.CreateDirectory(SaveLoc + @"\Sounds");
            Directory.CreateDirectory(SaveLoc + @"\Sprites");
            Debug.Log($"Directory Saves in {Directory.GetCurrentDirectory()}");
            pi.Start();
        }
    }
}