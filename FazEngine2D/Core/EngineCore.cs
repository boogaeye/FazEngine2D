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
    /// <summary>
    /// Engine Instance That gets started by the preloader and Creates Project Files
    /// </summary>
    public sealed class EngineInstance
    {
        public static List<FazEngineWindow> FazEngineWindows = new List<FazEngineWindow>();
        public static string SaveLoc;
        public static string Name;
        public static ProjectInfo Project;
        public static bool EngineDebug = false;
        /// <summary>
        /// Closes all the engines windows as well as the engine itself useful for closing the game completely
        /// </summary>
        public static void CloseEngine()
        {
            foreach (FazEngineWindow fw in FazEngineWindows)
            {
                fw.Dispose();
            }
            Environment.Exit(0);
        }
        /// <summary>
        /// Close all the engines windows while keeping the engine Open and Running
        /// </summary>
        public static void CloseWindows()
        {
            foreach (FazEngineWindow fw in FazEngineWindows)
            {
                fw.Dispose();
            }
        }
        /// <summary>
        /// Gets your project as its project type
        /// </summary>
        /// <typeparam name="T">Your Project Class</typeparam>
        /// <returns>Your Project as its Class Type</returns>
        public static T GetProjectAsType<T>() where T : ProjectInfo
        {
            return (T)Project;
        }
        void Log(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("[Engine]" + debug.ToString());
        }
        /// <summary>
        /// Method called for starting up the engine ONLY CALL THIS ONCE
        /// </summary>
        /// <param name="pi">Project Class</param>
        public void EngineStartUp(ProjectInfo pi)
        {
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
            Project = pi;
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