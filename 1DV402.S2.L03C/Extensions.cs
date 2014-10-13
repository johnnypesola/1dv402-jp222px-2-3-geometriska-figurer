using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L03C
{
    static class Extensions
    {
        public static string AsText(this ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Circle: return "Cirkel";
                case ShapeType.Cuboid: return "Rätblock";
                case ShapeType.Cylinder: return "Cylinder";
                case ShapeType.Ellipse: return "Ellips";
                case ShapeType.Rectangle: return "Rektangel";
                case ShapeType.Sphere: return "Sfär";
                default: throw new NotImplementedException("Textsträng för ShapeType:n saknas i metoden AsText.");
            }
        }

        public static string CenterAlignString(this string s, string other)
        {
            int sHalf = s.Length / 2;
            int otherHalf = other.Length / 2;

            int sStartLength = sHalf - otherHalf;
            int sEndStartPos = sHalf - otherHalf + other.Length;

            return String.Format("{0}{1}{2}", s.Substring(0, sStartLength), other, s.Substring(sEndStartPos));
        }
    }
}
