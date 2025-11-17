using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTLT04
{
    class AnimatedSprite
    {
        public Bitmap Sheet;
        public int FrameWidth, FrameHeight;
        public int FrameIndex = 0;
        public int FrameCount;
        public int FrameDelay = 10;
        public int SpriteOnRow;
        public int Rows;
        private int delayCounter = 0;

        public AnimatedSprite(string path, int frameW, int frameH, int spriteOnRow)
        {
            Sheet = new Bitmap(path);
            FrameWidth = frameW;
            FrameHeight = frameH;
            SpriteOnRow = spriteOnRow;
            Rows = Sheet.Height / FrameHeight;
            FrameCount = SpriteOnRow * Rows;
        }
        public void Draw(Graphics g, float x, float y, int widthScale, int heightScale)
            {
                int srcX = (FrameIndex % SpriteOnRow) * FrameWidth;
                int srcY = (FrameIndex / SpriteOnRow) * FrameHeight;
                Rectangle src = new Rectangle(srcX, srcY, FrameWidth, FrameHeight);
                Rectangle dest = new Rectangle((int)x,(int)y,widthScale,heightScale);
                g.DrawImage(Sheet, dest, src, GraphicsUnit.Pixel);
            }

        public void NextFrame()
        {
            delayCounter++;
            if (delayCounter >= FrameDelay)
            {
                delayCounter = 0;
                FrameIndex = (FrameIndex + 1) % FrameCount;
            }
        }
        
    }
}
