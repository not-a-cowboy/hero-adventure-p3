using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static hero_adventure.GameEngine;
namespace hero_adventure
{
    public class GameEngine
    {
        public enum Direction // movement directions
        {
            Up,
            Down,
            Left,
            Right
        }
        public enum GameState// current state of the game
        {
            InProgress,
            Complete,
            GameOver
        }

        private Level currentLevel;
        private HeroTile hero;       // stores the hero
        private int numLevels;
        private Random random;
        private const int maxSize = 20;
        private const int minSize = 10;
        private int currentLevelNumber = 1;


        private int successfulHeroMoves = 0;

        public GameEngine(int numLevels = 1) // default set to 1 level
        {
            this.numLevels = numLevels;
            random = new Random();

            LoadNewLevel();
        }
        private void NextLevel()
        {
            // Increase the current level number
            currentLevelNumber++;

            // Store the current level's HeroTile
            HeroTile currentHero = currentLevel.Hero;

            // Randomize new level size using minSize and maxSize
            int width = random.Next(minSize, maxSize + 1);
            int height = random.Next(minSize, maxSize + 1);


            currentLevel = new Level(width, height, currentLevelNumber, currentHero);
            hero = currentLevel.Hero;
            //LoadNewLevel();
        }


        public bool MoveHero(Direction direction) //reworked
        {
            if (gameState != GameState.InProgress)
            {
                return false;
            }
            HeroTile hero = currentLevel.Hero;
            currentLevel.Hero.UpdateVision(currentLevel);

            int deltaX = 0, deltaY = 0;

            switch (direction)
            {
                case Direction.Up:
                    deltaY = -1;
                    break;
                case Direction.Right:
                    deltaX = 1;
                    break;
                case Direction.Down:
                    deltaY = 1;
                    break;
                case Direction.Left:
                    deltaX = -1;
                    break;
                default:
                    return false;
            }

            int targetX = currentLevel.Hero.Position.X + deltaX;
            int targetY = currentLevel.Hero.Position.Y + deltaY;

            Tile targetTile = currentLevel.Tiles[targetX, targetY];

            if (targetTile is ExitTile)
            {
                if (currentLevelNumber == numLevels)
                {
                    gameState = GameState.Complete;
                    return false;
                }
                else
                {
                    NextLevel();
                    return true;
                }
            }

            

            if (targetTile is PickupTile pickUp) //4.3
            {
                pickUp.ApplyEffect(currentLevel.Hero);

                currentLevel.ReplaceToEmpty(targetTile.Position);

                currentLevel.MoveHeroTo(hero, targetTile.Position);

                currentLevel.Hero.UpdateVision(currentLevel);
                return true;
            }

            if (targetTile is EmptyTile)
            {
                currentLevel.Tiles[currentLevel.Hero.Position.X, currentLevel.Hero.Position.Y] = new EmptyTile(new Position(currentLevel.Hero.Position.X, currentLevel.Hero.Position.Y));

                currentLevel.Hero.Position.X = targetX;
                currentLevel.Hero.Position.Y = targetY;

                currentLevel.Tiles[targetX, targetY] = currentLevel.Hero;

                currentLevel.Hero.UpdateVision(CurrentLevel);

                return true;
            }


            return false;
        }
        public Level CurrentLevel //this property exposes the currentLevel field so it can be used in other classes and methods
        {
            get
            {
                return currentLevel;
            }
        }


        
        public void MovesEnemies()
        {
            foreach (EnemyTIle enemy in currentLevel.Enemy)
            {
                if (enemy == null || enemy.IsDead)
                    continue;

                if (enemy.GetMove(out Tile targetTile) && targetTile != null)
                {
                    currentLevel.SwapTiles(enemy, targetTile);
                    currentLevel.UpdateVision();
                }

            }
        }

        private void LoadNewLevel()// allows a new lvl to load 
        {
            int width = random.Next(minSize, maxSize);
            int height = random.Next(minSize, maxSize);

            // Create level with hero (if hero is null, Level creates one)
            currentLevel = new Level(width, height, currentLevelNumber, hero);

            // Store hero for easy access
            hero = currentLevel.Hero;
        }

        public override string ToString() // display current level or game state
        {
            if (gameState == GameState.Complete)
                return " Congratulations! You have completed the game!";
            else if (gameState == GameState.InProgress)
                return currentLevel.ToString();
            else
                return "Sorry Game Over!";
        }

        private int currentLevelIndex = 1; //the current level index
        private GameState gameState = GameState.InProgress;

        public GameState State => gameState;

        public void TriggerMovement(Direction dir)
        {
            if (!currentLevel.MoveHero(dir))
                return;

            // Now hero has actually moved — check exit
            if (currentLevel.Hero.Position.X == currentLevel.Exit.Position.X &&
                currentLevel.Hero.Position.Y == currentLevel.Exit.Position.Y)
            {
                if (currentLevelNumber >= numLevels)
                {
                    gameState = GameState.Complete;
                }
                else
                {
                    NextLevel();
                }
                return;
            }

            // Enemy moves every 2 hero moves
            successfulHeroMoves++;
            if (successfulHeroMoves % 2 == 0)
                MovesEnemies();
        }


        
        private bool HeroAttack(Direction direction)
        {
            // Updating the hero’s vision before the attack
            hero.UpdateVision(currentLevel);

            // Get hero’s vision tiles
            var visionField = typeof(CharacterTile)
                .GetField("plyr_vision", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Tile[] visionArray = (Tile[])visionField.GetValue(hero);

            // Map direction to vision index
            int visionIndex = -1;
            switch (direction)
            {
                case Direction.Up: visionIndex = 0; break;
                case Direction.Right: visionIndex = 1; break;
                case Direction.Down: visionIndex = 2; break;
                case Direction.Left: visionIndex = 3; break;
                default: return false;
            }

            Tile targetTile = visionArray[visionIndex];

            // attacks only if the tile is an enemy
            if (targetTile is CharacterTile targetCharacter)
            {
                // Reduce enemies HP accordingly 
                targetCharacter.HitPoints -= hero.Attackpwr;




                return true;
            }


            return false;

        }

        public void TriggerAttack(Direction direction)
        {
            //stops if character has died
            if (gameState == GameState.GameOver)
                return;

            bool attackSuccess = HeroAttack(direction);

            // create enemies attack after hero attacks first
            if (attackSuccess)
            {
                EnemiesAttack();
                //checks if character is dead
                if (hero.IsDead)
                {
                    gameState = GameState.GameOver;
                    return;
                }
            }

            // Update vision after all attacks
            currentLevel.UpdateVision();
        }

        private void EnemiesAttack()
        {
            foreach (EnemyTIle enemy in currentLevel.Enemy)
            {
                if (enemy == null || enemy.IsDead)
                    continue;

                // finds the targets that the enemy should attack
                CharacterTile[] targets = enemy.GetTarget();

                foreach (CharacterTile target in targets)
                {
                    if (target == null || target.IsDead)
                        continue;

                    // Enemy attacks 
                    target.HitPoints -= enemy.Attackpwr;

                    // when enemies die they still are replaced with an x
                    if (target.IsDead)
                    {
                        currentLevel.Tiles[target.X, target.Y] =
                            new EmptyTile(new Position(target.X, target.Y));
                    }
                }
            }
        }

        public string HeroStats
        {
            get
            {
                return $"Hero HP: {hero.HitPoints}/{hero.MaxHitPoints}";
            }
        }
    }
}









