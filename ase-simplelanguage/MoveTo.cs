using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    class MoveTo : Drawing
    {
        public MoveTo() : base()
        {

        }

        public MoveTo(Canvas c, int x, int y) : base(c, x, y)
        {

        }

        public override void draw(Graphics g)
        {
            c.xPos = x;
            c.yPos = y;
        }
    }
}
