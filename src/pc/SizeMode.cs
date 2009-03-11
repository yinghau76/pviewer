using System;
using System.Drawing;
using System.Windows.Forms;

namespace PViewer
{
    public enum SizeMode
    {
        FitToWindow, 
        FitToWindowWidth, 
        FitToWindowHeight,
        FitToWindowLargeOnly,
        ShowNativeSize, 
        StretchToWindow
    }

    /// <summary>
    /// Summary description for SizeModeBehavior.
    /// </summary>
    internal abstract class SizeModeBehavior
    {
        public abstract SizeMode Mode
        {
            get;
        }

        /// <summary>
        /// Apply this fit type to specified Control.
        /// </summary>
        /// <param name="Control"></param>
        /// <param name="sizeToShow"></param>
        /// <returns>The rectangle to show the image</returns>
        public abstract Rectangle Apply( ScrollableControl control, Size sizeToShow);

        public static SizeModeBehavior FromSizeMode(SizeMode mode)
        {
            switch (mode)
            {
            case SizeMode.FitToWindow:
                return FitToWindow.Instance;

            case SizeMode.FitToWindowLargeOnly:
                return FitToWindowLargeOnly.Instance;

            case SizeMode.ShowNativeSize:
                return ShowNativeSize.Instance;

            case SizeMode.StretchToWindow:
                return StretchToWindow.Instance;

            case SizeMode.FitToWindowWidth:
                return FitToWindowWidth.Instance;

            case SizeMode.FitToWindowHeight:
                return FitToWindowHeight.Instance;

            default:
                throw new ArgumentException("Invalid size mode: " + mode);
            }
        }

        public static bool SupportsTransition(SizeMode mode)
        {
            switch (mode)
            {
            case SizeMode.FitToWindow:
            case SizeMode.FitToWindowLargeOnly:
            case SizeMode.StretchToWindow:
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Show native size in specified Control.
    /// </summary>
    internal sealed class ShowNativeSize : SizeModeBehavior
    {
        private static ShowNativeSize instance;

        public static ShowNativeSize Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShowNativeSize();
                }

                return instance;
            }
        }

        private ShowNativeSize()
        {}

        /// <summary>
        /// To calculate the rectangle to show in specified control.
        /// </summary>
        /// <param name="control">the control to show in</param>
        /// <param name="sizeToShow">the size to show</param>
        /// <returns>The rectangle to show the image</returns>
        public static Rectangle CalcFitRectangle( Size clientSize, Size sizeToShow)
        {
            int x = 0;
            if (clientSize.Width > sizeToShow.Width)
            {
                x = (clientSize.Width - sizeToShow.Width) / 2;
            }

            int y = 0;
            if (clientSize.Height > sizeToShow.Height)
            {
                y = (clientSize.Height - sizeToShow.Height) / 2;
            }

            return new Rectangle( x, y, sizeToShow.Width, sizeToShow.Height);
        }

        public override Rectangle Apply( ScrollableControl control, Size sizeToShow)
        {
            control.AutoScroll = true;
            control.AutoScrollMinSize = sizeToShow;

            control.Invalidate();
            return CalcFitRectangle( control.ClientSize, sizeToShow);
        }

        public override SizeMode Mode
        {
            get { return SizeMode.ShowNativeSize; }
        }
    }

    /// <summary>
    /// Fit to specified Control.
    /// </summary>
    internal sealed class FitToWindow : SizeModeBehavior
    {
        private static FitToWindow instance;

        public static FitToWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FitToWindow();
                }

