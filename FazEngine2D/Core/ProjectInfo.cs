using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Core
{
    using FazEngine2D.Classes.Addons;
    public abstract class ProjectInfo
    {
        public abstract string Name { get; set; }
        public abstract string SavLoc { get; set; }
        public abstract List<PreloadedObject> PreloadedObjects { get; }
        public static ProjectInfo Inst;
        public void GetInst()
        {
            Inst = this;
        }
        public abstract void MainStartUp(string[] args);
        public virtual Task Start()
        {
            Inst = this;
            Debug.Log($"{Name} has Started");
            return Task.CompletedTask;
        }
    }
}
