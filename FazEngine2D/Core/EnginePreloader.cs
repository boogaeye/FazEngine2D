using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Core
{
    using FazEngine2D.Classes.Addons;
    public class EnginePreloader
    {
        public static List<PreloadedObject> preloadedObjects = new List<PreloadedObject>();
        public static void Main(string[] args, ProjectInfo GameType)
        {
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
            EngineInstance.Instance = new EngineInstance();
            EngineInstance.Instance.EngineStartUp(GameType);
        }
        public static PreloadedObject GetPreloadedObject(string name)
        {
            Debug.Preload($"Loading Preloaded Object {name}");
            return preloadedObjects.Where(e => e.Name == name).FirstOrDefault();
        }
        public static T GetPreloadedObject<T>() where T : PreloadedObject
        {
            Debug.Preload($"Loading Preloaded Object {typeof(T).Name}");
            return (T)preloadedObjects.Where(e => e.GetType() == typeof(T)).FirstOrDefault();
        }
        public static T GetPreloadedObject<T>(string name) where T : PreloadedObject
        {
            var n = preloadedObjects.Where(e => e.GetType() == typeof(T) && e.Name == name).FirstOrDefault();
            Debug.Preload($"Loading Preloaded Object {typeof(T).Name} with info:\nName: {n.Name}\nObj: {n.OgObj}");
            return (T)n;
        }
    }
}
