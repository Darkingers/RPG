namespace RPG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Stats = new System.Windows.Forms.TextBox();
            this.MessageBuffer = new System.Windows.Forms.TextBox();
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.Skill1 = new System.Windows.Forms.Button();
            this.Skill5 = new System.Windows.Forms.Button();
            this.Skill7 = new System.Windows.Forms.Button();
            this.Skill2 = new System.Windows.Forms.Button();
            this.Skill4 = new System.Windows.Forms.Button();
            this.Skill8 = new System.Windows.Forms.Button();
            this.Skill3 = new System.Windows.Forms.Button();
            this.Skill0 = new System.Windows.Forms.Button();
            this.Skill9 = new System.Windows.Forms.Button();
            this.Inventory = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.Skill6 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.Stats);
            this.panel1.Controls.Add(this.MessageBuffer);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 829);
            this.panel1.TabIndex = 1;
            // 
            // Stats
            // 
            this.Stats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Stats.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Stats.Enabled = false;
            this.Stats.Location = new System.Drawing.Point(3, 405);
            this.Stats.Multiline = true;
            this.Stats.Name = "Stats";
            this.Stats.ReadOnly = true;
            this.Stats.ShortcutsEnabled = false;
            this.Stats.Size = new System.Drawing.Size(194, 421);
            this.Stats.TabIndex = 1;
            // 
            // MessageBuffer
            // 
            this.MessageBuffer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageBuffer.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.MessageBuffer.Enabled = false;
            this.MessageBuffer.Location = new System.Drawing.Point(3, 3);
            this.MessageBuffer.Multiline = true;
            this.MessageBuffer.Name = "MessageBuffer";
            this.MessageBuffer.ReadOnly = true;
            this.MessageBuffer.ShortcutsEnabled = false;
            this.MessageBuffer.Size = new System.Drawing.Size(194, 396);
            this.MessageBuffer.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Canvas.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Canvas.Enabled = false;
            this.Canvas.Location = new System.Drawing.Point(215, 12);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(750, 750);
            this.Canvas.TabIndex = 12;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            // 
            // Skill1
            // 
            this.Skill1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill1.AutoEllipsis = true;
            this.Skill1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill1.Location = new System.Drawing.Point(275, 794);
            this.Skill1.Name = "Skill1";
            this.Skill1.Size = new System.Drawing.Size(53, 47);
            this.Skill1.TabIndex = 2;
            this.Skill1.Text = "1";
            this.Skill1.UseVisualStyleBackColor = true;
            // 
            // Skill5
            // 
            this.Skill5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill5.Location = new System.Drawing.Point(511, 794);
            this.Skill5.Name = "Skill5";
            this.Skill5.Size = new System.Drawing.Size(53, 47);
            this.Skill5.TabIndex = 6;
            this.Skill5.Text = "5";
            this.Skill5.UseVisualStyleBackColor = true;
            // 
            // Skill7
            // 
            this.Skill7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill7.Location = new System.Drawing.Point(629, 794);
            this.Skill7.Name = "Skill7";
            this.Skill7.Size = new System.Drawing.Size(53, 47);
            this.Skill7.TabIndex = 8;
            this.Skill7.Text = "7";
            this.Skill7.UseVisualStyleBackColor = true;
            // 
            // Skill2
            // 
            this.Skill2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill2.Location = new System.Drawing.Point(334, 794);
            this.Skill2.Name = "Skill2";
            this.Skill2.Size = new System.Drawing.Size(53, 47);
            this.Skill2.TabIndex = 3;
            this.Skill2.Text = "2";
            this.Skill2.UseVisualStyleBackColor = true;
            // 
            // Skill4
            // 
            this.Skill4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill4.Location = new System.Drawing.Point(452, 794);
            this.Skill4.Name = "Skill4";
            this.Skill4.Size = new System.Drawing.Size(53, 47);
            this.Skill4.TabIndex = 5;
            this.Skill4.Text = "4";
            this.Skill4.UseVisualStyleBackColor = true;
            // 
            // Skill8
            // 
            this.Skill8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill8.Location = new System.Drawing.Point(688, 794);
            this.Skill8.Name = "Skill8";
            this.Skill8.Size = new System.Drawing.Size(53, 47);
            this.Skill8.TabIndex = 9;
            this.Skill8.Text = "8";
            this.Skill8.UseVisualStyleBackColor = true;
            // 
            // Skill3
            // 
            this.Skill3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill3.Location = new System.Drawing.Point(393, 794);
            this.Skill3.Name = "Skill3";
            this.Skill3.Size = new System.Drawing.Size(53, 47);
            this.Skill3.TabIndex = 4;
            this.Skill3.Text = "3";
            this.Skill3.UseVisualStyleBackColor = true;
            // 
            // Skill0
            // 
            this.Skill0.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill0.Location = new System.Drawing.Point(216, 794);
            this.Skill0.Name = "Skill0";
            this.Skill0.Size = new System.Drawing.Size(53, 47);
            this.Skill0.TabIndex = 11;
            this.Skill0.Text = "0";
            this.Skill0.UseVisualStyleBackColor = true;
            // 
            // Skill9
            // 
            this.Skill9.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill9.Location = new System.Drawing.Point(747, 794);
            this.Skill9.Name = "Skill9";
            this.Skill9.Size = new System.Drawing.Size(53, 47);
            this.Skill9.TabIndex = 10;
            this.Skill9.Text = "9";
            this.Skill9.UseVisualStyleBackColor = true;
            // 
            // Inventory
            // 
            this.Inventory.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Inventory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Inventory.BackColor = System.Drawing.SystemColors.Control;
            this.Inventory.Location = new System.Drawing.Point(806, 794);
            this.Inventory.Name = "Inventory";
            this.Inventory.Size = new System.Drawing.Size(79, 47);
            this.Inventory.TabIndex = 13;
            this.Inventory.Text = "Inventory";
            this.Inventory.UseVisualStyleBackColor = false;
            this.Inventory.Click += new System.EventHandler(this.Inventory_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(891, 794);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(79, 47);
            this.button1.TabIndex = 14;
            this.button1.Text = "Menu";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // Skill6
            // 
            this.Skill6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill6.Location = new System.Drawing.Point(570, 794);
            this.Skill6.Name = "Skill6";
            this.Skill6.Size = new System.Drawing.Size(53, 47);
            this.Skill6.TabIndex = 7;
            this.Skill6.Text = "6";
            this.Skill6.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(982, 853);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Skill6);
            this.Controls.Add(this.Inventory);
            this.Controls.Add(this.Skill9);
            this.Controls.Add(this.Canvas);
            this.Controls.Add(this.Skill8);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Skill7);
            this.Controls.Add(this.Skill0);
            this.Controls.Add(this.Skill3);
            this.Controls.Add(this.Skill4);
            this.Controls.Add(this.Skill5);
            this.Controls.Add(this.Skill1);
            this.Controls.Add(this.Skill2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "RPG";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.TextBox MessageBuffer;
        private System.Windows.Forms.TextBox Stats;
        private System.Windows.Forms.Button Skill1;
        private System.Windows.Forms.Button Skill5;
        private System.Windows.Forms.Button Skill7;
        private System.Windows.Forms.Button Skill2;
        private System.Windows.Forms.Button Skill4;
        private System.Windows.Forms.Button Skill8;
        private System.Windows.Forms.Button Skill3;
        private System.Windows.Forms.Button Skill0;
        private System.Windows.Forms.Button Skill9;
        private System.Windows.Forms.Button Inventory;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Skill6;
    }
}

