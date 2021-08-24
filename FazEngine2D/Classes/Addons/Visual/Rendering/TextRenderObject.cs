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
    public sealed class TextRenderObject : Addon
    {
        public string Text;
        public int Width;
        public int Height;
        public void SetText(string test)
        {
            Text = test;
        }
    }
}
