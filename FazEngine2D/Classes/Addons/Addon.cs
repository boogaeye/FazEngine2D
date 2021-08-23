using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons
{
    using FazEngine2D.Core;
    public abstract class Addon : IDisposable
    {
        public GameObject GameObject { get; private set; }
        public object Obj { get => this; }
        public string Name { get => GetType().Name; }
        bool setGO = false;
        public bool Disposed { get; private set; } = false;
        public void SetGameObject(GameObject gameObject)
        {
            if (setGO)
            {
                Debug.Warn("Setting this addons GameObject is NOT possible anymore");
                return;
            }
            setGO = true;
            GameObject = gameObject;
        }

        public void Dispose()
        {
            Disposed = true;
        }
    }
}
