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
        WorldMap world;
        public myGame()
        {
            InitializeComponent();
            world = new WorldMap(ClientSize.Width, ClientSize.Height, this);
            this.Resize += myGame_Resize;
            this.DoubleBuffered = true;
            this.KeyPreview = true; // quan trọng
            this.KeyDown += Input.OnKeyDown;
            this.KeyUp += Input.OnKeyUp;
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

        private void myGame_Resize(object sender, EventArgs e)
        {
            world.Width = ClientSize.Width;
            world.Height = ClientSize.Height;
        }
    }
}