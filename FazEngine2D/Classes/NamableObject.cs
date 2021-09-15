using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes
{
    public abstract class NamableObject : object, IDisposable
    {
        public string Name { get; set; } = string.Empty;
        public object OgObj { get; set; } = new object();

        public virtual void Dispose()
        {
            Name = null;
            OgObj = null;
        }
    }
}
