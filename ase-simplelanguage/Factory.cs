using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ase_simplelanguage
{
    class Factory
    {
        public Drawing GetDrawing(String drawType)
        {
            switch(drawType)
            {
                case "drawto":
                    return new DrawLine();
                case "moveto":
                    return new MoveTo();
                case "circle":
                    return new Circle();
                case "triangle":
                    return new Triangle();
                case "rectangle":
                    return new Rectangle();
                default:
                    throw new ArgumentException("Factory error: "+drawType+" does not exist");
            }
        }
    }
}
