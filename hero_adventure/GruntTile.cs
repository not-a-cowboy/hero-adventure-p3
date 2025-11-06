using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    internal class GruntTile : EnemyTIle
    {
        public GruntTile(Position pos) : base(pos, 10, 1)
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
                return 'Ϫ';
            }
        }


        public override bool GetMove(Level level, out Tile target)
        {
            Random random = new Random();
            List<Tile> moves = new List<Tile>();

            this.UpdateVision(level);

            foreach (Tile tile in this.plyr_vision)
            {
                if (tile is EmptyTile)
                {
                    moves.Add(tile);
                }
            }

            if (moves.Count == 0)
            {
                target = null;
                return false;
            }

            target = moves[random.Next(moves.Count)];
            return true;
        }

        public override CharacterTile[] GetTarget()
        {
            List<CharacterTile> targets = new List<CharacterTile>();

            foreach (Tile tile in this.plyr_vision)
            {
                if (tile is HeroTile hero)
                {
                    targets.Add(hero);
                }
            }
            return targets.ToArray();
        }


        public CharacterTile[] GetTarget(Level level)
        {
            List<CharacterTile> targets = new List<CharacterTile>();
            this.UpdateVision(level);

            foreach (Tile tile in this.plyr_vision) // Use the new accessor
            {
                if (tile is HeroTile hero)
                {
                    targets.Add(hero);
                }
            }
            return targets.ToArray();
        }


        /*protected Tile[] GetVision()
        {
            return (Tile[])typeof(CharacterTile)
                .GetField("plyr_vision", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .GetValue(this);
        }*/
    }
}
