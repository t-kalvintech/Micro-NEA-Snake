using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private List<square> Snake = new List<square>();
        private square food = new square();

        int maxWidth;
        int maxHeight;

        int score;
        int highscore;

        Random rand = new Random();

        bool goLeft, goRight, goUp, goDown; 



        public Form1()
        {
            InitializeComponent();

            new settings();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left && settings.directions != "right")
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right && settings.directions != "left")
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up && settings.directions != "down")
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down && settings.directions != "up")
            {
                goDown = true;
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
        }

        private void StartGame(object sender, MouseEventArgs e)
        {
            RestartGame();
        }

        private void StopGame(object sender, MouseEventArgs e)
        {

        }

        private void GameTimerEvent(object sender, EventArgs e)
        {
            // setting directions
            if (goLeft)
            {
                settings.directions = "left";

            }
            if (goRight)
            {
                settings.directions = "right";

            }
            if (goDown)
            {
                settings.directions = "down";

            }
            if (goUp)
            {
                settings.directions = "up";

            }
            //end directions
            

            for (int i = Snake.Count-1; i >= 0; i--)
            {
                if (i==0)
                {
                    switch (settings.directions)
                    {
                        case "left":
                            Snake[i].x--;
                            break;
                        case "right":
                            Snake[i].x++;
                            break;
                        case "down":
                            Snake[i].y++;
                            break;
                        case "up":
                            Snake[i].y--;
                            break;
                    }


                    if (Snake[i].x<0)
                    {
                        Snake[i].x = maxWidth;
                    }
                    if (Snake[i].x > maxWidth)
                    {
                        Snake[i].x = 0;
                    }
                    if (Snake[i].y <0)
                    {
                        Snake[i].y = maxHeight;
                    }
                    if (Snake[i].y > maxHeight)
                    {
                        Snake[i].y = 0;
                    }

                    if(Snake[i].x == food.x && Snake[i].y == food.y)
                    {
                        EatFood();
                    }

                    for (int n = 1; n <Snake.Count ; n++)
                    {
                        if (Snake[i].x == Snake[n].x && Snake[i].y == Snake[n].y)
                        {
                            GameOver();
                        }
                    }



                }
                else
                {
                    Snake[i].x = Snake[i - 1].x;
                    Snake[i].y = Snake[i - 1].y;
                }


            }
            picCanvas.Invalidate();


        }

        private void UpdatePictureBoxGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;
            Brush snakeColour;
            for (int i = 0; i < Snake.Count; i++)
            {
                if (i==0)
                {
                    snakeColour = Brushes.White;
                }
                else
                {
                    snakeColour = Brushes.White;
                }

                canvas.FillEllipse(snakeColour, new Rectangle
                    (
                    Snake[i].x * settings.Width,
                    Snake[i].y * settings.Height,
                    settings.Width, settings.Height
                    ));

            }

            canvas.FillEllipse(Brushes.Red, new Rectangle
            (
            food.x * settings.Width,
            food.y * settings.Height,
            settings.Width, settings.Height
            ));


        }

        private void txtScore_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void RestartGame()
        {
            maxWidth = picCanvas.Width / settings.Width - 1;
            maxHeight = picCanvas.Height / settings.Height - 1;
            
            Snake.Clear();
            
            startButton.Enabled = false;
            stopButton.Enabled = false;
            score = 0;
            txtScore.Text = "Score: " + score;

            square head = new square { x= 10, y = 5 };
            Snake.Add(head);//adding the head part of the snake to the lsit

            for (int i= 0; i<10; i++)
            {
                square body = new square();
                Snake.Add(body);

            }

            food = new square { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };

            gameTimer.Start();


        }

        private void EatFood()
        {
            score += 1;
            txtScore.Text = "Score: " + score;
            square body = new square
            {
                x = Snake[Snake.Count - 1].x,
                y = Snake[Snake.Count - 1].y
            };

            Snake.Add(body);

            food = new square { x = rand.Next(2, maxWidth), y = rand.Next(2, maxHeight) };


        }

        private void GameOver()
        {
            gameTimer.Stop();
            stopButton.Enabled = true;

            txtHighScore.Text = "High Score:" + Environment.NewLine + highscore;
            txtHighScore.ForeColor = Color.Black;
            txtHighScore.TextAlign = ContentAlignment.MiddleCenter;
        }


    }
}
