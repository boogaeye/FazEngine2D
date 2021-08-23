using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Input
{
    using System.Windows.Input;
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Attributes;
    [NonAddable]
    public class InputEvent : Addon
    {
        [STAThread]
        public static bool KeyDown(Key key)
        {
            return Keyboard.IsKeyDown(key);
        }
        [STAThread]
        public static bool KeyUp(Key key)
        {
            return Keyboard.IsKeyUp(key);
        }
    }
}
