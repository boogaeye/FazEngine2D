using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons.Visual.Rendering
{
    using FazEngine2D.Core;
    using System.Drawing;

    public sealed class SpriteFile
    {
        public string Location;
        public string GetLocation()
        {
            return $@"{EngineInstance.SaveLoc}\Sprites\{Location}";
        }
        public Image GetImage()
        {
            return Image.FromFile(GetLocation());
        }
    }
}
