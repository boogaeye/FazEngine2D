using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Extentions
{
    using FazEngine2D.Classes;
    using FazEngine2D.Classes.Addons;
    public static class DebugExt
    {
        public static void Log(this GameObject gameObj, object log, bool Beep = false)
        {
            if (Beep) Console.Beep();
            Console.WriteLine($"[{gameObj.Name}] " + log);
        }
        public static void Log(this Addon gameObj, object log, bool Beep = false)
        {
            if (Beep) Console.Beep();
            Console.WriteLine($"[{gameObj.GameObject.Name} : {gameObj.Name}] " + log);
        }
    }
}
