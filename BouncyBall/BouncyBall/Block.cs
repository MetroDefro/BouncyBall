using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBall
{
    class Block
    {
        //블럭을 그릴 것
        protected Brush ballBrush = new SolidBrush(Color.Brown);
        protected Pen ballPen = new Pen(Color.Black);

        protected Rectangle[] rect;

        protected bool[] alive;

        public Block(int n, int x, int y)
        {
            rect = new Rectangle[n];
            for (int i = 0; i < rect.Length; i++)
            {
                rect[i] = new Rectangle(x + 30 * i, y , 30, 30);
            }

            alive = new bool[n];
            for (int i = 0; i < rect.Length; i++)
            {
                alive[i] = true;
            }         
        }

        public virtual void Draw(Graphics g)
        {
            for (int i = 0; i < rect.Length; i++)
            {
                if (alive[i])
                {
                    //g.DrawImage(Properties.Resources.breakBlock, rect);
                    g.FillRectangle(ballBrush, rect[i]);
                    g.DrawRectangle(ballPen, rect[i]);
                }
            }
        }

        public virtual void Bumped(int n)
        {

        }

        //블럭 충돌
        public void crash(Ball ball)
        {

            //블럭 묶음 첫 번째 블럭 충돌 검사
            if (alive[0])
            {
                if (ball.IntersectsWith(rect[0]) == 0)
                {
                    Bumped(0);
                    ball.Jump = false;
                    ball.Force = ball.Gravity;
                }
                else if (ball.IntersectsWith(rect[0]) == 1)
                    ball.Jump = true;
                else if (ball.IntersectsWith(rect[0]) == 3)
                    ball.Moving = -5;
            }

            //블럭 묶음 두 번째 부터 length-1번째 까지 충돌 검사
            for (int i = 1; i < rect.Length - 1; i++)
            {
                if (alive[i])
                {
                    if (ball.IntersectsWith2(rect[i]) == 0)
                    {
                        Bumped(i);
                        ball.Jump = false;
                        ball.Force = ball.Gravity;
                        break;
                    }
                    else if (ball.IntersectsWith2(rect[i]) == 1)
                    {
                        ball.Jump = true;
                        break;
                    }
                }
            }

            //블럭 묶음 마지막 블럭 충돌 검사
            if (alive[alive.Length - 1])
            {
                if (ball.IntersectsWith(rect[rect.Length - 1]) == 0)
                {
                    Bumped(rect.Length - 1);
                    ball.Jump = false;
                    ball.Force = ball.Gravity;
                }
                else if (ball.IntersectsWith(rect[rect.Length - 1]) == 1)
                    ball.Jump = true;
                else if (ball.IntersectsWith(rect[rect.Length - 1]) == 2)
                    ball.Moving = 5;
            }

        }

        //블록 리셋
        public void ResetBlock()
        {
            for (int i = 0; i < rect.Length; i++)
                alive[i] = true;
        }
    }
}
