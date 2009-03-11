using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using PViewer.Imaging;

namespace PViewer
{
    /// <summary>
    /// Summary description for ThumbnailListView.
    /// </summary>
    public class ThumbnailListView : System.Windows.Forms.ListView
    {
        enum ImageLoadingState
        {
            Idle,           // no image loading is in progress.
            Loading,        // image loading is in progress.
            CancelLoading,  // image loading should be canceled.
        }

        private ImageLoadingState imageLoadingState = ImageLoadingState.Idle;

        private delegate void SetThumbnailDelegate( ListViewItem item, Image thumbnail, out bool cancel);
        private delegate void ThumbnailLoadingDoneDelegate();
        private delegate void LoadThumbnailsDelegate(ImageCollection images);

        private ThumbnailLoadingDoneDelegate imageLoadingDone;
        private ImageCollection imagesToRefresh;

        private const int cxThumbnail = 120;
        private const int cyThumbnail = 120;

        public ThumbnailListView()
        {
            _format.Alignment = StringAlignment.Center;
            _format.Trimming = StringTrimming.EllipsisCharacter;
        }

        /// <summary>
        /// Set the image thumbnail for specified list item. Note that we have to consider threading issues here.
        /// </summary>
        /// <param name="item">[in] the list item to</param>
        /// <param name="thumbnail">[in] the image for specified list item</param>
        /// <param name="cancel">[out] indicate if image loading is canceled</param>
        private void SetListItemImage( ListViewItem item, Image thumbnail, out bool cancel)
        {
            if (this.InvokeRequired == false)
            {
                // cache image thumbnail
                ImageInfo ii = (ImageInfo) item.Tag;
                if (ii != null)
                {
                    ii.Thumbnail = thumbnail;

                    // invalidate the item rectangle to ensure repaint
                    item.ListView.Invalidate(GetItemRect(item.Index));
                }

                cancel = (imageLoadingState == ImageLoadingState.CancelLoading);
            }
            else
            {
                // Add thumbnail on the thread that owns the control's underlying window handle.
                SetThumbnailDelegate d = new SetThumbnailDelegate(SetListItemImage);

                // Avoid boxing and losing our return value.
                object inoutCancel = false;
                BeginInvoke( d, new Object[] { item, thumbnail, inoutCancel });

                cancel = (bool) inoutCancel;
            }
        }

