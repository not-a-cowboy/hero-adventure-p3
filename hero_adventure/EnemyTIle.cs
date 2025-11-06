using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    public abstract class EnemyTIle: CharacterTile
    {
        Position position;
        int hit_points;
        int attck_pwr;

        public EnemyTIle(Position position, int hit_points, int attck_pwr): base (position, hit_points, attck_pwr)
        {
        }

        public abstract bool GetMove(Level level, Tile tile);
        public abstract CharacterTile[] GetTarget();
    }
}
