using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons
{
    using FazEngine2D.Extentions;
    public abstract class Addon : NamableObject
    {
        public GameObject GameObject { get; private set; }
        public object Obj { get => this; }
        public new string Name { get => this.GetType().Name; }
        public bool IsActive = true;
        bool setGO = false;
        public bool Disposed { get; private set; } = false;
        /// <summary>
        /// Calls functions based on byte value 0 => Starter Functions, 1 => Update Functions, 2 => All Important Functions
        /// </summary>
        /// <param name="b">Value</param>
        public abstract void CallFunctionsBasedOnValue(byte b);
        public void SetGameObject(GameObject gameObject)
        {
            if (setGO)
            {
                this.Warn("Setting this addons GameObject is NOT possible anymore");
                return;
            }
            setGO = true;
            GameObject = gameObject;
        }
        public override string ToString()
        {
            return $"{Name}";
        }
        public override void Dispose()
        {
            Disposed = true;
            GameObject.Addons.Remove(this);
            GameObject = null;
        }
        
    }
    public class NoActiveAddonException : Exception
    {
        public NoActiveAddonException(string message) : base(message)
        {
            
        }
    }

}
