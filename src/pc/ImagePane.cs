using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace PViewer
{
	/// <summary>
	/// Summary description for ImagePane.
	/// </summary>
	public class ImagePane : BasePane
	{
        private PViewer.ImageBox imageBox;
        private System.Windows.Forms.ContextMenu contextMenu;
        private System.Windows.Forms.MenuItem menuItemProperties;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemOpenFile;
        private System.Windows.Forms.MenuItem menuItemLocateOnDisk;
        private System.Windows.Forms.MenuItem menuItemDeleteFromDisk;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ImagePane()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
        }

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.imageBox = new PViewer.ImageBox();
            this.contextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemOpenFile = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemLocateOnDisk = new System.Windows.Forms.MenuItem();
            this.menuItemDeleteFromDisk = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemProperties = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.BackColor = System.Drawing.Color.DarkGray;
            this.imageBox.ContextMenu = this.contextMenu;
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.ImageInfo = null;
            this.imageBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.imageBox.Location = new System.Drawing.Point(0, 20);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(216, 228);
            this.imageBox.TabIndex = 1;
            this.imageBox.Zoom = 1;
            this.imageBox.ImageUnloaded += new EventHandler(this.OnImageUnloaded);
            this.imageBox.ImageLoaded += new EventHandler(this.OnImageLoaded);
            // 
            // contextMenu
            // 
            this.contextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                        this.menuItemOpenFile,
                                                                                        this.menuItem4,
                                                                                        this.menuItemLocateOnDisk,
                                                                                        this.menuItemDeleteFromDisk,
                                                                                        this.menuItem2,
                                                                                        this.menuItemProperties});
            this.contextMenu.Popup += new System.EventHandler(this.OnPopupContextMenu);
            // 
            // menuItemOpenFile
            // 
            this.menuItemOpenFile.Index = 0;
            this.menuItemOpenFile.Text = "Open File";
            this.menuItemOpenFile.Click += new System.EventHandler(this.OnOpenFile);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "-";
            // 
            // menuItemLocateOnDisk
            // 
            this.menuItemLocateOnDisk.Index = 2;
            this.menuItemLocateOnDisk.Text = "Locate on Disk";
            this.menuItemLocateOnDisk.Click += new System.EventHandler(this.OnLocateOnDisk);
            // 
            // menuItemDeleteFromDisk
            // 
            this.menuItemDeleteFromDisk.Index = 3;
            this.menuItemDeleteFromDisk.Text = "Delete from Disk";
            this.menuItemDeleteFromDisk.Click += new System.EventHandler(this.OnDeleteFromDisk);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 4;
            this.menuItem2.Text = "-";
            // 
            // menuItemProperties
            // 
            this.menuItemProperties.Index = 5;
            this.menuItemProperties.Text = "&Properties";
            this.menuItemProperties.Click += new System.EventHandler(this.OnProperties);
            // 
            // ImagePane
            // 
            this.CaptionText = "Picture";
            this.Controls.Add(this.imageBox);
            this.Name = "ImagePane";
            this.Controls.SetChildIndex(this.imageBox, 0);
            this.ResumeLayout(false);

        }
		#endregion

        private void OnImageLoaded( object sender, EventArgs e)
        {
        }

        private void OnImageUnloaded(object sender, EventArgs e)
        {
            this.Text = "Empty";
        }

        private void OnProperties(object sender, System.EventArgs e)
        {
            ImagePropertiesForm form = new ImagePropertiesForm(Box.ImageInfo);
            form.ShowDialog();
        }

        private void OnOpenFile(object sender, System.EventArgs e)
        {
            Process.Start(Box.ImageInfo.ImagePath);
        }

        private void OnLocateOnDisk(object sender, System.EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(Box.ImageInfo.ImagePath));
        }

        private void OnDeleteFromDisk(object sender, System.EventArgs e)
        {
            Box.ImageInfo.Trash();
        }

        private void OnPopupContextMenu(object sender, System.EventArgs e)
        {
            menuItemProperties.Enabled = (Box.ImageInfo != null);
            menuItemOpenFile.Enabled = (Box.ImageInfo != null);
            menuItemLocateOnDisk.Enabled = (Box.ImageInfo != null);
            menuItemDeleteFromDisk.Enabled = (Box.ImageInfo != null);
        }

        public ImageBox Box
        {
            get
            {
                return imageBox;
            }
        }
	}
}
