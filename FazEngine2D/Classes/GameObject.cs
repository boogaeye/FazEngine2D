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
    using FazEngine2D.Attributes;
    using FazEngine2D.Core;
    public class GameObject : NamableObject, IDisposable
    {
        /// <summary>
        /// Gets the window the Object is currently in
        /// </summary>
        public FazEngineWindow FazEngineWindow;
        public List<Addon> Addons = new List<Addon>();
        public GameObjectTransform Transform { get; } = new GameObjectTransform();
        public bool Disposed { get; private set; } = false;

        public GameObject FindGameObjectByName(string name)
        {
            return FazEngineWindow.gameObjects.Where(e => e.Name == name).FirstOrDefault();
        }
        public static GameObject FindGameObjectByNameStaticly(string name)
        {
            
            return FazEngine2D.Core.EngineInstance.FazEngineWindows[0].FindGameObjectByName(name);
        }
        /// <summary>
        /// Adds an addon that isnt already an instance
        /// </summary>
        /// <param name="addon">Addon Type</param>
        public void AddAddon(Addon addon)
        {
            if (this.GetType().GetCustomAttributes(typeof(NonAddonAdder)).Any())
            {
                addon.Warn($"{addon} cant be added because {this.Name} doesnt allow addons to be added to it");
                return;
            }
            if (addon.GetType().GetCustomAttributes(typeof(OnlyAddableTo)).Any())
            {
                if (addon.GetType().GetCustomAttribute<OnlyAddableTo>().Addables != this.GetType())
                {
                    addon.Warn($"{addon} cant be added because it isnt allowed to add to type {GetType().Name}");
                    return;
                }
            }
            addon.SetGameObject(this);
            Addons.Add(addon);
            addon.Log($"Added {addon.Name} to {Name}");
        }
        /// <summary>
        /// Gets an Active Addon Otherwise it returns null
        /// </summary>
        /// <typeparam name="T">Addon Type</typeparam>
        /// <returns>Active Addon</returns>
        public T GetAddon<T>() where T : Addon
        {
            foreach (T a in Addons.Where(e => e.GetType() == typeof(T) && e.IsActive))
            {
                return a;
            }
            return null;
        }
        /// <summary>
        /// Gets any type of Addon of a type otherwise it will return null
        /// </summary>
        /// <typeparam name="T">Addon Type</typeparam>
        /// <returns>An Addon</returns>
        public T GetAllAddon<T>() where T : Addon
        {
            foreach (T a in Addons.Where(e => e.GetType() == typeof(T)))
            {
                return a;
            }
            return null;
        }
        /// <summary>
        /// Returns a list of Active Addons
        /// </summary>
        /// <typeparam name="T">Addon Type</typeparam>
        /// <returns>Active Addons</returns>
        public Addon[] GetAddons<T>() where T : Addon
        {
            return Addons.Where(e => e.GetType() == typeof(T) || e.GetType().BaseType == typeof(T) && e.IsActive).ToArray();
        }
        /// <summary>
        /// Returns a list of All Addons of a type
        /// </summary>
        /// <typeparam name="T">Addon Type</typeparam>
        /// <returns>Addons</returns>
        public Addon[] GetAllAddons<T>() where T : Addon
        {
            return Addons.Where(e => e.GetType() == typeof(T) || e.GetType().BaseType == typeof(T)).ToArray();
        }
        /// <summary>
        /// Destroy this object and all of its addons
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            var ad = new List<Addon>(Addons);
            foreach (Addon a in ad)
            {
                a.Dispose();
            }
            Disposed = true;
            if (FazEngineWindow != null)
            FazEngineWindow.gameObjects.Remove(this);
        }

        
        public GameObject(string name, FazEngineWindow gameWindow)
        {
            Name = name;
            OgObj = this;
            FazEngineWindow = gameWindow;
            gameWindow.gameObjects.Add(this);
            this.Log("Created myself yay!");
        }
        
        public GameObject(string name, IEnumerable<Addon> addons, FazEngineWindow gameWindow)
        {
            Name = name;
            OgObj = this;
            FazEngineWindow = gameWindow;
            Addons = addons.ToList();
            gameWindow.gameObjects.Add(this);
            this.Log("Created myself yay!");
        }
        
        public GameObject(string name, PresetWindow presetWindow)
        {
            Name = name;
            OgObj = this;
            if (presetWindow.gameObjects.Contains(this))
            {
                throw new GameObjectAlreadyExistsException($"{Name} already exists in {presetWindow.Name}");
            }
            this.Log($"Registered to {presetWindow.GetType().Name}");
            presetWindow.gameObjects.Add(this);
        }
        public GameObject()
        {
            
        }
        public override string ToString()
        {
            string reff = $"{Name}\n[pos:{this.Transform.Position}, scale: {this.Transform.Scale}]\nAddons:\n[";
            foreach (Addon addon in Addons)
            {
                reff += $"({addon.Name} : {addon.IsActive}), ";
            }
            reff += "]";
            return reff;
        }
    }
}
