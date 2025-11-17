using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLT04
{
    class WorldMap
    {
        public List<Entity> entities = new List<Entity>();
        public Player player;
        public WorldMap()
        {
            player = new Player(200, 200);
            entities.Add(player);
        }

        public void Update()
        {
            player.Update();
            foreach (var e in entities)
                e.Update();
        }

        public void Render(Graphics g)
        {
            foreach (var e in entities)
                e.Render(g);
        }
    }
}
