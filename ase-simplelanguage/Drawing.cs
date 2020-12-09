using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    abstract class Drawing : DrawingInterface
    {
        protected int x, y;
        protected Canvas c;

        public Drawing()
        {

        }

        public Drawing(Canvas c, int x, int y)
        {
            this.c = c;
            this.x = x;
            this.y = y;
        }

        public abstract void draw(Graphics g);
        public virtual void Set(Canvas c, params int[] list)
        {
            this.c = c;
            x = list[0];
            y = list[1];
        }
    }
}
