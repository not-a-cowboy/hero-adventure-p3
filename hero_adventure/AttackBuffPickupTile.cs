using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    [Serializable]
    internal class AttackBuffPickupTile : PickupTile
    {
        public AttackBuffPickupTile(Position position) : base(position)
        {
        }

        public override char Display
        {
            get { return '*'; }
        }

        public override void ApplyEffect(CharacterTile target)
        {
            target.SetDoubleDamage(3);
        }
    }
}
