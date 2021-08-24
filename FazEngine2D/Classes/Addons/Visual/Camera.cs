using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons.Visual
{
    
    public class Camera : Addon
    {
        public bool SizeFixerEnabled = true;
        public void SetRenderingCamera()
        {
            GameObject.Scene.CurrentRenderingCamera = this;
        }
    }
}
