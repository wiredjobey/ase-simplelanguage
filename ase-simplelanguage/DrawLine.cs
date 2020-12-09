using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    class DrawLine : Drawing
    {
        public DrawLine() : base()
        {

        }

        public DrawLine(Canvas c, int x, int y) : base(c, x, y)
        {

        }

        public override void draw(Graphics g)
        {
            g.DrawLine(c.pen, c.xPos, c.yPos, x, y);
        }
    }
}