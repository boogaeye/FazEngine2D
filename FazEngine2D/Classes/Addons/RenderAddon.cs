using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FazEngine2D.Attributes;

namespace FazEngine2D.Classes.Addons
{
    public abstract class RenderAddon : Addon
    {
        public abstract void Update();
        public abstract void Start(PictureBox pictureBox);
    }
}
