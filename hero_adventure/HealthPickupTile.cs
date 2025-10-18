using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    public class HealthPickupTile : PickupTile
    {
        public HealthPickupTile(Position pos) : base(pos) { }

        public override char Display => '+';

        public override void ApplyEffect(CharacterTile target)
        {
            if (target == null) return;
            target.Heal(10);
        }
    }
}
