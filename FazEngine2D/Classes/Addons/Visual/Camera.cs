using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons.Visual
{
    
    public sealed class Camera : Addon
    {
        public bool SizeFixerEnabled = true;

        public override void CallFunctionsBasedOnValue(byte b)
        {
            
        }

        public void SetRenderingCamera()
        {
            GameObject.FazEngineWindow.SetCamera(this);
        }
        public bool CanRenderCamera(FazEngineWindow fazEngineWindow)
        {
            return GameObject.FazEngineWindow == fazEngineWindow || !IsActive;
        }
    }
}
