using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLT04
{

    enum PlayerDirection
    {
        Left,
        Right,
        Up,
        Down
    }


    class FireBall : Entity
    {
        public float Speed { get; set; } = 16f;
        public PlayerDirection Dir { get; set; }

        private AnimatedSprite Sprite { get; set; }

        private myGame game;


        public FireBall(float x, float y, PlayerDirection dir, myGame form) : base(x, y, 100, 100)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            Dir = dir;
            game = form;
            Sprite = new AnimatedSprite(fixPath(@"Assets\Attack\fireball.png"), 128, 128, 2);



        }

        public override void Update()
        {
            switch (Dir)
            {
                case PlayerDirection.Left:
                    X -= Speed;
                    break;
                case PlayerDirection.Right:
                    X += Speed;
                    break;
                case PlayerDirection.Up:
                    Y -= Speed;
                    break;
                case PlayerDirection.Down:
                    Y += Speed;
                    break;
            }

            Sprite.NextFrame();
            if (X < -Width || X > game.ClientSize.Width || Y < -Height || Y > game.ClientSize.Height)
            {
                IsDestroyed = true;
                return;
            }
        }

        public override void Render(Graphics g)
        {
            var state = g.Save();

            if (!IsDestroyed)
            {
                switch (Dir)
                {
                    case PlayerDirection.Right:
                        g.TranslateTransform(X + 30, Y);
                        g.ScaleTransform(-1, 1);  
                        g.TranslateTransform(-Width / 2, -Height / 2);

                        break;
                    case PlayerDirection.Left: // base
                        g.TranslateTransform(X - 30, Y);
                        g.TranslateTransform(-Width / 2, -Height / 2);
                        break;
                    case PlayerDirection.Up:
                        g.TranslateTransform(X, Y - 30);
                        g.RotateTransform(90);    
                        g.TranslateTransform(-Width / 2, -Height / 2);
                        break;

                    case PlayerDirection.Down:
                        g.TranslateTransform(X, Y + 30);
                        g.RotateTransform(-90);   
                        g.TranslateTransform(-Width / 2, -Height / 2);
                        break;
                }
                Sprite.Draw(g, 0, 0, Width, Height);
                g.Restore(state);

            }
        }
    }
}
