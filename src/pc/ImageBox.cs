using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Forms;
using System.Resources;
using PViewer.Imaging;

namespace PViewer
{
    /// <summary>
    /// A user control to display image file.
    ///
    /// Implementation reference: http://www.codeproject.com/cs/miscctrl/DividerPanel.asp
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ImageBox))]
    public class ImageBox : System.Windows.Forms.UserControl
    {
        private Brush _brushDimCrop = new SolidBrush(Color.FromArgb(100, 128, 128, 128));
                
        private ImageDrawer _imageDrawer = new ImageDrawer();
        private Rectangle _previewRect;
        
        private Image _working;
        private bool _isOriginal;

        private bool _useTransition = false;
        public bool UseTransition
        {
            get { return _useTransition; }
            set 
            {
                if (!SizeModeBehavior.SupportsTransition(_sizeModeBehavior.Mode)) 
                {
                    throw new ArgumentException("Transition cannot be used with ShowNativeSize mode."); 
                }

                _useTransition = value; 
            }
        }

        ResourceManager resman = new ResourceManager(typeof(ImageBox));

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public ImageBox()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

#region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // ImageBox
            // 
            this.Name = "ImageBox";
            this.Resize += new System.EventHandler(this.OnResize);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
            this.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.MouseHover += new System.EventHandler(this.OnMouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
            this.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);

        }
#endregion

        /// <summary>
        /// Represent a selection area in an image.
        /// </summary>
        public class ImageSelection
        {
            private Rectangle _rect;

            public void Clear()
            {
                _rect = Rectangle.Empty;
            }

            public void SetRect(int l, int t, int r, int b)
            {
                _rect = Rectangle.FromLTRB(l, t, r, b);
            }

            public Rectangle Rect
            {
                get { return _rect; }
                set { _rect = value; }
            }

            public Rectangle NormalizedRect
            {
                get
                {
                    Rectangle rect = _rect;
                    if (rect.Width < 0)
                    {
                        rect.X = _rect.X + rect.Width;
                        rect.Width = -rect.Width;
                    }
                    if (_rect.Height < 0)
                    {
                        rect.Y = _rect.Y + rect.Height;
                        rect.Height = -rect.Height;
                    }
                    return rect;
                }
            }

            public bool IsEmpty
            {
                get
                {
                    return (_rect.Width == 0 || _rect.Height == 0);
                }
            }

            public Image CopyFromImage(Bitmap bmp)
            {
                if (IsEmpty)
                {
                    return (Image) bmp.Clone();
                }
                else
                {
                    Rectangle rect = NormalizedRect;
                    rect.Intersect(new Rectangle(0, 0, bmp.Size.Width, bmp.Size.Height));
                    return bmp.Clone(rect, PixelFormat.Format32bppArgb);
                }
            }
        }

        private ImageSelection _selection = new ImageSelection();
        
        public Image GetSelectedImage()
        {
            if (_imageInfo != null)
            {
                Bitmap bmp = (Bitmap) _imageInfo.Image;
                return _selection.CopyFromImage(bmp);
            }
            return null;
        }

#region Load/Unload
        public event EventHandler ImageLoaded;
        protected void OnImageLoaded()
        {
            if (ImageLoaded != null)
            {
                ImageLoaded(this, null);
            }
        }
        
        public event EventHandler ImageUnloaded;
        protected void OnImageUnloaded()
        {
            if (ImageUnloaded != null)
            {
                ImageUnloaded(this, null);
            }
        }

        private ImageInfo _imageInfo;
        public ImageInfo ImageInfo
        {
            get { return _imageInfo; }
            set
            {
                if (value != null)
                {
                    Load(value);
                }
                else
                {
                    Unload();
                }
            }
        }

        /// <summary>
        /// Unload current image.
        /// </summary>
        public void Unload()
        {
            if (_working != null)
            {
                if (!_isOriginal)
                    _working.Dispose();
                _working = null;
                Invalidate();
            }

            if (_imageInfo != null)
            {
                _imageInfo = null;
                OnImageUnloaded();
            }
        }

        /// <summary>
        /// Load specified image.
        /// </summary>
        private new void Load(ImageInfo imageInfo)
        {
            ImageInfo oldImageInfo = _imageInfo;
            Unload();

            if (_useTransition && oldImageInfo != null)
            {
                Image imageA = oldImageInfo.Detach();
                StartTransition(TimeSpan.FromSeconds(2), imageA, imageInfo.Image);
            }
            else
            {
                if (oldImageInfo != null)
                {
                    oldImageInfo.Unload();
                }

                _working = imageInfo.Image;
                _isOriginal = true;
                ApplyFitWindow();
            }

            _imageInfo = imageInfo;
            _selection.Clear();

			OnImageLoaded();
        }

