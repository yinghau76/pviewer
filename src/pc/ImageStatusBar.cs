using System;
using System.Windows.Forms;

namespace PViewer
{
	/// <summary>
	/// Summary description for ImageStatusBar.
	/// </summary>
    internal class ImageStatusBar : StatusBar
    {
        public void AddPanels()
        {
            for ( int i=0; i<6; i++)
            {
                StatusBarPanel panel = new StatusBarPanel();
                panel.BorderStyle = StatusBarPanelBorderStyle.Sunken;
                panel.AutoSize = StatusBarPanelAutoSize.Contents;

                Panels.Add(panel);
            }
        }

        public StatusBarPanel NavPanel
        {
            get 
            {
                return Panels[0];
            }
        }

        public StatusBarPanel DimensionPanel
        {
            get 
            {
                return Panels[1];
            }
        }

        public StatusBarPanel SizePanel
        {
            get 
            {
                return Panels[2];
            }
        }

        public StatusBarPanel DatePanel
        {
            get 
            {
                return Panels[3];
            }
        }

        public StatusBarPanel ZoomPanel
        {
            get 
            {
                return Panels[4];
            }
        }

        public StatusBarPanel CommentPanel
        {
            get 
            {
                return Panels[5];
            }
        }
    }
}
