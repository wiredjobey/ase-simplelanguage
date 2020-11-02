using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase_simplelanguage
{
    public partial class Form1 : Form
    {

        // initial pen colour and size
        const string penColour = "Black";
        const int penSize = 1;

        // bitmap size in pixels
        const int bmpX = 384;
        const int bmpY = 384;
        
        // bitmap to draw, using size constants defined above
        Bitmap Outputbmp = new Bitmap(bmpX, bmpY);

        // loading a canvas
        Canvas MyCanvas;

        public Form1()
        {
            InitializeComponent();
            MyCanvas = new Canvas(Graphics.FromImage(Outputbmp), penColour, penSize);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cmdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cmd = cmdTextBox.Text.Trim().ToLower();

                if (cmd.Equals("line"))
                {
                    MyCanvas.DrawLine(100, 140);
                    Console.WriteLine("line");
                } else if (cmd.Equals("square"))
                {
                    MyCanvas.DrawSquare(60);
                    Console.WriteLine("square");
                } else if (cmd.Equals("pen"))
                {
                    MyCanvas.EditPen(255, 255, 255, 4);
                    Console.WriteLine("pen");
                } else if (cmd.Equals("tri"))
                {
                    MyCanvas.DrawTriangle(0, 100, 50);
                    Console.WriteLine("triangle");
                }
                
                cmdTextBox.Text = "";
                Refresh();
            }
        }

        private void outputBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            gfx.DrawImageUnscaled(Outputbmp, 0, 0);
        }
    }
}
