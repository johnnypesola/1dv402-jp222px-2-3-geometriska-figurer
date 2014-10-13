using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L03C
{
    class Ellipse : Shape2D
    {
        public override double Area
        {
            get
            {
                double a = Length / 2;
                double b = Width / 2;
                return Math.PI * a * b;
            }
        }

        public override double Perimeter
        {
            get
            {
                double a = Length / 2;
                double b = Width / 2;
                return Math.PI * Math.Sqrt(2 * a * a + 2 * b * b);
            }
        }

        // Constructors
        public Ellipse(double diameter) : base(ShapeType.Circle, diameter, diameter)
        {
            
        }

        public Ellipse(double hdiameter, double vdiameter) : base(ShapeType.Ellipse, hdiameter, vdiameter)
        { 

        }
    }
}
