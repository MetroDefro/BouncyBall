using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BouncyBall
{
    class Stage
    {
        protected Ball ball;

        protected Star star;

        protected bool clear;

        public bool isGameOver;


        protected Block[] block;

        protected Obstacle[] obstacle;
        public Obstacle[] Obstacle { get => obstacle; set => obstacle = value; }

        private Special[] special;

        public Special[] Special { get => special; set => special = value; }

        private DashItem[] item;

        public bool Clear { get => clear; set => clear = value; }
        internal DashItem[] Item { get => item; set => item = value; }

        public Stage(Ball ball, Block[] block, int x, int y)
        {
            clear = false;

            this.ball = ball;
            this.block = block;

            star = new Star(x, y);

            isGameOver = false;
        }

        public virtual void Draw(Graphics g)
        {
            star.Draw(g);
            for (int i = 0; i < block.Length; i++)
                block[i].Draw(g);
            if (obstacle != null)
            {
                for (int i = 0; i < obstacle.Length; i++)
                    obstacle[i].Draw(g);
            }

            if(special != null)
            {
                for (int i = 0; i < special.Length; i++)
                    special[i].Draw(g);
            }

            if (item != null)
            {
                for (int i = 0; i < item.Length; i++)
                    item[i].Draw(g);
            }
        }

        public bool isClear()
        {
            return star.Rect.IntersectsWith(ball.Rect);
        }

        public virtual void Hit()
        {
            if (clear)
                return;
            for (int i = 0; i < block.Length; i++)
                block[i].crash(ball);

            if (obstacle != null)
            {
                crashOb(obstacle);
            }
            if (special != null)
            {
                for (int i = 0; i < special.Length; i++)
                    special[i].crash(ball);
            }
            if (item != null)
            {
                for (int i = 0; i < item.Length; i++)
                    item[i].crash(ball);
            }

        }

        /* 블럭 충돌
        //블럭 충돌
        protected void crash(Block[] b, Ball ball)
        {
            if (clear)
                return;

            //블럭 묶음 첫 번째 블럭 충돌 검사
            if (b[0].Alive)
            {
                if (ball.IntersectsWith(b[0].Rect) == 0)
                {
                    b[0].Bumped();
                    ball.Jump = false;
                    ball.Force = ball.Gravity;
                }
                else if (ball.IntersectsWith(b[0].Rect) == 1)
                    ball.Jump = true;
                else if (ball.IntersectsWith(b[0].Rect) == 3)
                    ball.Moving = -5;
            }

            //블럭 묶음 두 번째 부터 length-1번째 까지 충돌 검사
            for (int i = 1; i < b.Length - 1; i++)
            {
                if (b[i].Alive)
                {
                    if (ball.IntersectsWith2(b[i].Rect) == 0)
                    {
                        b[i].Bumped();
                        ball.Jump = false;
                        ball.Force = ball.Gravity;
                        break;
                    }
                    else if (ball.IntersectsWith2(b[i].Rect) == 1)
                    {
                        ball.Jump = true;
                        break;
                    }
                }
            }

            //블럭 묶음 마지막 블럭 충돌 검사
            if (b[b.Length - 1].Alive)
            {
                if (ball.IntersectsWith(b[b.Length - 1].Rect) == 0)
                {
                    b[b.Length - 1].Bumped();
                    ball.Jump = false;
                    ball.Force = ball.Gravity;
                }
                else if (ball.IntersectsWith(b[b.Length - 1].Rect) == 1)
                    ball.Jump = true;
                else if (ball.IntersectsWith(b[b.Length - 1].Rect) == 2)
                    ball.Moving = 5;
            }

        }
        */

        //장애물 충돌
        protected void crashOb(Obstacle[] o)
        {
            for (int i = 0; i < o.Length; i++)
            {
                if (ball.IntersectsWith(o[i].Rect) != 4)
                {
                    isGameOver = true;
                    break;
                }
            }
        }

        /*//블록 리셋
        protected void ResetBlock(Block[] b)
        {
            for (int i = 0; i < b.Length; i++)
                b[i].Alive = true;
        } 
        */

        public virtual void ResetStage()
        {
            isGameOver = false;
            for (int i = 0; i < block.Length; i++)
                block[i].ResetBlock();
            if (item != null)
            {
                for (int i = 0; i < item.Length; i++)
                    item[i].ResetItem();
            }

        }



        //public bool 
    }
}
