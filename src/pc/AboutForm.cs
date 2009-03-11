using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;

namespace PViewer
{
    /// <summary>
    /// Summary description for AboutForm.
    /// </summary>
    public class AboutForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label systemInfoLabel;
        private System.Windows.Forms.LinkLabel AuthorLinkLabel;
        private System.Windows.Forms.Button okButton;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public AboutForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            Version v = Assembly.GetEntryAssembly().GetName().Version;
            titleLabel.Text = "PViewer Version : " + v.Major + "." + v.Minor + "." + v.Revision + "." + v.Build;

            string strInfo;
            strInfo = ".NET Version: " + Environment.Version.ToString() + Environment.NewLine;
            strInfo += "OS Version: " + Environment.OSVersion.ToString() + Environment.NewLine;
            strInfo += "Boot Mode: " + SystemInformation.BootMode + Environment.NewLine;
            strInfo += "Working Set Memory: " + (Environment.WorkingSet / 1024) + "kb" + Environment.NewLine + Environment.NewLine;
            systemInfoLabel.Text = strInfo;
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
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
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
            this.okButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.AuthorLinkLabel = new System.Windows.Forms.LinkLabel();
            this.systemInfoLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // okButton
            //
            this.okButton.Location = new System.Drawing.Point(216, 128);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(72, 24);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.Click += new System.EventHandler(this.OnOK);
            //
            // titleLabel
            //
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(8, 8);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(45, 15);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "PViewer";
            //
            // pictureBox
            //
            this.pictureBox.Image = ((System.Drawing.Bitmap)(resources.GetObject("pictureBox.Image")));
            this.pictureBox.Location = new System.Drawing.Point(224, 16);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(64, 64);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            //
            // AuthorLinkLabel
            //
            this.AuthorLinkLabel.AutoSize = true;
            this.AuthorLinkLabel.LinkArea = new System.Windows.Forms.LinkArea(11, 12);
            this.AuthorLinkLabel.Location = new System.Drawing.Point(8, 32);
            this.AuthorLinkLabel.Name = "AuthorLinkLabel";
            this.AuthorLinkLabel.Size = new System.Drawing.Size(114, 15);
            this.AuthorLinkLabel.TabIndex = 3;
            this.AuthorLinkLabel.TabStop = true;
            this.AuthorLinkLabel.Text = "Written by Patrick Tsai";
            this.AuthorLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnAuthorLink);
            //
            // systemInfoLabel
            //
            this.systemInfoLabel.Location = new System.Drawing.Point(8, 56);
            this.systemInfoLabel.Name = "systemInfoLabel";
            this.systemInfoLabel.Size = new System.Drawing.Size(184, 96);
            this.systemInfoLabel.TabIndex = 4;
            this.systemInfoLabel.Text = "label2";
            //
            // AboutForm
            //
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 15);
            this.ClientSize = new System.Drawing.Size(298, 160);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                       this.systemInfoLabel,
                                       this.AuthorLinkLabel,
                                       this.titleLabel,
                                       this.pictureBox,
                                       this.okButton});
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.ResumeLayout(false);

        }
#endregion

        private void OnOK(object sender, System.EventArgs e)
        {
            Dispose();
        }

        private void OnAuthorLink(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            e.Link.Visited = true;
            System.Diagnostics.Process.Start("http://baby.homeip.net/patrick/");
        }
    }
}
