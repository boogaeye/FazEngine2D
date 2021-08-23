using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Core
{
    public sealed class Debug
    {
        public static void Log(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            if (debug == null) { Console.BackgroundColor = ConsoleColor.Red; return; }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine(debug.ToString());
        }
        public static void Warn(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            if (debug == null) { Console.BackgroundColor = ConsoleColor.Red; return; }
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(debug.ToString());
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public static void Error(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            if (debug == null) { Console.BackgroundColor = ConsoleColor.Red; return; }
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(debug.ToString());
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
