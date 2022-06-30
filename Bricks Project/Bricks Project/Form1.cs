using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bricks_Project
{
    public partial class Form1 : Form
    {

        bool goLeft;
        bool goRight;
        bool isGameOver;

        int score;
        int ballx;
        int bally;
        int playerSpeed;

        Random rnd = new Random();


        public Form1()
        {
            InitializeComponent();

            setupGame();
        }


        private void setupGame()
        {
            score = 0;  
            ballx = 5;  
            bally = 5;
            playerSpeed = 20;
            txtScore.Text = "Score: " + score; 

            gameTimer.Start();  

            foreach(Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                }
            }

        }


        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();

            txtScore.Text = "Score: " + score + " " + " " + message;
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;

            if(goLeft == true && player.Left > 0)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left < 1490)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left < 0 || ball.Left > 1572)
            {
                ballx = -ballx;
            }
            if (ball.Top < 0)
            {
                bally = -bally;
            }
            
                if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = rnd.Next(5, 20) * -1;

                if(ballx < 0)
                {
                    ballx = rnd.Next(5, 20) * -1;
                }
                else
                {
                    ballx = rnd.Next(5, 20);
                }
            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;
                        bally = -bally;
                        this.Controls.Remove(x);
                    }
                }
                if (x is PictureBox && (string)x.Tag == "blocks2")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        bally = -bally;   
                    }
                }
            }

            if (score == 36)
            {
                gameOver("Congrats, You Win!!");
            }

            if (ball.Top > 697)
            {
                gameOver("Too bad, try again!!");
            }
        }

        private void Keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }

        }


        private void Keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }
    }
}
