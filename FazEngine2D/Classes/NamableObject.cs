using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes
{
    public abstract class NamableObject : object, IDisposable
    {
        /// <summary>
        /// Name Of An Object
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Original Type of an object
        /// </summary>
        public object OgObj { get; set; } = new object();

        public virtual void Dispose()
        {
            Name = null;
            OgObj = null;
        }
    }
}
