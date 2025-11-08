using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{ 
   public class ExitTile: Tile
    {
       
            public ExitTile(Position position) : base(position) // constructor for exit tile
            {
            }

            public override char Display
            {
                get
                {
                    return 'E'; // symbol for exit tile
                }
            }
    }      
}



