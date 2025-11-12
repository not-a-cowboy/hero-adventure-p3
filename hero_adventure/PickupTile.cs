using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    [Serializable]
    public abstract class PickupTile : Tile
    {
        protected PickupTile(Position pos) : base(pos) { }

        // apply effect to a character
        public abstract void ApplyEffect(CharacterTile target);
    }
}
