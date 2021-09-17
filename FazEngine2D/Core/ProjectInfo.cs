using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Core
{
    using FazEngine2D.Classes.Addons;
    /// <summary>
    /// Project Info useful for starting Projects
    /// </summary>
    public abstract class ProjectInfo
    {
        /// <summary>
        /// Name Of the Project
        /// </summary>
        public abstract string Name { get; set; }
        /// <summary>
        /// Save Location of the project
        /// </summary>
        public abstract string SavLoc { get; set; }
        /// <summary>
        /// Preloaded SpriteFiles and AudioFiles
        /// </summary>
        public abstract List<PreloadedObject> PreloadedObjects { get; }
        [Obsolete]
        public static ProjectInfo Inst;
        [Obsolete]
        public void GetInst()
        {
            Inst = this;
        }
        /// <summary>
        /// Create a new instance of your project that Invokes the Start() Task.
        /// </summary>
        /// <param name="args"></param>
        public abstract void MainStartUp(string[] args);
        public virtual Task Start()
        {
            Debug.Log($"{Name} has Started");
            return Task.CompletedTask;
        }
    }
}
