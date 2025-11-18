using System;
using System.Drawing;

namespace BTLT04
{
    class Monster : Entity
    {
        public AnimatedSprite Sprite;
        public MonsterType type;
        public enum MonsterType
        {
            slime1,
            slime2,
            slime3,
        }
        public Monster(float x, float y, MonsterType type)
            : base(x, y, 64, 64)
        {
            this.type = type;
            switch (type)
            {
               case MonsterType.slime1:
                    Sprite = new AnimatedSprite(fixPath(@"Assets\Monster\Slime1\Idle\Slime1_Idle_body.png"), 64, 64, 6);
                    break;
                case MonsterType.slime2:
                    Sprite = new AnimatedSprite(fixPath(@"Assets\Monster\Slime2\Idle\Slime2_Idle_body.png"), 64, 64, 6);
                    break;
                case MonsterType.slime3:
                    Sprite = new AnimatedSprite(fixPath(@"Assets\Monster\Slime3\Idle\Slime3_Idle_body.png"), 64, 64, 6);
                    break;

            }
        }

        public override void Update()
        {
            Sprite.NextFrame();
        }

        public override void Render(Graphics g)
        {
            Sprite.Draw(g, X - Width/2, Y - Height/2, Width*2, Height*2);
        }
    }
}
