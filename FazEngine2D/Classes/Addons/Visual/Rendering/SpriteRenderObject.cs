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
    using FazEngine2D.Attributes;
    using FazEngine2D.Extentions;
    [OnlyAddableTo(typeof(GameObject))]
    public sealed class SpriteRenderObject : Addon
    {
        public Image Image;
        public SpriteFile Sprite;
        public float Width;
        public float Height;
        public float CropWidth;
        public float XCrop = 0;
        public float CropHeight;
        public float YCrop = 0;
        public void SetImageFromFile(SpriteFile sp)
        {
            this.Log($"Searching for " + sp.GetLocation());
            try
            {
                Image = sp.GetImage();
                if (Image != null)
                {
                    Width = Image.Width;
                    CropWidth = Image.Width;
                    Height = Image.Height;
                    CropHeight = Image.Height;
                    XCrop = 0;
                    YCrop = 0;
                    this.Log($"Image Got! {Width} : {Height}");
                    Sprite = sp;
                    this.Log(Sprite.Name);
                }
                else
                {
                    this.Log($"Your image is null buddy...");
                }
            }
            catch (FileNotFoundException e)
            {
                this.Error($"File Not Found 404\n{e.Message}");
            }
        }
        public void ChangeImageAnimation(int AnimationNumber)
        {
            if (Image != null)
            {
                if (Sprite != null)
                {
                    CropWidth = Sprite.Animations[AnimationNumber].Width;
                    XCrop = Sprite.Animations[AnimationNumber].X;
                    CropHeight = Sprite.Animations[AnimationNumber].Height;
                    YCrop = Sprite.Animations[AnimationNumber].Y;
                }
                else
                {
                    this.Error("Sprite File is not set....");
                }
            }
            else
            {
                this.Error("Image is not set...");
            }
        }
        public void SetImageFromFile(SpriteFile sp, int AnimationNumber)
        {
            if (sp != null)
            {
                this.Log("Sprite Exists");
                this.Log(sp.Name);
            }
            else
            {
                this.Error("sprite is null....");
            }
            this.Log($"Searching for " + sp.GetLocation());
            try
            {
                Image = sp.GetImage();
                if (Image != null)
                {
                    Width = Image.Width;
                    
                    Height = Image.Height;
                    CropWidth = sp.Animations[AnimationNumber].Width;
                    XCrop = sp.Animations[AnimationNumber].X;
                    CropHeight = sp.Animations[AnimationNumber].Height;
                    YCrop = sp.Animations[AnimationNumber].Y;
                    this.Log($"Image Got! {Width} : {Height}");
                    Sprite = sp;
                    this.Log(Sprite.Name);
                }
                else
                {
                    this.Log($"Your image is null buddy...");
                }
            }
            catch (FileNotFoundException e)
            {
                this.Error($"File Not Found 404\n{e.Message}");
            }
        }
        public void Scale(float scale)
        {
            Width *= scale;
            Height *= scale;
        }
        public override void Dispose()
        {
            base.Dispose();
            Image = null;
        }

        

        public override void CallFunctionsBasedOnValue(byte b)
        {
            
        }
    }
}
