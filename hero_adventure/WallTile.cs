using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    [Serializable]
    public class WallTile : Tile
    {
        public WallTile(Position position) : base(position) // constructor
        {
        }

        public override char Display // wall tile display
        {
            get
            {
                return '█';
            }
        }
    }
}
