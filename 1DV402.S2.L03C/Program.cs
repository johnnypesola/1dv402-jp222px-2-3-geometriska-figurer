using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L03C
{
    class Program
    {
        static void Main(string[] args)
        {
            Ellipse ellipseObj = new Ellipse((double)10, (double)10);

            Console.WriteLine(ellipseObj.Area);
        }

        Shape2D Randomize2DShapes()
        {
            return new Rectangle(50, 50);
        }

        Shape3D Randomize3DShapes()
        {
            return new Cuboid(50, 20, 20);
        }

        double ReadDomensions(ShapeType shapeType)
        {
            return 0;
        }

        double[] ReadDoublesGreaterThanZero(string prompt, int numberOfValues = 1)
        {
            return new double[] { 10 };
        }

        void ViewMenu()
        {

        }

        void ViewMenuErrorMessage()
        {

        }

        void ViewShapeDetails(Shape shape)
        {

        }

        void ViewShapes(Shape[] shapes)
        {

        }

    }
}
