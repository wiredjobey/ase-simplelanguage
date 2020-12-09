using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    class Rectangle : Drawing
    {
        public Rectangle()
        {

        }

        public Rectangle(Canvas c, int x, int y) : base(c, x, y)
        {

        }

        public override void draw(Graphics g)
        {
            if (c.fillOn)
            {
                g.FillRectangle(c.brush, c.xPos, c.yPos, x, y);
            }
            else
            {
                g.DrawRectangle(c.pen, c.xPos, c.yPos, x, y);
            }
        }
    }
}
