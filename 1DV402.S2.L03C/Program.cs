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
            /*
             Metoden Main ska anropa metoden ViewMenu() för att visa en meny. Så längs som användaren inte 
                väljer att avsluta applikationen ska menyn visas på nytt efter att en figurs, eller flera figurers, detaljer 
                presenterats.
                Väljer användaren att skapa en figur ska metoden CreateShape() anropas som skapar och returnerar 
                en referens till ett objekt som symboliserar den figur som valts. Referensen till objektet används sedan 
                vid anrop av ViewShapeDetail() som presenterar figurens detaljer.
                Användaren ska även kunna välja att slumpa 2D-figurer och 3D-figurer. Då ska metoden 
                Randomize2DShapes() respektive Randomize3DShapes() anropas. Arrayen med referenser som 
                metoderna returnerar ska sorteras och skickas med som argument vid anrop av metoden 
                ViewShapes() som presenterar figurernas detaljer i form av en enkel tabell.
                Meny alternativen är numrerade från 0 till 8 och väljer inte användaren ett värde i det slutna intervallet 
                mellan 0 och 8 ska ett felmeddelande visas och användaren uppmanas att trycka på en tangent för att 
                få en ny chans att ange ett korrekt menyalternativ.
             * 
             * Metoden Main() i klassen Program ska se till att en meny visas där användaren kan välja 
                för vilken typ av figur beräkningar och presentation ska göras. Det ska även vara möjligt att 
                slumpa 2D- eller 3D-figurer, som sorteras med avseende på area respektive volym, och 
                presentera en tabell över de framslumpad
             * 
             * Metoden Main() i klassen Program ska se till att figurer av typerna Ellipse och 
                Rectangle slumpas, sorteras och presenteras.
             */
            bool exitProgram = false;
            Shape2D[] random2DShapes;
            Shape3D[] random3DShapes;

            do
            {
                Console.Clear();

                ViewMenu();

                // Read choice
                switch(Console.ReadLine())
                {
                    case "0": exitProgram = true;  break;
                    case "1": ViewShapeDetails(CreateShape(ShapeType.Rectangle)); break;
                    case "2": ViewShapeDetails(CreateShape(ShapeType.Circle)); break;
                    case "3": ViewShapeDetails(CreateShape(ShapeType.Ellipse)); break;
                    case "4": ViewShapeDetails(CreateShape(ShapeType.Cuboid)); break;
                    case "5": ViewShapeDetails(CreateShape(ShapeType.Cylinder)); break;
                    case "6": ViewShapeDetails(CreateShape(ShapeType.Sphere)); break;
                    case "7": random2DShapes = Randomize2DShapes(); Array.Sort(random2DShapes); ViewShapes(random2DShapes); break;
                    case "8": random3DShapes = Randomize3DShapes(); Array.Sort(random3DShapes); ViewShapes(random3DShapes); break;
                    default: ViewMenuErrorMessage("FEL! Ange ett nummer mellan 0 och 8"); break;
                }

                // Give user invisible choise of exiting.
                if(!exitProgram)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("\n   Tryck på tangent för att fortsätta, Escape för att avsluta   ");
                    Console.ResetColor();

                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                    {
                        exitProgram = true;
                    }
                }

            // Continue until user exits program
            } while (!exitProgram);
        }

        private static Shape CreateShape(ShapeType shapeType)
        {
            /*
             * Den privata statiska metoden CreateShape ska läsa in en figurs dimensioner, skapa objektet och 
                returnera en referens till det. Metoden ska ha en parameter av typen ShapeType vars värde bestämmer 
                vilken typ av figur som ska skapas.
                Metoden ansvara för att en rubrik skrivs ut där det framgår vilken typ av figur dimensionerna ska 
                anges för.
             * 
                Att ”översätta” ett enum-värde till text kan inte göras genom att överskugga ToString() för den 
                uppräkningsbara typen ShapeType. Det är dock fullt möjligt att skapa en utökningsmetod, 
                Extensions.AsText(), som gör detta.
                Strängen med figurens typ ska centreras rubriken. Det finns ingen metod i klassen String där en 
                sträng kan centreras i förhållande till en annan sträng. Elegantast löses även detta med hjälp av en 
                utökningsmetod, Extensions.CenterAlignString().
                CreateShape läser inte några värden utan det ska ske genom anrop av metoden ReadDimensions()
                som returnerar en array med värden av typen double där antalet värden i array beror på typen av figur.
             */

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
            /*
             * Den privata statiska metoden Randomize2DShapes() ska slumpa mellan 5 och 20 figurer vars längder 
                och bredder slumpas till värden av typen double i det halvöppna intervallet mellan 5,0 och 100,0
                ([5, 100[).
                Typ av figur ska också slumpas och då måste en ”switch”-sats användas tillsammans med 
                uppräkningsbara typen ShapeType, som ska användas för att typomvandla heltalet 0 till 
                ShapeType.Ellips, heltalet 1 till ShapeType.Circle och heltalet 2 till ShapeType.Rectangle.
                Referenserna till figurerna ska sparas i en array som metoden returnerar en referens till.
             */

            Random random = new Random();
            int numberOfShapes = random.Next(5, 21);
            Shape2D[] returnShapes = new Shape2D[numberOfShapes];

            for(int i = 0; i < numberOfShapes; i++)
            {
                switch(random.Next(3))
                {
                    case 0: returnShapes[i] = new Ellipse((random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble())); break;
                    case 1: returnShapes[i] = new Ellipse((random.Next(5, 100) + random.NextDouble())); break;
                    case 2: returnShapes[i] = new Rectangle((random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble())); break;
                }
            }

            return returnShapes;
        }

        private static Shape3D[] Randomize3DShapes()
        {
            /*
             * Den privata statiska metoden Randomize3DShapes() ska slumpa mellan 5 och 20 figurer vars 
                dimensioner slumpas till värden av typen double i det halvöppna intervallet mellan 5,0 och 100,0
                ([5, 100[).
                Typ av figur ska också slumpas och då måste en ”switch”-sats användas tillsammans med 
                uppräkningsbara typen ShapeType, som ska användas för att typomvandla heltalet 3 till 
                ShapeType.Cuboid, heltalet 4 till ShapeType.Cylinder och heltalet 5 till ShapeType.Sphere.
                Referenserna till figurerna ska sparas i en array som metoden returnerar en referens till.
             */

            Random random = new Random();
            int numberOfShapes = random.Next(5, 21);
            Shape3D[] returnShapes = new Shape3D[numberOfShapes];

            for (int i = 0; i < numberOfShapes; i++)
            {
                switch (random.Next(3))
                {
                    case 0: returnShapes[i] = new Cuboid((random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble())); break;
                    case 1: returnShapes[i] = new Cylinder((random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble()), (random.Next(5, 100) + random.NextDouble())); break;
                    case 2: returnShapes[i] = new Sphere((random.Next(5, 100) + random.NextDouble())); break;
                }
            }

            return returnShapes;
        }

        private static double[] ReadDimensions(ShapeType shapeType)
        {
            /*
             * Den privata statiska metoden ReadDimensions() ska returnera en array där elementens värden är av 
                typen double och större än 0. 
                Till metoden ska det vara möjligt att skicka med ett argument av typen ShapeType som bestämmer 
                argumenten, ledtext och antal värden, som används vid anrop av metoden 
                ReadDoublesGreaterThanZero() som sköter själva inläsningen av figurens dimensioner.
             */

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
            /*
             * Den privata statiska metoden ReadDoublesGreaterThanZero() ska returnera en array där elementens
                värden är av typen double och större än 0.
             * 
                Till metoden ska det vara möjligt att skicka med två argument. Det första argumentet ska vara en sträng 
                med information som ska visas i anslutning till där inmatningen av värdet sker. Det andra argumentet 
                ska ange hur många värden som ska anges. I Figur C.18 har olika strängar, och antalet förväntade 
                värden, skickats med som argument vid anropet av metoden.
             * 
                Samtliga värden ska användaren kunna ange på en och samma rad. Den inlästa strängen ska sedan 
                delas upp i delsträngar där varje delsträng ska tolkas till ett värde av typen double. Om det inmatade 
                inte kan tolkas som ett korrekt värde, eller om antalet angivna värden inte överensstämmer med det 
                andra argumentets värde, ska användaren få en chans att göra en ny inmatning efter att ett tydligt 
                felmeddelande presenterats (se Figur C.19).
             */
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
            /*
             * Den privata statiska metoden ViewMenu() ska bara presentera en meny. Någon inläsning ska inte 
                göras av metoden.
             */
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

        private static void ViewMenuErrorMessage(string message) // <- ska inte ha något argument egentligen.
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(String.Format("\n{0}\n", message));
            Console.ResetColor();
        } 

        private static void ViewShapeDetails(Shape shape)
        {
            /*
             * Den privata statiska metoden ViewShapewDetail() ska presentera en figurs detaljer. Vid anrop av 
                metoden skickas ett argument med som refererar till figurens vars detaljer ska presenteras. Parametern 
                shape av typen Shape refererar till figuren. Genom att utnyttja att basklasserna Shape2D och Shape3D
                överskuggar metoden ToString() förenklas koden väsentligt då en figurs detaljer ska presenteras.
             */
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine(Extensions.CenterAlignString("║                                      ║", "Detaljer"));
            Console.WriteLine("╚══════════════════════════════════════╝\n");
            Console.ResetColor();

            Console.WriteLine(shape.ToString());
            Console.WriteLine("\n───────────────────────────────────");
        }

        private static void ViewShapes(Shape[] shapes)
        {
            /*
                Den privata statiska metoden ViewShapes() ska presentera figurerna som skickas till metoden i en 
                enkel tabell. Metoden måste se till en figurs detaljer presenteras genom att använda en formaterad 
                textbeskrivning för varje objekt, d.v.s. ToString(”R”) måste användas då ett objekts detaljer ska 
                presenteras.
             */

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