                return instance;
            }
        }

        private FitToWindow()
        {}

        public static Rectangle CalcFitRectangle( Size clientSize, Size sizeToShow)
        {
            int w = clientSize.Height * sizeToShow.Width / sizeToShow.Height;
            if (w <= clientSize.Width)
            {
                return new Rectangle( (clientSize.Width - w) / 2, 0, w, clientSize.Height);
            }
            else
            {
                int h = clientSize.Width * sizeToShow.Height / sizeToShow.Width;
                return new Rectangle( 0, (clientSize.Height - h) / 2, clientSize.Width, h);
            }
        }

        public override Rectangle Apply( ScrollableControl control, Size sizeToShow)
        {
            control.AutoScroll = false;

            control.Invalidate();
            return CalcFitRectangle( control.ClientSize, sizeToShow);
        }

        public override SizeMode Mode
        {
            get { return SizeMode.FitToWindow; }
        }
    }

    /// <summary>
    /// Fit to the width of specified Control.
    /// </summary>
    internal sealed class FitToWindowWidth : SizeModeBehavior
    {
        private static FitToWindowWidth instance;

        public static FitToWindowWidth Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FitToWindowWidth();
                }

                return instance;
            }
        }

        private FitToWindowWidth()
        {}

        public static Rectangle CalcFitRectangle( Size clientSize, Size sizeToShow)
        {
            int top = 0;
            int w = clientSize.Width;
            int h = sizeToShow.Height * w / sizeToShow.Width;
            if (h > clientSize.Height)
            {
                w -= SystemInformation.VerticalScrollBarWidth;
                h = sizeToShow.Height * w / sizeToShow.Width;
            }
            else
            {
                top = (clientSize.Height - h) / 2;
            }

            return new Rectangle( 0, top, w, h);
        }

        public override Rectangle Apply( ScrollableControl control, Size sizeToShow)
        {
            Rectangle rect = Rectangle.Empty;

            control.AutoScroll = false;

            rect = CalcFitRectangle( control.ClientSize, sizeToShow);
            control.AutoScrollMinSize = rect.Size;
            control.AutoScrollPosition = Point.Empty;

            control.AutoScroll = true;

            control.Invalidate();
            return rect;
        }

        public override SizeMode Mode
        {
            get { return SizeMode.FitToWindowWidth; }
        }
    }

    /// <summary>
    /// Fit to the height of specified Control.
    /// </summary>
    internal sealed class FitToWindowHeight : SizeModeBehavior
    {
        private static FitToWindowHeight instance;

        public static FitToWindowHeight Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FitToWindowHeight();
                }

                return instance;
            }
        }

        private FitToWindowHeight()
        {}

        public static Rectangle CalcFitRectangle( Size clientSize, Size sizeToShow)
        {
            int left = 0;
            int h = clientSize.Height;
            int w = sizeToShow.Width * h / sizeToShow.Height;
            if (w > clientSize.Width)
            {
                h -= SystemInformation.HorizontalScrollBarHeight;
                w = sizeToShow.Width * h / sizeToShow.Height;
            }
            else
            {
                left = (clientSize.Width - w) / 2;
            }

            return new Rectangle( left, 0, w, h);
        }

        public override Rectangle Apply( ScrollableControl control, Size sizeToShow)
        {
            Rectangle rect = Rectangle.Empty;

            control.AutoScroll = false;

            rect = CalcFitRectangle( control.ClientSize, sizeToShow);
            control.AutoScrollMinSize = rect.Size;
            control.AutoScrollPosition = Point.Empty;

            control.AutoScroll = true;

            control.Invalidate();
            return rect;
        }

        public override SizeMode Mode
        {
            get { return SizeMode.FitToWindowHeight; }
        }
    }

    /// <summary>
    /// Fit to the height of specified Control.
    /// </summary>
    internal sealed class FitToWindowLargeOnly : SizeModeBehavior
    {
        private static FitToWindowLargeOnly instance;

        public static FitToWindowLargeOnly Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FitToWindowLargeOnly();
                }

                return instance;
            }
        }

        private FitToWindowLargeOnly()
        {}

        private static bool ShouldDoFitToWindow( Size clientSize, Size sizeToShow)
        {
            return (sizeToShow.Width > clientSize.Width ||
                    sizeToShow.Height > clientSize.Height);
        }

        public static Rectangle CalcFitRectangle( Size clientSize, Size sizeToShow)
        {
            if (ShouldDoFitToWindow( clientSize, sizeToShow))
            {
                return FitToWindow.CalcFitRectangle( clientSize, sizeToShow);
            }
            else 
            {
                return ShowNativeSize.CalcFitRectangle( clientSize, sizeToShow);
            }
        }

        public override Rectangle Apply( ScrollableControl control, Size sizeToShow)
        {
            if (ShouldDoFitToWindow( control.ClientSize, sizeToShow))
            {
                return FitToWindow.Instance.Apply( control, sizeToShow);
            }
            else 
            {
                return ShowNativeSize.Instance.Apply( control, sizeToShow);
            }
        }

        public override SizeMode Mode
        {
            get { return SizeMode.FitToWindowLargeOnly; }
        }
    }

    /// <summary>
    /// Stretch to specified control.
    /// </summary>
    internal sealed class StretchToWindow : SizeModeBehavior
    {
        private static StretchToWindow instance;

        public static StretchToWindow Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StretchToWindow();
                }

                return instance;
            }
        }

        private StretchToWindow()
        {}

        public static Rectangle CalcFitRectangle( Size clientSize, Size sizeToShow)
        {
            return new Rectangle( 0, 0, clientSize.Width, clientSize.Height);
        }

        public override Rectangle Apply( ScrollableControl control, Size sizeToShow)
        {
            control.AutoScroll = false;

            control.Invalidate();
            return CalcFitRectangle( control.ClientSize, sizeToShow);
        }

        public override SizeMode Mode
        {
            get { return SizeMode.StretchToWindow; }
        }
    }
}
