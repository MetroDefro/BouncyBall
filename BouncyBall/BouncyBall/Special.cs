using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBall
{
    class Special
    {
        private Rectangle rect;

        public Special(int x, int y)
        {
            Rect = new Rectangle(x, y, 30, 30);
        }

        public Rectangle Rect { get => rect; set => rect = value; }

        public void Draw(Graphics g)
        {
            g.DrawImage(Properties.Resources.JumpSpecial, Rect);
        }

        public void crash(Ball ball)
        {
            if (ball.IntersectsWith(rect) == 0)
            {
                ball.Jump = false;
                ball.Force = 20;
            }
            else if (ball.IntersectsWith(rect) == 1)
                ball.Jump = true;
            else if (ball.IntersectsWith(rect) == 3)
                ball.Moving = -5;
        }
    }
}
