using System;

namespace PViewer
{
    /// <summary>
    /// Preference for viewer application.
    /// </summary>
    public class ViewerPreference
    {
        private bool bFullScreen;
        public bool FullScreen
        {
            get
            {
                return bFullScreen;
            }
            set
            {
                bFullScreen = value;
            }
        }
    }
}
