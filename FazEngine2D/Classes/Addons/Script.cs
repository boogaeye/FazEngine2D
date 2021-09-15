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
        public virtual void Update()
        {

        }
        public virtual void KeyPressEvent(Keys k, KeyPressType kt)
        {
            
        }
        public override void CallFunctionsBasedOnValue(byte b)
        {
            switch (b)
            {
                case 0:
                    Start();
                    break;
                case 1:
                    Update();
                    break;
                case 2:
                    Start();
                    Update();
                    break;
            }
        }
    }
}
