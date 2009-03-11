using System;
using NUnit.Framework;
using PViewer.Imaging;
using System.IO;

namespace PViewer.Test
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
    [TestFixture]
	public class TestImageNavigator
	{
        ImageNavigator _nav;

        [SetUp]
        public void SetUp()
        {
            _nav = new ImageNavigator();
        }

        [Test]
        public void TestEmptyNavigation()
        {
            Assert.IsNull(_nav.CurrentImage);
            Assert.AreEqual( _nav.Current, ImageNavigator.None);
            Assert.IsNotNull(_nav.Collection);
            Assert.IsFalse(_nav.AtFirst());
            Assert.IsFalse(_nav.AtLast());

            // These goto operations should not introduce any exception.
            _nav.GotoFirst();
            _nav.GotoLast();
            _nav.GotoNext();
            _nav.GotoPrevious();
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestGotoNonExistingFile()
        {
            _nav.Goto("non-existing-file");
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void TestAddNonExistingFile()
        {
            _nav.Collection.Add("non-existing-file");
        }
	}
}
