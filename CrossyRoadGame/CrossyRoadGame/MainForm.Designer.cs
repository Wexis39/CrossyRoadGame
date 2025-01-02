namespace CrossyRoadGame
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tmrChickenAnim = new System.Windows.Forms.Timer(components);
            picRoad = new PictureBox();
            picChickenCollision = new PictureBox();
            lblLevel = new Label();
            labelLevel = new Label();
            tmrVehicleMovement = new System.Windows.Forms.Timer(components);
            tmrCreateVehicle = new System.Windows.Forms.Timer(components);
            picChicken = new PictureBox();
            tmrStartNextLevel = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)picRoad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picChickenCollision).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picChicken).BeginInit();
            SuspendLayout();
            // 
            // tmrChickenAnim
            // 
            tmrChickenAnim.Enabled = true;
            tmrChickenAnim.Interval = 120;
            tmrChickenAnim.Tick += tmrChickenAnim_Tick;
            // 
            // picRoad
            // 
            picRoad.BackgroundImage = Properties.Resources.Road;
            picRoad.BackgroundImageLayout = ImageLayout.Stretch;
            picRoad.Location = new Point(12, 12);
            picRoad.Name = "picRoad";
            picRoad.Size = new Size(960, 637);
            picRoad.TabIndex = 1;
            picRoad.TabStop = false;
            // 
            // picChickenCollision
            // 
            picChickenCollision.BackColor = Color.Red;
            picChickenCollision.Location = new Point(481, 594);
            picChickenCollision.Name = "picChickenCollision";
            picChickenCollision.Size = new Size(24, 45);
            picChickenCollision.TabIndex = 2;
            picChickenCollision.TabStop = false;
            // 
            // lblLevel
            // 
            lblLevel.AutoSize = true;
            lblLevel.BackColor = Color.Transparent;
            lblLevel.Font = new Font("Comic Sans MS", 32.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLevel.ForeColor = Color.Yellow;
            lblLevel.Location = new Point(934, 0);
            lblLevel.Name = "lblLevel";
            lblLevel.Size = new Size(51, 60);
            lblLevel.TabIndex = 3;
            lblLevel.Text = "1";
            // 
            // labelLevel
            // 
            labelLevel.AutoSize = true;
            labelLevel.BackColor = Color.Transparent;
            labelLevel.Font = new Font("Comic Sans MS", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelLevel.ForeColor = Color.Yellow;
            labelLevel.Location = new Point(827, 8);
            labelLevel.Name = "labelLevel";
            labelLevel.Size = new Size(112, 51);
            labelLevel.TabIndex = 3;
            labelLevel.Text = "Level";
            // 
            // tmrVehicleMovement
            // 
            tmrVehicleMovement.Interval = 30;
            tmrVehicleMovement.Tick += tmrVehicleMovement_Tick;
            // 
            // tmrCreateVehicle
            // 
            tmrCreateVehicle.Enabled = true;
            tmrCreateVehicle.Interval = 50;
            tmrCreateVehicle.Tick += tmrCreateVehicle_Tick;
            // 
            // picChicken
            // 
            picChicken.BackColor = Color.Transparent;
            picChicken.BackgroundImage = Properties.Resources.ChickenIdle1;
            picChicken.BackgroundImageLayout = ImageLayout.Stretch;
            picChicken.Location = new Point(435, 539);
            picChicken.Name = "picChicken";
            picChicken.Size = new Size(114, 100);
            picChicken.TabIndex = 4;
            picChicken.TabStop = false;
            // 
            // tmrStartNextLevel
            // 
            tmrStartNextLevel.Interval = 1600;
            tmrStartNextLevel.Tick += tmrStartNextLevel_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 192, 192);
            ClientSize = new Size(984, 661);
            Controls.Add(picChickenCollision);
            Controls.Add(picChicken);
            Controls.Add(labelLevel);
            Controls.Add(lblLevel);
            Controls.Add(picRoad);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CrossyRoad";
            Load += MainForm_Load;
            ((System.ComponentModel.ISupportInitialize)picRoad).EndInit();
            ((System.ComponentModel.ISupportInitialize)picChickenCollision).EndInit();
            ((System.ComponentModel.ISupportInitialize)picChicken).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer tmrChickenAnim;
        private PictureBox picRoad;
        private PictureBox picChickenCollision;
        private Label lblLevel;
        private Label labelLevel;
        private System.Windows.Forms.Timer tmrVehicleMovement;
        private System.Windows.Forms.Timer tmrCreateVehicle;
        private PictureBox picChicken;
        private System.Windows.Forms.Timer tmrStartNextLevel;
    }
}
