using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime;
using FazEngine2D.Extentions;
using System.Windows.Input;

namespace FazEngine2D.Classes
{
    public class GameObject : object
    {
        public string Name;
        public object OgObj;
        public List<Addon> Addons = new List<Addon>();
        public void AddAddon(Addon addon)
        {
            Addons.Add(addon);
        }
        public Addon GetAddon<T>() where T : Addon
        {
            foreach (T a in Addons)
            {
                return a;
            }
            return null;
        }
        public Addon[] GetAddons(Type T)
        {
            return Addons.Where(e => e.GetType().Name == T.Name).ToArray();
        }

        public GameObject(string name, object obj, GameWindow gameWindow)
        {
            Name = name;
            OgObj = obj;
            gameWindow.activeGameObjects.Add(this);
        }
        public GameObject(string name, GameWindow gameWindow)
        {
            Name = name;
            OgObj = this;
            gameWindow.activeGameObjects.Add(this);
        }
    }
    public class GameWindow
    {
        public Form Form;
        public GameObject GameObject;
        public List<GameObject> activeGameObjects = new List<GameObject>();
        public GameWindow()
        {
            Form = new Form();
            GameObject = new GameObject("GameWindow", this, this);
            FazEngine2D.Core.EngineInstance.Windows.Add(this);
            Task.Run(() => UpdateLoop());
        }

        public void StartUpdate()
        {
            foreach (GameObject g in activeGameObjects)
            {
                foreach (Script addon in g.Addons)
                {
                    addon.Start();
                }
            }
            this.Log("Update Starter Started");
        }

        public async Task UpdateLoop()
        {
            StartUpdate();
            this.Log("Starting Update");
            int Frame = 0;
            while (Frame < int.MaxValue)
            {
                foreach (GameObject g in activeGameObjects)
                {
                    foreach (Script addon in g.Addons)
                    {
                        addon.Update();
                    }
                }
                Frame++;
            }
        }
    }
    
    public class AudioFile
    {
        public string Location;
    }
    public abstract class Addon
    {
        public GameObject GameObject { get; }
        public object Obj { get => this; }
        public string Name { get => GetType().Name; }
        
    }
    public class Script : Addon
    {
        public virtual void Start()
        {
            this.Log(" Has started");
        }
        public virtual void Update()
        {
            
        }
    }

    public class Bruh : Script
    {
        public override void Start()
        {
            base.Start();
            this.Log($"Hi my name is {Name}");
        }
        public override void Update()
        {
            base.Update();
            
        }
    }
}
