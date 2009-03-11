namespace PViewer
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class BasePane : UserControl
    {
        // Events
        public event PaneActiveEventHandler PaneActive;

        // Methods
        public BasePane()
        {
            components = null;
            SetStyle(ControlStyles.DoubleBuffer | (ControlStyles.AllPaintingInWmPaint | (ControlStyles.ResizeRedraw | ControlStyles.UserPaint)), true);
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            caption = new PaneCaption();
            SuspendLayout();
            caption.Dock = DockStyle.Top;
            caption.Font = new Font("Arial", 9f, FontStyle.Bold);
            caption.Location = new Point(1, 1);
            caption.Name = "caption";
            caption.Size = new Size(214, 20);
            caption.TabIndex = 0;
            Controls.Add(caption);
            DockPadding.All = 1;
            Name = "BasePane";
            Size = new Size(216, 248);
            ResumeLayout(false);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            caption.Active = true;
            if (PaneActive != null)
            {
                PaneActive(this, EventArgs.Empty);
            }
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            caption.Active = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc;
            rc = new Rectangle(0, 0, Width - 1, Height - 1);
            rc.Inflate((0 - DockPadding.All) + 1, (0 - DockPadding.All) + 1);
            e.Graphics.DrawRectangle(SystemPens.ControlDark, rc);
            base.OnPaint(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (DesignMode)
            {
                caption.Width = Width;
            }
        }


        // Properties
        public bool Active
        {
            get
            {
                return caption.Active;
            }
        }

        public PaneCaption CaptionControl
        {
            get
            {
                return caption;
            }
        }

        [Description("The pane caption."), Category("Appearance")]
        public string CaptionText
        {
            get
            {
                return caption.Text;
            }
            set
            {
                caption.Text = value;
            }
        }


        // Fields
        private PaneCaption caption;
        private Container components;

        // Nested Types
        public delegate void PaneActiveEventHandler(object sender, EventArgs e);
    }
}

