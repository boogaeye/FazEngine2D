using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes
{
    using FazEngine2D.Extentions;
    using FazEngine2D.Classes.Addons;
    using FazEngine2D.Attributes;
    [NonAddonAdder]
    public abstract class PresetWindow : PreloadedObject
    {
        public List<GameObject> gameObjects { get; } = new List<GameObject>();
        public Task CreateObjects(FazEngineWindow gameWindow)
        {
            foreach (GameObject g in gameObjects)
            {
                CreateObject(g, gameWindow);
                this.Log(g.Name);
            }
            Load(gameWindow);
            return Task.CompletedTask;
        }
        public abstract void Load(FazEngineWindow gameWindow);
        void CreateObject(GameObject obj, FazEngineWindow gameWindow)
        {
            if (obj.GetType() == typeof(FazEngineWindow))
            {
                this.Warn("Spawning a game window isnt allowed in preset windows use a script to do that");
                return;
            }
            new GameObject(obj.Name, obj.Addons, gameWindow);
            this.Log($"Creating {obj.Name} and sending it to {gameWindow.Name}");
        }
        public GameObject FindGameObject(string name)
        {
            return gameObjects.Where(e => e.Name == name).FirstOrDefault();
        }
    }
    public class GameObjectAlreadyExistsException : Exception
    {
        public GameObjectAlreadyExistsException(string message) : base(message)
        {

        }
    }
}
