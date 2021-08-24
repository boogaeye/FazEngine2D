using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using FazEngine2D.Core;

namespace FazEngine2D.Classes.Addons.Visual.Rendering
{
    using System.IO;
    public sealed class SpriteRenderObject : Addon
    {
        public Image Image;
        public PictureBox Control;
        public int Width;
        public int Height;
        public void SetImageFromFile(SpriteFile sp)
        {
            Debug.Log($"Searching for " + sp.GetLocation());
            try
            {
                Image = sp.GetImage();
                if (Image != null)
                {
                    Debug.Log("Image Got!");
                    Width = Image.Width;
                    Height = Image.Height;
                }
            }
            catch (FileNotFoundException e)
            {
                Debug.Error($"File Not Found 404\n{e.Message}");
            }
        }
    }
}
