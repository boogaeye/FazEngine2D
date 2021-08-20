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
    public class GameObject : object
    {
        public string Name;
        public object OgObj;
        public List<Addon> Addons = new List<Addon>();
        public void AddAddon(Addon addon)
        {
            addon.SetGameObject(this);
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
            this.SetGameObject(this.GameObject);
        }
    }
}
