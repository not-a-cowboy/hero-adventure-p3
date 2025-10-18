using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    public abstract class Tile
    {

        private Position position; //positioning field

        public int X 
        { 
            get { return position.X; } // x variable 
            set { position.X = value; }
        }

        public int Y // y variable
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public Tile(Position position) //constructor for the tile class 
        {
            this.position = position;
        }

        //optional
        public Position pos 
            { get { return position; } }

        public abstract char Display // abstract display property
        {  
            get;
        }

        public Position Position //this property is used to help us manipulate certain tiles within the Level Class, especially the HeroTile class within the Level constructor
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }
    }
    //okay bro

}
