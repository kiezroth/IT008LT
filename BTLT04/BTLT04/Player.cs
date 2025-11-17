using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLT04
{
    class Player : Entity
    {
        public AnimatedSprite currentSprite;
        public enum PlayerState
        {
            IdleDown,
            IdleUp,
            IdleLeft,
            IdleRight,
            RunDown,
            RunUp,
            RunLeft,
            RunRight
        }
        Dictionary<PlayerState, AnimatedSprite> Sprites;
        public float Speed = 5;
        public int Facing = 1; // 1: phải, -1: trái
        public float shootCooldown = 1000f; // 1000 ms = 1 giây
        public float shootTimer = 0f;

        public Player(float x, float y)
        {
            Width = Height = 96;
            X = x;
            Y = y;
            Sprites = new Dictionary<PlayerState, AnimatedSprite>
            {
                {
                    PlayerState.IdleDown,
                    new AnimatedSprite(fixPath(@"Assets\Character\Idle\SorceressDownIdle.png"),48, 48, 6)
                },

                {
                    PlayerState.IdleLeft,
                    new AnimatedSprite(fixPath(@"Assets\Character\Idle\SorceressLeftIdle.png"),48, 48, 6)
                },

                {
                    PlayerState.IdleRight,
                    new AnimatedSprite(fixPath(@"Assets\Character\Idle\SorceressRightIdle.png"),48, 48, 6)
                },

                {
                    PlayerState.IdleUp,
                    new AnimatedSprite(fixPath(@"Assets\Character\Idle\SorceressUpIdle.png"),48, 48, 6)
                },

                {
                    PlayerState.RunDown,
                    new AnimatedSprite(fixPath(@"Assets\Character\Run\SorceressDownRun.png"),48, 48, 6)
                },

                {
                    PlayerState.RunLeft,
                    new AnimatedSprite(fixPath(@"Assets\Character\Run\SorceressLeftRun.png"),48, 48, 6)
                },

                {
                    PlayerState.RunRight,
                    new AnimatedSprite(fixPath(@"Assets\Character\Run\SorceressRightRun.png"),48, 48, 6)
                },

                {
                    PlayerState.RunUp,
                    new AnimatedSprite(fixPath(@"Assets\Character\Run\SorceressUpRun.png"),48, 48, 6)
                }
            };
            currentSprite = Sprites[PlayerState.IdleDown];
        }

        public override void Update()
        {
            shootTimer -= 16;
            if (Input.Left)
            {
                X -= Speed; Facing = -1;
                if (currentSprite != Sprites[PlayerState.RunLeft])
                    currentSprite = Sprites[PlayerState.RunLeft];
                currentSprite.NextFrame();
            }
            else
            if (Input.Right)
            {
                X += Speed; Facing = 1; currentSprite.NextFrame();
                if (currentSprite != Sprites[PlayerState.RunRight])
                    currentSprite = Sprites[PlayerState.RunRight];
                currentSprite.NextFrame();
            }
            else
            if (Input.Up)
            {
                Y -= Speed; currentSprite.NextFrame();
                if (currentSprite != Sprites[PlayerState.RunUp])
                    currentSprite = Sprites[PlayerState.RunUp];
                currentSprite.NextFrame();
            }
            else
            if (Input.Down)
            {
                Y += Speed; currentSprite.NextFrame();
                if (currentSprite != Sprites[PlayerState.RunDown])
                    currentSprite = Sprites[PlayerState.RunDown];
                currentSprite.NextFrame();
            }
            else
            {
                if (currentSprite == Sprites[PlayerState.RunLeft])
                    currentSprite = Sprites[PlayerState.IdleLeft];
                else
                if (currentSprite == Sprites[PlayerState.RunRight])
                    currentSprite = Sprites[PlayerState.IdleRight];
                else
                if (currentSprite == Sprites[PlayerState.RunDown])
                    currentSprite = Sprites[PlayerState.IdleDown];
                else
                if (currentSprite == Sprites[PlayerState.RunUp])
                    currentSprite = Sprites[PlayerState.IdleUp];
                currentSprite.NextFrame();
            }
        }


        public override void Render(Graphics g)
        {
            currentSprite.Draw(g, X, Y, Width, Height);
        }

        public FireBall Shoot(myGame form)
        {
            float startX = X + Width / 2;
            float startY = Y + Height / 2;
            PlayerDirection dir;

            switch (currentSprite)
            {
                case var s when s == Sprites[PlayerState.RunLeft] || s == Sprites[PlayerState.IdleLeft]:
                    dir = PlayerDirection.Left; break;
                case var s when s == Sprites[PlayerState.RunRight] || s == Sprites[PlayerState.IdleRight]:
                    dir = PlayerDirection.Right; break;
                case var s when s == Sprites[PlayerState.RunUp] || s == Sprites[PlayerState.IdleUp]:
                    dir = PlayerDirection.Up; break;
                default:
                    dir = PlayerDirection.Down; break;
            }
            shootTimer = shootCooldown;
            return new FireBall(startX, startY, dir, form);
        }

    }

}