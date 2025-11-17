using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLT04
{
    public partial class myGame : Form
    {
        WorldMap world = new WorldMap();
        public myGame()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            timer.Start();
        }
        private void GameLoop(object sender, EventArgs e)
        {
            world.Update();
            Invalidate(); 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            world.Render(e.Graphics);
        }
    }
}
