namespace hero_adventure
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDisplay = new System.Windows.Forms.Label();
            this.grbLegend = new System.Windows.Forms.GroupBox();
            this.lblLevelSize = new System.Windows.Forms.Label();
            this.lblHeroCoords = new System.Windows.Forms.Label();
            this.lblMovement = new System.Windows.Forms.Label();
            this.grbLegend.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDisplay
            // 
            this.lblDisplay.Font = new System.Drawing.Font("Consolas", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisplay.Location = new System.Drawing.Point(293, 59);
            this.lblDisplay.Name = "lblDisplay";
            this.lblDisplay.Size = new System.Drawing.Size(194, 345);
            this.lblDisplay.TabIndex = 0;
            this.lblDisplay.Text = "label1";
            this.lblDisplay.Click += new System.EventHandler(this.lblDisplay_Click);
            // 
            // grbLegend
            // 
            this.grbLegend.Controls.Add(this.lblMovement);
            this.grbLegend.Controls.Add(this.lblHeroCoords);
            this.grbLegend.Controls.Add(this.lblLevelSize);
            this.grbLegend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbLegend.Location = new System.Drawing.Point(538, 59);
            this.grbLegend.Name = "grbLegend";
            this.grbLegend.Size = new System.Drawing.Size(200, 185);
            this.grbLegend.TabIndex = 1;
            this.grbLegend.TabStop = false;
            this.grbLegend.Text = "Stats:";
            // 
            // lblLevelSize
            // 
            this.lblLevelSize.AutoSize = true;
            this.lblLevelSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelSize.Location = new System.Drawing.Point(7, 30);
            this.lblLevelSize.Name = "lblLevelSize";
            this.lblLevelSize.Size = new System.Drawing.Size(53, 20);
            this.lblLevelSize.TabIndex = 0;
            this.lblLevelSize.Text = "label1";
            this.lblLevelSize.Click += new System.EventHandler(this.lblLevelSize_Click);
            // 
            // lblHeroCoords
            // 
            this.lblHeroCoords.AutoSize = true;
            this.lblHeroCoords.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeroCoords.Location = new System.Drawing.Point(11, 65);
            this.lblHeroCoords.Name = "lblHeroCoords";
            this.lblHeroCoords.Size = new System.Drawing.Size(53, 20);
            this.lblHeroCoords.TabIndex = 1;
            this.lblHeroCoords.Text = "label1";
            // 
            // lblMovement
            // 
            this.lblMovement.AutoSize = true;
            this.lblMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMovement.Location = new System.Drawing.Point(11, 99);
            this.lblMovement.Name = "lblMovement";
            this.lblMovement.Size = new System.Drawing.Size(53, 20);
            this.lblMovement.TabIndex = 2;
            this.lblMovement.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grbLegend);
            this.Controls.Add(this.lblDisplay);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grbLegend.ResumeLayout(false);
            this.grbLegend.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDisplay;
        private System.Windows.Forms.GroupBox grbLegend;
        private System.Windows.Forms.Label lblMovement;
        private System.Windows.Forms.Label lblHeroCoords;
        private System.Windows.Forms.Label lblLevelSize;
    }
}

