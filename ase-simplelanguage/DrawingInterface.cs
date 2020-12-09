using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    interface DrawingInterface
    {
        void draw(Graphics g);

        void Set(Canvas c, params int[] list);
    }
}
