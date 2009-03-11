using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using PViewer.Imaging;
using XPBurn;

namespace PViewer
{
	/// <summary>
	/// Summary description for BurningForm.
	/// </summary>
	public class BurningForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.ColumnHeader columnHeaderMessage;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ListView listViewProgress;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public BurningForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.listViewProgress = new System.Windows.Forms.ListView();
            this.columnHeaderMessage = new System.Windows.Forms.ColumnHeader();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listViewProgress
            // 
            this.listViewProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewProgress.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                                                                                               this.columnHeaderMessage});
            this.listViewProgress.Location = new System.Drawing.Point(8, 8);
            this.listViewProgress.Name = "listViewProgress";
            this.listViewProgress.Size = new System.Drawing.Size(460, 244);
            this.listViewProgress.TabIndex = 0;
            this.listViewProgress.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderMessage
            // 
            this.columnHeaderMessage.Text = "Messages";
            this.columnHeaderMessage.Width = 465;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.Location = new System.Drawing.Point(8, 260);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // BurningForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(480, 294);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.listViewProgress);
            this.Name = "BurningForm";
            this.Text = "Burning";
            this.TopMost = true;
            this.ResumeLayout(false);

        }
		#endregion

        private void AddMessage(string msg)
        {
            listViewProgress.Items.Add(new ListViewItem(msg));
        }

        private XPBurnCD _burnCD;

        public void BurnToCD(ImageCollection images)
        {
            if (_burnCD != null)
            {
                throw new InvalidOperationException("An existing burning session is in progress.");
            }

            try
            {
                _burnCD = new XPBurnCD();
                _burnCD.ActiveFormat = RecordType.afData;

                _burnCD.AddProgress += new NotifyCDProgress(OnAddProgress);
                _burnCD.BurnComplete += new NotifyCompletionStatus(OnBurnComplete);
                _burnCD.ClosingDisc += new NotifyEstimatedTime(OnClosingDisc);
                _burnCD.EraseComplete += new NotifyCompletionStatus(OnEraseComplete);
                _burnCD.PreparingBurn += new NotifyEstimatedTime(OnPreparingBurn);
                _burnCD.RecorderChange += new NotifyPnPActivity(OnRecorderChange);
                _burnCD.TrackProgress += new NotifyCDProgress(OnTrackProgress);
                _burnCD.BlockProgress +=new NotifyCDProgress(OnBlockProgress);
                // _burnCD.ErrorHappened +=new NotifyErrorStatus(ErrorHappened);
            
                foreach (ImageInfo ii in images)
                {
                    _burnCD.AddFile(ii.ImagePath, ii.ImagePath);
                }

                if (_burnCD.MediaInfo.isWritable)
                {
                    _burnCD.RecordDisc(false, false);
                }
            }
            catch (Exception ex)
            {
                AddMessage(ex.ToString());
            }
        }

        private void OnAddProgress(int nCompletedSteps, int nTotalSteps)
        {
            AddMessage(string.Format("add progress {0}/{1}", nCompletedSteps, nTotalSteps));
        }

        private void OnBurnComplete(uint status)
        {
            AddMessage(string.Format("burn complete, status = {0}", status));
            
            _burnCD = null;
            
            Close();
        }

        private void OnClosingDisc(int nEstimatedSeconds)
        {
            AddMessage(string.Format("closing disc, estimated sec = {0}", nEstimatedSeconds));
        }

        private void OnEraseComplete(uint status)
        {
            AddMessage(string.Format("erase complete"));
        }

        private void OnPreparingBurn(int nEstimatedSeconds)
        {
            AddMessage(string.Format("preparing burn, estimated sec = {0}", nEstimatedSeconds));
        }

        private void OnRecorderChange()
        {
            AddMessage(string.Format("recorder change"));
        }

        private void OnTrackProgress(int nCompletedSteps, int nTotalSteps)
        {
            AddMessage(string.Format("track progress {0}/{1}", nCompletedSteps, nTotalSteps));
        }

        private void OnBlockProgress(int nCompletedSteps, int nTotalSteps)
        {
            AddMessage(string.Format("block progress {0}/{1}", nCompletedSteps, nTotalSteps));
        }

        private void OnCancel(object sender, System.EventArgs e)
        {
            if (_burnCD.IsBurning)
            {
                _burnCD.Cancel = true;
            }
        }

        private void ErrorHappened(Exception ex)
        {
            AddMessage(ex.ToString());
        }
    }
}
