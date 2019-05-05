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
            this.Details = new System.Windows.Forms.TextBox();
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
            this.Skill6 = new System.Windows.Forms.Button();
            this.EquipmentList = new System.Windows.Forms.ListView();
            this.InventoryPanel = new System.Windows.Forms.Panel();
            this.InventoryList = new System.Windows.Forms.ListView();
            this.SkillView = new System.Windows.Forms.ListView();
            this.Mapview = new System.Windows.Forms.ListView();
            this.Maps = new System.Windows.Forms.ComboBox();
            this.Characters = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            this.InventoryPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel1.Controls.Add(this.Details);
            this.panel1.Controls.Add(this.Stats);
            this.panel1.Controls.Add(this.MessageBuffer);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 829);
            this.panel1.TabIndex = 1;
            // 
            // Details
            // 
            this.Details.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Details.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Details.Enabled = false;
            this.Details.Location = new System.Drawing.Point(3, 550);
            this.Details.Multiline = true;
            this.Details.Name = "Details";
            this.Details.ReadOnly = true;
            this.Details.ShortcutsEnabled = false;
            this.Details.Size = new System.Drawing.Size(194, 276);
            this.Details.TabIndex = 2;
            // 
            // Stats
            // 
            this.Stats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Stats.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Stats.Enabled = false;
            this.Stats.Location = new System.Drawing.Point(3, 269);
            this.Stats.Multiline = true;
            this.Stats.Name = "Stats";
            this.Stats.ReadOnly = true;
            this.Stats.ShortcutsEnabled = false;
            this.Stats.Size = new System.Drawing.Size(194, 276);
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
            this.MessageBuffer.Size = new System.Drawing.Size(194, 260);
            this.MessageBuffer.TabIndex = 0;
            // 
            // Canvas
            // 
            this.Canvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Canvas.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Canvas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Canvas.Cursor = System.Windows.Forms.Cursors.Default;
            this.Canvas.Location = new System.Drawing.Point(215, 15);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(750, 750);
            this.Canvas.TabIndex = 12;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.Canvas_Paint);
            this.Canvas.DoubleClick += new System.EventHandler(this.Canvas_DoubleClick);
            this.Canvas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Canvas_MouseClick);
            // 
            // Skill1
            // 
            this.Skill1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill1.AutoEllipsis = true;
            this.Skill1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill1.Location = new System.Drawing.Point(275, 794);
            this.Skill1.Name = "Skill1";
            this.Skill1.Size = new System.Drawing.Size(53, 47);
            this.Skill1.TabIndex = 2;
            this.Skill1.Text = "1";
            this.Skill1.UseVisualStyleBackColor = true;
            this.Skill1.Click += new System.EventHandler(this.Skill1_Click);
            // 
            // Skill5
            // 
            this.Skill5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill5.Location = new System.Drawing.Point(511, 794);
            this.Skill5.Name = "Skill5";
            this.Skill5.Size = new System.Drawing.Size(53, 47);
            this.Skill5.TabIndex = 6;
            this.Skill5.Text = "5";
            this.Skill5.UseVisualStyleBackColor = true;
            this.Skill5.Click += new System.EventHandler(this.Skill5_Click);
            // 
            // Skill7
            // 
            this.Skill7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill7.Location = new System.Drawing.Point(629, 794);
            this.Skill7.Name = "Skill7";
            this.Skill7.Size = new System.Drawing.Size(53, 47);
            this.Skill7.TabIndex = 8;
            this.Skill7.Text = "7";
            this.Skill7.UseVisualStyleBackColor = true;
            this.Skill7.Click += new System.EventHandler(this.Skill7_Click);
            // 
            // Skill2
            // 
            this.Skill2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill2.Location = new System.Drawing.Point(334, 794);
            this.Skill2.Name = "Skill2";
            this.Skill2.Size = new System.Drawing.Size(53, 47);
            this.Skill2.TabIndex = 3;
            this.Skill2.Text = "2";
            this.Skill2.UseVisualStyleBackColor = true;
            this.Skill2.Click += new System.EventHandler(this.Skill2_Click);
            // 
            // Skill4
            // 
            this.Skill4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill4.Location = new System.Drawing.Point(452, 794);
            this.Skill4.Name = "Skill4";
            this.Skill4.Size = new System.Drawing.Size(53, 47);
            this.Skill4.TabIndex = 5;
            this.Skill4.Text = "4";
            this.Skill4.UseVisualStyleBackColor = true;
            this.Skill4.Click += new System.EventHandler(this.Skill4_Click);
            // 
            // Skill8
            // 
            this.Skill8.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill8.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill8.Location = new System.Drawing.Point(688, 794);
            this.Skill8.Name = "Skill8";
            this.Skill8.Size = new System.Drawing.Size(53, 47);
            this.Skill8.TabIndex = 9;
            this.Skill8.Text = "8";
            this.Skill8.UseVisualStyleBackColor = true;
            this.Skill8.Click += new System.EventHandler(this.Skill8_Click);
            // 
            // Skill3
            // 
            this.Skill3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill3.Location = new System.Drawing.Point(393, 794);
            this.Skill3.Name = "Skill3";
            this.Skill3.Size = new System.Drawing.Size(53, 47);
            this.Skill3.TabIndex = 4;
            this.Skill3.Text = "3";
            this.Skill3.UseVisualStyleBackColor = true;
            this.Skill3.Click += new System.EventHandler(this.Skill3_Click);
            // 
            // Skill0
            // 
            this.Skill0.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill0.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill0.Location = new System.Drawing.Point(216, 794);
            this.Skill0.Name = "Skill0";
            this.Skill0.Size = new System.Drawing.Size(53, 47);
            this.Skill0.TabIndex = 11;
            this.Skill0.Text = "0";
            this.Skill0.UseVisualStyleBackColor = true;
            this.Skill0.Click += new System.EventHandler(this.Skill0_Click);
            // 
            // Skill9
            // 
            this.Skill9.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill9.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill9.Location = new System.Drawing.Point(747, 794);
            this.Skill9.Name = "Skill9";
            this.Skill9.Size = new System.Drawing.Size(53, 47);
            this.Skill9.TabIndex = 10;
            this.Skill9.Text = "9";
            this.Skill9.UseVisualStyleBackColor = true;
            this.Skill9.Click += new System.EventHandler(this.Skill9_Click);
            // 
            // Inventory
            // 
            this.Inventory.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Inventory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Inventory.BackColor = System.Drawing.SystemColors.Control;
            this.Inventory.Location = new System.Drawing.Point(806, 794);
            this.Inventory.Name = "Inventory";
            this.Inventory.Size = new System.Drawing.Size(76, 47);
            this.Inventory.TabIndex = 13;
            this.Inventory.Text = "Inventory";
            this.Inventory.UseVisualStyleBackColor = false;
            this.Inventory.Click += new System.EventHandler(this.Inventory_Click);
            // 
            // Skill6
            // 
            this.Skill6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Skill6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Skill6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Skill6.Location = new System.Drawing.Point(570, 794);
            this.Skill6.Name = "Skill6";
            this.Skill6.Size = new System.Drawing.Size(53, 47);
            this.Skill6.TabIndex = 7;
            this.Skill6.Text = "6";
            this.Skill6.UseVisualStyleBackColor = true;
            this.Skill6.Click += new System.EventHandler(this.Skill6_Click);
            // 
            // EquipmentList
            // 
            this.EquipmentList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.EquipmentList.AllowDrop = true;
            this.EquipmentList.BackColor = System.Drawing.Color.Sienna;
            this.EquipmentList.HotTracking = true;
            this.EquipmentList.HoverSelection = true;
            this.EquipmentList.Location = new System.Drawing.Point(3, 3);
            this.EquipmentList.MultiSelect = false;
            this.EquipmentList.Name = "EquipmentList";
            this.EquipmentList.ShowItemToolTips = true;
            this.EquipmentList.Size = new System.Drawing.Size(246, 662);
            this.EquipmentList.TabIndex = 15;
            this.EquipmentList.UseCompatibleStateImageBehavior = false;
            this.EquipmentList.View = System.Windows.Forms.View.SmallIcon;
            this.EquipmentList.ItemActivate += new System.EventHandler(this.EquipmentList_ItemActivate);
            this.EquipmentList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.EquipmentList_ItemDrag);
            this.EquipmentList.DragEnter += new System.Windows.Forms.DragEventHandler(this.EquipmentList_DragEnter);
            // 
            // InventoryPanel
            // 
            this.InventoryPanel.BackColor = System.Drawing.Color.Peru;
            this.InventoryPanel.Controls.Add(this.InventoryList);
            this.InventoryPanel.Controls.Add(this.EquipmentList);
            this.InventoryPanel.Location = new System.Drawing.Point(250, 50);
            this.InventoryPanel.Name = "InventoryPanel";
            this.InventoryPanel.Size = new System.Drawing.Size(675, 668);
            this.InventoryPanel.TabIndex = 2;
            this.InventoryPanel.Visible = false;
            // 
            // InventoryList
            // 
            this.InventoryList.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.InventoryList.AllowDrop = true;
            this.InventoryList.BackColor = System.Drawing.Color.Sienna;
            this.InventoryList.GridLines = true;
            this.InventoryList.Location = new System.Drawing.Point(253, 3);
            this.InventoryList.MultiSelect = false;
            this.InventoryList.Name = "InventoryList";
            this.InventoryList.ShowItemToolTips = true;
            this.InventoryList.Size = new System.Drawing.Size(419, 662);
            this.InventoryList.TabIndex = 16;
            this.InventoryList.TileSize = new System.Drawing.Size(60, 60);
            this.InventoryList.UseCompatibleStateImageBehavior = false;
            this.InventoryList.View = System.Windows.Forms.View.Tile;
            this.InventoryList.ItemActivate += new System.EventHandler(this.InventoryList_ItemActivate);
            this.InventoryList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.InventoryList_ItemDrag);
            this.InventoryList.DragEnter += new System.Windows.Forms.DragEventHandler(this.InventoryList_DragEnter);
            // 
            // SkillView
            // 
            this.SkillView.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.SkillView.BackColor = System.Drawing.Color.Sienna;
            this.SkillView.Location = new System.Drawing.Point(250, 50);
            this.SkillView.Name = "SkillView";
            this.SkillView.ShowItemToolTips = true;
            this.SkillView.Size = new System.Drawing.Size(675, 665);
            this.SkillView.TabIndex = 17;
            this.SkillView.TileSize = new System.Drawing.Size(60, 60);
            this.SkillView.UseCompatibleStateImageBehavior = false;
            this.SkillView.View = System.Windows.Forms.View.Tile;
            this.SkillView.Visible = false;
            this.SkillView.ItemActivate += new System.EventHandler(this.SkillView_ItemActivate);
            // 
            // Mapview
            // 
            this.Mapview.Activation = System.Windows.Forms.ItemActivation.TwoClick;
            this.Mapview.BackColor = System.Drawing.Color.Sienna;
            this.Mapview.Location = new System.Drawing.Point(250, 50);
            this.Mapview.Name = "Mapview";
            this.Mapview.ShowItemToolTips = true;
            this.Mapview.Size = new System.Drawing.Size(675, 665);
            this.Mapview.TabIndex = 18;
            this.Mapview.TileSize = new System.Drawing.Size(60, 60);
            this.Mapview.UseCompatibleStateImageBehavior = false;
            this.Mapview.View = System.Windows.Forms.View.Tile;
            this.Mapview.Visible = false;
            // 
            // Maps
            // 
            this.Maps.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Maps.FormattingEnabled = true;
            this.Maps.Location = new System.Drawing.Point(888, 771);
            this.Maps.Name = "Maps";
            this.Maps.Size = new System.Drawing.Size(82, 24);
            this.Maps.TabIndex = 19;
            this.Maps.SelectedIndexChanged += new System.EventHandler(this.Maps_SelectedIndexChanged);
            // 
            // Characters
            // 
            this.Characters.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Characters.FormattingEnabled = true;
            this.Characters.Location = new System.Drawing.Point(888, 806);
            this.Characters.Name = "Characters";
            this.Characters.Size = new System.Drawing.Size(82, 24);
            this.Characters.TabIndex = 20;
            this.Characters.SelectedIndexChanged += new System.EventHandler(this.Characters_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(982, 853);
            this.Controls.Add(this.Characters);
            this.Controls.Add(this.Maps);
            this.Controls.Add(this.Mapview);
            this.Controls.Add(this.SkillView);
            this.Controls.Add(this.InventoryPanel);
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
            this.InventoryPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Button Skill6;
        private System.Windows.Forms.ListView EquipmentList;
        private System.Windows.Forms.Panel InventoryPanel;
        private System.Windows.Forms.ListView InventoryList;
        private System.Windows.Forms.ListView SkillView;
        private System.Windows.Forms.TextBox Details;
        private System.Windows.Forms.ListView Mapview;
        private System.Windows.Forms.ComboBox Maps;
        private System.Windows.Forms.ComboBox Characters;
    }
}

