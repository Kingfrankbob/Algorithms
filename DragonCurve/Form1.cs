using System;
using System.Drawing;
using System.Windows.Forms;

namespace DragonCurve
{
    public partial class Form1 : Form
    {
        private int iteration = 22; // Increase the number of iterations
        private System.Windows.Forms.Timer timer; // Declare as a System.Windows.Forms.Timer

        public Form1()
        {
            InitializeComponent();
            timer = new System.Windows.Forms.Timer(); // Initialize as a System.Windows.Forms.Timer
            timer.Interval = 100; // Adjust the interval as needed
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Redraw the Dragon Curve on each tick
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawDragonCurve(e.Graphics, iteration, ClientSize.Width, ClientSize.Height);
        }

        private void DrawDragonCurve(Graphics g, int iterations, int width, int height)
        {
            // Initialize the starting point and direction
            PointF currentPoint = new PointF(width / 2, height / 2);
            PointF direction = new PointF(5, 0);

            // Draw the Dragon Curve using the provided DrawDragonLine function
            DrawDragonLine(g, iterations, Direction.Right, currentPoint.X, currentPoint.Y, direction.X * width, direction.Y * height);
        }

        private void DrawDragonLine(Graphics gr, int level, Direction turn_towards, float x1, float y1, float dx, float dy)
        {
            if (level <= 0)
            {
                // Calculate the endpoint coordinates
                float x2 = x1 + dx;
                float y2 = y1 + dy;

                // Draw the line segment
                gr.DrawLine(Pens.Red, x1, y1, x2, y2);
            }
            else
            {
                float nx = dx / 2;
                float ny = dy / 2;
                float dx2 = -ny + nx;
                float dy2 = nx + ny;

                if (turn_towards == Direction.Right)
                {
                    // Turn to the right.
                    float x2 = x1 + dx2;
                    float y2 = y1 + dy2;

                    // Draw the line segment
                    gr.DrawLine(Pens.Red, x1, y1, x2, y2);

                    DrawDragonLine(gr, level - 1, Direction.Right, x1, y1, dx2, dy2);
                    DrawDragonLine(gr, level - 1, Direction.Left, x2, y2, dy2, -dx2);
                }
                else
                {
                    // Turn to the left.
                    float x2 = x1 + dy2;
                    float y2 = y1 - dx2;

                    // Draw the line segment
                    gr.DrawLine(Pens.Red, x1, y1, x2, y2);

                    DrawDragonLine(gr, level - 1, Direction.Right, x1, y1, dy2, -dx2);
                    DrawDragonLine(gr, level - 1, Direction.Left, x2, y2, dx2, dy2);
                }
            }
        }

        private enum Direction
        {
            Left,
            Right
        }
    }
}