using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hero_adventure;


public class Level
{
    // 2D array of tiles
    private Tile[,] tiles;
    private int width;
    private int height;
    private ExitTile exit; // exit tile field
    private HeroTile hero; // hero stored as a field
    private EnemyTIle[] enemy;
    private static Random rand = new Random(); // shared random generator
    private PickupTile[] pickupArray; //4.3

    public PickupTile[] PickupArray //4.3
    {
        get
        {
            return pickupArray;
        }
    }

    public void ReplaceToEmpty(Position pos) //4.3
    {
        tiles[pos.X, pos.Y] = new EmptyTile(new Position(pos.X, pos.Y));
    }
    public void MoveHeroTo(HeroTile hero, Position newPos)
    {
        // Replace old hero position with empty
        tiles[hero.Position.X, hero.Position.Y] = new EmptyTile(new Position(hero.Position.X, hero.Position.Y));

        hero.Position = newPos;

        // If the target tile was an ExitTile, keep it in the array but track hero separately
        if (tiles[newPos.X, newPos.Y] is ExitTile)
        {
            // Don’t overwrite the exit, just note hero is there
            tiles[newPos.X, newPos.Y] = hero; // you can keep hero for display, but track exit separately
        }
        else
        {
            tiles[newPos.X, newPos.Y] = hero;
        }
    }

    //helper method for gameengine triggermovement
    public bool MoveHero(GameEngine.Direction direction)
    {
        // Get the current hero position
        Position oldPos = hero.Position;
        Position newPos = new Position(oldPos.X, oldPos.Y);

        // Adjust position based on direction
        switch (direction)
        {
            case GameEngine.Direction.Up:
                newPos.Y -= 1;
                break;
            case GameEngine.Direction.Down:
                newPos.Y += 1;
                break;
            case GameEngine.Direction.Left:
                newPos.X -= 1;
                break;
            case GameEngine.Direction.Right:
                newPos.X += 1;
                break;
        }

        // Check bounds
        if (newPos.X < 0 || newPos.X >= width || newPos.Y < 0 || newPos.Y >= height)
            return false;

        // Check if tile is empty or a pickup
        Tile targetTile = tiles[newPos.X, newPos.Y];

        if (targetTile is EmptyTile)
        {
            MoveHeroTo(hero, newPos); // Move the existing hero
            UpdateVision();
            return true;
        }
        else if (targetTile is PickupTile pickup)
        {
            pickup.ApplyEffect(hero);
            ReplaceToEmpty(newPos);
            MoveHeroTo(hero, newPos);
            UpdateVision();
            return true;
        }

        return false;
    }

    // read-only properties
    public int Width => this.width;
    public int Height => this.height;
    public Tile[,] Tiles => this.tiles;
    public HeroTile Hero => this.hero;
    public ExitTile Exit => this.exit;
    public EnemyTIle[] Enemy => this.enemy;
    public enum TileType
    {
        Empty,
        Wall,
        Hero,
        Exit,
        Enemy,
        Pickup //4.3

    }

    public Level(int width, int height, int num_of_enemies, HeroTile hero = null, int numOfPickUps = 1)
    {
        this.width = width;
        this.height = height;
        tiles = new Tile[this.width, this.height];
        enemy = new EnemyTIle[num_of_enemies];

        InitialiseTiles();

        // Placing a hero
        Position heroPos = GetRandomEmptyPosition();
        if (hero == null)
        {
            this.hero = (HeroTile)CreateTile(TileType.Hero, heroPos);
        }
        else
        {
            tiles[hero.X, hero.Y] = new EmptyTile(new Position(hero.X, hero.Y));
            this.hero = hero;
            tiles[hero.X, hero.Y] = hero;
            //heroPos.X = hero.X;
            //heroPos.Y = hero.Y;
            //this.hero = hero;
            //tiles[hero.X, hero.Y] = hero;
        }

        // Place exit
        Position exitPos = GetRandomEmptyPosition();
        exit = (ExitTile)CreateTile(TileType.Exit, exitPos);


        for (int i = 0; i < this.enemy.Length; i++)
        {
            Position enemyPos = GetRandomEmptyPosition();
            this.enemy[i] = (EnemyTIle)CreateTile(TileType.Enemy, enemyPos);
        }

        //4.3
        pickupArray = new PickupTile[numOfPickUps];
        for (int i = 0; i < numOfPickUps; i++)
        {
            Position pickupPos = GetRandomEmptyPosition();
            pickupArray[i] = (PickupTile)CreateTile(TileType.Pickup, pickupPos);
        }

        UpdateVision();
    }

