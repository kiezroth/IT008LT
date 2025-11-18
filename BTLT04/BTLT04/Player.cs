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
        public Dictionary<PlayerState, AnimatedSprite> Sprites;
        public float Speed = 5;
        public float shootCooldown = 1000f; // 1000 ms = 1 giây
        public float shootTimer = 0f;

        public Player(float x, float y)
        {
            Width = 64;
            Height = 96;
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
                X -= Speed;
                if (currentSprite != Sprites[PlayerState.RunLeft])
                    currentSprite = Sprites[PlayerState.RunLeft];
                currentSprite.NextFrame();
            }
            else
            if (Input.Right)
            {
                X += Speed;currentSprite.NextFrame();
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

            currentSprite.Draw(g, X - 16, Y, 96, 96);
        }

        public FireBall Shoot(myGame form)
        {
            float startX = X;
            float startY = Y;
            PlayerDirection dir;

            switch (currentSprite)
            {
                case var s when s == Sprites[PlayerState.RunLeft] || s == Sprites[PlayerState.IdleLeft]:
                    startX -= Width;
                    startY += Height/4;
                    dir = PlayerDirection.Left; break;
                case var s when s == Sprites[PlayerState.RunRight] || s == Sprites[PlayerState.IdleRight]:
                    startX += Width / 2;
                    startY += Height/4;
                    dir = PlayerDirection.Right; break;
                case var s when s == Sprites[PlayerState.RunUp] || s == Sprites[PlayerState.IdleUp]:
                    startY -= Height / 2;
                    dir = PlayerDirection.Up; break;
                default:
                    dir = PlayerDirection.Down;
                    startY += Height / 2;
                    break;
            }
            shootTimer = shootCooldown;
            return new FireBall(startX,startY, dir, form);
        }

    }

}