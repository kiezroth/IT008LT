using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BTLT04
{
    class Monster : Entity
    {
        public AnimatedSprite Sprite;
        public Monster(float x, float y) : base(x, y, 64, 64)
        {
            Width = 64; 
            Height = 64; // hitbox Monster
            //Sprite = new AnimatedSprite(fixPath(@"<Đường dẫn>"), <FrameWidth>, <FrameHeight>, <Số frame 1 hàng>);
        }
        public override void Update() 
        {
            Sprite.NextFrame();
            //Hành động
        }

        public override void Render(Graphics g)
        {
            Sprite.Draw(g, X, Y, Width, Height);
        }
    }
}
