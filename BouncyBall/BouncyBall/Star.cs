using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBall
{
    class Star
    {

        // 이 별을 먹으면 스테이지 전환

        private Rectangle rect;

        public Star(int x, int y)
        {
            Rect = new Rectangle(x, y, 30, 30);
        }

        public Rectangle Rect { get => rect; set => rect = value; }

        public void Draw(Graphics g)
        {
            //별 그리기
            g.DrawImage(Properties.Resources.star, Rect);
        }
    }
}
