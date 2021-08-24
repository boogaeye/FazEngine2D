using FazEngine2D.Attributes;
using FazEngine2D.Classes;
using FazEngine2D.Classes.Addons;
using FazEngine2D.Classes.Addons.Visual;
using FazEngine2D.Classes.Addons.Visual.Rendering;
using FazEngine2D.Classes.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public static void DebugWindow(GameWindow gameWindow)
        {
            var gw = new GameWindow();
            gw.Name = "Debugging Window";
            var go = new GameObject("TextObj", gw);
            var cam = new GameObject("CameraObj", gw);
            go.AddAddon(new TextRenderObject());
            cam.AddAddon(new Camera());
            TextRenderObject tro = go.GetAddon<TextRenderObject>();
            gw.AddAddon(new DebugWindow(tro, gameWindow, cam));
            gw.ChangeBGColor(System.Drawing.Color.Purple);
        }

    }
    [OnlyAddableTo(typeof(GameWindow))]
    public sealed class DebugWindow : Script
    {
        public TextRenderObject Text;
        public GameWindow GWTrack;
        public Camera Camera;
        public DebugWindow(TextRenderObject w, GameWindow gameWindow, GameObject Cam)
        {
            Text = w;
            GWTrack = gameWindow;
            Camera = Cam.GetAddon<Camera>();
        }
        public override void Start()
        {
            base.Start();
            Camera.SetRenderingCamera();
        }
        public override void Update(int frameNumber)
        {
            base.Update(frameNumber);
            Text.Text = string.Empty;
            foreach (GameObject g in GWTrack.activeGameObjects)
            {
                Text.Text += $"\n{g}";
            }
        }
        public override void KeyPressEvent(Keys k, KeyPressType kt)
        {
            base.KeyPressEvent(k, kt);
            if (k == Keys.S)
            {
                Camera.GameObject.Position -= new Vector2(0, -10);
            }
        }
    }
}
