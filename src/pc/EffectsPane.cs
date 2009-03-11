using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using PViewer.Imaging;

namespace PViewer
{
	public class EffectsPane : PViewer.BasePane
	{
        private System.Windows.Forms.ListView listView;
		private System.ComponentModel.IContainer components = null;

		public EffectsPane()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
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
            this.listView = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.BackColor = System.Drawing.Color.DarkGray;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.Location = new System.Drawing.Point(1, 21);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(214, 226);
            this.listView.TabIndex = 1;
            this.listView.VisibleChanged += new System.EventHandler(this.OnVisibleChanged);
            // 
            // EffectsPane
            // 
            this.Controls.Add(this.listView);
            this.DockPadding.All = 1;
            this.Name = "EffectsPane";
            this.Controls.SetChildIndex(this.listView, 0);
            this.ResumeLayout(false);

        }
		#endregion

        public ListView List
        {
            get
            {
                return listView;
            }
        }

        private ImageNavigator _navToUpdate;

        public void OnImageNavigation(object sender, EventArgs e)
        {
            ImageNavigator nav = (ImageNavigator) sender;

            // don't bother to update thumbnail if this pane is not visible
            if (Visible)
            {
                UpdateEffectThumbnails(nav.CurrentImage);
                _navToUpdate = null;
            }
            else
            {
                _navToUpdate = nav;
            }
        }

        private const int cxThumbnail = 120;
        private const int cyThumbnail = 120;

        private void UpdateEffectThumbnails(ImageInfo imageInfo)
        {
            // Remove existing effect thumbnails.
            listView.Items.Clear();

            if (imageInfo == null) 
            {
                return;
            }

            Image image = imageInfo.Image;

            // Create a normal thumbnail first.
            Rectangle thumbRect = FitSize.FitToWindowLargeOnly(new Size(cxThumbnail,cyThumbnail), image.Size);
            using (Image thumb = ImageHelper.GetThumbnail(image, thumbRect.Width, thumbRect.Height, BackColor))
            {
                // Create new image list.
                ImageList imageListLarge = new ImageList();
                imageListLarge.ColorDepth = ColorDepth.Depth24Bit;
                imageListLarge.ImageSize = new Size( thumb.Width, thumb.Height);
                listView.LargeImageList = imageListLarge;

                // Add thumbnail of all effects.
                using (Bitmap effectThumb = (Bitmap) thumb.Clone())
                {
                    CSharpFilters.BitmapFilter.GrayScale(effectThumb);
                    AddEffectThumbnail( effectThumb, "Gray Scale");
                }

                using (Bitmap effectThumb = (Bitmap) thumb.Clone())
                {
                    CSharpFilters.BitmapFilter.MeanRemoval(effectThumb, 9);
                    AddEffectThumbnail( effectThumb, "MeanRemoval");
                }

                using (Bitmap effectThumb = (Bitmap) thumb.Clone())
                {
                    CSharpFilters.BitmapFilter.Smooth(effectThumb, 1);
                    AddEffectThumbnail( effectThumb, "Smooth");
                }

                using (Bitmap effectThumb = (Bitmap) thumb.Clone())
                {
                    CSharpFilters.BitmapFilter.EmbossLaplacian(effectThumb);
                    AddEffectThumbnail( effectThumb, "EmbossLaplacian");
                }

                using (Bitmap effectThumb = (Bitmap) thumb.Clone())
                {
                    CSharpFilters.BitmapFilter.EdgeDetectQuick(effectThumb);
                    AddEffectThumbnail( effectThumb, "EdgeDetectQuick");
                }
            }
        }

        private void AddEffectThumbnail(Bitmap effectThumb, string name)
        {
            listView.LargeImageList.Images.Add(effectThumb);
                    
            ListViewItem item = new ListViewItem();
            item.ImageIndex = listView.LargeImageList.Images.Count - 1;
            item.Text = name;
            listView.Items.Add(item);
        }

        private void OnSelectedEffectChanged(object sender, System.EventArgs e)
        {
        }

        private void OnVisibleChanged(object sender, System.EventArgs e)
        {
            if (Visible && _navToUpdate != null)
            {
                UpdateEffectThumbnails(_navToUpdate.CurrentImage);
                _navToUpdate = null;
            }
        }
	}
}

