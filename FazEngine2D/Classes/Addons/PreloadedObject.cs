using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons
{
    public abstract class PreloadedObject : GameObject
    {
        public new string Name { get; set; }
        public abstract void PreloadState();
        public override string ToString()
        {
            return $"[PreloadObj : {this.GetType().Name}]{Name}";
        }
    }
}
