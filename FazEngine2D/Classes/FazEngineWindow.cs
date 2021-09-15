using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes
{
    using FazEngine2D.Extentions;
    using FazEngine2D.Classes.Addons;
    using System.Threading;
    using System.Windows.Forms;
    using System.Drawing;
    using FazEngine2D.Classes.Addons.Visual.Rendering;
    using FazEngine2D.Core;

    public class FazEngineWindow : GameObject
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        Form Form;
        Thread GameLoop, FormLoop;
        bool allowUpdate = true;
        public double FramesAlive { get; private set; }
        public double StrayFrames { get; private set; }
        public double ForceResets { get; private set; }
        public int frameTime = 1;
        bool WindowWarns = false;
        public float FrameSafeTime 
        {
            get
            {
                if (StrayFrames > 0 && FramesAlive > 0)
                {
                    return (float)(StrayFrames / FramesAlive) + 1;
                }
                else
                {
                    return 1;
                }
            }
        }
        public new void Dispose()
        {
            foreach (Addon addon in new List<Addon>(Addons))
            {
                addon.Dispose();
            }
            foreach (GameObject gameObject in new List<GameObject>(gameObjects))
            {
                if (gameObject != this)
                gameObject.Dispose();
            }
            gameObjects.Remove(this);
            
        }
        public FazEngineWindow(string name = "Game Window", KnownColor color = KnownColor.Black, bool windowWarn = true)
        {
            Name = name;
            FazEngineWindow = this;
            Form = new WindowHelper();
            Form.BackColor = Color.FromKnownColor(color);
            WindowWarns = windowWarn;
            EngineInstance.FazEngineWindows.Add(this);
            gameObjects.Add(this);
            this.Log("I got created...");
            StartUp();
        }
        void StartUp()
        {
            this.Log("Starting up...");
            FormLoop = new Thread(StartForm);
            FormLoop.Start();
            FormLoop.Name = $"{Name}FormLoop";
        }
        void StartForm()
        {
            Form.Paint += Form_Paint;
            Form.FormClosing += Form_FormClosing;
            Form.ResizeBegin += Form_ResizeBegin;
            Form.ResizeEnd += Form_ResizeEnd;
            Form.KeyDown += Form_KeyDown;
            GameLoop = new Thread(FrameUpdate);
            GameLoop.Start();
            GameLoop.Name = $"{Name}GameLoop";
            Application.Run(Form);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (GameObject g in gameObjects)
            {
                foreach (Script script in g.GetAddons<Script>())
                {
                    script.KeyPressEvent(e.KeyCode, Input.KeyPressType.Down);
                }
            }
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            
        }

        private void Form_ResizeBegin(object sender, EventArgs e)
        {
            
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form.Paint -= Form_Paint;
            Form.FormClosing -= Form_FormClosing;
            Form.KeyDown -= Form_KeyDown;
            this.Log("Exiting Application...");
            GameLoop.Abort();
            Dispose();
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            float wdth = Screen.PrimaryScreen.Bounds.Width;
            float hight = Screen.PrimaryScreen.Bounds.Height;
            float ratio = wdth / hight;
            float ratioWidth = Form.Width / wdth;
            float ratioHeight = Form.Height / hight;
            Form.Text = Name;
            foreach (GameObject game in new List<GameObject>(gameObjects))
            {
                if (game.GetAddon<SpriteRenderObject>() != null)
                {
                    if (game.GetAddon<SpriteRenderObject>().Image != null)
                        g.DrawImage(game.GetAddon<SpriteRenderObject>().Image, new RectangleF((game.Transform.Position.X * ratio), (game.Transform.Position.Y * ratio), (game.GetAddon<SpriteRenderObject>().Width * ratioWidth), (game.GetAddon<SpriteRenderObject>().Height * ratioHeight)), new RectangleF(game.GetAddon<SpriteRenderObject>().XCrop, game.GetAddon<SpriteRenderObject>().YCrop, game.GetAddon<SpriteRenderObject>().CropWidth, game.GetAddon<SpriteRenderObject>().CropHeight), GraphicsUnit.Pixel);

                }
                if (game.GetAddon<TextRenderObject>() != null)
                {
                    if (game.GetAddon<TextRenderObject>() != null)
                        g.DrawString(game.GetAddon<TextRenderObject>().Text, new Font(FontFamily.GenericMonospace, 12, FontStyle.Regular), Brushes.Black, (game.Transform.Position.X), (game.Transform.Position.Y));

                }
            }
            allowUpdate = true;
        }

        void FrameUpdate()
        {
            while (GameLoop.IsAlive)
            {
                Thread.Sleep((int)Math.Round(frameTime * FrameSafeTime));
                StrayFrames++;
                if (StrayFrames > 30)
                {
                    if (WindowWarns)
                    this.Warn($"Stray Frames are over 30????\nFrame {FramesAlive}\nFrame SafeTime: {FrameSafeTime}\nStray Frames {StrayFrames}\nForcefully Allowing Window Update {ForceResets}...");
                    allowUpdate = true;
                    ForceResets++;
                }
                if (allowUpdate)
                {
                    FramesAlive++;
                    StrayFrames = 0;
                    if (EngineInstance.EngineDebug)
                    this.Log("Stray Frames Reset");
                    if (FramesAlive == Double.MaxValue)
                    {
                        FramesAlive = 2;
                    }
                    if (FramesAlive == 1)
                    {
                        foreach (GameObject g in gameObjects)
                        {
                            foreach (Addon a in g.Addons)
                            {
                                a.CallFunctionsBasedOnValue(0);
                            }
                        }
                    }
                    else
                    {
                        foreach (GameObject g in gameObjects)
                        {
                            foreach (Addon a in g.Addons)
                            {
                                a.CallFunctionsBasedOnValue(1);
                            }
                        }
                    }
                    if (FormLoop.IsAlive && Form.Created)
                        Form.BeginInvoke((MethodInvoker)delegate { Form.Refresh(); });
                    allowUpdate = false;
                }
            }
        }
    }
}
