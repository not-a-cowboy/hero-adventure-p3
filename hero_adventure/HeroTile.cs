using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    public class HeroTile : CharacterTile
    {
        public HeroTile(Position position) : base(position, 40, 5) // hero with 40 hit points and 5 attack power
        {
        }

        public override char Display // hero's tile display
        {
            get
            {
                if (IsDead)
                {
                    return 'X'; // hero symbol if dead
                }
                else
                {
                    return '▼';  // hero symbol if alive
                }


            }
        }

    }
}