#endregion

#region Sizing
        private Rectangle PreviewToDrawing(Rectangle rect)
        {
            rect.Offset(AutoScrollPosition.X, AutoScrollPosition.Y);
            return rect;
        }

        private Rectangle CalcPreviewRect(Size sizeToShow, double zoom)
        {
            Rectangle rect;

            if (zoom != 1)
            {
                // Calculate original size
                rect = _sizeModeBehavior.Apply(this, sizeToShow);

                // Apply the zoom
                sizeToShow.Width = (int) (rect.Width * _zoom);
                sizeToShow.Height = (int) (rect.Height * _zoom);

                // Show it in native size mode.
                rect = ShowNativeSize.Instance.Apply(this, sizeToShow);
            }
            else
            {
                rect = _sizeModeBehavior.Apply(this, sizeToShow);
            }

            return rect;
        }

        private void ApplyFitWindow()
        {
            if (_working != null)
            {
                _previewRect = CalcPreviewRect(_working.Size, _zoom);
            }
        }

        private void OnResize(object sender, System.EventArgs e)
        {
            ApplyFitWindow();
        }

        private SizeModeBehavior _sizeModeBehavior = FitToWindow.Instance;
        [Bindable(true), 
         Category("Image"), 
         DefaultValue(SizeMode.FitToWindow),
         Description("Specifies how image is resized for displaying.")]
        public SizeMode SizeMode
        {
            get { return _sizeModeBehavior.Mode; }
            set
            {
                if (!SizeModeBehavior.SupportsTransition(value) && _useTransition)
                {
                    throw new ArgumentException("Transition cannot be used with ShowNativeSize mode.");
                }

                _sizeModeBehavior = SizeModeBehavior.FromSizeMode(value);
                ApplyFitWindow();
            }
        }
#endregion

#region Drawing
        private InterpolationMode _interpolationMode = InterpolationMode.High;
        [Bindable(true), 
         Category("Image"), 
         DefaultValue(InterpolationMode.HighQualityBicubic),
         Description("Specifies how image data is interpolated between endpoints.")]
        public InterpolationMode InterpolationMode
        {
            get
            {
                return _interpolationMode;
            }
            set
            {
                _interpolationMode = value;
                Invalidate();
            }
        }

        public Rectangle DrawingRectangle
        {
            get
            {
                return PreviewToDrawing(_previewRect);
            }
        }

        protected void Draw(Graphics g, Image bmp)
        {
            // Draw image in specified graphics.
            if (bmp != null)
            {
                // drawing the image
                Rectangle rc = DrawingRectangle;
                g.InterpolationMode = _interpolationMode;
                g.CompositingQuality = CompositingQuality.HighQuality;
                _imageDrawer.Draw(g, bmp, rc);

                // draw selection rectangle
                DrawSelectRect(g);
            }
        }

        private Rectangle GetPointRect(int x, int y)
        {
            return new Rectangle(x - 3, y - 3, 6, 6);
        }

        public void DrawSelectRect(Graphics g)
        {
            if (_selection.IsEmpty)
            {
                return;
            }

            Rectangle rect = _selection.NormalizedRect;
            rect = Rectangle.FromLTRB(
                Math.Min(rect.Left, rect.Right), Math.Min(rect.Top, rect.Bottom),
                Math.Max(rect.Left, rect.Right), Math.Max(rect.Top, rect.Bottom));

            Rectangle selectRect = RectangleMapping.SrcToDst(_working.Size, _previewRect, rect);
            selectRect.Offset(AutoScrollPosition.X, AutoScrollPosition.Y);

            // use alpha-blending brush to dim non-selection area.
            using (Region region = new Region(DrawingRectangle))
            {
                region.Exclude(selectRect);
                g.FillRegion(_brushDimCrop, region);
            }
            
            ControlPaint.DrawFocusRectangle(g, selectRect, Color.White, Color.Black);
            g.FillRectangle(SystemBrushes.ActiveCaptionText, GetPointRect(selectRect.Left, selectRect.Top));
            g.FillRectangle(SystemBrushes.ActiveCaptionText, GetPointRect(selectRect.Right, selectRect.Top));
            g.FillRectangle(SystemBrushes.ActiveCaptionText, GetPointRect(selectRect.Left, selectRect.Bottom));
            g.FillRectangle(SystemBrushes.ActiveCaptionText, GetPointRect(selectRect.Right, selectRect.Bottom));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw(e.Graphics, _working);
        }
