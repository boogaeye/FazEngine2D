using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons.Audio
{
    using FazEngine2D.Core;
    public sealed class AudioFile
    {
        public string Location;
        public string GetLocation()
        {
            return $@"{EngineInstance.SaveLoc}\Sounds\{Location}";
        }
    }
}
