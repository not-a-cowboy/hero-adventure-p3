using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace hero_adventure
{
    public abstract class CharacterTile : Tile
    {// abstract class for character tiles
        private int hit_points; 
        private int max_hit_pts;
        private int attack_pwr;
        private Tile[] plyr_vision;


        public int HitPoints
        {
            get { return hit_points; } // current hit points
            set { hit_points = value; }
        }

        public int MaxHitPoints
        {
            get { return max_hit_pts; } //max hit points 
        }

        public int Attackpwr // hero attack power
        {
            get { return attack_pwr; }
        }

        public bool IsDead 

        {
            get { return hit_points <= 0; } // checking if character is dead
        }


        public CharacterTile (Position position, int hit_points, int attack_pwr) : base (position)
        {
            this.hit_points = 40;
            this.max_hit_pts = 40;
            this.attack_pwr = 5;
            this.plyr_vision = new Tile[5]; // placeholder for vision array
        }
        public abstract override char Display { get; } //character visionTiles display

        public void UpdateVision(Level level)
        {

            int x = this.X;
            int y = this.Y;

            // Ensure plyr_vision has 5 elements
            if (plyr_vision == null || plyr_vision.Length != 5)
                plyr_vision = new Tile[5];

            // 0 = Up
            plyr_vision[0] = (y - 1 >= 0) ? level.Tiles[x, y - 1] : null;
            // 1 = Right
            plyr_vision[1] = (x + 1 < level.Width) ? level.Tiles[x + 1, y] : null;
            // 2 = Down
            plyr_vision[2] = (y + 1 < level.Height) ? level.Tiles[x, y + 1] : null;
            // 3 = Left
            plyr_vision[3] = (x - 1 >= 0) ? level.Tiles[x - 1, y] : null;
            // 4 = Current
            plyr_vision[4] = level.Tiles[x, y];

        }

        public void Heal(int amount)
        {
            HitPoints += amount; // increase current hit points
            if (HitPoints > MaxHitPoints) // ensure it doesn't exceed maximum
            {
                HitPoints = MaxHitPoints;
            }
        }
    }
}