#endregion

#region Rotation and Zoom
        private double _zoom = 1;
        public double Zoom
        {
            get { return _zoom; }
            set
            {
                if (value < 0 || value > 100) 
                {
                    throw new ArgumentOutOfRangeException();
                }
                
                _zoom = value;
                ApplyFitWindow();
            }
        }

        public double ZoomRatio
        {
            get
            {
                Rectangle dest = _previewRect;
                double ratio = (double) dest.Width * dest.Height / (_working.Width * _working.Height);
                return Math.Sqrt(ratio);
            }
        }

        public void RotateFlip(RotateFlipType rf)
        {
            if (_working == null)
            {
                throw new InvalidOperationException("Image is not available");
            }

            _working.RotateFlip(rf);
            ApplyFitWindow();
        }

#endregion

        class MouseOperation
        {
            public virtual void OnMouseDown(object sender, MouseEventArgs e) {}
            public virtual void OnMouseMove(object sender, MouseEventArgs e) {}
            public virtual void OnMouseUp(object sender, MouseEventArgs e) {}
        }

        class MoveRect : MouseOperation
        {
            private Point _ptDown;
            private Rectangle _rectDown;

            public override void OnMouseDown(object sender, MouseEventArgs e)
            {
                ImageBox box = (ImageBox) sender;

                _ptDown = RectangleMapping.DstToSrc(box._working.Size, box._previewRect, new Point(e.X, e.Y));
                _rectDown = box._selection.Rect;
            }

            public override void OnMouseMove(object sender, MouseEventArgs e)
            {
                ImageBox box = (ImageBox) sender;

                Point pt = RectangleMapping.DstToSrc(box._working.Size, box._previewRect, new Point(e.X, e.Y));
                Rectangle rect = _rectDown;
                rect.Offset(pt.X - _ptDown.X, pt.Y - _ptDown.Y);
                box._selection.Rect = rect;

                box.Invalidate();
            }
        }

        class SelectRect : MouseOperation
        {
            private Point GetDstPoint(ImageBox box, int x, int y)
            {
                Point ptMouse = new Point(x - box.AutoScrollPosition.X, y - box.AutoScrollPosition.Y);
                return RectangleMapping.DstToSrc(box._working.Size, box._previewRect, ptMouse);
            }

            public override void OnMouseDown(object sender, MouseEventArgs e)
            {
                ImageBox box = (ImageBox) sender;

                Point pt = GetDstPoint(box, e.X, e.Y);
                box._selection.SetRect(pt.X, pt.Y, pt.X, pt.Y);
            }

            public override void OnMouseMove(object sender, MouseEventArgs e)
            {
                ImageBox box = (ImageBox) sender;

                Point pt = GetDstPoint(box, e.X, e.Y);
                box._selection.SetRect(box._selection.Rect.Left, box._selection.Rect.Top, pt.X, pt.Y);
                box.Invalidate();
            }

            public override void OnMouseUp(object sender, MouseEventArgs e) 
            {
                ImageBox box = (ImageBox) sender;
                box.Invalidate();
            }
        }

        private MouseOperation _op;

#region Mouse Handling
        protected override void OnMouseWheel(MouseEventArgs args)
        {
            // Do nothing.
        }

        private void OnMouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
			if (_working != null && e.Button == MouseButtons.Left)
			{
                Rectangle previewRect = RectangleMapping.SrcToDst(_working.Size, _previewRect, _selection.Rect);
                if (previewRect.Contains(e.X, e.Y))
                {
                    _op = new MoveRect();
                }
                else
                {
                    _op = new SelectRect();
                }

                _op.OnMouseDown(this, e);
                Capture = true;
			}
        }

        private void OnMouseEnter(object sender, System.EventArgs e)
        {
        }

        private void OnMouseHover(object sender, System.EventArgs e)
        {
        }

        private void OnMouseLeave(object sender, System.EventArgs e)
        {
        }

        private void OnMouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (_op != null)
            {
                _op.OnMouseMove(this, e);
            }
            else if (_working != null)
            {
                Rectangle previewRect = RectangleMapping.SrcToDst(_working.Size, _previewRect, _selection.Rect);
                if (previewRect.Contains(e.X, e.Y))
                {
                    Cursor = Cursors.SizeAll;
                }
                else
                {
                    Cursor = Cursors.Default;
                }
            }
        }

        private void OnMouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.Capture)
            {
                this.Capture = false;
            }   
     
            if (_op != null)
            {
                _op.OnMouseUp(this, e);
                _op = null;
            }
        }
