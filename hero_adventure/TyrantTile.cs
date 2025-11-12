using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    [Serializable]
    internal class TyrantTile :EnemyTIle
    {
        public TyrantTile(Position pos, Level level) : base(pos, 15, 5, level) 
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
                return '§';
            }
        }

        public override bool GetMove(Level level, out Tile target)
        {
            target = null;

            if (IsDead)
            {
                return false;
            }

            int heroX = level.Hero.X;
            int heroY = level.Hero.Y;
            int newX = this.X;
            int newY = this.Y;

            //will move the tyrant towards the hero horizontally
            if (heroX > this.X) 
            {                 
                newX += 1;
            }
            else if (heroX < this.X) 
            {
                newX -= 1; 
            }
                
            //will move the tyrant towards the hero vertically
            if (heroY > this.Y) 
            {
                newY += 1; 
            }                
            else if (heroY < this.Y) 
            {
                newY -= 1; 
            }
                
            // prevents tyrant from moving onto the hero tile/position
            if (newX == heroX && newY == heroY)
            {
                return false;
            }

            target = level.Tiles[newX, newY];

            if (target is EmptyTile) 
            {
                return true; 
            }
                
            return false;
        }

        public override CharacterTile[] GetTarget()
        {
            List<CharacterTile> targets = new List<CharacterTile>();

            Tile[,] tileArray = level.Tiles; // 'level' is the protected Level field in EnemyTile
            int tyrantX = this.Position.X;
            int tyrantY = this.Position.Y;


            for (int x = 0; x < level.Width; x++) 
            {
                if (x != tyrantX && tileArray[x, tyrantY] is CharacterTile target) //will not target its own x coordinate
                {
                    targets.Add(target);
                }
            }

            for (int y = 0; y < level.Height; y++)
            {
                if (y != tyrantY && tileArray[tyrantX, y] is CharacterTile target) //will not target its own y coordinate
                {
                    targets.Add(target);
                }
            }

            return targets.ToArray();
        }
    }
}
