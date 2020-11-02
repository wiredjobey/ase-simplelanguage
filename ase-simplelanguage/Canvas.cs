using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ase_simplelanguage
{
    /// <summary>
    /// Canvas class stores methods for drawing to a bitmap
    /// </summary>
    class Canvas
    {
        // initialise drawing area (graphics)
        Graphics gfx;

        // initialise pen and its x and y position
        Pen pen;
        int xPos, yPos;

        /// <summary>
        /// Constructor for canvas which adds the drawing area and sets the initial pen parameters (position, colour, size)
        /// </summary>
        /// <param name="gfx">Graphics context for drawing area</param>
        /// <param name="colour">Colour of the pen (in Color.FromName() predefined colours)</param>
        /// <param name="size">Size of the pen (in pixels)</param>
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

        /// <summary>
        /// Draw a circle from the top left of hte current pen position (xPos, yPos)
        /// </summary>
        /// <param name="rad"> Radius of the circle (in pixels)</param>
        public void DrawCircle(int rad)
        {
            gfx.DrawEllipse(pen, xPos, yPos, xPos + (rad * 2), yPos + (rad * 2));
        }

        /// <summary>
        /// Draw a line from current pen position (xPos, yPos) to new position (toX, toY)
        /// </summary>
        /// <param name="toX">New x position (in pixels)</param>
        /// <param name="toY">New y position (in pixels)</param>
        public void DrawLine(int toX, int toY)
        {
            gfx.DrawLine(pen, xPos, yPos, toX, toY);
            xPos = toX;
            yPos = toY;
        }
        
        /// <summary>
        /// Draw a triangle which arrives at the initial pen position (xPos, yPos)
        /// </summary>
        /// <param name="toX">New x position (in pixels)</param>
        /// <param name="toY">New y position (in pixels)</param>
        /// <param name="toZ">Third z position (in pixels)</param>
        public void DrawTriangle (int toX, int toY, int toZ)
        {
            // turn the position coordinates into points and put them in a point array
            Point point1 = new Point(xPos, yPos);
            Point point2 = new Point(toX, toY);
            Point point3 = new Point(toY, toZ);
            Point[] tripoints = { point1, point2, point3 };

            gfx.DrawPolygon(pen, tripoints);
        }

        /// <summary>
        /// Draw a square which arrives at the initial pen position (xPos, yPos)
        /// </summary>
        /// <param name="size">Size of the square (in pixels)</param>
        public void DrawSquare(int size)
        {
            gfx.DrawRectangle(pen, xPos, yPos, xPos + size, yPos + size);
        }
    }
}