    private EnemyTIle CreateEnemyTile (Position pos)
    {
        //random number used as a percentage to calculate the chance of an enemy spawning
        int spawnChance = rand.Next(100);

        // number 0–49 = Grunt(50%), number 50–79 = Warlock(30%), number 80–99 = Tyrant(20%)
        if (spawnChance < 50)
        {
            return new GruntTile(pos, this);
        }
        else if (spawnChance < 80)
        {
            return new WarlockTile(pos, this);
        }
        else
        {
            return new TyrantTile(pos, this);
        }
    }

    private PickupTile CreatePickupTile(Position pos)
    {
        int roll = rand.Next(0,3);

        if (roll < 2)
        {
            return new HealthPickupTile(pos);
        }
        else
        {
            return new AttackBuffPickupTile(pos);
        }
    }

    private Tile CreateTile(TileType type, Position pos)
    {
        Tile t = null; // temporary tile variable

        switch (type)
        {
            case TileType.Empty:
                t = new EmptyTile(pos); //creates and empty tile
                break;
            case TileType.Wall:
                t = new WallTile(pos); // creates a wall tile
                break;
            case TileType.Hero:
                t = new HeroTile(pos); // creates a hero tile
                break;
            case TileType.Exit:
                t = new ExitTile(pos); // creates an exit tile
                break;
            case TileType.Enemy:
                t = CreateEnemyTile(pos); // creates an enemy tile
                break;
            case TileType.Pickup: //4.3
                t = new HealthPickupTile(pos);
                break;
        }

        tiles[pos.X, pos.Y] = t;
        return t;
    }

    private void InitialiseTiles()
    {// wall boundries and empty spaces      

        for (int i = 0; i < width; i++) // loops through the width
        {
            for (int j = 0; j < height; j++)
            {
                Position p = new Position(i, j);
                if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                {
                    tiles[i, j] = new WallTile(p);
                    //CreateTile(TileType.Wall, new Position(i, j));
                }
                else
                {
                    tiles[i, j] = new EmptyTile(p);
                    //CreateTile(TileType.Empty, new Position(i, j));
                }

            }
        }
    }


    public void SwapTiles(Tile t1, Tile t2)
    {
        if (t1 == null || t2 == null)
            return;

        Position pos1 = t1.Position;
        Position pos2 = t2.Position;

        // Swaps them in the tiles array
        tiles[pos1.X, pos1.Y] = t2;
        tiles[pos2.X, pos2.Y] = t1;

        // Update their stored positions
        t1.Position = pos2;
        t2.Position = pos1;
    }

    public void UpdateVision()
    {
        hero.UpdateVision(this);

        foreach (EnemyTIle enemy in enemy)
        {
            if (enemy != null && !enemy.IsDead)
            {
                enemy.UpdateVision(this);
            }
        }
    }

    private Position GetRandomEmptyPosition()
    {  // gets a random empty position within the level
        Position pos;
        do
        {
            int x = rand.Next(1, width - 1);
            int y = rand.Next(1, height - 1);
            pos = new Position(x, y);
        }
        while (!(tiles[pos.X, pos.Y] is EmptyTile));

        return pos;
    }


    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (hero.X == x && hero.Y == y)
                    sb.Append(hero.Display);
                else
                    sb.Append(tiles[x, y].Display);
            }
            sb.Append('\n');
        }
        return sb.ToString();
    }

}
