using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    [Serializable]
    public class EmptyTile : Tile
    {
        public EmptyTile(Position position) : base(position) //constructor for the empty tile
        {
        }

        public override char Display // display for the empty tile
        {
            get
            {
                return '.';
            }
        }

    }
}
