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
    [OnlyAddableTo(typeof(GameObject))]
    public sealed class TextRenderObject : Addon
    {
        public string Text;
        public int Width;
        public int Height;
        public Brush TextColor = Brushes.Black;
        public int FontSize = 10;


        public void SetText(string test)
        {
            Text = test;
        }
        public override void Dispose()
        {
            base.Dispose();
            Text = null;
        }

        public override void CallFunctionsBasedOnValue(byte b)
        {
            return;
        }
    }
}
