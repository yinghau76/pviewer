// MRU.cs: Contributed by Michael Weinhardt[mikedub@bigpond.com]
// An MRU menu helper -- see the sample for usage
#region Copyright ?2002-2003 The Genghis Group 
/*
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from the
 * use of this software.
 * 
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, subject to the following restrictions:
 * 
 * 1. The origin of this software must not be misrepresented; you must not claim
 * that you wrote the original software. If you use this software in a product,
 * an acknowledgment in the product documentation is IsRequired, as shown here:
 * 
 * Portions Copyright ?2002-2003 The Genghis Group (http://www.genghisgroup.com/).
 * 
 * 2. No substantial portion of the source code of this library may be redistributed
 * without the express written permission of the copyright holders, where
 * "substantial" is defined as enough code to be recognizably from this library. 
*/
#endregion
#region History 
/*
 * 06/10/02 - Initial Version
 */
#endregion
#region TODOs

// TODO: DO IT NOW: Apply C# commenting: http://www.gotdotnet.com/team/csharp/Information/Whitepapers/XMLDocs.doc
// TODO: Review exception usage
// TODO: Resource strings for exceptions???

#endregion
#region QUESTIONs

// QUESTION: Handle file storage too (Isolated Storage)?

#endregion

using System;
using System.Collections.Specialized;    // StringCollection
using System.Windows.Forms;              // MenuItem
using System.Text;                       // StringBuilder
using System.Collections;                // ICollection
using System.ComponentModel;             // Component, various Attributes

namespace Genghis.Windows.Forms
{
    public enum MRUStyle
    {
        InMenu = 0,
        InSubMenu = 1
    }

    public class MRU : Component
    {
        public MRU()
        {
            _mruFileList = new MRUStringCollection(_capacity);
        }

        public int Count
        {
            get
            {
                return _mruFileList.Count;
            }
        }

        public void Add(string mruFileName)
        {
            _mruFileList.Add(mruFileName.ToLower());
        }

        public void Clear()
        {
            _mruFileList.Clear();
        }

        [DefaultValue(40), Category("Appearance")]
        public int TextWidth
        {
            get
            {
                return _textWidth;
            }
            set
            {
                _textWidth = value;
            }
        }

