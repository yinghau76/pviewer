using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace PViewer
{
    /// <summary>
    /// This class contains various information for a single image.
    /// </summary>
    public class ImageInfo
    {
        private Image _image;
        private string _imagePath;
        private long _bytes;
        private DateTime _lastWriteTime;

        public int Width = 0;
        public int Height = 0;
        public PixelFormat PixelFormat = PixelFormat.Undefined;

        public ImageInfo(string bitmapPath)
        {
            _imagePath = bitmapPath;
            _image = null;

            System.IO.FileInfo fi = new System.IO.FileInfo(_imagePath);
            _bytes = fi.Length;
            _lastWriteTime = fi.LastWriteTime;
        }

        public string ImagePath
        {
            get
            {
                return _imagePath;
            }

            set
            {
                if (value == String.Empty)
                {
                    value = null;
                }

                if (_imagePath != value)
                {
                    _imagePath = value;
                    _image = null;
                }
            }
        }

        public Image Image
        {
            get
            {
                return _image;
            }

            set
            {
                Debug.Assert(_imagePath != null);
                _image = value;

                if (_image != null)
                {
                    // Cache these information so that we can get it even when bitmap data is freed.
                    Width = _image.Width;
                    Height = _image.Height;
                    PixelFormat = _image.PixelFormat;
                }
            }
        }

        public long Size
        {
            get 
            {
                return _bytes;
            }
        }

        public string SizeString
        {
            get
            {
                if (Size < 1024) 
                {
                    return Size.ToString();
                }
                else
                {
                    return String.Format( "{0} KB", Size/1024);
                }
            }
        }

        public DateTime LastWriteTime
        {
            get
            {
                return _lastWriteTime;
            }
        }

        public double CompressionRatio
        {
            get
            {
                double fRatio = (double) _bytes / (Width * Height * Image.GetPixelFormatSize(PixelFormat) / 8);
                return fRatio;
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)] public int wFunc;
            public string pFrom;
            public string pTo;
            public int fFlags;
            [MarshalAs(UnmanagedType.Bool)] public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }

        private const int FO_DELETE = 0x03;

		private const int FOF_MULTIDESTFILES        = 0x0001;
		private const int FOF_CONFIRMMOUSE          = 0x0002;
		private const int FOF_SILENT                = 0x0004;  // don't create progress/report
		private const int FOF_RENAMEONCOLLISION     = 0x0008;
		private const int FOF_NOCONFIRMATION        = 0x0010;  // Don't prompt the user.
		private const int FOF_WANTMAPPINGHANDLE     = 0x0020;  // Fill in SHFILEOPSTRUCT.hNameMappings
		private const int FOF_ALLOWUNDO             = 0x0040;
		private const int FOF_FILESONLY             = 0x0080;  // on *.*, do only files
		private const int FOF_SIMPLEPROGRESS        = 0x0100;  // means don't show names of files
		private const int FOF_NOCONFIRMMKDIR        = 0x0200;  // don't confirm making any needed dirs
		private const int FOF_NOERRORUI             = 0x0400;  // don't put up error UI
        private const int FOF_NOCOPYSECURITYATTRIBS = 0x0800;  // don't copy NT file Security Attributes
		private const int FOF_NORECURSION           = 0x1000;  // don't recurse into directories.
        private const int FOF_NO_CONNECTED_ELEMENTS = 0x2000;  // don't operate on connected elements.
		private const int FOF_WANTNUKEWARNING       = 0x4000;  // during delete operation, warn if nuking instead of recycling (partially overrides FOF_NOCONFIRMATION)
		private const int FOF_NORECURSEREPARSE      = 0x8000;  // treat reparse points as objects, not containers

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);

        public bool Trash()
        {
            SHFILEOPSTRUCT fileOp = new SHFILEOPSTRUCT();
            fileOp.wFunc = FO_DELETE;
            fileOp.pFrom = ImagePath + '\0' + '\0';
            fileOp.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION | FOF_NOERRORUI;
            int ret = SHFileOperation(ref fileOp);

            return (ret == 0 && !fileOp.fAnyOperationsAborted);
        }
    }

    /// <summary>
    /// Represent a collection of images.
    /// </summary>
    public class ImageCollection
    {
        // Fields

        protected int _nUpdating = 0;
        protected ArrayList _images = new ArrayList();

        // Events

        public class ImageAddedEventArgs : EventArgs
        {
            public ImageInfo _imageAdded;

            public ImageAddedEventArgs(ImageInfo imageAdded)
            {
                _imageAdded = imageAdded;
            }
        }

        public event EventHandler ImageAddedEvent;

        public class ImageRemovedEventArgs : EventArgs
        {
            public ImageInfo _imageRemoved;
            public int _index;

            public ImageRemovedEventArgs(ImageInfo imageRemoved, int index)
            {
                _imageRemoved = imageRemoved;
                _index = index;
            }
        }

        public event EventHandler ImageRemovedEvent;

        protected class PathComparer : IComparer
        {
            public int Compare( object x, object y)
            {
                ImageInfo image1 = (ImageInfo) x;
                ImageInfo image2 = (ImageInfo) y;

                return image1.ImagePath.CompareTo(image2.ImagePath);
            }
        }

        public void BeginUpdate()
        {
            ++_nUpdating;
        }

        public void EndUpdate()
        {
            --_nUpdating;
        }

        public ImageInfo this [int index]
        {
            get
            {
                return (ImageInfo) _images[index];
            }

            set
            {
                _images[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return _images.Count;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _images.GetEnumerator();
        }

        public void Clear()
        {
            _images.Clear();
        }

        public void Add(string bitmapPath)
        {
            ImageInfo image = new ImageInfo(bitmapPath);
            _images.Add(image);

            if (_nUpdating <= 0 && ImageAddedEvent != null)
            {
                ImageAddedEvent( this, new ImageAddedEventArgs(image));
            }
        }

        public void RemoveAt(int index)
        {
            ImageInfo image = this[index];
            _images.RemoveAt(index);

            if (_nUpdating <= 0 && ImageRemovedEvent != null)
            {
                ImageRemovedEvent( this, new ImageRemovedEventArgs( image, index));
            }
        }

        public int IndexOf(string bitmapPath)
        {
            for ( int i = 0; i < _images.Count; ++i)
            {
                if (this[i].ImagePath == bitmapPath)
                {
                    return i;
                }
            }

            return -1;
        }

        public void SortByPath()
        {
            _images.Sort(new PathComparer());
        }
    }

    /// <summary>
    /// Represent a collection of images in the same local directory.
    /// </summary>
    public class DirectoryImageCollection : ImageCollection
    {
		static readonly string[] _imageTypes = 
		{ 
			"*.jpg", "*.jpeg", "*.gif", "*.bmp", "*.tif", "*.tiff", "*.png" 
		};

        private string _dir;

        public string Directory
        {
            get
            {
                return _dir;
            }

            set
            {
                DirectoryInfo di = new DirectoryInfo(value);
                Collect(di);
            }
        }

        public event EventHandler CollectionEvent;

        /// <summary>
        /// Collect images by specified filename.
        /// </summary>
        /// <param name="fileName"></param>
        public void Collect(string fileName)
        {
            FileInfo fi = new FileInfo(fileName);

            // An optimization to prevent collecting the same directory again.
            if (fi.Directory.FullName == Directory && IndexOf(fileName) >= 0)
            {
                return ;
            }

            Collect(fi.Directory);
        }

        /// <summary>
        /// Collect images in specified directory.
        /// </summary>
        /// <param name="di"></param>
        public void Collect(DirectoryInfo di)
        {
            _dir = di.FullName;

            BeginUpdate();

            try
            {
                Clear();

                // Find all type of image files in the same directory.
                foreach (string imageType in _imageTypes)
                {
                    FileInfo[] files = di.GetFiles(imageType);
                    foreach (FileInfo fi in files)
                    {
                        Add(fi.FullName);
                    }
                }

                // Sort according to filename.
                SortByPath();
            }
            catch (SystemException ex)
            {
                // Throw Exception when accessing directory: C:\System Volume Information	 // do nothing
                Debug.WriteLine("Exception: " + ex);
            }
            finally
            {
                EndUpdate();
            }

            if (CollectionEvent != null)
            {
                CollectionEvent( this, EventArgs.Empty);
            }
        }

        public DirectoryImageCollection FindNextCollection(int direction)
        {
            // Get all sibling directory.
            DirectoryInfo dirCur = new DirectoryInfo(Directory);
            DirectoryInfo dirParent = dirCur.Parent;

            if (dirParent == null)
            {
                return null; // it is root directory already.
            }

            DirectoryInfo[] dirs = dirParent.GetDirectories();

            int startIndex = 0;
            for ( int i = 0; i < dirs.Length; i++)
            {
                if (dirs[i].FullName == Directory)
                {
                    startIndex = i;
                    break;
                }
            }

            for ( int i = NextIndex( startIndex + direction, dirs.Length); i != startIndex; )
            {
                DirectoryImageCollection collection = new DirectoryImageCollection();
                collection.Collect(dirs[i]);
                if (collection.Count > 0) // if this folder contains any image files.
                {
                    return collection;
                }

                i = NextIndex( i + direction, dirs.Length);
            }

            return null;
        }

        private int NextIndex( int i, int len)
        {
            Debug.Assert(len > 0);

            if (i < 0)
            {
                return len - 1;
            }
            else if (i >= len)
            {
                return 0;
            }

            return i;
        }
    }

    /// <summary>
    /// To manage the navigation of images.
    /// </summary>
    public class ImageNavigator
    {
        private ImageCollection _collection;

        public const int None = int.MinValue;
        private int _current = None;

        public event EventHandler NavigationEvent;

        public ImageCollection Collection
        {
            get
            {
                return _collection;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Collection cannot be null");
                }

                _collection = value;
                _current = None;
                UpdateCurrent();
            }
        }

        public ImageInfo CurrentImage
        {
            get
            {
                if (_current < 0)
                {
                    return null;
                }

                return _collection[_current];
            }
        }

        public int Current
        {
            get
            {
                return _current;
            }

            set
            {
                Goto(value);
            }
        }

        public bool AtFirst()
        {
            if (_collection.Count <= 0)
            {
                return false;
            }

            return _current == 0;
        }

        public bool AtLast()
        {
            if (_collection.Count <= 0)
            {
                return false;
            }

            return _current == _collection.Count - 1;
        }

        public void GotoFirst()
        {
            if (_collection.Count <= 0)
            {
                return;
            }

            Current = 0;
        }

        public void GotoLast()
        {
            if (_collection.Count <= 0)
            {
                return;
            }

            Current = _collection.Count - 1;
        }

        public void GotoPrevious()
        {
            if (AtFirst())
            {
                GotoLast();
            }
            else if (Current >= 0)
            {
                Current = Current - 1;
            }
            else
            {
                throw new InvalidOperationException("Current is not valid. Unable to navigate previous item.");
            }
        }

        public void GotoNext()
        {
            if (AtLast())
            {
                GotoFirst();
            }
            else if (Current >= 0)
            {
                Current = Current + 1;
            }
            else
            {
                throw new InvalidOperationException("Current is not valid. Unable to navigate next item.");
            }
        }

        private void Goto(int current)
        {
            if (_current != current)
            {
                if (CurrentImage != null) 
                {
                    CurrentImage.Image = null;
                }

                if ((current < 0 && current != None) || current >= _collection.Count)
                {
                    throw new ArgumentOutOfRangeException( "Index out of range", "Current");
                }

                _current = current;
                UpdateCurrent();
            }
        }

        public void UpdateCurrent()
        {
            if (NavigationEvent != null)
            {
                NavigationEvent( this, EventArgs.Empty);
            }
        }

        public void Goto(string fileName)
        {
            int current = _collection.IndexOf(fileName);
            if (current >= 0)
            {
                Goto(current);
            }
            else
            {
                throw new ArgumentException(String.Format("{0} is not found", fileName));
            }
        }
    }
}
