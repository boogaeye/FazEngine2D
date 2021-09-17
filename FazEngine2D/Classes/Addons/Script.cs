using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons
{
    using FazEngine2D.Classes.Addons.Visual.Rendering;
    using FazEngine2D.Classes.Input;
    using FazEngine2D.Extentions;
    using System.Windows.Forms;

    public class Script : Addon
    {
        /// <summary>
        /// Gets called at Frame 0 of a window or when this object is instantiated
        /// </summary>
        public virtual void Start()
        {
            this.Log("Has started");
        }
        /// <summary>
        /// Updates Every Frame As Long As this Addon is Active
        /// </summary>
        public virtual void Update()
        {

        }
        /// <summary>
        /// Gets called on events where the client presses their keys in the game window specified
        /// </summary>
        /// <param name="k">Key Pressed</param>
        /// <param name="kt">Key Pressed Type</param>
        public virtual void KeyPressEvent(Keys k, KeyPressType kt)
        {
            
        }
        
        public sealed override void CallFunctionsBasedOnValue(byte b)
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
        /// <summary>
        /// Copies a GameObject
        /// </summary>
        /// <param name="game">Game Object to be copied</param>
        /// <param name="Location">Location of where it spawns</param>
        /// <returns>The Copied GameObject</returns>
        public GameObject Instantiate(GameObject game, Vector2 Location = null)
        {
            var copied = new GameObject($"{game.Name}(1)", game.FazEngineWindow);
            if (Location == null)
                Location = Vector2.Zero();
            foreach (Addon addon in game.Addons)
            {
                
                var newerAddon = Activator.CreateInstance(addon.GetType());
                copied.AddAddon((Addon)newerAddon);
                ((Addon)newerAddon).CallFunctionsBasedOnValue(0);
                if (addon.GetType() == typeof(SpriteRenderObject))
                {
                    ((SpriteRenderObject)newerAddon).SetImageFromFile(((SpriteRenderObject)addon).Sprite);
                }
            }
            copied.Transform.Position = Location;
            return copied;
        }
    }
}