        [DefaultValue(10), Category("Appearance")]
        public int Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                _capacity = value;
                _mruFileList.Capacity = value;
            }
        }

        [DefaultValue(MRUStyle.InMenu), Category("Appearance")]
        public MRUStyle Style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
            }
        }

        [Category("Appearance")]
        public MenuItem Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                _parent = value;
                MenuItem miParent = (MenuItem)_parent.Parent;
                miParent.Popup += new System.EventHandler(MRUMenuItem_Popup);
            }
        }

        public string GetFileName(int menuItemIndex)
        {
            return _mruFileList[GetMRUHelper().GetMRUIndex(_parent, menuItemIndex)];
        }

        public string this[int index]
        {
            get
            {
                return _mruFileList[index];
            }
        }

        public delegate void onMRUClick(object sender, string FileName);
        public event onMRUClick MRUClick;

        private void MRUMenuItem_Popup(object sender, System.EventArgs e)
        {
            GetMRUHelper().Render(_parent, _mruFileList, _textWidth, new System.EventHandler(MRUMenuItem_Click));
        }

        private void MRUMenuItem_Click(object sender, System.EventArgs e)
        {
            MenuItem mi = (MenuItem) sender;
            string fileName = _mruFileList[GetMRUHelper().GetMRUIndex(_parent, mi.Index)];
            Add(fileName);
            MRUClick(this, fileName);
        }

        private IMRUHelper GetMRUHelper()
        {
            if ( _style == MRUStyle.InMenu)
                return new InMenuMRUHelper();
            else
                return new InSubMenuMRUHelper();
        }

        private int _textWidth = 40;
        private int _capacity = 10;
        private MRUStyle _style = MRUStyle.InMenu;
        private MenuItem _parent = null;
        private MRUStringCollection _mruFileList = null;
    }

    public class MRUMenuItem : MenuItem
    {
        public MRUMenuItem(string text, System.EventHandler onClick) : base(text, onClick)
        {}
    }

    internal class InMenuMRUHelper : IMRUHelper
    {
        public int GetMRUIndex(MenuItem parent, int menuItemIndex)
        {
            return menuItemIndex - parent.Index - 1;
        }

        public void Render(MenuItem parent, MRUStringCollection mruFileList, int textWidth, System.EventHandler mruMenuItem_Click)
        {
            // Check parameter values
            if ( parent == null )
                throw new ArgumentNullException("parent cannot be null");
            if ( mruFileList == null )
                throw new ArgumentNullException("mruFileList cannot be null");
            if ( textWidth < 0 )
                throw new ArgumentOutOfRangeException("textWidth cannot be less than zero");
            if ( mruMenuItem_Click == null )
                throw new ArgumentNullException("menuItemOnClick cannot be null");

            // Clear MenuItems
            Menu.MenuItemCollection menuItems = parent.Parent.MenuItems;
            MenuItem miFirst = menuItems[parent.Index + 1];
            while ( miFirst is MRUMenuItem )
            {
                menuItems.Remove(miFirst);
                miFirst = menuItems[parent.Index + 1];
            }
            parent.Enabled = false;
            parent.Visible = true;

            // Fill MRU menu
            if ( mruFileList.Count > 0 )
            {
                int menuPosition = 0;
                foreach( string mruFileName in mruFileList )
                {
                    menuPosition++;
                    string formattedAccessKey = MenuFormatter.FormatAccessKey(menuPosition);
                    string formattedFilePath = MenuFormatter.FormatFilePath(mruFileName, textWidth);
                    MenuItem miNew = new MRUMenuItem(formattedAccessKey + " " + formattedFilePath, new System.EventHandler(mruMenuItem_Click));
                    parent.Parent.MenuItems.Add(parent.Index + menuPosition, miNew);
                }
                parent.Enabled = true;
                parent.Visible = false;
            }
        }
    }

    internal class InSubMenuMRUHelper : IMRUHelper
    {
        public int GetMRUIndex(MenuItem parent, int menuItemIndex)
        {
            return menuItemIndex;
        }

        public void Render(MenuItem parent, MRUStringCollection mruFileList, int textWidth, System.EventHandler mruMenuItem_Click)
        {
            // Check parameter values
            if ( parent == null )
                throw new ArgumentNullException("parent cannot be null");
            if ( mruFileList == null )
                throw new ArgumentNullException("mruFileList cannot be null");
            if ( textWidth < 0 )
                throw new ArgumentOutOfRangeException("textWidth cannot be less than zero");
            if ( mruMenuItem_Click == null )
                throw new ArgumentNullException("menuItemOnClick cannot be null");

            // Clear MenuItems
            parent.MenuItems.Clear();
            parent.Enabled = false;

            if ( mruFileList.Count > 0 )
            {
                // Fill and resequence submenu
                foreach( string mruFileName in mruFileList )
                {
                    string formattedAccessKey = MenuFormatter.FormatAccessKey(parent.MenuItems.Count + 1);
                    string formattedFilePath = MenuFormatter.FormatFilePath(mruFileName, textWidth);
                    MenuItem miNew = new MRUMenuItem(formattedAccessKey + " " + formattedFilePath, new System.EventHandler(mruMenuItem_Click));
                    parent.MenuItems.Add(parent.MenuItems.Count, miNew);
                }
                parent.Enabled = true;
            }
        }
    }

    internal interface IMRUHelper
    {
        int GetMRUIndex(MenuItem parent, int menuItemIndex);
        void Render(MenuItem parent, MRUStringCollection _mruFileList, int textWidth, System.EventHandler mruMenuItem_Click);
    }

    internal class MRUStringCollection : ICollection
    {
        public MRUStringCollection(int capacity)
        {
            _capacity = capacity;
        }

#region ICollection

        public int Count
        {
            get
            {
                return _mruStringCollection.Count;
            }
        }

        public void CopyTo(Array array, int index)
        {
            string[] tmpArray = new string[0];
            _mruStringCollection.CopyTo(tmpArray, index);
            array = tmpArray;
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public MRUStringCollection SyncRoot
        {
            get
            {
                return null;
            }
        }

        int ICollection.Count
        {
            get
            {
                return Count;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            CopyTo(array, index);
        }

        bool ICollection.IsSynchronized
        {
            get
            {
                return IsSynchronized;
            }
        }

        object ICollection.SyncRoot
        {
            get
            {
                return SyncRoot;
            }
        }

#endregion

#region IEnumerable

        public IEnumerator GetEnumerator()
        {
            return new MRUStringCollectionEnumerator(_mruStringCollection);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

#endregion

#region IEnumerator

    public class MRUStringCollectionEnumerator : IEnumerator
        {
            public MRUStringCollectionEnumerator(StringCollection mruStringCollection)
            {
                _mruStringCollection = mruStringCollection;
                Reset();
            }

            public string Current
            {
                get
                {
                    // If the enumerator points to an element, return it
                    if ( (_mruStringIndex < 0) || (_mruStringIndex > _mruStringCollection.Count - 1) )
                        throw new InvalidOperationException();
                    return _mruStringCollection[_mruStringIndex];
                }
            }

            public bool MoveNext()
            {
                // Move enumerator to next element
                return ( ++_mruStringIndex < _mruStringCollection.Count );
            }

            public void Reset()
            {
                // Move enumerator to beginning of the collection, before the first element
                _mruStringIndex = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            bool IEnumerator.MoveNext()
            {
                return MoveNext();
            }

            void IEnumerator.Reset()
            {
                Reset();
            }

            int _mruStringIndex;
            StringCollection _mruStringCollection;
        }

#endregion

        public int Add(string value)
        {
            // Get the index of this value
            int valueIndex = _mruStringCollection.IndexOf(value);

            // Don't do anything if this file is the first item
            if ( valueIndex == 0 )
                return -1;

            // Move file to top if already in the list, unless it's the first item
            if ( valueIndex > 0 )
                _mruStringCollection.RemoveAt(valueIndex);

            // Insert new item at top
            _mruStringCollection.Insert(0, value);

            // Remove superfluous
            if ( _capacity < Count )
                TrimToCapacity();

            return 0;
        }

        public void Clear()
        {
            _mruStringCollection.Clear();
        }

        public string this[int index]
        {
            get
            {
                return _mruStringCollection[index];
            }
            set
            {
                _mruStringCollection[index] = value;
            }
        }

        public int Capacity
        {
            get
            {
                return _capacity;
            }
            set
            {
                if ( value < 0 )
                    throw new ArgumentOutOfRangeException("capacity");
                _capacity = value;
                if ( _capacity < Count )
                    TrimToCapacity();
            }
        }

        private void TrimToCapacity()
        {
            for ( int i = Count - 1; i >= _capacity; i-- )
            {
                _mruStringCollection.RemoveAt(_capacity);
            }
        }

        int _capacity = int.MaxValue;
        StringCollection _mruStringCollection = new StringCollection();
    }

    internal class MenuFormatter
    {
        public static string FormatAccessKey(int listPosition)
        {
            // Build numeric access key
            if ( listPosition <= 9 )
                return string.Format("&{0}", listPosition);
            else if ( listPosition == 10 )
                return "1&0";
            else
                return listPosition.ToString();
        }

        /// <summary>
        /// Produces display friendly short filename.
        /// </summary>
        /// <param name="FileNameWithPath">Filename with path to shrink.</param>
        /// <param name="maxLength">Maximum length to use.</param>
        /// <returns>Short filename.</returns>
        /// <credit>Altered slightly from original post by James Berry from the win_tech_off_topic list</credit>
        public static string FormatFilePath(string fileNameWithPath, int maxLength)
        {
            // We will begin by taking the string and splitting it apart into an array
            // Check if we are within the max length then return the whole string
            if ( fileNameWithPath.Length <= maxLength )
                return fileNameWithPath;

            // Split the string into an array using the \ as a delimiter
            char[] seperator = {'\\'};
            string[] pathBits;
            pathBits = fileNameWithPath.Split(seperator);

            // The first value of the array is taken in case we need to create the string
            StringBuilder sb = new StringBuilder();
            int length = sb.Length;
            int beginLength = pathBits[0].Length + 3;
            bool addHeader = false;
            string pathItem;
            int pathItemLength;

            // Now we loop backwards through the string
            for ( int pathItemIndex = pathBits.Length - 1; pathItemIndex > 0; pathItemIndex-- )
            {
                pathItem = '\\' + pathBits[pathItemIndex];
                pathItemLength = pathItem.Length;

                // Check if adding the current item does not increase the length of the
                // max string
                if ( length + pathItemLength <= maxLength )
                {
                    // In this case we can afford to add the item
                    sb.Insert(0, pathItem);
                    length += pathItemLength;
                }
                else
                    break;

                // Check if there is room to add the header and if so then reserve it by
                // incrementing the length
                if ( (addHeader == false) && (length + beginLength <= maxLength) )
                {
                    addHeader = true;
                    length += beginLength;
                }
            }

            // It is possible that the last value in the array itself was long
            // In such case simply use the substring of the last value
            if ( sb.Length == 0 )
                return pathBits[pathBits.Length - 1].Substring(0, maxLength);

            // Add the header if the bool is true
            if ( addHeader == true )
                sb.Insert(0, pathBits[0] + "\\...");
            return sb.ToString();
        }
    }
}
