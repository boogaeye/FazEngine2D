using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FazEngine2D.Classes
{
    using FazEngine2D.Extentions;
    using FazEngine2D.Classes.Addons;
    public class GameWindow
    {
        public Form Form;
        public GameObject GameObject;
        public List<GameObject> activeGameObjects = new List<GameObject>();
        public GameWindow()
        {
            Form = new Form();
            GameObject = new GameObject("GameWindow", this, this);
            FazEngine2D.Core.EngineInstance.Windows.Add(this);
            Task.Run(() => UpdateLoop());
        }

        public void StartUpdate()
        {
            foreach (GameObject g in activeGameObjects)
            {
                foreach (Script addon in g.Addons)
                {
                    addon.Start();
                }
            }
            GameObject.Log("Update Starter Started");
        }

        public async Task UpdateLoop()
        {
            StartUpdate();
            GameObject.Log("Starting Update");
            int Frame = 0;
            while (Frame < int.MaxValue)
            {
                foreach (GameObject g in activeGameObjects)
                {
                    foreach (Script addon in g.Addons)
                    {
                        addon.Update();
                    }
                }
                Frame++;
            }
        }
    }
}
