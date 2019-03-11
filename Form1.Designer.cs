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
            this.Details = new System.Windows.Forms.Label();
            this.StatWatcher = new System.Windows.Forms.Label();
            this.Skills = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Details
            // 
            this.Details.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Details.AutoSize = true;
            this.Details.BackColor = System.Drawing.Color.Transparent;
            this.Details.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Details.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Details.Location = new System.Drawing.Point(12, 224);
            this.Details.Name = "Details";
            this.Details.Size = new System.Drawing.Size(65, 24);
            this.Details.TabIndex = 0;
            this.Details.Text = "Details";
            // 
            // StatWatcher
            // 
            this.StatWatcher.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StatWatcher.AutoSize = true;
            this.StatWatcher.BackColor = System.Drawing.Color.Transparent;
            this.StatWatcher.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.StatWatcher.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.StatWatcher.Location = new System.Drawing.Point(12, 9);
            this.StatWatcher.Name = "StatWatcher";
            this.StatWatcher.Size = new System.Drawing.Size(49, 24);
            this.StatWatcher.TabIndex = 2;
            this.StatWatcher.Text = "Stats";
            // 
            // Skills
            // 
            this.Skills.AutoSize = true;
            this.Skills.BackColor = System.Drawing.Color.Transparent;
            this.Skills.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Skills.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.Skills.Location = new System.Drawing.Point(12, 544);
            this.Skills.Name = "Skills";
            this.Skills.Size = new System.Drawing.Size(52, 24);
            this.Skills.TabIndex = 3;
            this.Skills.Text = "Skills";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::RPG.Properties.Resources.Form;
            this.ClientSize = new System.Drawing.Size(982, 753);
            this.Controls.Add(this.Skills);
            this.Controls.Add(this.Details);
            this.Controls.Add(this.StatWatcher);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "RPG";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Details;
        private System.Windows.Forms.Label StatWatcher;
        private System.Windows.Forms.Label Skills;
    }
}

