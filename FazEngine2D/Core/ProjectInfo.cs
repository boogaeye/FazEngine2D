using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Core
{
    public abstract class ProjectInfo
    {
        public abstract string Name { get; set; }
        public abstract string SavLoc { get; set; }
        public virtual void Start()
        {
            Debug.Log($"{Name} has Started");
        }
    }
}
