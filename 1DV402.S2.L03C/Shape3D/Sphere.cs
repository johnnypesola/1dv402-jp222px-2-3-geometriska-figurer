using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L03C
{
    class Sphere : Shape3D
    {
        // Properties
        public override double MantelArea
        {
            get
            {
                return _baseShape.Perimeter * Height;
            }
        }

        public override double TotalSurfaceArea
        {
            get
            {
                return MantelArea + 2 * _baseShape.Area;
            }
        }

        public override double Volume
        {
            get
            {
                return _baseShape.Area * Height;
            }
        }

        // Constructor
        public Sphere(double radius) : base(ShapeType.Sphere, new Ellipse(radius * 2), radius * 2)
        {
            
        }
    }
}
