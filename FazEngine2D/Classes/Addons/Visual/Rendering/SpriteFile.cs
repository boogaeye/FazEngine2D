using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons.Visual.Rendering
{
    using FazEngine2D.Core;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using FazEngine2D.Extentions;

    public sealed class SpriteFile : PreloadedObject
    {
        public string Location { get; }
        public Image RenderedSprite { get; private set; }
        public List<RectangleF> Animations = new List<RectangleF>();
        public SpriteFile(string PreloadName, string location)
        {
            Name = PreloadName;
            Location = location;
        }
        public override void Dispose()
        {
            base.Dispose();
            Debug.Error("Disposing??");
        }
        public SpriteFile(string PreloadName, string location, IEnumerable<RectangleF> animations)
        {
            Name = PreloadName;
            Location = location;
            Animations = animations.ToList();
            this.Log(this.GetType().Name);
        }
        public string GetLocation()
        {
            return $@"{EngineInstance.SaveLoc}\Sprites\{Location}";
        }
        public Image GetImage()
        {
            if (RenderedSprite == null)
            {
                RenderedSprite = new Bitmap(Image.FromFile(GetLocation()));
                //Name = new FileInfo(GetLocation()).Name;
                this.Log($"Got Object in {GetLocation()}");
            }
            return RenderedSprite;
        }
        public Vector2 TopLeft()
        {
            return Vector2.Zero();
        }
        public Vector2 TopRight()
        {
            return new Vector2(GetImage().Width - Screen.PrimaryScreen.Bounds.X, 0);
        }
        public Vector2 BottomRight()
        {
            return new Vector2(GetImage().Width - Screen.PrimaryScreen.Bounds.X , GetImage().Height  - Screen.PrimaryScreen.Bounds.Y);
        }
        public Vector2 BottomLeft(FazEngineWindow fazEngineWindow)
        {
            return new Vector2(0, GetImage().Height - fazEngineWindow.WinSize.Y);
        }
        public Vector2 BottomMiddle()
        {
            return new Vector2(Screen.PrimaryScreen.Bounds.X / 2, GetImage().Height - Screen.PrimaryScreen.Bounds.Y);
        }
        public Vector2 TopMiddle()
        {
            return new Vector2(Screen.PrimaryScreen.Bounds.X / 2, 0);
        }

        public override void PreloadState()
        {
            this.Log($"This got preloaded? {this}");
        }
    }
}
