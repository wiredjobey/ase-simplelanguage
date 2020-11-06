using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        // loading a canvas and parser
        Canvas MyCanvas;
        Parser p = new Parser();

        public Form1()
        {
            InitializeComponent();
            MyCanvas = new Canvas(Graphics.FromImage(Outputbmp), penColour, penSize);
        }

        private void cmdTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmdTextBox.Text == "run")
                {
                    string[] commands = edTextBox.Lines;

                    for (int i = 0; i < commands.Length; i++)
                    {
                        p.parseCommand(commands[i], MyCanvas);
                    }

                    cmdTextBox.Text = "";
                    Refresh();
                }
                else
                {
                    p.parseCommand(cmdTextBox.Text, MyCanvas);
                    cmdTextBox.Text = "";
                    Refresh();
                }
            }
        }

        private void outputBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            gfx.DrawImageUnscaled(Outputbmp, 0, 0);
        }
    }
}
