using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Input
{
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Attributes;
    using System.Windows.Forms;
    using FazEngine2D.Extentions;
    [Obsolete("Can not be used... use KeyPressEvent will be deleted soon")]
    [OnlyAddableTo(typeof(FazEngineWindow))]
    public sealed class InputEvent : Addon
    {
        List<Keys> DownKeys = new List<Keys>();
        List<Keys> UpKeys = new List<Keys>();
        List<Keys> WaitingUpKeys = new List<Keys>();
        List<Keys> PressedKeys = new List<Keys>();
        public void KeyUpdate(Keys k, KeyPressType kpt)
        {
            
        }
        public bool KeyDown(Keys key)
        {
            return DownKeys.Contains(key);
        }
        public bool KeyUp(Keys key)
        {
            return UpKeys.Contains(key);
        }
        public bool KeyPressed(Keys key)
        {
            return PressedKeys.Contains(key);
        }
        void Update()
        {
            
        }
        public override void CallFunctionsBasedOnValue(byte b)
        {
            switch (b)
            {
                case 3:
                    Update();
                    break;
            }
        }
    }
    public enum KeyPressType
    {
        Down,
        Press,
        Up
    }
}