#endregion

#region Transition
        class Transition
        {
            private Image _imageA;
            public Image ImageA
            {
                get { return _imageA; }
            }

            private Image _imageB;
            public Image ImageB
            {
                get { return _imageB; }
            }

            public TimeSpan _duration;
            private Timer _timer;
            
            public delegate void TransitionProgress(Transition transition, float progress);
            private TransitionProgress _progressHandler;

            private DateTime _startTime;

            public Transition(Image imageA, Image imageB, TimeSpan duration, int interval, TransitionProgress handler)
            {
                _imageA = imageA;
                _imageB = imageB;

                _duration = duration;

                // Setup a timer to show transition.
                _timer = new Timer();
                _timer.Interval = interval;
                _timer.Tick += new EventHandler(OnTransitionTimer);

                _progressHandler = handler;
            }

            public event EventHandler OnStart;

            public void Start()
            {
                _startTime = DateTime.Now;
                _timer.Start();

                if (OnStart != null)
                {
                    OnStart(this, EventArgs.Empty);
                }
            }

            public event EventHandler OnStop;

            public void Stop()
            {
                _timer.Stop();
                _imageA.Dispose();

                if (OnStop != null)
                {
                    OnStop(this, EventArgs.Empty);
                }
            }

            public event EventHandler OnFinish;

            private void OnTransitionTimer(object sender, EventArgs e)
            {
                TimeSpan sofar = DateTime.Now - _startTime;
                float progress = (float)(sofar.TotalMilliseconds / _duration.TotalMilliseconds);
                progress = Math.Min(progress, 1);

                _progressHandler(this, progress);
            
                if (progress >= 1)
                {
                    Stop();

                    if (OnFinish != null)
                    {
                        OnFinish(this, EventArgs.Empty);
                    }
                }
            }
        }

        Transition _transition;

        public void StartTransition(TimeSpan duration, Image imageA, Image imageB)
        {
            if (_transition != null)
            {
                _transition.Stop();
            }

            _transition = new Transition(imageA, imageB, duration, 50, 
                new Transition.TransitionProgress(OnTransitionProgress));
            _transition.OnStart += new EventHandler(OnTransitionStart);
            _transition.OnFinish += new EventHandler(OnTransitionFinish);
            _transition.Start();
        }

        private void OnTransitionStart(object sender, EventArgs e)
        {
            OnTransitionProgress(_transition, 0);
        }

        private void OnTransitionProgress(Transition transition, float progress)
        {
            if (ClientSize.Width == 0 || ClientSize.Height == 0)
            {
                return; // do nothing if the window is minimized
            }

            Rectangle rectA = CalcPreviewRect(transition.ImageA.Size, 1);
            Rectangle rectB = CalcPreviewRect(transition.ImageB.Size, 1);
            Size sizeWorking = new Size(Math.Max(rectA.Width, rectB.Width), Math.Max(rectA.Height, rectB.Height));

            if (_working == null || _working.Size != sizeWorking)
            {
                _working = new Bitmap(sizeWorking.Width, sizeWorking.Height, PixelFormat.Format24bppRgb);
                _isOriginal = false;
            }
            
            using (Graphics g = Graphics.FromImage(_working))
            {
                g.FillRectangle(new SolidBrush(Color.DarkGray), ClientRectangle);
            }
            
            rectA.X = (sizeWorking.Width - rectA.Width) / 2;
            rectA.Y = (sizeWorking.Height - rectA.Height) / 2;
            rectB.X = (sizeWorking.Width - rectB.Width) / 2;
            rectB.Y = (sizeWorking.Height - rectB.Height) / 2;
            ImageHelper.BlendImages(_working, transition.ImageA, rectA, transition.ImageB, rectB, progress);
            
            ApplyFitWindow();
            Invalidate();
            Update();
        }

        private void OnTransitionFinish(object sender, EventArgs e)
        {
            _working.Dispose();

            // Use original image as working image.
            _working = _transition.ImageB;
            _isOriginal = true;
            ApplyFitWindow();

            _transition = null;
        }
#endregion
    }
}
