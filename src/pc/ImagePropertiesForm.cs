using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PViewer.Imaging;
using System.IO;

namespace PViewer
{
	/// <summary>
	/// Summary description for ImagePropertiesForm.
	/// </summary>
	public class ImagePropertiesForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.Label labelLocation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.ListView listViewExif;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.ColumnHeader columnHeaderProperty;
        private System.Windows.Forms.ColumnHeader columnHeaderValue;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        ImageInfo _imageInfo;

		public ImagePropertiesForm(ImageInfo imageInfo)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            
            _imageInfo = imageInfo;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelFilename = new System.Windows.Forms.Label();
            this.labelLocation = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labelSize = new System.Windows.Forms.Label();
            this.listViewExif = new System.Windows.Forms.ListView();
            this.columnHeaderProperty = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderValue = new System.Windows.Forms.ColumnHeader();
            this.buttonOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filename: ";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Location:";
            // 
            // labelFilename
            // 
            this.labelFilename.Location = new System.Drawing.Point(64, 8);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(328, 23);
            this.labelFilename.TabIndex = 2;
            this.labelFilename.Text = "{filename}";
            // 
            // labelLocation
            // 
            this.labelLocation.Location = new System.Drawing.Point(64, 40);
            this.labelLocation.Name = "labelLocation";
            this.labelLocation.Size = new System.Drawing.Size(328, 23);
            this.labelLocation.TabIndex = 3;
            this.labelLocation.Text = "{location}";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Size:";
            // 
            // labelSize
            // 
            this.labelSize.Location = new System.Drawing.Point(64, 72);
            this.labelSize.Name = "labelSize";
            this.labelSize.Size = new System.Drawing.Size(328, 23);
            this.labelSize.TabIndex = 5;
            this.labelSize.Text = "{size}";
            // 
            // listViewExif
            // 
            this.listViewExif.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                           this.columnHeaderProperty,
                                                                                           this.columnHeaderValue});
            this.listViewExif.Location = new System.Drawing.Point(8, 104);
            this.listViewExif.Name = "listViewExif";
            this.listViewExif.Size = new System.Drawing.Size(384, 224);
            this.listViewExif.TabIndex = 6;
            this.listViewExif.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderProperty
            // 
            this.columnHeaderProperty.Text = "Property";
            this.columnHeaderProperty.Width = 144;
            // 
            // columnHeaderValue
            // 
            this.columnHeaderValue.Text = "Value";
            this.columnHeaderValue.Width = 236;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(163, 344);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 24);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.OnOK);
            // 
            // ImagePropertiesForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.ClientSize = new System.Drawing.Size(400, 382);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.listViewExif);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelLocation);
            this.Controls.Add(this.labelFilename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ImagePropertiesForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Properties";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Closed += new System.EventHandler(this.OnClosed);
            this.ResumeLayout(false);

        }
		#endregion

        private void OnLoad(object sender, System.EventArgs e)
        {
            labelFilename.Text = Path.GetFileName(_imageInfo.ImagePath);
            labelLocation.Text = Path.GetDirectoryName(_imageInfo.ImagePath);
            labelSize.Text = _imageInfo.SizeString;

            ListViewItem item;
            if (_imageInfo.Exif.Make.Length > 0)
            {
                item = new ListViewItem();
                item.Text = "Camera make";
                item.SubItems.Add(_imageInfo.Exif.Make);
                listViewExif.Items.Add(item);

                item = new ListViewItem();
                item.Text = "Camera model";
                item.SubItems.Add(_imageInfo.Exif.Model);
                listViewExif.Items.Add(item);

                item = new ListViewItem();
                item.Text = "ISO";
                item.SubItems.Add(_imageInfo.Exif.Iso.ToString());
                listViewExif.Items.Add(item);

                item = new ListViewItem();
                item.Text = "Flash";
                item.SubItems.Add(_imageInfo.Exif.Flash? "Used" : "Not Used");
                listViewExif.Items.Add(item);

                item = new ListViewItem();
                item.Text = "Exposure time";
                item.SubItems.Add(_imageInfo.Exif.ExposureTime.ToString());
                listViewExif.Items.Add(item);

                item = new ListViewItem();
                item.Text = "Aperture";
                item.SubItems.Add(_imageInfo.Exif.Aperture.ToString());
                listViewExif.Items.Add(item);
            }
        }

        private void OnClosed(object sender, System.EventArgs e)
        {
            _imageInfo = null;
        }

        private void OnOK(object sender, System.EventArgs e)
        {
            this.Close();
        }
	}
}
