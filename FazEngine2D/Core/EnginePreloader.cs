using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Core
{
    using FazEngine2D.Classes.Addons;
    /// <summary>
    /// Engine Preloader and Object Preloader Useful for SpriteFiles, AudioFiles, and PresetWindows
    /// </summary>
    public class EnginePreloader
    {
        public static List<PreloadedObject> preloadedObjects = new List<PreloadedObject>();
        public static EnginePreloader Inst;
        public bool Preloaded;
        public EnginePreloader()
        {
            if (Inst != null)
            {
                Debug.Warn("Creation of another Engine Preloader is forbidden");
                return;
            }
            Inst = this;
        }
        public static void Main(string[] args, ProjectInfo GameType)
        {
            new EnginePreloader();
            if (Inst.Preloaded)
            {
                Debug.Warn("This method has already ran once and will not run again to prevent double Preloads");
                return;
            }
            Inst.Preloaded = true;
            var e = "Starting Engine with arguments [";
            foreach (string a in args)
            {
                e += a + ',';
            }
            e += $"]. got {GameType.Name} for the game Class name";
            Debug.Preload(e);
            foreach (Type preloadedObject in GameType.GetType().Assembly.GetTypes().Where(j => j.IsClass && !j.IsAbstract && j.IsSubclassOf(typeof(PreloadedObject))))
            {
                Debug.Preload(preloadedObject.Name);
                object @object = Activator.CreateInstance(preloadedObject);
                preloadedObjects.Add((PreloadedObject)@object);
                Debug.Preload($"Preloaded {((PreloadedObject)@object).Name}");
                ((PreloadedObject)@object).PreloadState();
            }
            new EngineInstance().EngineStartUp(GameType);
        }
        /// <summary>
        /// Gets a preloaded object by name
        /// </summary>
        /// <param name="name">Name of Object</param>
        /// <returns>First Object Of That Name</returns>
        public static PreloadedObject GetPreloadedObject(string name)
        {
            Debug.Preload($"Loading Preloaded Object {name}");
            return preloadedObjects.Where(e => e.Name == name).FirstOrDefault();
        }
        /// <summary>
        /// Gets a preloaded object by Type
        /// </summary>
        /// <typeparam name="T">Type of Preloaded Object</typeparam>
        /// <returns>First Preloaded object of that type</returns>
        public static T GetPreloadedObject<T>() where T : PreloadedObject
        {
            Debug.Preload($"Loading Preloaded Object {typeof(T).Name}");
            return (T)preloadedObjects.Where(e => e.GetType() == typeof(T)).FirstOrDefault();
        }
        /// <summary>
        /// Gets a preloaded object by name and type
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="name">Name</param>
        /// <returns>First Object With Both the name and type</returns>
        public static T GetPreloadedObject<T>(string name) where T : PreloadedObject
        {
            var n = preloadedObjects.Where(e => e.GetType() == typeof(T) && e.Name == name).FirstOrDefault();
            Debug.Preload($"Loading Preloaded Object {typeof(T).Name} with info:\nName: {n.Name}\nObj: {n.OgObj}");
            return (T)n;
        }
    }
}
