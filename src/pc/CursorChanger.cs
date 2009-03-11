// CursorChanger.cs: Contributed by Shawn Wildermuth [swildermuth@adoguy.com]
// A class to change the cursor for the current window.

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
 * an acknowledgment in the product documentation is required, as shown here:
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
 * 7/13/2002:
 * -Initial Development
*/
#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Genghis;

namespace Genghis.Windows.Forms
{
    /// <summary>
    /// A class to change the cursor for the current window.
    /// Upon Disposal, the class will return the original
    /// cursor.
    /// </summary>
    /// <example>This example shows how the cursor will be changed during the scope
    /// of the object (before disposal):
    /// <code>
    /// using (ChangeCursor cursorChanger = new CursorChanger(Cursors.WaitCursor))
    /// {
    ///   ...
    /// }
    /// </code>
    /// </example>
    public class CursorChanger : IDisposable
    {
        /// <summary>
        /// Constructs a new CursorChanger object specifying the new Cursor to show.
        /// </summary>
        /// <param name="newCursor">The new cursor to show during the lifetime
        /// of the CursorChanger object</param>
        public CursorChanger(Cursor newCursor)
        {
            // Cache the original Cursor
            _originalCursor = Cursor.Current;

            // Change the cursor
            Cursor.Current = newCursor;
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~CursorChanger()
        {
            Dispose();
        }

        /// <summary>
        /// Dispose method.  Returns the original
        /// </summary>
        public void Dispose()
        {
            Cursor.Current = _originalCursor;
            GC.SuppressFinalize(this);
        }

        Cursor _originalCursor;
    }
}
