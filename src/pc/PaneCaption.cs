namespace PViewer
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;
    using System.Windows.Forms;

    public class PaneCaption : UserControl
    {
        // Methods
        public PaneCaption()
        {
            _active = false;
            _antiAlias = true;
            _allowActive = true;
            _colorActiveText = Color.Black;
            _colorInactiveText = Color.White;
            _colorActiveLow = Color.FromArgb(0xff, 0xa5, 0x4e);
            _colorActiveHigh = Color.FromArgb(0xff, 0xe1, 0x9b);
            _colorInactiveLow = Color.FromArgb(3, 0x37, 0x91);
            _colorInactiveHigh = Color.FromArgb(90, 0x87, 0xd7);
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | (ControlStyles.ResizeRedraw | ControlStyles.UserPaint)), true);
            Height = 20;
            _format = new StringFormat();
            _format.FormatFlags = StringFormatFlags.NoWrap;
            _format.LineAlignment = StringAlignment.Center;
            _format.Trimming = StringTrimming.EllipsisCharacter;
            Font = new Font("arial", 9f, FontStyle.Bold);
            ActiveTextColor = _colorActiveText;
            InactiveTextColor = _colorInactiveText;
            CreateGradientBrushes();
        }

        private void CreateGradientBrushes()
        {
            if ((Width > 0) && (Height > 0))
            {
                if (_brushActive != null)
                {
                    _brushActive.Dispose();
                }
                _brushActive = new LinearGradientBrush(DisplayRectangle, _colorActiveHigh, _colorActiveLow, LinearGradientMode.Vertical);
                if (_brushInactive != null)
                {
                    _brushInactive.Dispose();
                }
                _brushInactive = new LinearGradientBrush(DisplayRectangle, _colorInactiveHigh, _colorInactiveLow, LinearGradientMode.Vertical);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void DrawCaption(Graphics g)
        {
            RectangleF ef2;
            g.FillRectangle(BackBrush, DisplayRectangle);
            if (_antiAlias)
            {
                g.TextRenderingHint = TextRenderingHint.AntiAlias;
            }
            ef2 = new RectangleF(4f, 0f, (float) (DisplayRectangle.Width - 4), (float) DisplayRectangle.Height);
            RectangleF ef1 = ef2;
            g.DrawString(Text, Font, TextBrush, ef1, _format);
        }

        [DebuggerStepThrough]
        private void InitializeComponent()
        {
            Size size1;
            Name = "PaneCaption";
            size1 = new Size(150, 30);
            Size = size1;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_allowActive)
            {
                Focus();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            DrawCaption(e.Graphics);
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CreateGradientBrushes();
        }


        // Properties
        [Description("The active state of the caption, draws the caption with different gradient colors."), Category("Appearance"), DefaultValue(false)]
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
                Invalidate();
            }
        }

        [Description("High color of the active gradient."), DefaultValue(typeof(Color), "255, 225, 155"), Category("Appearance")]
        public Color ActiveGradientHighColor
        {
            get
            {
                return _colorActiveHigh;
            }
            set
            {
                if (value.Equals(Color.Empty))
                {
                    value = Color.FromArgb(0xff, 0xe1, 0x9b);
                }
                _colorActiveHigh = value;
                CreateGradientBrushes();
                Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "255, 165, 78"), Description("Low color of the active gradient.")]
        public Color ActiveGradientLowColor
        {
            get
            {
                return _colorActiveLow;
            }
            set
            {
                if (value.Equals(Color.Empty))
                {
                    value = Color.FromArgb(0xff, 0xa5, 0x4e);
                }
                _colorActiveLow = value;
                CreateGradientBrushes();
                Invalidate();
            }
        }

        [Description("Color of the text when active."), Category("Appearance"), DefaultValue(typeof(Color), "Black")]
        public Color ActiveTextColor
        {
            get
            {
                return _colorActiveText;
            }
            set
            {
                if (value.Equals(Color.Empty))
                {
                    value = Color.Black;
                }
                _colorActiveText = value;
                _brushActiveText = new SolidBrush(_colorActiveText);
                Invalidate();
            }
        }

        [DefaultValue(true), Category("Appearance"), Description("True always uses the inactive state colors, false maintains an active and inactive state.")]
        public bool AllowActive
        {
            get
            {
                return _allowActive;
            }
            set
            {
                _allowActive = value;
                Invalidate();
            }
        }

        [Description("If should draw the text as antialiased."), Category("Appearance"), DefaultValue(true)]
        public bool AntiAlias
        {
            get
            {
                return _antiAlias;
            }
            set
            {
                _antiAlias = value;
                Invalidate();
            }
        }

        private LinearGradientBrush BackBrush
        {
            get
            {
                return (LinearGradientBrush) ((_active && _allowActive) ? _brushActive : _brushInactive);
            }
        }

        [DefaultValue(typeof(Color), "90, 135, 215"), Description("High color of the inactive gradient."), Category("Appearance")]
        public Color InactiveGradientHighColor
        {
            get
            {
                return _colorInactiveHigh;
            }
            set
            {
                if (value.Equals(Color.Empty))
                {
                    value = Color.FromArgb(90, 0x87, 0xd7);
                }
                _colorInactiveHigh = value;
                CreateGradientBrushes();
                Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "3, 55, 145"), Description("Low color of the inactive gradient."), Category("Appearance")]
        public Color InactiveGradientLowColor
        {
            get
            {
                return _colorInactiveLow;
            }
            set
            {
                if (value.Equals(Color.Empty))
                {
                    value = Color.FromArgb(3, 0x37, 0x91);
                }
                _colorInactiveLow = value;
                CreateGradientBrushes();
                Invalidate();
            }
        }

        [Category("Appearance"), DefaultValue(typeof(Color), "White"), Description("Color of the text when inactive.")]
        public Color InactiveTextColor
        {
            get
            {
                return _colorInactiveText;
            }
            set
            {
                if (value.Equals(Color.Empty))
                {
                    value = Color.White;
                }
                _colorInactiveText = value;
                _brushInactiveText = new SolidBrush(_colorInactiveText);
                Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Description("Text that is displayed in the label."), Browsable(true), Category("Appearance")]
        public new string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        private SolidBrush TextBrush
        {
            get
            {
                return (SolidBrush) ((_active && _allowActive)? _brushActiveText : _brushInactiveText);
            }
        }


        // Fields
        private bool _active;
        private bool _allowActive;
        private bool _antiAlias;
        private LinearGradientBrush _brushActive;
        private SolidBrush _brushActiveText;
        private LinearGradientBrush _brushInactive;
        private SolidBrush _brushInactiveText;
        private Color _colorActiveHigh;
        private Color _colorActiveLow;
        private Color _colorActiveText;
        private Color _colorInactiveHigh;
        private Color _colorInactiveLow;
        private Color _colorInactiveText;
        private StringFormat _format;

        // Nested Types
        private class Consts
        {
            // Methods
            public Consts()
            {
            }


            // Fields
            public const string DefaultFontName = "arial";
            public const int DefaultFontSize = 9;
            public const int DefaultHeight = 20;
            public const int PosOffset = 4;
        }
    }
}

