using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLT04
{
    class Player : Entity
    {
        public AnimatedSprite Sprite;
        public float Speed = 5;
        public int Facing = 1; // 1: phải, -1: trái

        public Player(float x, float y)
        {
            Width = Height = 96;
            X = x;
            Y = y;
            Sprite = new AnimatedSprite(fixPath(@"Assets\Character\Idle\SorceressDownIdle.png"), 48,48,6);
        }

        public override void Update()
        {
            Sprite.NextFrame();
            if (Input.Left) { X -= Speed; Facing = -1; Sprite.NextFrame(); }
            if (Input.Right) { X += Speed; Facing = 1; Sprite.NextFrame(); }
            if (Input.Up) { Y -= Speed; Sprite.NextFrame(); }
            if (Input.Down) { Y += Speed; Sprite.NextFrame(); }
        }

        public override void Render(Graphics g)
        {
            Sprite.Draw(g, X, Y,Width,Height);
        }
    }
}
