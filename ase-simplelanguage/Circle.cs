using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    class Circle : Drawing
    {
        private int rad;

        public Circle() : base()
        {

        }

        public Circle(Canvas c, int x, int y, int rad) : base(c, x, y)
        {
            this.rad = rad;
        }

        public override void Set(Canvas c, params int[] list)
        {
            base.c = c;
            rad = list[0];
        }

        public override void draw(Graphics g)
        {
            if (c.fillOn)
            {
                g.FillEllipse(c.brush, c.xPos, c.yPos, (rad * 2), (rad * 2));
            } 
            else
            {
                g.DrawEllipse(c.pen, c.xPos, c.yPos, (rad * 2), (rad * 2));
            }
        }
    }
}
