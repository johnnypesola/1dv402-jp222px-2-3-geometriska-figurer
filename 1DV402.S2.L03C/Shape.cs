using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L03C
{
    enum ShapeType
    {
        Ellipse,
        Circle,
        Rectangle,
        Cuboid,
        Cylinder,
        Sphere
    }

    abstract class Shape
    {
        // Properties
        public ShapeType ShapeType { get; private set; }

        public bool IsShape3D
        {
            get
            {
                return (ShapeType == ShapeType.Cuboid || ShapeType == ShapeType.Cylinder || ShapeType == ShapeType.Sphere);
            }
        }

        // Constructor
        protected Shape(ShapeType shapeType)
        {
            ShapeType = shapeType;
        }

        // Methods
        public abstract string ToString(string format);

    }
}
