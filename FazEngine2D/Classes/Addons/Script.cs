using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons
{
    using FazEngine2D.Extentions;
    public class Script : Addon
    {
        public virtual void Start()
        {
            this.Log(" Has started");
        }
        public virtual void Update()
        {

        }
    }
}
