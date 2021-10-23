using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BouncyBall
{
    public partial class BouncyBall : Form
    {
        static string str;

        private Ball ball;

        private int stageNum;
        private int maxStage;

        private bool gameClear;

        private Stage[] stage;

        public BouncyBall()
        {
            stageNum = 0;
            maxStage = 6;

            ball = new Ball();
            stage = new Stage[maxStage+1];

            ReadStageFile();
            InitializeComponent();
        }

        private void ReadStageFile()
        {
            StreamReader streamReader = new StreamReader("stage.txt");

            int stageIndex = 0;
            while (streamReader != null)
            {
                if (streamReader.ReadLine() == null)
                    break;
                StageElement stageElement = new StageElement();

                if (streamReader.ReadLine() == "블록")
                {
                    int blockAmount = int.Parse(streamReader.ReadLine());
                    Block[] stageBlock = new Block[blockAmount];
                    int blockIndex = 0;
                    str = streamReader.ReadLine();
                    if (str == "일반블록")
                    {
                        string line = streamReader.ReadLine();
                        int i = 0;
                        for (int j = 0; j < line.Split(',').Length; j += 3)
                        {
                            stageBlock[blockIndex++] = new Block(
                                int.Parse(line.Split(',')[i++]), int.Parse(line.Split(',')[i++]), int.Parse(line.Split(',')[i++])
                                    );
                        }
                        str = streamReader.ReadLine();
                    }
                    if (str == "부서지는블록")
                    {
                        string line = streamReader.ReadLine();
                        int i = 0;
                        for (int j = 0; j < line.Split(',').Length; j += 3)
                        {
                            stageBlock[blockIndex++] = new BreakBlock(
                                int.Parse(line.Split(',')[i++]), int.Parse(line.Split(',')[i++]), int.Parse(line.Split(',')[i++])
                                    );
                        }
                        str = streamReader.ReadLine();
                    }
                    stageElement.block = stageBlock;
                }

                if (str == "장애물")
                {
                    //int obstacleAmount = int.Parse(streamReader.ReadLine());
                    string line = streamReader.ReadLine();
                    int obstacleAmount = line.Split(',').Length / 2;
                    Obstacle[] stageObstacle = new Obstacle[obstacleAmount];
                    int obstacleIndex = 0;

                    int i = 0;
                    for (int j = 0; j < line.Split(',').Length; j += 2)
                    {
                        stageObstacle[obstacleIndex++] = new Obstacle(
                            int.Parse(line.Split(',')[i++]), int.Parse(line.Split(',')[i++])
                                );
                    }
                    stageElement.obstacle = stageObstacle;
                    str = streamReader.ReadLine();
                }
                if (str == "점프블록")
                {
                    string line = streamReader.ReadLine();
                    int specialAmount = line.Split(',').Length / 2;
                    Special[] stageSpecial = new Special[specialAmount];
                    int specialIndex = 0;

                    int i = 0;
                    for (int j = 0; j < line.Split(',').Length; j += 2)
                    {
                        stageSpecial[specialIndex++] = new Special(
                            int.Parse(line.Split(',')[i++]), int.Parse(line.Split(',')[i++])
                                );
                    }
                    stageElement.special = stageSpecial;
                    str = streamReader.ReadLine();
                }
                if (str == "아이템")
                {
                    string line = streamReader.ReadLine();
                    int itemAmount = line.Split(',').Length / 2;
                    DashItem[] stageItem = new DashItem[itemAmount];
                    int itemIndex = 0;

                    int i = 0;
                    for (int j = 0; j < line.Split(',').Length; j += 2)
                    {
                        stageItem[itemIndex++] = new DashItem(
                            int.Parse(line.Split(',')[i++]), int.Parse(line.Split(',')[i++])
                                );
                    }
                    stageElement.dashItems = stageItem;
                    str = streamReader.ReadLine();
                }

                if (str == "별")
                {
                    string line = streamReader.ReadLine();
                    stageElement.x = int.Parse(line.Split(',')[0]);
                    stageElement.y = int.Parse(line.Split(',')[1]);

                }

                if (streamReader.ReadLine() == "스테이지 끝")
                {
                    stage[stageIndex] = new Stage(
                        ball, stageElement.block, stageElement.x, stageElement.y
                            );
                    if (stageElement.obstacle != null)
                        stage[stageIndex].Obstacle = stageElement.obstacle;
                    if(stageElement.special != null)
                        stage[stageIndex].Special = stageElement.special;
                    if (stageElement.dashItems != null)
                        stage[stageIndex].Item = stageElement.dashItems;
                    stageIndex++;
                }


            }

        }


        private void BouncyBall_Load(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            ball.Draw(e.Graphics);
            stage[stageNum].Draw(e.Graphics);
            if (gameClear)
            {
                e.Graphics.DrawImage(Properties.Resources.GameClear, 120,140);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            ball.Bounce();
            ball.MoveSide();
            ball.Move();

            //블록 충돌 검사
            stage[stageNum].Hit();

            if (ball.IsGameOver() || stage[stageNum].isGameOver)
            {               
                stage[stageNum].ResetStage();
                ball.reset();
            }
            //gameOver.Text = " " +stage[stageNum].isGameOver;

            if (stage[stageNum].isClear())
            {
                if (stageNum == maxStage)
                {
                    gameClear = true;
                    //gameOver.Text = "게임클리어";
                    timer1.Stop();
                }
                else
                {
                    stage[stageNum].Clear = true;
                    stageNum++;
                    ball.reset();
                }
            }

            panel1.Invalidate();
        }


        private void BouncyBall_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    ball.Right = true;
                    break;

                case Keys.Left:
                    ball.Left = true;
                    break;
                case Keys.A:
                    ball.UseItem();
                    break;
            }
        }

        private void BouncyBall_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    ball.Right = false;
                    break;

                case Keys.Left:
                    ball.Left = false;
                    break;
            }
        }
    }
}