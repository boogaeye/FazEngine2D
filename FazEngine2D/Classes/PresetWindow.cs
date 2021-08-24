using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes
{
    using FazEngine2D.Core;
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Attributes;
    [NonAddonAdder]
    public abstract class PresetWindow : GameObject
    {
        public List<GameObject> gameObjects { get; } = new List<GameObject>();
        public abstract void Load(GameWindow gameWindow);
        public void CreateObject(GameObject obj, GameWindow gameWindow)
        {
            if (obj.GetType() == typeof(GameWindow))
            {
                Debug.Warn("Spawning a game window isnt allowed in present windows use a script to do that");
                return;
            }
            new GameObject(obj.Name, obj.Addons, gameWindow);
        }
    }
    public class GameObjectAlreadyExistsException : Exception
    {
        public GameObjectAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
