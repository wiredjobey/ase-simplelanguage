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
    public class Canvas
    {
        // initialise drawing area (graphics)
        public Graphics gfx;

        // initialise pen, brush and their x and y position
        public Pen pen;
        public SolidBrush brush;
        public int xPos, yPos;

        // initialise factory and drawing instance
        Factory fact = new Factory();
        Drawing d;

        // a boolean to set whether a shape should be filled
        public bool fillOn;

        public Canvas()
        {

        }

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
            brush = new SolidBrush(Color.FromName(colour));
        }

        /// <summary>
        /// Clear the drawing area
        /// </summary>
        public void Clear()
        {
            gfx.Clear(Color.Transparent);
        }


        /// <summary>
        /// Change the colour of the pen and brush
        /// </summary>
        /// <param name="colour">colour to change to, must be a predefined colour</param>
        public void Colour(string colour)
        {
            pen.Color = Color.FromName(colour);
            brush.Color = Color.FromName(colour);
        }

        /// <summary>
        /// Toggle filling the drawn shapes
        /// </summary>
        /// <param name="b">boolean, fill on or off</param>
        public void PenFill(bool b)
        {
            fillOn = b;
        }

        /// <summary>
        /// Move the pen pointer to a new position
        /// </summary>
        /// <param name="toX">New x position (in pixels)</param>
        /// <param name="toY">New y position (in pixels)</param>
        public void MoveTo(int toX, int toY)
        {
            d = fact.GetDrawing("moveto");
            d.Set(this, toX, toY);
            d.draw(gfx);
        }

        /// <summary>
        /// Draw a circle from the top left of the current pen position (xPos, yPos)
        /// </summary>
        /// <param name="rad"> Radius of the circle (in pixels)</param>
        public void DrawCircle(int rad)
        {
            d = fact.GetDrawing("circle");
            d.Set(this, rad);
            d.draw(gfx);
        }

        /// <summary>
        /// Draw a line from current pen position (xPos, yPos) to new position (toX, toY)
        /// </summary>
        /// <param name="toX">New x position (in pixels)</param>
        /// <param name="toY">New y position (in pixels)</param>
        public void DrawLine(int toX, int toY)
        {
            d = fact.GetDrawing("drawto");
            d.Set(this, toX, toY);
            d.draw(gfx);

            MoveTo(toX, toY);
        }
        
        /// <summary>
        /// Draw a triangle which arrives at the initial pen position (xPos, yPos)
        /// </summary>
        /// <param name="toX">New x position (in pixels)</param>
        /// <param name="toY">New y position (in pixels)</param>
        /// <param name="toA">a position (in pixels)</param>
        /// <param name="toB">b position (in pixels)</param>
        public void DrawTriangle(int toX, int toY, int toA, int toB)
        {
            d = fact.GetDrawing("triangle");
            d.Set(this, toX, toY, toA, toB);
            d.draw(gfx);
        }

        /// <summary>
        /// Draw a rectangle which arrives at the initial pen position (xPos, yPos)
        /// </summary>
        /// <param name="toX">width of the rectangle (in pixels)</param>
        /// <param name="toY">height of the rectangle (in pixels)</param>
        public void DrawRectangle(int toX, int toY)
        {
            d = fact.GetDrawing("rectangle");
            d.Set(this, toX, toY);
            d.draw(gfx);
        }
    }
}
