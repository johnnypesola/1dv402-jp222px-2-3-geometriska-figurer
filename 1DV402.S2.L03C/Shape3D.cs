using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L03C
{
    abstract class Shape3D : Shape, IComparable
    {
        protected Shape2D _baseShape;
        private double _height;

        // Properties
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Värdet Height måste vara högre än 0.");
                }
                _height = value;
            }
        }

        public abstract double MantelArea { get; }

        public abstract double TotalSurfaceArea { get; }

        public abstract double Volume { get; }

        // Constructors
        protected Shape3D(ShapeType shapeType, Shape2D baseShape, double height) : base(shapeType)
        {
            /*
             * Konstruktorn, som ska vara ”protected”, ansvarar för att fälten, via egenskaper då sådana finns, 
                tilldelas de värden konstruktorns parametrar har.
             */

            Height = height;
            _baseShape = baseShape;
        }

        // Methods
        public int CompareTo(object obj)
        {
            /*
             * Metoden ska jämföra två objekt med avseende på deras volymer. 
                • Refererar parametern till null ska ett heltal större än 0 returneras.
                • Refererar parametern till ett objekt som inte är av typen Shape3D ska ett undantag av typen 
                ArgumentException kastas.
                • Refererar parametern till ett objekt vars volym är större än det anropande objektet ska ett 
                heltal mindre än 0 returneras.
                • Refererar parametern till ett objekt vars volym är mindre än det anropande objektet ska ett 
                heltal större än 0 returneras.
             */
            // If obj is null
            if (obj == null)
            {
                return 1;
            }

            Shape3D testObj = obj as Shape3D;

            // If obj isnt a Shape3D
            if (testObj == null)
            {
                throw new ArgumentException("Objektet är inte av typen Shape3D");
            }

            if (testObj.Volume > this.Volume)
            {
                return -1;
            }
            else if (testObj.Volume < this.Volume)
            {
                return 1;
            }
            else
            {
                // They must be the same
                return 0;
            }
        }

        public override string ToString()
        {
            return ToString("G");
        }

        public override string ToString(string format)
        {
            /*
             * Den publika metoden ToString(string format) har som uppgift att returnera en sträng 
                representerande värdet av en instans. Formatsträngen ska bestämma hur textbeskrivningen av 
                instansen ska formateras. 
                Är formatsträngen ”G”, en tomsträng eller null, ska strängen formateras så separata rader innehåller 
                ledtext och värden för figurens läng, bredd, höjd, mantelarea, begränsningsarea och volym.
                Figur C.10
                Är formatsträngen ”R” ska strängen formateras så en rad innehåller ledtext och värden för figurens 
                längd, bredd, höjd, mantelarea, begränsningsara och volym.
                Figur C.11.
                Alla övriga värden på formatsträngen ska leda till att ett undantag av typen FormatException kastas.
             */

            if (format == null || format == "" || format == "G")
            {
                return String.Format("{1,-17}{0,-10}{2,10:0.0}\n{3,-17}{0,-10}{4,10:0.0}\n{5,-17}{0,-10}{6,10:0.0}\n{7,-17}{0,-10}{8,10:0.0}\n{9,-17}{0,-10}{10,10:0.0}\n{11,-17}{0,-10}{12,10:0.0}", 
                                        ":", "Längd", _baseShape.Length, "Bredd", _baseShape.Width, "Höjd", Height, "Mantelarea", MantelArea, "Begränsningarea", TotalSurfaceArea, "Volym", Volume);
            }
            else if (format == "R")
            {
                return String.Format("{0,-10}{1,7:0.0}{2,7:0.0}{3,7:0.0}{4,14:0.0}{5,14:0.0}{6,14:0.0}", this.ShapeType, _baseShape.Length, _baseShape.Width, Height, MantelArea, TotalSurfaceArea, Volume);
            }
            else
            {
                throw new FormatException("Argumentet måste vara en tomsträng, 'G', eller 'R'");
            }
        }
    }
}
