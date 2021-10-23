using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBall
{
    class Ball
    {
        //공을 그릴 것
        private Brush ballBrush = new SolidBrush(Color.Yellow);
        private Brush dashBrush = new SolidBrush(Color.Black);
        private Pen ballPen = new Pen(Color.Black);

        private Rectangle rect;

        //튀기기
        private int gravity = 14;
        private int force;
        static private bool jump;


        //이동하기
        private bool right;
        private bool left;

        //좌 우 맞았음
        private int moving;

        //블럭 어디에 맞았나
        private Rectangle interRect;

        //대쉬 아이템 얻었나?
        private bool dash;

        public bool Jump { get => jump; set => jump = value; }
        public int Force { get => force; set => force = value; }
        public int Gravity { get => gravity; set => gravity = value; }
        public bool Right { get => right; set => right = value; }
        public bool Left { get => left; set => left = value; }
        public int Moving { get => moving; set => moving = value; }
        public Rectangle Rect { get => rect; set => rect = value; }
        public bool Dash { get => dash; set => dash = value; }


        public Ball()
        {
            dash = false;
            Rect = new Rectangle(50, 320, 20, 20);
            jump = true;
            force = 0;
            moving = 0;
        }

        public void Draw(Graphics g)
        {
            if (Dash)
                g.FillEllipse(dashBrush, rect);
            else
                g.FillEllipse(ballBrush, rect);
            g.DrawEllipse(ballPen, rect);
        }

        public void Bounce()
        {
            if (!jump)
            {
                force -= 1;
                rect.Y -= force;
                if (force == 0)
                    jump = true;
            }
            else
            {
                rect.Y += force;
                force += 1;
            }
        }

        public void UseItem()
        {
            if (!Dash)
                return;
            if (right)
                rect.X += 100;
            if (left)
                rect.X -= 100;
            Dash = false;
        }

        public int IntersectsWith(Rectangle block)
        {

            interRect = Rectangle.Intersect(rect, block);

            //interRect의 x, y와 block의 x, y 비교해서 왼쪽, 오른쪽, 위, 아래 어디에서 맞았는지 비교?
            if (Rect.IntersectsWith(block))
            {

                //왼쪽에 맞음
                if (block.X == interRect.X && interRect.Width < 10)
                    return 3;
                //오른쪽에 맞음
                else if (block.X + block.Width == interRect.X + interRect.Width && interRect.Width < 10)
                    return 2;

                if (block.Y < rect.Y && rect.Y < block.Y + block.Height)
                {
                    // 아랫면에 부딫혔을때
                    rect.Y = block.Y + block.Height;
                    return 1;
                }
                else if (block.Y < rect.Y + rect.Height && rect.Y + rect.Height < block.Y + block.Height)
                {
                    // 윗면에 부딫혔을때
                    rect.Y = block.Y - rect.Height;
                    return 0;
                }
                else
                    return 4;
            }
            else
                return 4;
        }

        public int IntersectsWith2(Rectangle block)
        {
            if (Rect.IntersectsWith(block))
            {
                if (block.Y < rect.Y && rect.Y < block.Y + block.Height)
                {
                    // 아랫면에 부딫혔을때
                    rect.Y = block.Y + block.Height;

                    return 1;
                }
                else if (block.Y < rect.Y + rect.Height && rect.Y + rect.Height < block.Y + block.Height)
                {
                    // 윗면에 부딫혔을때
                    rect.Y = block.Y - rect.Height;
                    return 0;
                }
            }

            return 4;

        }
        public void Move()
        {
            if (right)
                rect.X += 5;
            else if (left)
                rect.X -= 5;
        }

        public void MoveSide()
        {
            if (moving < 0)
                moving += 1;
            else if (moving > 0)
                moving -= 1;
            else
                return;
            rect.X += moving;
            return;
        }

        public bool IsGameOver()
        {
            if (Rect.Y > 500)
                return true;
            else
                return false;
        }

        public void reset()
        {
            dash = false;
            rect.X = 50;
            rect.Y = 320;

            jump = true;
            force = 0;
            moving = 0;
        }
    }
}
