using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L03C
{
    class Rectangle : Shape2D
    {
        public override double Area
        {
            get
            {
                return Width * Length;
            }
        }

        public override double Perimeter
        {
            get
            {
                return 2 * Length + 2 * Width;
            }
        }

        // Constructor
        public Rectangle(double length, double width) : base(ShapeType.Rectangle, length, width)
        {
            
        }
    }
}
