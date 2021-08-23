using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using FazEngine2D.Core;

namespace FazEngine2D.Classes.Addons
{
    using System.IO;
    public sealed class SpriteRenderObject : RenderAddon
    {
        public Image Image;
        public PictureBox Control;
        public int Width;
        public int Height;
        public void SetImageFromFile(string loc)
        {
            Debug.Log($"Searching for " + $@"{EngineInstance.SaveLoc}\Sprites\{loc}");
            try
            {
                Image = Image.FromFile($@"{EngineInstance.SaveLoc}\Sprites\{loc}");
                if (Image != null)
                {
                    Debug.Log("Image Got!");
                    Width = Image.Width;
                    Height = Image.Height;
                }
            }catch(FileNotFoundException e)
            {
                Debug.Error($"File Not Found 404\n{e.Message}");
            }
        }

        public override void Start(PictureBox pictureBox)
        {
            Control = pictureBox;
            
        }

        public override void Update()
        {
            try
            {
                //if (graphics != null)
                //graphics.DrawImage(Image, new PointF(25, 25));
                if (Control == null) return;
                Control.Image = Image;
            }
            catch(ArgumentNullException e)
            {
                Debug.Error($"Image object is null please use SetImageFromFile(string) to remove this error\n{e.Message}");
            }
        }
    }
}
