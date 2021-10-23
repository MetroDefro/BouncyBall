using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBall
{
    class Obstacle
    {

        private Rectangle rect;

        public Obstacle(int x, int y)
        {
            Rect = new Rectangle(x, y, 30, 30);
        }

        public Rectangle Rect { get => rect; set => rect = value; }

        public void Draw(Graphics g)
        {
            g.DrawImage(Properties.Resources.ObstacleBlock, Rect);
        }



        /*//장애물 충돌
        public bool crash(Ball ball)
        {
            for (int i = 0; i < rect.Length; i++)
            {
                if (ball.IntersectsWith(rect[i]) == 0)
                {
                    return true;
                }
            }

            return false;
        }
        */
    }
}
