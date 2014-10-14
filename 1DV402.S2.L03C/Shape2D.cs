using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S2.L03C
{
    abstract class Shape2D : Shape, IComparable
    {
        private double _length;
        private double _width;

        // Propterties
        public double Length
        {
            get
            {
                return _length;
            }
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentException("Värdet Length måste vara högre än 0.");
                }
                _length = value;
            }
        }

        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Värdet Width måste vara högre än 0.");
                }
                _width = value;
            }
        }

        public abstract double Area
        {
            /*
             *  Publik abstrakt egenskap av typen double representerande en figurs area. 
             */
            get;
        }

        public abstract double Perimeter
        {
            /*
            * Publik abstrakt egenskap av typen double representerande en figurs omkrets.
            */
            get;
        }

        // Constructor
        protected Shape2D(ShapeType shapeType, double length, double width) : base(shapeType) // Set shapetype via parent class constructor.
        {
            /*
             * Konstruktorn, som ska vara ”protected”, ansvara för att fälten, via egenskaperna, tilldelas de värden 
                konstruktorns parametrar har.
             */

            Width = width;
            Length = length;
        }

        // Methods
        public int CompareTo(object obj)
        {
            /*
             * Metoden ska jämföra två objekt med avseende på deras areor. 
                • Refererar parametern till null ska ett heltal större än 0 returneras.
                • Refererar parametern till ett objekt som inte är av typen Shape2D ska ett undantag av typen 
                ArgumentException kastas.
                • Refererar parametern till ett objekt vars area är större än det anropande objektet ska ett heltal 
                mindre än 0 returneras.
                • Refererar parametern till ett objekt vars area är mindre än det anropande objektet ska ett heltal 
                större än 0 returneras.
                • Refererar parametern till ett objekt vars area är lika med det anropande objektet ska heltalet 0
                returneras.
             */

            // If obj is null
            if(obj == null)
            {
                return 1;
            }

            Shape2D testObj = obj as Shape2D;

            // If obj isnt a Shape2D
            if (testObj == null)
            {
                throw new ArgumentException("Objektet är inte av typen Shape2D");
            }

            if(testObj.Area > this.Area)
            {
                return -1;
            }
            else if(testObj.Area < this.Area)
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
             * 
             * Är formatsträngen ”G”, en tomsträng eller null, ska strängen formateras så separata rader innehåller 
                ledtext och värden för figurens läng, bredd, omkrets och area.
             * 
                Är formatsträngen ”R” ska strängen formateras så en rad innehåller ledtext och värden för figurens 
                längd, bredd, omkrets och area.
                
             * Alla övriga värden på formatsträngen ska leda till att ett undantag av typen FormatException kastas.
                Metoden ToString() ska returnera en sträng formaterad enligt kraven för formatsträngen ”G”.
             * 
             */

            if(format == null || format == "" || format == "G")
            {
                return String.Format("{1,-17}{0,-10}{2,10:0.0}\n{3,-17}{0,-10}{4,10:0.0}\n{5,-17}{0,-10}{6,10:0.0}\n{7,-17}{0,-10}{8,10:0.0}",
                                        ":", "Längd", Length, "Bredd", Width, "Omkrets", Perimeter, "Area", Area);
            }
            else if(format == "R")
            {
                return String.Format("{0,-10}{1,7:0.0}{2,7:0.0}{3,12:0.0}{4,12:0.0}", this.ShapeType, Length, Width, Perimeter, Area);
            }
            else
            {
                throw new FormatException("Argumentet måste vara en tomsträng, 'G', eller 'R'");
            }
        }
    }
}
