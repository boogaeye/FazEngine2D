using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FazEngine2D.Classes
{
    using FazEngine2D.Extentions;
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Core;
    using System.Drawing;
    using System.Threading;

    public class GameWindow : GameObject
    {
        WindowHelper Window;
        public List<GameObject> activeGameObjects = new List<GameObject>();
        Thread GameLoop = null;
        Thread AppLoop = null;
        public Camera CurrentRenderingCamera = null;
        public bool IsChangingScene { get; private set; } = false;
        public GameWindow()
        {
            Name = "GameWindow";
            OgObj = this;
            Scene = this;
            activeGameObjects.Add(this);
            FazEngine2D.Core.EngineInstance.Windows.Add(this);
            IsChangingScene = true;
            StartUpMethod();
        }
        public void ChangeFormScene(WindowHelper form)
        {
            return;
            IsChangingScene = true;
            //Window.Close();
            //Window = form;
            Task.Run(() => FormStarter());
        }

        public void StartUpMethod()
        {
            
            //Task.Run(() => UpdateLoop());
            AppLoop = new Thread(FormStarter);
            AppLoop.Start();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (IsChangingScene) return;
            var gh = new List<GameObject>(activeGameObjects);
            foreach (GameObject g in gh)
            {
                g.Dispose();
            }
            Core.EngineInstance.Windows.Remove(this);
            if (Core.EngineInstance.Windows.Count == 0)
            {
                Console.Title = "Close?";
                this.Log("GoodBye World");
            }
            Window.FormClosed -= Window_Closed;
            Window.Paint -= Window_Paint;
            Window.Move -= Window_Move;
            GameLoop.Abort();
        }


        public Task StartUpdate()
        {
            try
            {
                foreach (GameObject g in activeGameObjects)
                {
                    this.Log(g.Name);
                    foreach (Script addon in g.GetAddons<Script>())
                    {
                        addon.Log(addon.Name);
                        try
                        {
                            if (!addon.Disposed)
                                addon.Start();
                        }
                        catch (Exception e)
                        {
                            Debug.Error(e);
                        }
                    }
                    //foreach (SpriteRenderObject addon in g.GetAddons<SpriteRenderObject>())
                    //{
                    //    try
                    //    {
                    //        if (!addon.Disposed)
                    //        {
                    //            addon.Start((PictureBox)Form.Controls[0]);
                    //        }
                    //    }
                    //    catch (Exception e)
                    //    {
                    //        Debug.Error(e);
                    //    }
                    //}
                }
            } catch (Exception e)
            {
                Debug.Log(e.Message);
            }
            this.Log("Update Starter Started");
            return Task.CompletedTask;
        }
        public void FormStarter()
        {
            Window = new WindowHelper();
            GameLoop = new Thread(UpdateLoop);
            GameLoop.Start();
            this.Log("Form Started");
            Window.FormClosed += Window_Closed;
            Window.Paint += Window_Paint;
            Window.Move += Window_Move;
            IsChangingScene = false;
            
            Application.Run(Window);
            //Application.Run();
        }

        private void Window_Move(object sender, EventArgs e)
        {
            this.Position = new Vector2(Window.Bounds.X, Window.Bounds.Y);
        }

        private void Window_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int wdth = Screen.PrimaryScreen.Bounds.Width;
            int hight = Screen.PrimaryScreen.Bounds.Height;
            float offsetX;
            float offsetY;
            if (CurrentRenderingCamera == null)
            {
                offsetX = 0;
                offsetY = 0;
            }
            else
            {
                offsetX = CurrentRenderingCamera.GameObject.Position.X;
                offsetY = CurrentRenderingCamera.GameObject.Position.Y;
            }
            foreach (GameObject ig in activeGameObjects)
            {
                foreach (SpriteRenderObject sp in ig.GetAddons<SpriteRenderObject>())
                {
                    if (sp.Image != null)
                    g.DrawImage(sp.Image, (ig.Position.X * Window.Width / wdth) + (offsetX * -1), (ig.Position.Y * Window.Height / hight) + (offsetY * -1), sp.Width * Window.Width / wdth, sp.Height * Window.Height / hight);
                    
                }
            }
            if (EngineInstance.EngineDebug)
            Debug.Log("Window Painted");
        }

        public void UpdateLoop()
        {
            this.Log("Starting Update");
            Task.Run(() => StartUpdate());
            int Frame = 0;
            while (GameLoop.IsAlive)
            {
                
                Window.BeginInvoke((MethodInvoker)delegate{ if (Window.Handle != null) Window.Refresh(); Window.Bounds = new Rectangle((int)this.Position.X, (int)this.Position.Y, Window.Width, Window.Height); });
                Thread.Sleep(1);
                if (FazEngine2D.Core.EngineInstance.EngineDebug)
                {
                    this.Log($"Window Update Frame {Frame}");
                }
                foreach (GameObject g in activeGameObjects)
                {
                    try
                    {
                        foreach (Script addon in g.GetAddons<Script>())
                        {
                            try
                            {
                                if (!addon.Disposed)
                                    addon.Update();
                            }
                            catch (Exception e)
                            {
                                Debug.Error(e);
                            }

                        }
                        foreach (SpriteRenderObject addon in g.GetAddons<SpriteRenderObject>())
                        {
                            try
                            {
                                if (!addon.Disposed)
                                    addon.Update();
                            }
                            catch (Exception e)
                            {
                                Debug.Error(e);
                            }
                        }
                    }catch(Exception e)
                    {
                        Debug.Log(e.Message);
                    }
                    
                }
                Frame++;
            }
        }
    }
}
