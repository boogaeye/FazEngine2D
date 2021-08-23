using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes
{
    using System.Windows.Forms;
    public class Vector2
    {
        public float X;
        public float Y;
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }
        public Vector2()
        {
            X = 0;
            Y = 0;
        }
        public Vector2 Zero()
        {
            return new Vector2(0, 0);
        }
        public Vector2 MiddleScreen()
        {
            return new Vector2(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
        }
        public override string ToString()
        {
            return $"Vec2[{X}, {Y}]";
        }
    }
}
