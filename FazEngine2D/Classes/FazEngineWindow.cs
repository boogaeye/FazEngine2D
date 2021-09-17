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
    using FazEngine2D.Classes.Addons.Visual;

    public class FazEngineWindow : GameObject
    {
        public List<GameObject> gameObjects = new List<GameObject>();
        Form Form;
        public Vector2 WinSize { get => new Vector2(Form.Width, Form.Height); }
        Thread GameLoop, FormLoop;
        Dictionary<Action, double> ScheduledForNextUpdate = new Dictionary<Action, double>();
        bool allowUpdate = true;
        public Camera Camera { get; set; }
        public double FramesAlive { get; private set; }
        public double StrayFrames { get; private set; }
        public double ForceResets { get; private set; }
        public int frameTime = 1;
        bool WindowWarns = false, ForcefulTransforms = true;
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
        /// <summary>
        /// Forces an update to the window use this with caution though....
        /// </summary>
        public void ForceUpdate()
        {
            ForceResets++;
            allowUpdate = true;
        }
        /// <summary>
        /// Sets the camera to render for this window
        /// </summary>
        /// <param name="camera"></param>
        /// <returns>Rendering camera</returns>
        public Camera SetCamera(Camera camera)
        {
            return Camera = camera;
        }
        /// <summary>
        /// Purge this window and its gameobjects
        /// </summary>
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
            if (Form.Created)
            Form.BeginInvoke((MethodInvoker)delegate { Form.Close(); });
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
            Transform.Scale = WinSize;
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
            Form.Move += Form_Move;
            Form.KeyDown += Form_KeyDown;
            Form.KeyUp += Form_KeyUp;
            GameLoop = new Thread(FrameUpdate);
            GameLoop.Start();
            GameLoop.Name = $"{Name}GameLoop";
            Application.Run(Form);
        }

        private void Form_Move(object sender, EventArgs e)
        {
            Transform.Position = new Vector2(Form.Location.X, Form.Location.Y);
        }

        private void Form_KeyUp(object sender, KeyEventArgs e)
        {
            foreach (GameObject gameObject in new List<GameObject>(gameObjects))
            {
                foreach (Script script in gameObject.GetAddons<Script>())
                {
                    script.KeyPressEvent(e.KeyCode, Input.KeyPressType.Up);
                }
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (GameObject gameObject in new List<GameObject>(gameObjects))
            {
                foreach (Script script in gameObject.GetAddons<Script>())
                {
                    script.KeyPressEvent(e.KeyCode, Input.KeyPressType.Down);
                }
            }
        }

        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            Transform.Scale = WinSize;
            Transform.Position = new Vector2(Form.Location.X, Form.Location.Y);
            ForcefulTransforms = true;
        }

        private void Form_ResizeBegin(object sender, EventArgs e)
        {
            ForcefulTransforms = false;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form.Paint -= Form_Paint;
            Form.FormClosing -= Form_FormClosing;
            Form.ResizeEnd -= Form_ResizeEnd;
            Form.ResizeBegin -= Form_ResizeBegin;
            Form.Move -= Form_Move;
            Form.KeyDown -= Form_KeyDown;
            Form.KeyUp -= Form_KeyUp;
            this.Log("Exiting Application...");
            GameLoop.Abort();
            Dispose();
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            float wdth = Screen.PrimaryScreen.Bounds.Width;
            float hight = Screen.PrimaryScreen.Bounds.Height;
            //float ratio = wdth / hight;
            float ratio = 1;
            float ratioWidth = Form.Width / wdth;
            float ratioHeight = Form.Height / hight;
            Form.Text = Name;
            if (ForcefulTransforms)
            {
                Form.Location = new Point((int)Transform.Position.X, (int)Transform.Position.Y);
                Form.Width = (int)Transform.Scale.X;
                Form.Height = (int)Transform.Scale.Y;
            }
            if (Camera != null && !Camera.CanRenderCamera(this))
            {
                Camera = null;
                this.Warn("Cant render this camera maybe you tried to render a scene this gameobject wasnt in\nEither way try to fix this...");
            }
            if (Camera != null)
            {
                float CameraX = Camera.GameObject.Transform.Position.X;
                float CameraY = Camera.GameObject.Transform.Position.Y;
                foreach (GameObject game in new List<GameObject>(gameObjects))
                {
                    if (game.GetAddon<SpriteRenderObject>() != null)
                    {
                        if (game.GetAddon<SpriteRenderObject>().Image != null)
                            g.DrawImage(game.GetAddon<SpriteRenderObject>().Image, new RectangleF(((game.Transform.Position.X - CameraX) * ratio), ((game.Transform.Position.Y - CameraY) * ratio), ((game.GetAddon<SpriteRenderObject>().Width * game.Transform.Scale.X) * Camera.GameObject.Transform.Scale.X * ratioWidth), ((game.GetAddon<SpriteRenderObject>().Height * game.Transform.Scale.Y) * Camera.GameObject.Transform.Scale.Y * ratioHeight)), new RectangleF(game.GetAddon<SpriteRenderObject>().XCrop, game.GetAddon<SpriteRenderObject>().YCrop, game.GetAddon<SpriteRenderObject>().CropWidth, game.GetAddon<SpriteRenderObject>().CropHeight), GraphicsUnit.Pixel);

                    }
                    if (game.GetAddon<TextRenderObject>() != null)
                    {
                        if (game.GetAddon<TextRenderObject>() != null)
                            g.DrawString(game.GetAddon<TextRenderObject>().Text, new Font(FontFamily.GenericMonospace, game.GetAddon<TextRenderObject>().FontSize, FontStyle.Regular), game.GetAddon<TextRenderObject>().TextColor, ((game.Transform.Position.X - CameraX) * ratio), ((game.Transform.Position.Y - CameraY) * ratio));

                    }
                }
            }
            allowUpdate = true;
        }
        /// <summary>
        /// Schedules a task to invoke next Frame Update
        /// </summary>
        /// <param name="task">Task to be invoked</param>
        public void ScheduleForNextUpdate(Action task)
        {
            ScheduledForNextUpdate.Add(task, FramesAlive + 2);
        }
        /// <summary>
        /// Schedules a task to invoke next Frame Update
        /// </summary>
        /// <param name="framesWaiting">How Many Frames Wait Until Invoked</param>
        /// <param name="task">Task to be Invoked</param>
        public void ScheduleForNextUpdate(double framesWaiting, Action task)
        {
            ScheduledForNextUpdate.Add(task, FramesAlive + framesWaiting);
        }

        void FrameUpdate()
        {
            while (GameLoop.IsAlive)
            {
                Thread.Sleep((int)Math.Round(frameTime * FrameSafeTime));
                StrayFrames++;
                if (StrayFrames > 30 && Form.WindowState != FormWindowState.Minimized)
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
                    try
                    {
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
                    }
                    catch (Exception)
                    {
                        this.Warn("Somethings wrong here...");
                    }
                    
                    
                    allowUpdate = false;
                    if (FramesAlive % 2 == 1)
                    {
                        Thread.Sleep(1);
                        foreach (GameObject g in gameObjects)
                        {
                            foreach (Addon a in g.Addons)
                            {
                                a.CallFunctionsBasedOnValue(3);
                            }
                        }
                    }
                    try
                    {
                        foreach (KeyValuePair<Action, double> task in new Dictionary<Action, double>(ScheduledForNextUpdate))
                        {
                            try
                            {
                                if (FramesAlive >= task.Value)
                                    task.Key.Invoke();
                            }
                            catch (Exception e)
                            {
                                this.Error($"{e.Message}\n{e.StackTrace}\nCouldnt Run Action Next Update....");
                            }
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        if(WindowWarns)
                        this.Warn("That was too fast... we will try these operations again then...");
                    }
                    ScheduledForNextUpdate.Clear();
                }
            }
        }
    }
}
