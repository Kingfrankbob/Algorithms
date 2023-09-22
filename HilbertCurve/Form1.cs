using System;
using System.Drawing;
using System.Windows.Forms;

namespace HilbertCurve;

public partial class Form1 : Form
{
    private int[] xcoor = new int[10000000];
    private int[] ycoor = new int[10000000];
    private int LO = 0;
    private int n = 10; // Default iteration value
    private int height = 5;
    private int currentStep = 0;
    private System.Windows.Forms.Timer timer;

    public Form1()
    {
        InitializeForm();

        // Set the initial position
        int x = 0;
        int y = 0;

        // Create and start the timer
        timer = new System.Windows.Forms.Timer();
        timer.Interval = 5; // 50ms delay (adjust as needed)
        timer.Tick += Timer_Tick;
        timer.Start();

        // Draw the first step of the Hilbert curve
        Hilbert(2, 3, 4, 1, n, height, ref x, ref y);
    }

    private void InitializeForm()
    {
        this.Text = "Hilbert Curve";
        this.DoubleBuffered = true;
        this.Paint += OnPaint;
        this.ClientSize = new Size(800, 800);
    }

    private void Move(int j, int h, ref int x, ref int y)
    {
        if (j == 1) y -= h;
        else if (j == 2) x += h;
        else if (j == 3) y += h;
        else if (j == 4) x -= h;

        xcoor[LO] += x;
        ycoor[LO] += y;
        LO++;
    }

    private void Hilbert(int r, int d, int t, int u, int i, int h, ref int x, ref int y)
    {
        if (i > 0)
        {
            i--;
            Hilbert(d, r, u, t, i, h, ref x, ref y);
            Move(r, h, ref x, ref y);
            Hilbert(r, d, t, u, i, h, ref x, ref y);
            Move(d, h, ref x, ref y);
            Hilbert(r, d, t, u, i, h, ref x, ref y);
            Move(t, h, ref x, ref y);
            Hilbert(u, t, d, r, i, h, ref x, ref y);
        }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        if (currentStep < LO)
        {
            currentStep++;
            Invalidate(); // Redraw the curve up to the current step
        }
        else
        {
            timer.Stop(); // Stop the timer when the drawing is complete
        }
    }

    private void OnPaint(object sender, PaintEventArgs e)
    {
        // Draw the Hilbert curve up to the current step with gradient color
        using (Pen pen = new Pen(Color.Black))
        {
            for (int i = 1; i < currentStep; i++)
            {
                e.Graphics.DrawLine(pen, xcoor[i - 1], ycoor[i - 1], xcoor[i], ycoor[i]);
            }
        }
    }

}