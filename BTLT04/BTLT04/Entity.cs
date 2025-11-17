using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLT04
{
    abstract class Entity
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsDestroyed { get; set; } = false;
        public Entity(float x = 0, float y = 0, int width = 0, int height = 0)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        public string fixPath(string path)
        {
            string result = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\" + @path));
            return result;
        }
        public abstract void Update();
        public abstract void Render(Graphics g);
        public RectangleF GetBounds()
        {
            return new RectangleF(X, Y, Width, Height);
        }
        public bool IsCollidingWith(Entity other)               // Kiểm tra đụng độ
        {
            return this.GetBounds().IntersectsWith(other.GetBounds());
        }
    }
}
