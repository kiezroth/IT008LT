using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLT04
{
    class WorldMap
    {
        public List<Entity> entities = new List<Entity>();
        public MonsterManager monsterManage;
        public Player player;
        public int Width, Height;
        public static myGame form;

        public WorldMap(int w, int h, myGame F)
        {
            form = F;
            Width = w;
            Height = h;
            monsterManage = new MonsterManager(this);
            Player dummy = new Player(0, 0);
            player = new Player(Width / 2f - dummy.Width / 2f, Height / 2f - dummy.Height / 2f);
            entities.Add(player);
        }

        public void Update()
        {
            player.Update();

            if (Input.IsShooting && player.shootTimer <= 0)
                ShootFireBall(form);

            ClampPlayerToWorld();

            monsterManage.Update();

            foreach (var e in entities)
                e.Update();

            entities.RemoveAll(e => e.IsDestroyed);
        }
        public void Render(Graphics g)
        {
            foreach (var e in entities)
                e.Render(g);
        }
        private void ClampPlayerToWorld()
        {
            int maxX = Width - player.Width;
            int maxY = Height - player.Height;
            if (player.X < 0) player.X = 0;
            if (player.Y < 0) player.Y = 0;

            if (player.X + player.Width > Width) player.X = maxX;
            if (player.Y + player.Height > Height) player.Y = maxY;
        }
        public void ShootFireBall(myGame form)
        {
            FireBall fb = player.Shoot(form);
            entities.Add(fb);
        }
        public void SpawnMonster(myGame form)
        {

        }
    }
}