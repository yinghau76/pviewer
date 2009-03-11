namespace PViewer
{
    partial class MainForm
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
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.listViewImages = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.Add(this.menuItem1);
            this.mainMenu1.MenuItems.Add(this.menuItem2);
            // 
            // menuItem1
            // 
            this.menuItem1.Text = "OK";
            // 
            // menuItem2
            // 
            this.menuItem2.MenuItems.Add(this.menuItem3);
            this.menuItem2.Text = "Menu";
            // 
            // menuItem3
            // 
            this.menuItem3.Text = "About";
            this.menuItem3.Click += new System.EventHandler(this.OnAbout);
            // 
            // listViewImages
            // 
            this.listViewImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewImages.Location = new System.Drawing.Point(0, 0);
            this.listViewImages.Name = "listViewImages";
            this.listViewImages.Size = new System.Drawing.Size(373, 268);
            this.listViewImages.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(373, 268);
            this.Controls.Add(this.listViewImages);
            this.Menu = this.mainMenu1;
            this.Name = "MainForm";
            this.Text = "PViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.ListView listViewImages;
    }
}

