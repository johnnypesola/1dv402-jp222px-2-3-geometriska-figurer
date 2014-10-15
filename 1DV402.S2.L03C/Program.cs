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
            bool exitProgram = false;
            Shape2D[] random2DShapes;
            Shape3D[] random3DShapes;

            do
            {
                Console.Clear();

                ViewMenu();

                // Read choice and do appropriate action.
                try
                {
                    switch (Console.ReadLine())
                    {
                        case "0": exitProgram = true; break;
                        case "1": ViewShapeDetail(CreateShape(ShapeType.Rectangle)); break;
                        case "2": ViewShapeDetail(CreateShape(ShapeType.Circle)); break;
                        case "3": ViewShapeDetail(CreateShape(ShapeType.Ellipse)); break;
                        case "4": ViewShapeDetail(CreateShape(ShapeType.Cuboid)); break;
                        case "5": ViewShapeDetail(CreateShape(ShapeType.Cylinder)); break;
                        case "6": ViewShapeDetail(CreateShape(ShapeType.Sphere)); break;
                        case "7":
                            random2DShapes = Randomize2DShapes();
                            Array.Sort(random2DShapes); 
                            ViewShapes(random2DShapes); 
                            break;
                        case "8":
                            random3DShapes = Randomize3DShapes();
                            Array.Sort(random3DShapes);
                            ViewShapes(random3DShapes);
                            break;
                        default: throw new FormatException("FEL! Ange ett nummer mellan 0 och 8");
                    }
                } 
                catch(Exception e)
                {
                    ViewMenuErrorMessage(e.Message);
                }

                // Display continue text.
                if(!exitProgram)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("\n   Tryck på tangent för att fortsätta   ");
                    Console.ResetColor();
                    Console.ReadKey();
                }

                Console.WriteLine();

            // Continue until user exits program
            } while (!exitProgram);
        }

        private static Shape CreateShape(ShapeType shapeType)
        {
            double[] shapeDimensions;

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine(Extensions.CenterAlignString("║                                      ║", Extensions.AsText(shapeType)));
            Console.WriteLine("╚══════════════════════════════════════╝\n");
            Console.ResetColor();

            // Get input on dimensions
            shapeDimensions = ReadDimensions(shapeType);

            // Create shape
            switch (shapeType)
            {
                case ShapeType.Circle: return new Ellipse(shapeDimensions[0]);
                case ShapeType.Cuboid: return new Cuboid(shapeDimensions[0], shapeDimensions[1], shapeDimensions[2]);
                case ShapeType.Cylinder: return new Cylinder(shapeDimensions[0], shapeDimensions[1], shapeDimensions[2]);
                case ShapeType.Ellipse: return new Ellipse(shapeDimensions[0], shapeDimensions[1]);
                case ShapeType.Rectangle: return new Rectangle(shapeDimensions[0], shapeDimensions[1]);
                case ShapeType.Sphere: return new Sphere(shapeDimensions[0]);
                default: throw new NotImplementedException("Stöd för ShapeType:n saknas i metoden CreateShape.");
            }
        }

        private static Shape2D[] Randomize2DShapes()
        {
            Random random = new Random();
            int numberOfShapes = random.Next(5, 21);
            Shape2D[] returnShapes = new Shape2D[numberOfShapes];

            for(int i = 0; i < numberOfShapes; i++)
            {
                switch(random.Next(3))
                {
                    case 0: 
                        returnShapes[i] = new Ellipse((random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble()));
                        break;
                    case 1:
                        returnShapes[i] = new Ellipse((random.Next(5, 100) + random.NextDouble()));
                        break;
                    case 2:
                        returnShapes[i] = new Rectangle((random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble()));
                        break;
                }
            }

            return returnShapes;
        }

        private static Shape3D[] Randomize3DShapes()
        {
            Random random = new Random();
            int numberOfShapes = random.Next(5, 21);
            Shape3D[] returnShapes = new Shape3D[numberOfShapes];

            for (int i = 0; i < numberOfShapes; i++)
            {
                switch (random.Next(3))
                {
                    case 0:
                        returnShapes[i] = new Cuboid((random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble()));
                        break;
                    case 1:
                        returnShapes[i] = new Cylinder((random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble()));
                        break;
                    case 2:
                        returnShapes[i] = new Sphere((random.Next(5, 100) + random.NextDouble()));
                        break;
                }
            }

            return returnShapes;
        }

        private static double[] ReadDimensions(ShapeType shapeType)
        {
            switch (shapeType)
            {
                case ShapeType.Circle: return ReadDoublesGreaterThanZero("Ange figurens diameter: ", 1);
                case ShapeType.Cuboid: return ReadDoublesGreaterThanZero("Ange figurens längd, bredd och höjd: ", 3);
                case ShapeType.Cylinder: return ReadDoublesGreaterThanZero("Ange figurens horisontella radie, vertikala radie och höjd: ", 3);
                case ShapeType.Ellipse: return ReadDoublesGreaterThanZero("Ange figurens horisontella radie och vertikala radie: ", 2);
                case ShapeType.Rectangle: return ReadDoublesGreaterThanZero("Ange figurens längd och bredd: ", 2);
                case ShapeType.Sphere: return ReadDoublesGreaterThanZero("Ange figurens radie: ", 1);
                default: throw new NotImplementedException("Textsträng för ShapeType:n saknas i metoden AsText.");
            }
        }

        private static double[] ReadDoublesGreaterThanZero(string prompt, int numberOfValues = 1)
        {
            double[] parsedInput = new double[numberOfValues];
            string[] inputStrings;

            while(true)
            {
                try
                {
                    // Display user with custom message to enter values
                    Console.Write(prompt);

                    // Get and split input
                    inputStrings = Console.ReadLine().Split(new char[] {' ' ,','});

                    // Check count of given input
                    if(inputStrings.Length != numberOfValues)
                    {
                        throw new FormatException("FEL! Antal angivna värden stämmer inte.");
                    }

                    // Loop trough input strings and try to parse them
                    for(int i = 0; i < inputStrings.Length; i++)
                    {
                        // Try to parse input
                        if (!double.TryParse(inputStrings[i], out parsedInput[i]))
                        {
                            // Failed to parse.
                            throw new FormatException("FEL! Ett fel inträffade då figurens dimensioner tolkades.");
                        }
                        else if(parsedInput[i] <= 0)
                        {
                            throw new FormatException("FEL! De angivna värdena måste vara högre än 0.");
                        }
                    }

                    // Return parsed values
                    return parsedInput;
                }
                catch(Exception e)
                {
                    ViewMenuErrorMessage(e.Message);
                }
            }
        }

        private static void ViewMenu()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║                                      ║");
            Console.WriteLine(Extensions.CenterAlignString("║                                      ║", "Geometriska figurer"));
            Console.WriteLine("║                                      ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine("\n0. Avsluta.");
            Console.WriteLine("1. Rektangel.");
            Console.WriteLine("2. Cirkel.");
            Console.WriteLine("3. Ellips.");
            Console.WriteLine("4. Rätblock.");
            Console.WriteLine("5. Cylinder.");
            Console.WriteLine("6. Sfär.");
            Console.WriteLine("7. Slumpa 2D-figurer.");
            Console.WriteLine("8. Slumpa 3D-figurer.");
            Console.WriteLine("\n────────────────────────────────────────\n");
            Console.Write("Ange menyval [0-8]: ");
        }

        private static void ViewMenuErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(String.Format("\n  {0}  \n", message));
            Console.ResetColor();
        } 

        private static void ViewShapeDetail(Shape shape)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine(Extensions.CenterAlignString("║                                      ║", "Detaljer"));
            Console.WriteLine("╚══════════════════════════════════════╝\n");
            Console.ResetColor();

            Console.WriteLine(shape.ToString());
            Console.WriteLine("\n───────────────────────────────────────");
        }

        private static void ViewShapes(Shape[] shapes)
        {
            Console.Clear();

            if (shapes[0].IsShape3D)
            {
                Console.WriteLine("─────────────────────────────────────────────────────────────────────────");
                Console.WriteLine(String.Format("{0,-10}{1,7}{2,7}{3,7}{4,14}{5,14}{6,14}", "Figur", "Längd", "Bredd", "Höjd", "Mantelarea", "Begräns.area", "Volym"));
                Console.WriteLine("─────────────────────────────────────────────────────────────────────────");

            }
            else
            {
                Console.WriteLine("─────────────────────────────────────────────────────────────────────────");
                Console.WriteLine(String.Format("{0,-10}{1,7}{2,7}{3,12}{4,12}", "Figur", "Längd", "Bredd", "Omkrets", "Area"));
                Console.WriteLine("─────────────────────────────────────────────────────────────────────────");
            }

            foreach(Shape shape in shapes)
            {
                Console.WriteLine(shape.ToString("R"));
            }
        }
    }
}
