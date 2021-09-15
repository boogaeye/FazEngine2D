using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FazEngine2D.Classes
{
    using System.Windows.Forms;
    public sealed class Vector2
    {
        public float X;
        public float Y;
        public static Vector2 operator -(Vector2 a, Vector2 b) => new Vector2(a.X - b.X, a.Y - b.Y);
        public static Vector2 operator +(Vector2 a, Vector2 b) => new Vector2(a.X + b.X, a.Y + b.Y);
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
        public static Vector2 Zero()
        {
            return new Vector2(0, 0);
        }
        public static Vector2 MiddleScreen()
        {
            return new Vector2(Screen.PrimaryScreen.Bounds.Width / 2, Screen.PrimaryScreen.Bounds.Height / 2);
        }
        public static Vector2 MiddleGameWindow(FazEngineWindow gameWindow)
        {
            return new Vector2(gameWindow.Transform.Scale.X / 2, gameWindow.Transform.Scale.Y / 2);
        }
        public override string ToString()
        {
            return $"Vec2[{X}, {Y}]";
        }
    }
}
