using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBall
{
    class BreakBlock : Block
    {

        public BreakBlock(int n, int x, int y): base(n, x, y)
        {
            
        }
        public override void Bumped(int n)
        {
            alive[n] = false;
        }

        public override void Draw(Graphics g)
        {
            for (int i = 0; i < rect.Length; i++)
            {
                if (alive[i])
                {
                    g.DrawImage(Properties.Resources.breakBlock, rect[i]);
                    g.DrawRectangle(ballPen, rect[i]);
                }
            }
        }
    }
}