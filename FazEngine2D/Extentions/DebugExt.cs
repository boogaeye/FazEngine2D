using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Extentions
{
    using FazEngine2D.Classes;
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Core;
    public static class DebugExt
    {
        public static void Log(this NamableObject gameObj, object log, bool Beep = false)
        {
            Debug.Log($"[{gameObj.Name}] {log}", Beep);
        }
        public static void Log(this Addon gameObj, object log, bool Beep = false)
        {
            if (gameObj.GameObject == null)
            {
                Debug.Log($"[Detached Object : {gameObj.Name}] {log}", Beep);
                return;
            }
            Debug.Log($"[{gameObj.GameObject.Name} : {gameObj.Name}] {log}", Beep);
        }
        public static void Warn(this Addon gameObj, object log, bool Beep = false)
        {
            if (gameObj.GameObject == null)
            {
                Debug.Warn($"[Detached Object : {gameObj.Name}] {log}", Beep);
                return;
            }
            Debug.Warn($"[{gameObj.GameObject.Name} : {gameObj.Name}] {log}", Beep);
        }
        public static void Warn(this NamableObject gameObj, object log, bool Beep = false)
        {
            Debug.Warn($"[{gameObj.Name}] {log}", Beep);
        }
        public static void Error(this Addon gameObj, object log, bool Beep = true)
        {
            if (gameObj.GameObject == null)
            {
                Debug.Error($"[Detached Object : {gameObj.Name}] {log}", Beep);
                return;
            }
            Debug.Error($"[{gameObj.GameObject.Name} : {gameObj.Name}] {log}", Beep);
        }
        public static void Error(this NamableObject gameObj, object log, bool Beep = true)
        {
            Debug.Error($"[{gameObj.Name}] {log}", Beep);
        }
    }
}
