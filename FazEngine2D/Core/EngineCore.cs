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

namespace FazEngine2D.Core
{
    public sealed class EngineInstance
    {
        public static List<FazEngineWindow> FazEngineWindows = new List<FazEngineWindow>();
        public static EngineInstance Instance;
        public static string SaveLoc;
        public static string Name;
        public static bool EngineDebug = false;
        void Log(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("[Engine]" + debug.ToString());
        }
        public void EngineStartUp(ProjectInfo pi)
        {
            Instance = this;
            Log("Starting Faz Engine...");
            Log(Directory.GetCurrentDirectory());
            Log("Waiting For Project...");
            ProjectStructure(pi);
            Console.ReadLine();
        }

        void ProjectStructure(ProjectInfo pi)
        {
            Log("Starting Project Structuring");
            Name = pi.Name;
            Log($"Got {Name}");
            SaveLoc = $@"{Name}\{pi.SavLoc}";
            Log($"Got saving location {SaveLoc}");
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
            Directory.CreateDirectory(SaveLoc + @"\Music");
            Log($"Directory Saves in {Directory.GetCurrentDirectory()}");
            foreach (PreloadedObject p in pi.PreloadedObjects)
            {
                EnginePreloader.preloadedObjects.Add(p);
                Debug.Preload($"{p.Name} : {p.GetType().Name} get Preloaded from Project");
                p.PreloadState();
                
            }
            try
            {
                Task.Run(() => pi.Start());
            } catch (Exception e)
            {
                Log($"Faz Engine Caught an error from {pi.Name}'s start method\n{e.Message}\n{e.StackTrace}");
            }
            Log(pi.Name + " got started from Faz Engine!");
        }
    }
}