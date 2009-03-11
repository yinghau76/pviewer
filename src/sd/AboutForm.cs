using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace PViewer
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();

            Version v = Assembly.GetExecutingAssembly().GetName().Version;
            labelTitle.Text = "PViewer Version : " + v.Major + "." + v.Minor + "." + v.Revision + "." + v.Build;

            string strInfo;
            strInfo = ".NET Version: " + Environment.Version.ToString() + "\n";
            strInfo += "OS Version: " + Environment.OSVersion.ToString() + "\n";
            labelSysInfo.Text = strInfo;
        }
    }
}