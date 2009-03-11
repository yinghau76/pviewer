/*======================================================================/

Copyright (C) 2004 Daniel Fisher(lennybacon).  All rights reserved.

THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
PARTICULAR PURPOSE.

For more information email: info@lennybacon.com
=======================================================================*/

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web;

namespace StaticDust
{
	/// <summary>
	/// Summary description for image2html.
	/// </summary>
	public class AsciiArt
	{
		public static string ConvertImage(Stream stream)
		{
			StringBuilder _asciiart = new StringBuilder();

			#region load image
			Image _img = Image.FromStream(stream);
			Bitmap _image = new Bitmap(_img, new Size(_img.Width, _img.Height));
			_img.Dispose();


			Rectangle bounds = new Rectangle(0, 0, _image.Width, _image.Height);
			#endregion

			#region greyscale image
			ColorMatrix _matrix = new ColorMatrix();

			_matrix[0,0] = 1/3f;
			_matrix[0,1] = 1/3f;
			_matrix[0,2] = 1/3f;
			_matrix[1,0] = 1/3f;
			_matrix[1,1] = 1/3f;
			_matrix[1,2] = 1/3f;
			_matrix[2,0] = 1/3f;
			_matrix[2,1] = 1/3f;
			_matrix[2,2] = 1/3f;

			ImageAttributes _attributes = new ImageAttributes();
			_attributes.SetColorMatrix(_matrix);


			Graphics gphGrey = Graphics.FromImage(_image);
			gphGrey.DrawImage(_image, bounds, 0, 0, _image.Width, _image.Height,
				GraphicsUnit.Pixel, _attributes);

			gphGrey.Dispose();
			#endregion

			#region ascii image
			for(int h=0; h<_image.Height/10; h++)
			{
				int _startY = (h*10);

				for(int w=0; w<_image.Width/5; w++)
				{
					int _startX = (w*5);

					int _allBrightness = 0;

					for(int y=0; y<10; y++)
					{
						for(int x=0; x<10; x++)
						{
							int _cY = y + _startY;
							int _cX = x + _startX;
							try
							{
								Color _c = _image.GetPixel(_cX, _cY);
								int _b = (int)(_c.GetBrightness() * 10);
								_allBrightness = (_allBrightness + _b);
							}
							catch
							{
								_allBrightness = (_allBrightness + 10);
							}
						}
					}

					int _sb = (_allBrightness/10);
					if(_sb<25)
					{
						_asciiart.Append("#");
					}
					else if(_sb<30)
					{
						_asciiart.Append("@");
					}
					else if(_sb<40)
					 {
						 _asciiart.Append("\xd8");
					 }
					else if (_sb < 45)
					{
						_asciiart.Append("$");
					}
					else if (_sb < 50)
					{
						_asciiart.Append("&");
					}
					else if (_sb < 55)
					{
						_asciiart.Append("\xa4");
					}
					else if (_sb < 60)
					{
						_asciiart.Append("~");
					}
					else if (_sb < 60)
					{
						_asciiart.Append("\xb7");
					}
					else if (_sb < 70)
					{
						_asciiart.Append("\xa8");
					}
					else if (_sb <80)
					{
						_asciiart.Append("\xb4");
					}
					else
					{
						_asciiart.Append(" ");
					}
				}
				_asciiart.Append("\n");

			}
			#endregion
			
			//clean up
			_image.Dispose();


			return _asciiart.ToString();

		}
	}
}