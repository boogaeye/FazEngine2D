using FazEngine2D.Classes.Addons.Visual.Rendering;
using FazEngine2D.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes.Addons.Visual
{
    public class Collider2D : Addon
    {
        public List<Vector2> vector2s;
        public float Length = 10;
        public float Height = 10;
        
        public bool CollisionDetected(Vector2 destination)
        {
            Debug.Log(destination);
            foreach (GameObject fw in GameObject.FazEngineWindow.gameObjects)
            {
                if (fw != GameObject) 
                {
                    if (fw.GetAddon<Collider2D>() != null)
                    {
                        Debug.Log(fw.Name);
                        Debug.Log($"{fw.GetAddon<Collider2D>().GameObject.Transform.Position.X} <= {destination.X} <= {fw.GetAddon<Collider2D>().Length + fw.GetAddon<Collider2D>().GameObject.Transform.Position.X} {fw.GetAddon<Collider2D>().GameObject.Transform.Position.X <= destination.X && destination.X <= fw.GetAddon<Collider2D>().Length + fw.GetAddon<Collider2D>().GameObject.Transform.Position.X}");
                        if (fw.GetAddon<Collider2D>().GameObject.Transform.Position.X <= destination.X && destination.X <= fw.GetAddon<Collider2D>().Length + fw.GetAddon<Collider2D>().GameObject.Transform.Position.X)
                        {
                            if (fw.GetAddon<Collider2D>().GameObject.Transform.Position.Y <= destination.Y && destination.Y <= fw.GetAddon<Collider2D>().Height + fw.GetAddon<Collider2D>().GameObject.Transform.Position.Y)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public void UpdateHitDetection()
        {
            
        }
        public override void CallFunctionsBasedOnValue(byte b)
        {
            switch (b)
            {
                case 1:
                    UpdateHitDetection();
                    break;
            }
        }
    }
}
