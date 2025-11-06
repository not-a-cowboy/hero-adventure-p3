using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hero_adventure
{
    public partial class Form1 : Form

    {

        private GameEngine engine;
        private Label controlsLabel;
        public Form1()
        {
            InitializeComponent();
            engine = new GameEngine(10); // create game engine with 10 levels
            this.KeyPreview = true;              // makes sure the form gets key presses
            this.KeyDown += Form1_KeyDown;       // hook up your key handler
            InitializeControlsLabel(); // shows the temp message 

            UpdateDisplay();
        }
        private void InitializeControlsLabel() // shows temp message for 7 seconds
        {
            controlsLabel = new Label();
            controlsLabel.Text = "Use WASD to move";
            controlsLabel.AutoSize = true;
            controlsLabel.Location = new Point(10, 10); // adjust as needed
            this.Controls.Add(controlsLabel);

            // Attack label
            Label attackLabel = new Label();
            attackLabel.Text = "Use I, J, K and L to attack";
            attackLabel.AutoSize = true;

            attackLabel.Location = new Point(controlsLabel.Left, controlsLabel.Bottom + 5); // 5px spacing
            this.Controls.Add(attackLabel);

            var timer = new Timer();
            timer.Interval = 7000; // 7 seconds
            timer.Tick += (s, e) =>
            {
                controlsLabel.Visible = false;
                timer.Stop();
            };
            timer.Start();
        }
        public void UpdateDisplay() // updates the display with the current game state
        {
            lblDisplay.Text = engine.ToString();

            if (engine.CurrentLevel.Hero != null) //this will display the heroes coordinates to the player. this code was initially used to help us debug the herotile when the hero tile was being placed yet not displayed on the level. then we decided that we like this feature
            {
                lblHeroCoords.Text = $"Hero Position: ({engine.CurrentLevel.Hero.Position.X}, {engine.CurrentLevel.Hero.Position.Y})";
                lblHeroCoords.ForeColor = Color.Green; //makes heroes position stats display in green text
            }
            else
            {
                lblHeroCoords.Text = "Hero not found";
                lblHeroCoords.ForeColor = Color.Red;
            }
            lblLevelSize.Text = $"Level Size: ({engine.CurrentLevel.Width} x {engine.CurrentLevel.Height})"; //displays the width and height dimensions of each level

            //to achieve our game displaying position coordinates, we declared properties that expose our GameEngines CurrentLevel field as well as our Positions x and y coordinates

        }

        private void Form1_Load(object sender, EventArgs e) // form load event
        {


        }
    
     private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    //engine.TriggerMovement(GameEngine.Direction.Up);
                    engine.MoveHero(GameEngine.Direction.Up);
                    break;
                case Keys.A:
                    //engine.TriggerMovement(GameEngine.Direction.Left);
                    engine.MoveHero(GameEngine.Direction.Left);
                    break;
                case Keys.S:
                    //engine.TriggerMovement(GameEngine.Direction.Down);
                    engine.MoveHero(GameEngine.Direction.Down);
                    break;
                case Keys.D:
                    //engine.TriggerMovement(GameEngine.Direction.Right);
                    engine.MoveHero(GameEngine.Direction.Right);
                    break;

            }
            // attack controls
            if (e.KeyCode == Keys.J)
                engine.TriggerAttack(GameEngine.Direction.Left);
            else
            {
                if (e.KeyCode == Keys.L)
                    engine.TriggerAttack(GameEngine.Direction.Right);
                else if (e.KeyCode == Keys.I)
                    engine.TriggerAttack(GameEngine.Direction.Up);
                else if (e.KeyCode == Keys.K)
                    engine.TriggerAttack(GameEngine.Direction.Down);
            }
        
    





          UpdateDisplay();
            if (e.KeyCode == Keys.W)
            {
                lblMovement.Text = $"Hero is moving: Up";
            }
            else if (e.KeyCode == Keys.A)
            {
                lblMovement.Text = $"Hero is moving: Left";
            }
            else if (e.KeyCode == Keys.S)
            {
                lblMovement.Text = $"Hero is moving: Down";
            }
            else if (e.KeyCode == Keys.D)
            {
                lblMovement.Text = $"Hero is moving: Right";
            }

        }

        private void lblDisplay_Click(object sender, EventArgs e)
        {

        }

        private void lblLevelSize_Click(object sender, EventArgs e)
        {

        }
    }   
}

