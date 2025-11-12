using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    [Serializable]
    internal class WarlockTile : EnemyTIle
    { 
        public WarlockTile(Position pos, Level level) : base(pos, 10, 5, level) 
        { 
        }

        public override char Display
        {
            get
            {
                if (IsDead)
                {
                    return 'X';
                }
                return 'ᐂ';
            }
        }
        public override bool GetMove(Level level, out Tile tile)
        {
            tile = null; // warlock cannot move
            return false; // returns no movement
        }

        public override CharacterTile[] GetTarget()
        {
            List<CharacterTile> targets = new List<CharacterTile>();

            // These are the positions for all 8 adjacent tiles
            int[] tileX = { -1, 0, 1, -1, 1, -1, 0, 1 };
            int[] tileY = { -1, -1, -1, 0, 0, 1, 1, 1 };

            for (int i = 0; i < tileX.Length; i++)
            {
                int newX = Position.X + tileX[i];
                int newY = Position.Y + tileY[i];

                if (newX >= 0 && newX < level.Width && newY >= 0 && newY < level.Height)
                {
                    Tile t = level.Tiles[newX, newY];
                    if (t is CharacterTile character && !character.IsDead)
                    {
                        targets.Add(character);
                    }
                }
            }

            return targets.ToArray();
        }

    }
    
}
