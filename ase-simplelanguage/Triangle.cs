using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    class Triangle : Drawing
    {
        private int a, b;

        public Triangle() : base()
        {

        }

        public Triangle(Canvas c, int x, int y, int a, int b) : base(c, x, y)
        {
            this.a = a;
            this.b = b;
        }

        public override void Set(Canvas c, params int[] list)
        {
            base.Set(c, list);
            a = list[2];
            b = list[3];
        }

        public override void draw(Graphics g)
        {
            Point point1 = new Point(c.xPos, c.yPos);
            Point point2 = new Point(x, y);
            Point point3 = new Point(a, b);
            Point[] tripoints = { point1, point2, point3 };

            if (c.fillOn)
            {
                g.FillPolygon(c.brush, tripoints);
            }
            else
            {
                g.DrawPolygon(c.pen, tripoints);
            }
        }
    }
}
