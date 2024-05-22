namespace MMS
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.filesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downscaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shannonFanoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.izTxtUSlikuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uTxtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filteriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contrastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sharpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.edgeDetectDifToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.randomJitterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filesToolStripMenuItem,
            this.editToolStripMenuItem,
            this.downscaleToolStripMenuItem,
            this.shannonFanoToolStripMenuItem,
            this.filteriToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(792, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // filesToolStripMenuItem
            // 
            this.filesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.filesToolStripMenuItem.Name = "filesToolStripMenuItem";
            this.filesToolStripMenuItem.Size = new System.Drawing.Size(52, 24);
            this.filesToolStripMenuItem.Text = "Files";
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.loadToolStripMenuItem.Text = "Load";
            this.loadToolStripMenuItem.Click += new System.EventHandler(this.loadToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(125, 26);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // downscaleToolStripMenuItem
            // 
            this.downscaleToolStripMenuItem.Name = "downscaleToolStripMenuItem";
            this.downscaleToolStripMenuItem.Size = new System.Drawing.Size(95, 24);
            this.downscaleToolStripMenuItem.Text = "Downscale";
            this.downscaleToolStripMenuItem.Click += new System.EventHandler(this.downscaleToolStripMenuItem_Click);
            // 
            // shannonFanoToolStripMenuItem
            // 
            this.shannonFanoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.izTxtUSlikuToolStripMenuItem,
            this.uTxtToolStripMenuItem});
            this.shannonFanoToolStripMenuItem.Name = "shannonFanoToolStripMenuItem";
            this.shannonFanoToolStripMenuItem.Size = new System.Drawing.Size(117, 24);
            this.shannonFanoToolStripMenuItem.Text = "Shannon-Fano";
            // 
            // izTxtUSlikuToolStripMenuItem
            // 
            this.izTxtUSlikuToolStripMenuItem.Name = "izTxtUSlikuToolStripMenuItem";
            this.izTxtUSlikuToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.izTxtUSlikuToolStripMenuItem.Text = "Iz txt u sliku";
            this.izTxtUSlikuToolStripMenuItem.Click += new System.EventHandler(this.izTxtUSlikuToolStripMenuItem_Click);
            // 
            // uTxtToolStripMenuItem
            // 
            this.uTxtToolStripMenuItem.Name = "uTxtToolStripMenuItem";
            this.uTxtToolStripMenuItem.Size = new System.Drawing.Size(169, 26);
            this.uTxtToolStripMenuItem.Text = "U txt";
            this.uTxtToolStripMenuItem.Click += new System.EventHandler(this.uTxtToolStripMenuItem_Click);
            // 
            // filteriToolStripMenuItem
            // 
            this.filteriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contrastToolStripMenuItem,
            this.sharpenToolStripMenuItem,
            this.edgeDetectDifToolStripMenuItem,
            this.randomJitterToolStripMenuItem});
            this.filteriToolStripMenuItem.Name = "filteriToolStripMenuItem";
            this.filteriToolStripMenuItem.Size = new System.Drawing.Size(60, 24);
            this.filteriToolStripMenuItem.Text = "Filteri";
            // 
            // contrastToolStripMenuItem
            // 
            this.contrastToolStripMenuItem.Name = "contrastToolStripMenuItem";
            this.contrastToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.contrastToolStripMenuItem.Text = "Contrast";
            this.contrastToolStripMenuItem.Click += new System.EventHandler(this.contrastToolStripMenuItem_Click);
            // 
            // sharpenToolStripMenuItem
            // 
            this.sharpenToolStripMenuItem.Name = "sharpenToolStripMenuItem";
            this.sharpenToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.sharpenToolStripMenuItem.Text = "Sharpen";
            this.sharpenToolStripMenuItem.Click += new System.EventHandler(this.sharpenToolStripMenuItem_Click);
            // 
            // edgeDetectDifToolStripMenuItem
            // 
            this.edgeDetectDifToolStripMenuItem.Name = "edgeDetectDifToolStripMenuItem";
            this.edgeDetectDifToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.edgeDetectDifToolStripMenuItem.Text = "Edge detect dif.";
            this.edgeDetectDifToolStripMenuItem.Click += new System.EventHandler(this.edgeDetectDifToolStripMenuItem_Click);
            // 
            // randomJitterToolStripMenuItem
            // 
            this.randomJitterToolStripMenuItem.Name = "randomJitterToolStripMenuItem";
            this.randomJitterToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.randomJitterToolStripMenuItem.Text = "Random jitter";
            this.randomJitterToolStripMenuItem.Click += new System.EventHandler(this.randomJitterToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem filesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem downscaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem shannonFanoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem izTxtUSlikuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uTxtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filteriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contrastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sharpenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem edgeDetectDifToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem randomJitterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
    }
}

