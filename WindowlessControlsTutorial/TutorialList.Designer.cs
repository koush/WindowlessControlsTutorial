namespace WindowlessControlsTutorial
{
    partial class TutorialList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.myHost = new WindowlessControls.VerticalStackPanelHost();
            this.myExitMenuItem = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.myExitMenuItem);
            // 
            // myHost
            // 
            this.myHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myHost.Location = new System.Drawing.Point(0, 0);
            this.myHost.Name = "myHost";
            this.myHost.Size = new System.Drawing.Size(240, 268);
            this.myHost.TabIndex = 0;
            // 
            // myExitMenuItem
            // 
            this.myExitMenuItem.Text = "Exit";
            this.myExitMenuItem.Click += new System.EventHandler(this.myExitMenuItem_Click);
            // 
            // TutorialList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.myHost);
            this.Menu = this.mainMenu1;
            this.Name = "TutorialList";
            this.Text = "TutorialList";
            this.ResumeLayout(false);

        }

        #endregion

        private WindowlessControls.VerticalStackPanelHost myHost;
        private System.Windows.Forms.MenuItem myExitMenuItem;
    }
}