using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FazEngine2D.Classes;

namespace FazEngine2D.Extentions
{
    public static class DebugExt
    {
        public static void Log(this object gameObj, object log, bool Beep = false)
        {
            if (Beep) Console.Beep();
            Console.WriteLine($"[{gameObj}] " + log);
        }
        public static void Log(this GameObject gameObj, object log, bool Beep = false)
        {
            if (Beep) Console.Beep();
            Console.WriteLine($"[{gameObj.Name}] " + log);
        }
    }

    public static class ObjectExt
    {
        public static GameObject ConvertToGameObject(this object obj, string name)
        {
            return new GameObject(name, obj, Core.EngineInstance.Windows[0]);
        }
    }
}
