using FazEngine2D.Classes.Addons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Attributes
{
    public class OnlyAddableTo : Attribute
    {
        public Type Addables;
        public OnlyAddableTo(Type a)
        {
            Addables = a;
        }
    }
}
