using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace BTLT04
{
    class MonsterManager
    {
        private Random rnd = new Random();
        private Random rnd2 = new Random();
        public int worldW, worldH;
        public WorldMap world;
        public List<Monster> Monsters;
        public MonsterManager( WorldMap map)
        {
            Monsters = new List<Monster>();
            worldW = map.Width;
            worldH = map.Height;
            world = map;
        }
        public void Update()
        {
            if (Monsters.Count == 0)
                SpawnWave();

            foreach (var m in Monsters)
            {
                foreach (var e in world.entities.ToList())
                {
                    if (e is FireBall fb && fb.IsCollidingWith(m))
                    {
                        m.IsDestroyed = true;
                        fb.IsDestroyed = true;
                    }
                }
                m.Update();
            }
            Monsters.RemoveAll(m => m.IsDestroyed);
        }

        private void SpawnWave()
        {
            int count = rnd.Next(3, 6);
            for (int i = 0; i < count; i++)
                SpawnOneMonster_NoOverlap();
        }

        private void SpawnOneMonster_NoOverlap()
        {
            int x, y;
            bool ok;

            do
            {
                x = rnd.Next(0, worldW - 64);
                y = rnd.Next(0, worldH - 64);

                ok = true;

                foreach (var m in Monsters)
                {
                    if (Distance(x, y, m.X, m.Y) < 70)
                    {
                        ok = false;
                        break;
                    }
                }

            } while (!ok);

            int t = rnd2.Next(0, 100) % 3;
            Monster monster;
            switch (t)
            {
                case 0:
                    monster = new Monster(x, y, Monster.MonsterType.slime1);
                    break;
                case 1:
                    monster = new Monster(x, y, Monster.MonsterType.slime2);
                    break;
                case 2:
                    monster = new Monster(x, y, Monster.MonsterType.slime3);
                    break;
                default:
                    monster = new Monster(x, y, Monster.MonsterType.slime1);
                    break;
            }
            Monsters.Add(monster);
            world.entities.Add(monster);
        }

        private float Distance(float x1, float y1, float x2, float y2)
        {
            float dx = x1 - x2;
            float dy = y1 - y2;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }

        public void Render(Graphics g)
        {
            foreach (var m in Monsters)
                m.Render(g);
        }
        public string fixPath(string path)
        {
            string result = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\" + @path));
            return result;
        }
    }
}
