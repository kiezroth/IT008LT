using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTLT04
{
    static class Input
    {
        public static bool Left, Right, Up, Down;

        public static void OnKeyDown(object s, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) Left = true;
            if (e.KeyCode == Keys.Right) Right = true;
            if (e.KeyCode == Keys.Up) Up = true;
            if (e.KeyCode == Keys.Down) Down = true;
        }

        public static void OnKeyUp(object s, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) Left = false;
            if (e.KeyCode == Keys.Right) Right = false;
            if (e.KeyCode == Keys.Up) Up = false;
            if (e.KeyCode == Keys.Down) Down = false;
        }
    }

}
