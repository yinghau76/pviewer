using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PViewer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void OnAbout(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog();
        }
    }
}