using FazEngine2D.Attributes;
using FazEngine2D.Classes;
using FazEngine2D.Classes.Addons;
using FazEngine2D.Classes.Addons.Visual;
using FazEngine2D.Classes.Addons.Visual.Rendering;
using FazEngine2D.Classes.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("[Info]" + debug.ToString());
        }
        public static void Warn(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            if (debug == null) { Console.BackgroundColor = ConsoleColor.Red; return; }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("[Warning]" + debug.ToString());
        }
        public static void Error(object debug, bool Beep = true)
        {
            if (Beep) Console.Beep();
            if (debug == null) { Console.BackgroundColor = ConsoleColor.Red; return; }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[Error]" + debug.ToString());
        }
        public static void Preload(object debug, bool Beep = false)
        {
            if (Beep) Console.Beep();
            if (debug == null) { Console.BackgroundColor = ConsoleColor.Red; return; }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[PreloadedObject]" + debug.ToString());
        }
        public static void DebugWindow(FazEngineWindow gameWindow)
        {
            var gw = new FazEngineWindow(color: KnownColor.Purple, windowWarn: false);
            gw.Name = "Debugging Window";
            var go = new GameObject("TextObj", gw);
            var cam = new GameObject("CameraObj", gw);
            go.AddAddon(new TextRenderObject());
            cam.AddAddon(new Camera());
            TextRenderObject tro = go.GetAddon<TextRenderObject>();
            gw.AddAddon(new DebugWindow(tro, gameWindow, cam));
            //gw.ChangeBGColor(System.Drawing.Color.Purple);
        }

    }
    [OnlyAddableTo(typeof(FazEngineWindow))]
    public sealed class DebugWindow : Script
    {
        public TextRenderObject Text;
        public FazEngineWindow GWTrack;
        public Camera Camera;
        public DebugWindow(TextRenderObject w, FazEngineWindow gameWindow, GameObject Cam)
        {
            Cam.Dispose();
            Text = w;
            GWTrack = gameWindow;
            //Camera = Cam.GetAddon<Camera>();
        }
        public override void Start()
        {
            base.Start();
            //Camera.SetRenderingCamera();
        }
        public override void Update()
        {
            base.Update();
            Text.Text = $"Frame SafeTime: {GWTrack.FrameSafeTime}\nStray Frames: {GWTrack.StrayFrames}\nFrames: {GWTrack.FramesAlive}\nForceResets: {GWTrack.ForceResets}";
            foreach (PreloadedObject preloadedObject in EnginePreloader.preloadedObjects)
            {
                Text.Text += $"\n{preloadedObject}";
            }
            foreach (GameObject g in GWTrack.gameObjects)
            {
                Text.Text += $"\n{g}";
            }
        }
        public override void KeyPressEvent(Keys k, KeyPressType kt)
        {
            base.KeyPressEvent(k, kt);
            if (k == Keys.S)
            {
                Camera.GameObject.Transform.Position -= new Vector2(0, -10);
            }
        }
    }
}
