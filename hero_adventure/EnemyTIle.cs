using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    [Serializable]
    public abstract class EnemyTIle: CharacterTile
    {
        protected Level level;

        Position position;
        int hit_points;
        int attck_pwr;

        public EnemyTIle(Position position, int hit_points, int attck_pwr, Level level): base (position, hit_points, attck_pwr)
        {
            {
                this.level = level;
            }
        }

        public abstract bool GetMove(Level level, out Tile tile);
        public abstract CharacterTile[] GetTarget();

    }
}
