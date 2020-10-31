using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase_simplelanguage
{
    class Canvas
    {
        Graphics gfx;
        Pen pen;
        int xPos, yPos;

        public Canvas(Graphics gfx, string colour, int size)
        {
            this.gfx = gfx;
            xPos = 0;
            yPos = 0;
            pen = new Pen(Color.FromName(colour), size);
        }

        public void EditPen(int r, int g, int b, int pixelSize)
        {
            pen = new Pen(Color.FromArgb(r, g, b), pixelSize);
        }

        public void DrawLine(int toX, int toY)
        {
            gfx.DrawLine(pen, xPos, yPos, toX, toY);
            xPos = toX;
            yPos = toY;
        }
        
        public void DrawSquare(int size)
        {
            gfx.DrawRectangle(pen, xPos, yPos, xPos + size, yPos + size);
        }
    }
}
