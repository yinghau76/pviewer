using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PViewer
{
	public class ThumbnailPane : PViewer.BasePane
	{
        private PViewer.ThumbnailListView listView;
		private System.ComponentModel.IContainer components = null;

		public ThumbnailPane()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.listView = new PViewer.ThumbnailListView();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.Color.DarkGray;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Location = new System.Drawing.Point(1, 1);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(214, 246);
            this.listView.TabIndex = 1;
            // 
            // ThumbnailPane
            // 
            this.BackColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(this.listView);
            this.DockPadding.All = 1;
            this.Name = "ThumbnailPane";
            this.Controls.SetChildIndex(this.listView, 0);
            this.ResumeLayout(false);

        }
		#endregion

        public ThumbnailListView List
        {
            get
            {
                return listView;
            }
        }
	}
}

