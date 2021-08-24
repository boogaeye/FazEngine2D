using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons
{
    using FazEngine2D.Classes.Input;
    using FazEngine2D.Extentions;
    using System.Windows.Forms;

    public class Script : Addon
    {
        public virtual void Start()
        {
            this.Log("Has started");
        }
        public virtual void Update(int frameNumber)
        {

        }
        public virtual void KeyPressEvent(Keys k, KeyPressType kt)
        {
            if (k == Keys.Escape && kt == KeyPressType.Down)
            {
                GameObject.Scene.Dispose();
            }
        }
    }
}
