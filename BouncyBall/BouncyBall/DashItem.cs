using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBall
{
    class DashItem
    {
        private Rectangle rect;

        protected bool alive;

        public DashItem(int x, int y)
        {
            Rect = new Rectangle(x, y, 30, 30);
            alive = true;
        }

        public Rectangle Rect { get => rect; set => rect = value; }

        public void Draw(Graphics g)
        {
            if (alive)
            {
                g.DrawImage(Properties.Resources.DashItem, Rect);
            }

        }

        public void crash(Ball ball)
        {
            if (!alive)
                return;
            if (ball.IntersectsWith(rect) != 4)
            {
                alive = false;
                //볼 더블터치시 그 방향으로 길게 날라감
                ball.Dash = true;
            }
        }

        public void ResetItem()
        {
            alive = true;
        }
    }
}
