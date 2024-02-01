namespace PathfindingFun
{
    partial class MainForm
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.MouseScreenTextBox = new System.Windows.Forms.TextBox();
            this.MouseGridTextBox = new System.Windows.Forms.TextBox();
            this.MouseGroupBox = new System.Windows.Forms.GroupBox();
            this.MouseGridLabel = new System.Windows.Forms.Label();
            this.MouseScreenLabel = new System.Windows.Forms.Label();
            this.RunAIButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.SmallGridButton = new System.Windows.Forms.RadioButton();
            this.LargeGridButton = new System.Windows.Forms.RadioButton();
            this.GridGroupBox = new System.Windows.Forms.GroupBox();
            this.RandomMazeButton = new System.Windows.Forms.Button();
            this.GridSizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.WallDensityLabel = new System.Windows.Forms.Label();
            this.RandomnessBar = new System.Windows.Forms.TrackBar();
            this.RandomizeButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.DiagonalCheckBox = new System.Windows.Forms.CheckBox();
            this.AIGroupBox = new System.Windows.Forms.GroupBox();
            this.TieBreakerCheckBox = new System.Windows.Forms.CheckBox();
            this.UseGCheckBox = new System.Windows.Forms.CheckBox();
            this.DrawOpenlistCheckBox = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.HeuristicComboBox = new System.Windows.Forms.ComboBox();
            this.MouseGroupBox.SuspendLayout();
            this.GridGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RandomnessBar)).BeginInit();
            this.AIGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.White;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Location = new System.Drawing.Point(197, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(600, 600);
            this.Panel1.TabIndex = 0;
            this.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            this.Panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.Panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // MouseScreenTextBox
            // 
            this.MouseScreenTextBox.Location = new System.Drawing.Point(72, 16);
            this.MouseScreenTextBox.Name = "MouseScreenTextBox";
            this.MouseScreenTextBox.Size = new System.Drawing.Size(100, 20);
            this.MouseScreenTextBox.TabIndex = 1;
            // 
            // MouseGridTextBox
            // 
            this.MouseGridTextBox.Location = new System.Drawing.Point(72, 38);
            this.MouseGridTextBox.Name = "MouseGridTextBox";
            this.MouseGridTextBox.Size = new System.Drawing.Size(100, 20);
            this.MouseGridTextBox.TabIndex = 2;
            // 
            // MouseGroupBox
            // 
            this.MouseGroupBox.Controls.Add(this.MouseGridLabel);
            this.MouseGroupBox.Controls.Add(this.MouseScreenLabel);
            this.MouseGroupBox.Controls.Add(this.MouseScreenTextBox);
            this.MouseGroupBox.Controls.Add(this.MouseGridTextBox);
            this.MouseGroupBox.Location = new System.Drawing.Point(9, 12);
            this.MouseGroupBox.Name = "MouseGroupBox";
            this.MouseGroupBox.Size = new System.Drawing.Size(182, 67);
            this.MouseGroupBox.TabIndex = 3;
            this.MouseGroupBox.TabStop = false;
            this.MouseGroupBox.Text = "Mouse";
            // 
            // MouseGridLabel
            // 
            this.MouseGridLabel.AutoSize = true;
            this.MouseGridLabel.Location = new System.Drawing.Point(6, 41);
            this.MouseGridLabel.Name = "MouseGridLabel";
            this.MouseGridLabel.Size = new System.Drawing.Size(50, 13);
            this.MouseGridLabel.TabIndex = 4;
            this.MouseGridLabel.Text = "Grid Pos:";
            // 
            // MouseScreenLabel
            // 
            this.MouseScreenLabel.AutoSize = true;
            this.MouseScreenLabel.Location = new System.Drawing.Point(6, 19);
            this.MouseScreenLabel.Name = "MouseScreenLabel";
            this.MouseScreenLabel.Size = new System.Drawing.Size(65, 13);
            this.MouseScreenLabel.TabIndex = 3;
            this.MouseScreenLabel.Text = "Screen Pos:";
            // 
            // RunAIButton
            // 
            this.RunAIButton.Location = new System.Drawing.Point(8, 395);
            this.RunAIButton.Name = "RunAIButton";
            this.RunAIButton.Size = new System.Drawing.Size(182, 23);
            this.RunAIButton.TabIndex = 4;
            this.RunAIButton.Text = "Run";
            this.RunAIButton.UseVisualStyleBackColor = true;
            this.RunAIButton.Click += new System.EventHandler(this.RunAI_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(8, 453);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(182, 23);
            this.ClearButton.TabIndex = 5;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // SmallGridButton
            // 
            this.SmallGridButton.AutoSize = true;
            this.SmallGridButton.Checked = true;
            this.SmallGridButton.Location = new System.Drawing.Point(9, 19);
            this.SmallGridButton.Name = "SmallGridButton";
            this.SmallGridButton.Size = new System.Drawing.Size(72, 17);
            this.SmallGridButton.TabIndex = 6;
            this.SmallGridButton.TabStop = true;
            this.SmallGridButton.Text = "Small Grid";
            this.SmallGridButton.UseVisualStyleBackColor = true;
            this.SmallGridButton.CheckedChanged += new System.EventHandler(this.SmallGridButton_CheckedChanged);
            this.SmallGridButton.Click += new System.EventHandler(this.SmallGridButton_Click);
            // 
            // LargeGridButton
            // 
            this.LargeGridButton.AutoSize = true;
            this.LargeGridButton.Location = new System.Drawing.Point(9, 43);
            this.LargeGridButton.Name = "LargeGridButton";
            this.LargeGridButton.Size = new System.Drawing.Size(110, 17);
            this.LargeGridButton.TabIndex = 7;
            this.LargeGridButton.TabStop = true;
            this.LargeGridButton.Text = "Large Grid (Pixels)";
            this.LargeGridButton.UseVisualStyleBackColor = true;
            this.LargeGridButton.CheckedChanged += new System.EventHandler(this.LargeGridButton_CheckedChanged);
            this.LargeGridButton.Click += new System.EventHandler(this.LargeGridButton_Click);
            // 
            // GridGroupBox
            // 
            this.GridGroupBox.Controls.Add(this.RandomMazeButton);
            this.GridGroupBox.Controls.Add(this.GridSizeUpDown);
            this.GridGroupBox.Controls.Add(this.WallDensityLabel);
            this.GridGroupBox.Controls.Add(this.RandomnessBar);
            this.GridGroupBox.Controls.Add(this.SmallGridButton);
            this.GridGroupBox.Controls.Add(this.RandomizeButton);
            this.GridGroupBox.Controls.Add(this.LargeGridButton);
            this.GridGroupBox.Location = new System.Drawing.Point(8, 85);
            this.GridGroupBox.Name = "GridGroupBox";
            this.GridGroupBox.Size = new System.Drawing.Size(182, 152);
            this.GridGroupBox.TabIndex = 8;
            this.GridGroupBox.TabStop = false;
            this.GridGroupBox.Text = "Grid";
            // 
            // RandomMazeButton
            // 
            this.RandomMazeButton.Location = new System.Drawing.Point(9, 121);
            this.RandomMazeButton.Name = "RandomMazeButton";
            this.RandomMazeButton.Size = new System.Drawing.Size(163, 23);
            this.RandomMazeButton.TabIndex = 14;
            this.RandomMazeButton.Text = "Randomize Maze";
            this.RandomMazeButton.UseVisualStyleBackColor = true;
            this.RandomMazeButton.Click += new System.EventHandler(this.RandomMazeButton_Click);
            // 
            // GridSizeUpDown
            // 
            this.GridSizeUpDown.Location = new System.Drawing.Point(125, 43);
            this.GridSizeUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.GridSizeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.GridSizeUpDown.Name = "GridSizeUpDown";
            this.GridSizeUpDown.Size = new System.Drawing.Size(47, 20);
            this.GridSizeUpDown.TabIndex = 13;
            this.GridSizeUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.GridSizeUpDown.ValueChanged += new System.EventHandler(this.GridSizeUpDown_ValueChanged);
            // 
            // WallDensityLabel
            // 
            this.WallDensityLabel.AutoSize = true;
            this.WallDensityLabel.Location = new System.Drawing.Point(7, 69);
            this.WallDensityLabel.Name = "WallDensityLabel";
            this.WallDensityLabel.Size = new System.Drawing.Size(66, 13);
            this.WallDensityLabel.TabIndex = 12;
            this.WallDensityLabel.Text = "Wall Density";
            // 
            // RandomnessBar
            // 
            this.RandomnessBar.AutoSize = false;
            this.RandomnessBar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.RandomnessBar.Location = new System.Drawing.Point(73, 69);
            this.RandomnessBar.Maximum = 35;
            this.RandomnessBar.Name = "RandomnessBar";
            this.RandomnessBar.Size = new System.Drawing.Size(99, 20);
            this.RandomnessBar.TabIndex = 11;
            this.RandomnessBar.TickFrequency = 5;
            this.RandomnessBar.Value = 25;
            // 
            // RandomizeButton
            // 
            this.RandomizeButton.Location = new System.Drawing.Point(9, 92);
            this.RandomizeButton.Name = "RandomizeButton";
            this.RandomizeButton.Size = new System.Drawing.Size(163, 23);
            this.RandomizeButton.TabIndex = 10;
            this.RandomizeButton.Text = "Randomize Walls";
            this.RandomizeButton.UseVisualStyleBackColor = true;
            this.RandomizeButton.Click += new System.EventHandler(this.RandomizeButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(8, 424);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(182, 23);
            this.ResetButton.TabIndex = 9;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // DiagonalCheckBox
            // 
            this.DiagonalCheckBox.AutoSize = true;
            this.DiagonalCheckBox.Checked = true;
            this.DiagonalCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DiagonalCheckBox.Location = new System.Drawing.Point(6, 19);
            this.DiagonalCheckBox.Name = "DiagonalCheckBox";
            this.DiagonalCheckBox.Size = new System.Drawing.Size(107, 17);
            this.DiagonalCheckBox.TabIndex = 10;
            this.DiagonalCheckBox.Text = "Allow Diagonals?";
            this.DiagonalCheckBox.UseVisualStyleBackColor = true;
            // 
            // AIGroupBox
            // 
            this.AIGroupBox.Controls.Add(this.TieBreakerCheckBox);
            this.AIGroupBox.Controls.Add(this.UseGCheckBox);
            this.AIGroupBox.Controls.Add(this.DrawOpenlistCheckBox);
            this.AIGroupBox.Controls.Add(this.label3);
            this.AIGroupBox.Controls.Add(this.HeuristicComboBox);
            this.AIGroupBox.Controls.Add(this.DiagonalCheckBox);
            this.AIGroupBox.Location = new System.Drawing.Point(8, 243);
            this.AIGroupBox.Name = "AIGroupBox";
            this.AIGroupBox.Size = new System.Drawing.Size(183, 146);
            this.AIGroupBox.TabIndex = 11;
            this.AIGroupBox.TabStop = false;
            this.AIGroupBox.Text = "AI";
            // 
            // TieBreakerCheckBox
            // 
            this.TieBreakerCheckBox.AutoSize = true;
            this.TieBreakerCheckBox.Checked = true;
            this.TieBreakerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TieBreakerCheckBox.Location = new System.Drawing.Point(6, 88);
            this.TieBreakerCheckBox.Name = "TieBreakerCheckBox";
            this.TieBreakerCheckBox.Size = new System.Drawing.Size(109, 17);
            this.TieBreakerCheckBox.TabIndex = 14;
            this.TieBreakerCheckBox.Text = "Use Tie Breaker?";
            this.TieBreakerCheckBox.UseVisualStyleBackColor = true;
            // 
            // UseGCheckBox
            // 
            this.UseGCheckBox.AutoSize = true;
            this.UseGCheckBox.Checked = true;
            this.UseGCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UseGCheckBox.Location = new System.Drawing.Point(6, 64);
            this.UseGCheckBox.Name = "UseGCheckBox";
            this.UseGCheckBox.Size = new System.Drawing.Size(62, 17);
            this.UseGCheckBox.TabIndex = 13;
            this.UseGCheckBox.Text = "Use G?";
            this.UseGCheckBox.UseVisualStyleBackColor = true;
            // 
            // DrawOpenlistCheckBox
            // 
            this.DrawOpenlistCheckBox.AutoSize = true;
            this.DrawOpenlistCheckBox.Checked = true;
            this.DrawOpenlistCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DrawOpenlistCheckBox.Location = new System.Drawing.Point(6, 41);
            this.DrawOpenlistCheckBox.Name = "DrawOpenlistCheckBox";
            this.DrawOpenlistCheckBox.Size = new System.Drawing.Size(147, 17);
            this.DrawOpenlistCheckBox.TabIndex = 12;
            this.DrawOpenlistCheckBox.Text = "Draw Considered Nodes?";
            this.DrawOpenlistCheckBox.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "H =";
            // 
            // HeuristicComboBox
            // 
            this.HeuristicComboBox.DisplayMember = "Manhatten";
            this.HeuristicComboBox.FormattingEnabled = true;
            this.HeuristicComboBox.Items.AddRange(new object[] {
            "Manhatten",
            "Diagonal Distance",
            "Euclidean Distance",
            "None (Dijkstra)"});
            this.HeuristicComboBox.Location = new System.Drawing.Point(37, 114);
            this.HeuristicComboBox.Name = "HeuristicComboBox";
            this.HeuristicComboBox.Size = new System.Drawing.Size(140, 21);
            this.HeuristicComboBox.TabIndex = 0;
            this.HeuristicComboBox.ValueMember = "Manhatten";
            this.HeuristicComboBox.SelectedValueChanged += new System.EventHandler(this.HeuristicComboBox_SelectedValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(809, 622);
            this.Controls.Add(this.AIGroupBox);
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.GridGroupBox);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.RunAIButton);
            this.Controls.Add(this.MouseGroupBox);
            this.Controls.Add(this.Panel1);
            this.Name = "MainForm";
            this.Text = "Pathfinding Fun";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.MouseGroupBox.ResumeLayout(false);
            this.MouseGroupBox.PerformLayout();
            this.GridGroupBox.ResumeLayout(false);
            this.GridGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridSizeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RandomnessBar)).EndInit();
            this.AIGroupBox.ResumeLayout(false);
            this.AIGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel1;
        private System.Windows.Forms.TextBox MouseScreenTextBox;
        private System.Windows.Forms.TextBox MouseGridTextBox;
        private System.Windows.Forms.GroupBox MouseGroupBox;
        private System.Windows.Forms.Button RunAIButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.RadioButton SmallGridButton;
        private System.Windows.Forms.RadioButton LargeGridButton;
        private System.Windows.Forms.Label MouseGridLabel;
        private System.Windows.Forms.Label MouseScreenLabel;
        private System.Windows.Forms.GroupBox GridGroupBox;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button RandomizeButton;
        private System.Windows.Forms.TrackBar RandomnessBar;
        private System.Windows.Forms.CheckBox DiagonalCheckBox;
        private System.Windows.Forms.GroupBox AIGroupBox;
        private System.Windows.Forms.ComboBox HeuristicComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label WallDensityLabel;
        private System.Windows.Forms.CheckBox DrawOpenlistCheckBox;
        private System.Windows.Forms.NumericUpDown GridSizeUpDown;
        private System.Windows.Forms.CheckBox UseGCheckBox;
        private System.Windows.Forms.Button RandomMazeButton;
        private System.Windows.Forms.CheckBox TieBreakerCheckBox;
    }
}

