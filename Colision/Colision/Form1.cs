using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colision
{
    public partial class Form1 : Form
    {
        Bitmap canva;
        Graphics g;
        PictureBox pictureBox;
        int w = 800, h = 800;
        List<Ball> balls = new List<Ball>();

        private void timer1_Tick(object sender, EventArgs e)
        {
            g.Clear(Color.Black);
            
            for (int i = 0; i < balls.Count; i++)
            {
                for (int j = i; j < balls.Count; j++)
                {
                    balls[i].Collide(balls[j]);
                }
                balls[i].wall();
                balls[i].Update();
                g.FillEllipse(new SolidBrush(balls[i].c), balls[i].p.X, balls[i].p.Y, balls[i].size, balls[i].size);
            }
            pictureBox.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            balls.Add(new Ball(canva.Size));
        }

        public Form1()
        {
            InitializeComponent();
            canva = new Bitmap(w, h);
            g = Graphics.FromImage(canva);
            pictureBox = new PictureBox
            {
                Image = canva,
                Size = new Size(w, h),
                Location = new Point(0, 0),
                BackColor = Color.Black
            };
            balls.Add(new Ball(pictureBox.Size));
            this.Controls.Add(pictureBox);
        }
    }
}
