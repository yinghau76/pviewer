using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace PViewer
{
	/// <summary>
	/// Summary description for ImageOp.
	/// </summary>
	public interface ImageOp
	{
         Image Execute(Image image);
	}

    public class BaseImageOp : ImageOp
    {
        #region ImageOp Members

        protected ImageOp _innerOp;

        public BaseImageOp()
        {
        }

        public BaseImageOp(ImageOp innerOp)
        {
            _innerOp = innerOp;
        }

        public virtual Image Execute(Image image)
        {
            return _innerOp == null ? image : _innerOp.Execute(image);
        }

        #endregion
    }

    public class AddImageInfo : BaseImageOp
    {
        #region ImageOp Members

        private Font _drawFont = new Font( "Verdana", 10);
        private	SolidBrush _drawBrush = new SolidBrush(Color.White);

        public AddImageInfo(ImageOp innerOp) : base(innerOp)
        {
        }

        public override Image Execute(Image image)
        {
            if ((image.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
            {
                return image; // do nothing.
            }

            using (Graphics g = Graphics.FromImage(_innerOp.Execute(image)))
			{
				string info = String.Format( "{0}x{1}", image.Width, image.Height);
				g.DrawString( info, _drawFont, _drawBrush, 10, 10);
			}

            return image;
        }

        #endregion
    }

    public class InvertImage : BaseImageOp
    {
        public InvertImage(ImageOp innerOp) : base(innerOp)
        {
        }

        public override Image Execute(Image image)
        {
            Image result = _innerOp.Execute(image);
            if (result is Bitmap)
            {
                CSharpFilters.BitmapFilter.Invert(result as Bitmap);
            }

            return result;
        }
    }
}
