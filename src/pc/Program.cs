using System;
using System.Windows.Forms;

namespace PViewer
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new MainForm(args));
        }
    }
}