        /// <summary>
        /// This method will be called by delegate asynchronously.
        /// </summary>
        private void LoadThumbnails(ImageCollection images)
        {
            try
            {
                bool cancel = false;
                for ( int i = 0; i < Items.Count && i < images.Count && !cancel; i++)
                {
                    // [TODO] prevent loading the entire image for thumbnail...
                    using (Image image = ImageFast.FromFile(images[i].ImagePath))
                    {
                        // generate thumbnail padding with background color
                        Image thumbnail = ImageHelper.GetThumbnail(image, cxThumbnail, cyThumbnail, BackColor);

                        SetListItemImage( Items[i], thumbnail, out cancel);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.Write(ex.ToString());
            }

            BeginInvoke( imageLoadingDone, null);
        }

        /// <summary>
        /// Called when image loading is done.
        ///
        /// effects: switch the image loading state to Idle.
        /// </summary>
        private void ImageLoadingDone()
        {
            imageLoadingState = ImageLoadingState.Idle;
            imageLoadingDone = null;
        }

        /// <summary>
        /// Called when image loading is canceled.
        /// effects: switch the image loading state to Idle and refresh list view.
        /// </summary>
        private void ImageLoadingDoneAndRefresh()
        {
            ImageLoadingDone();
            RefreshThumbnails(imagesToRefresh);

            imagesToRefresh = null;
        }

        private void CancelLoading(ImageCollection images)
        {
            imagesToRefresh = images;

            // remove all attached ImageInfo before canceling the loading to prevent flash
            foreach (ListViewItem item in Items)
            {
                item.Tag = null;
            }

            // change the status to cancel image loading
            imageLoadingState = ImageLoadingState.CancelLoading;

            // delay the refresh until image loading is canceled
            imageLoadingDone = new ThumbnailLoadingDoneDelegate(ImageLoadingDoneAndRefresh);
        }

        public void RefreshThumbnails(ImageCollection images)
        {
            if (imageLoadingState == ImageLoadingState.Loading ||
                imageLoadingState == ImageLoadingState.CancelLoading)
            {
                CancelLoading(images);
                return;
            }

            Debug.Assert(imageLoadingState == ImageLoadingState.Idle);

            // Show them in list view.
            Items.Clear();
            ImageList imageListLarge = new ImageList();
            imageListLarge.ColorDepth = ColorDepth.Depth24Bit;
            imageListLarge.ImageSize = new Size( cxThumbnail, cyThumbnail);
            LargeImageList = imageListLarge;

            BeginUpdate();

            // Add items first to prevent flickers.
            foreach (ImageInfo imageFile in images)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = -1;
                item.Text = Path.GetFileName(imageFile.ImagePath);
                item.Tag = imageFile;
                Items.Add(item);
            }

            EndUpdate();

            // Switch the state.
            imageLoadingState = ImageLoadingState.Loading;
            imageLoadingDone = new ThumbnailLoadingDoneDelegate(ImageLoadingDone);

            // Use asynchronous delegate to load image thumbnails.
            LoadThumbnailsDelegate loadThumbnails = new LoadThumbnailsDelegate(LoadThumbnails);
            loadThumbnails.BeginInvoke( images, null, new Object());
        }

        #region "custom-draw code"

        private class Win32
        {
            // Methods
            public Win32()
            {}

            public const int CDDS_PREPAINT = 0x01;
            public const int CDDS_ITEM = 0x10000;
            public const int CDDS_ITEMPREPAINT = CDDS_ITEM | CDDS_PREPAINT;

            public const int CDRF_DODEFAULT = 0x00;
            public const int CDRF_SKIPDEFAULT = 0x04;
            public const int CDRF_NOTIFYITEMDRAW = 0x20;
            
            public const int NM_CUSTOMDRAW = -12;
            public const int NM_SETFOCUS = -7;

            public const int LVN_ITEMCHANGED = -101;

            public const int WM_NOTIFY = 0x4e;
            public const int OCM_BASE = 0x2000;
            public const int OCM_NOTIFY = WM_NOTIFY + OCM_BASE;

            public const int WM_ERASEBKGND = 20;
            public const int WM_NCPAINT = 0x85;
            
            [StructLayout(LayoutKind.Sequential)]
            public struct NMCUSTOMDRAW
            {
                public Win32.NMHDR hdr;
                public int dwDrawStage;
                public IntPtr hdc;
                public Win32.RECT rc;
                public int dwItemSpec;
                public int uItemState;
                public IntPtr lItemlParam;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct NMHDR
            {
                public IntPtr hwndFrom;
                public int idFrom;
                public int code;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
        }

        /// <summary>
        /// override the window proc and look for the custom draw message and 
        /// for certain messages to control when the background really needs 
        /// to be painted
        /// </summary>
        /// <param name="m"></param>
        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode=true)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32.WM_NCPAINT)
            {
                // always repaint background if get WM_NCPAINT
                _backgroundDirty = true;
            }

            if (m.Msg == Win32.WM_ERASEBKGND)
            {
                // see if should repaint the background or not
                if (!ProcessBackground())
                {
                    return;
                }
            }

            // look for messages to owner draw the listview items
            if (m.Msg == Win32.OCM_NOTIFY)
            {
                // get the notification info
                Win32.NMHDR nmhdr = (Win32.NMHDR) m.GetLParam(typeof(Win32.NMHDR));

                // turn off background painting when get the item changed message
                if (nmhdr.code == Win32.LVN_ITEMCHANGED)
                {
                    _paintBackground = false;
                }

                // check for custom draw message
                if (nmhdr.hwndFrom.Equals(Handle) && (nmhdr.code == Win32.NM_CUSTOMDRAW))
                {
                    _paintBackground = true;

                    // process the message, returns true if performed custom drawing, otherwise false
                    if (ProcessListCustomDraw(ref m))
                    {
                        return;
                    }
                }
            }
            base.WndProc(ref m);
        }

        /// <summary></summary>
        /// <returns>return true if Windows should paint the background 
        /// otherwise false to prevent Windows from painting the background</returns>
        private bool ProcessBackground()
        {
            bool paintBackground = true;

            // see if the top item has moved since the last paint background message
            if (!_backgroundDirty && !_paintBackground && Items.Count > 0)
            {
                if (!_topItemPos.Equals(GetItemRect(0).Location))
                {
                    _topItemPos = GetItemRect(0).Location;
                    _paintBackground = true;
                }
            }

            // windows should not paint the background when both flags are cleared
            if (!_paintBackground && !_backgroundDirty)
            {
                paintBackground = false;
            }

            // reset the override flag
            _backgroundDirty = false;

            return paintBackground;
        }

