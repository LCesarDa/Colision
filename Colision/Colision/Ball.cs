using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Colision
{
    public class Ball
    {
        public PointF p, v;
        public int size;
        static Random rand = new Random();
        public Color c;
        Size bound;

        public Ball(Size s)
        {
            bound = s;
            size = rand.Next(35, 150);
            p = new PointF(rand.Next(0, s.Width -size), rand.Next(0, s.Height - size));
            v = new PointF(rand.Next(-20, 20), rand.Next(-20, 20));
            c = Color.FromArgb(rand.Next(255), rand.Next(255), rand.Next(255));
            
        }

        public void Collide(Ball other)
        {
            float dx = p.X - other.p.X;
            float dy = p.Y - other.p.Y;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);

            if (distance < (size / 2) + (other.size / 2))
            {
                float angle = (float)Math.Atan2(dy, dx);
                float magnitude1 = (float)Math.Sqrt(v.X * v.X + v.Y * v.Y);
                float magnitude2 = (float)Math.Sqrt(other.v.X * other.v.X + other.v.Y * other.v.Y);
                float direction1 = (float)Math.Atan2(v.Y, v.X);
                float direction2 = (float)Math.Atan2(other.v.Y, other.v.X);

                float new_vx1 = magnitude1 * (float)Math.Cos(direction1 - angle);
                float new_vy1 = magnitude1 * (float)Math.Sin(direction1 - angle);
                float new_vx2 = magnitude2 * (float)Math.Cos(direction2 - angle);
                float new_vy2 = magnitude2 * (float)Math.Sin(direction2 - angle);

                float final_vx1 = ((size - other.size) * new_vx1 + (other.size + other.size) * new_vx2) / (size + other.size);
                float final_vx2 = ((size + size) * new_vx1 + (other.size - size) * new_vx2) / (size + other.size);
                float final_vy1 = new_vy1;
                float final_vy2 = new_vy2;

                v.X = (float)Math.Cos(angle) * final_vx1 + (float)Math.Cos(angle + Math.PI / 2) * final_vy1;
                v.Y = (float)Math.Sin(angle) * final_vx1 + (float)Math.Sin(angle + Math.PI / 2) * final_vy1;
                other.v.X = (float)Math.Cos(angle) * final_vx2 + (float)Math.Cos(angle + Math.PI / 2) * final_vy2;
                other.v.Y = (float)Math.Sin(angle) * final_vx2 + (float)Math.Sin(angle + Math.PI / 2) * final_vy2;
            }
        }

        public void wall ()
        {
            if (p.X - size < 0 || p.X + size > bound.Width)
            {
                v.X = -v.X;
            }

            if (p.Y - size < 0 || p.Y + size > bound.Height)
            {
                v.Y = -v.Y;
            }
        }
        public void Update()
        {
            p.X += v.X;
            p.Y += v.Y;
         }
    }
}
