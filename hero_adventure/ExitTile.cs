using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{ 
   public class ExitTile: Tile
    {

        private bool isLocked = true;

        public bool IsLocked 
        { 
            get { return isLocked; } 
        }

            public ExitTile(Position position) : base(position) // constructor for exit tile
            {
            }

            public override char Display
            {
                get
                {
                    if (isLocked)
                    {
                        return '▓';
                    }
                    else
                    { 
                        return '▒';
                    }
                     
                }
            }

        public void Unlocked()
        {
            isLocked = false;
        }
    }      
}