        /// <summary>
        /// one step closer to detecting if a listview item should be drawn
        /// see http://msdn.microsoft.com/library/default.asp?url=/library/en-us/shellcc/platform/commctls/custdraw/custdraw.asp
        /// </summary>
        /// <param name="m"></param>
        /// <returns>return true if the listview item was drawn</returns>
        private bool ProcessListCustomDraw(ref Message m)
        {
            bool drawSelf = false;

            // get custom draw information
            Win32.NMCUSTOMDRAW customDraw = (Win32.NMCUSTOMDRAW) m.GetLParam(typeof(Win32.NMCUSTOMDRAW));

            // return different values in the message depending on the draw stage
            switch (customDraw.dwDrawStage)
            {
            case Win32.CDDS_PREPAINT:
                m.Result = new IntPtr(Win32.CDRF_NOTIFYITEMDRAW);
                break;

            case Win32.CDDS_ITEMPREPAINT:
                m.Result = new IntPtr(Win32.CDRF_SKIPDEFAULT);
                // finally, draw the listview item
                using (Graphics g = Graphics.FromHdc(customDraw.hdc))
                {
                    DrawItem(g, customDraw.dwItemSpec);
                    drawSelf = true;
                }
                break;

            default:
                m.Result = new IntPtr(Win32.CDRF_DODEFAULT);
                break;
            }

            return drawSelf;
        }

        // called when the thumbnail needs to be drawn
        private new void DrawItem(Graphics g, int index)
        {
            // get the item that needs to be drawn
            ListViewItem item = Items[index];

            // calculate area to draw thumbnail, usually would center the vertical position
            // but this moves the thumbnail down when the title contains a very long
            // string, instead, center the horizontal position, but always draw
            // the vertical position from a set amount from the top
            int h = Font.Height;
            Rectangle rc = new Rectangle(item.Bounds.Left + ((item.Bounds.Width - cxThumbnail) / 2), item.Bounds.Top + 2, cxThumbnail, cyThumbnail);

            ImageInfo ii = (ImageInfo) item.Tag;
            if (ii != null && ii.Thumbnail != null)
            {
                // draw the thumbnail image
                Image thumbnail = ii.Thumbnail;
                g.DrawImage(thumbnail, (int) (rc.Left + ((cxThumbnail - thumbnail.Width) / 2)), (int) (rc.Top + ((cyThumbnail - thumbnail.Height) / 2)), thumbnail.Width, thumbnail.Height);

                // erase the thicker selected border
                if (!item.Selected)
                {
                    g.DrawRectangle(_penBack, rc);
                }

                // border
                g.DrawRectangle(item.Selected? _penSelected : _penFrame, rc);

                // title, calculate the area to draw the title
                RectangleF ef1 = new RectangleF((float) rc.Left, (float) (rc.Bottom + 4), (float) rc.Width, (float) (h + 1));
                g.FillRectangle(item.Selected? _brushSelected : _brushBack, ef1);

                // draw the title, different background if selected or not
                g.DrawString(item.Text, Font, Brushes.Black, ef1, _format);
            }
        }

        private bool IsItemVisible(int index)
        {
            if (Items.Count > index)
            {
                Rectangle rc = GetItemRect(index);
                if (DisplayRectangle.Contains(rc.Left, rc.Top) || 
                    DisplayRectangle.Contains(rc.Right, rc.Bottom))
                {
                    return true;
                }
            }
            return false;
        }
 
        private class Consts
        {
            public static Color BackColor = Color.DarkGray;
            public static Color FrameColor = Color.FromArgb(0xe1, 0xdf, 0xd0);
            public static Color SelectedColor = Color.FromArgb(240, 0xed, 0xdb);
            public const int SelectedFrameSize = 3;
        }

        private bool _backgroundDirty = false;
        private bool _paintBackground = false;
        private Pen _penBack = new Pen(Color.DarkGray, 3f);
        private Pen _penFrame = new Pen(Consts.FrameColor);
        private Pen _penSelected = new Pen(Consts.SelectedColor, 3f);

        private SolidBrush _brushBack = new SolidBrush(Consts.BackColor);
        private SolidBrush _brushSelected = new SolidBrush(Consts.SelectedColor);
        private StringFormat _format = new StringFormat();
        private Point _topItemPos = new Point(0, 0);
        #endregion
    }
}
