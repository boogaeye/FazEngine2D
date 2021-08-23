using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime;
using System.Windows.Input;

namespace FazEngine2D.Classes
{
    using FazEngine2D.Extentions;
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Classes.Audio;
    using FazEngine2D.Attributes;
    public class GameObject : object, IDisposable
    {
        public string Name;
        public object OgObj;
        public GameWindow Scene;
        public List<Addon> Addons = new List<Addon>();
        public Vector2 Position = new Vector2();
        public bool Disposed { get; private set; } = false;
        public void AddAddon(Addon addon)
        {
            if (addon.GetType().GetCustomAttributes(typeof(NonAddable)).Any())
            {
                addon.Warn($"{addon} cant be added because its a nonaddable");
                return;
            }
            addon.SetGameObject(this);
            Addons.Add(addon);
            addon.Log($"Added {addon.Name} to {Name}");
        }
        public T GetAddon<T>() where T : Addon
        {
            foreach (T a in Addons.Where(e => e.GetType() == typeof(T)))
            {
                return a;
            }
            return null;
        }
        public Addon[] GetAddons<T>() where T : Addon
        {
            return Addons.Where(e => e.GetType() == typeof(T) || e.GetType().BaseType == typeof(T)).ToArray();
        }

        public void Dispose()
        {
            var ad = new List<Addon>(Addons);
            foreach (Addon a in ad)
            {
                a.Dispose();
            }
            Disposed = true;
            Scene.activeGameObjects.Remove(this);
        }

        public GameObject(string name, object obj, GameWindow gameWindow)
        {
            Name = name;
            OgObj = obj;
            Scene = gameWindow;
            gameWindow.activeGameObjects.Add(this);
        }
        public GameObject(string name, GameWindow gameWindow)
        {
            Name = name;
            OgObj = this;
            Scene = gameWindow;
            gameWindow.activeGameObjects.Add(this);
            this.Log("Created myself yay!");
        }
        public GameObject()
        {
            
        }
    }
}
