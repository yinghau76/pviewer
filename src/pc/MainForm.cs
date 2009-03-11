using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Genghis.Windows.Forms;
using PViewer.Imaging;

namespace PViewer
{
    public class MainForm : System.Windows.Forms.Form
    {
#region Windows Form Designer generated code
        private System.Windows.Forms.MainMenu menuMain;
        private System.Windows.Forms.MenuItem menuItemFile;
        private System.Windows.Forms.MenuItem menuItemOpen;
        private System.Windows.Forms.MenuItem menuItemPrevious;
        private System.Windows.Forms.MenuItem menuItemNext;
        private System.Windows.Forms.MenuItem menuItemExit;
        private System.Windows.Forms.MenuItem menuItemHelp;
        private System.Windows.Forms.MenuItem menuItemAbout;
        private System.Windows.Forms.MenuItem menuItemView;
        private System.Windows.Forms.MenuItem menuItemFitToWindow;
        private System.Windows.Forms.MenuItem menuItemShowNativeSize;
        private System.Windows.Forms.MenuItem menuItemStretchToWindow;
        private PViewer.ImageStatusBar statusBar;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.MenuItem menuItemBicubic;
        private System.Windows.Forms.MenuItem menuItemBilinear;
        private System.Windows.Forms.MenuItem menuItemHigh;
        private System.Windows.Forms.MenuItem menuItemHighQualityBicubic;
        private System.Windows.Forms.MenuItem menuItemHighQualityBilinear;
        private System.Windows.Forms.MenuItem menuItemLow;
        private System.Windows.Forms.MenuItem menuItemNearestNeighbor;
        private System.Windows.Forms.MenuItem menuItemInterpolationMode;
        private System.Windows.Forms.MenuItem menuItemFullScreen;
        private System.Windows.Forms.MenuItem menuItemSep3;
        private System.Windows.Forms.MenuItem menuItemSep;
        private System.Windows.Forms.MenuItem menuItemSep4;
        private System.Windows.Forms.MenuItem menuItemRecentPictures;
        private System.Windows.Forms.MenuItem menuItemSep5;
        private System.Windows.Forms.MenuItem menuItemDelete;
        private System.Windows.Forms.MenuItem menuItemCopy;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemSep6;
        private System.Windows.Forms.MenuItem menuItemFirst;
        private System.Windows.Forms.MenuItem menuItemLast;
        private System.Windows.Forms.MenuItem menuItemSep7;
        private System.Windows.Forms.ToolBar toolBar;
        private System.Windows.Forms.ToolBarButton toolBarButtonOpen;
        private System.Windows.Forms.ImageList imageListToolbar;
        private System.Windows.Forms.ToolBarButton toolBarButtonRefresh;
        private System.Windows.Forms.ToolBarButton toolBarButtonPrevious;
        private System.Windows.Forms.ToolBarButton toolBarButtonNext;
        private System.Windows.Forms.ToolBarButton toolBarButtonZoomIn;
        private System.Windows.Forms.ToolBarButton toolBarButtonZoomOut;
        private System.Windows.Forms.ToolBarButton toolBarButtonOptions;
        private System.Windows.Forms.ToolBarButton toolBarButtonAbout;
        private System.Windows.Forms.MenuItem menuItemEdit;
        private System.Windows.Forms.MenuItem menuItemSep8;
        private System.Windows.Forms.MenuItem menuItemRotate;
        private System.Windows.Forms.MenuItem menuItemFlip;
        private System.Windows.Forms.MenuItem menuItemRotate90R;
        private System.Windows.Forms.MenuItem menuItemRotate90L;
        private System.Windows.Forms.MenuItem menuItemRotate180;
        private System.Windows.Forms.MenuItem menuItemFlipHorizontal;
        private System.Windows.Forms.MenuItem menuItemFlipVertical;
        private System.Windows.Forms.MenuItem menuItemWalkObjectGraph;
        private System.Windows.Forms.MenuItem menuItemDebug;
        private System.Windows.Forms.MenuItem menuItemGcCollect;
        private System.Windows.Forms.MenuItem menuItemAsciiArt;
        private PViewer.ThumbnailPane paneThumbnail;
        private PViewer.EffectsPane paneEffects;
        private PViewer.ExplorerPane paneExplorer;
        private PViewer.ImagePane paneImage;
        private System.Windows.Forms.Splitter splitterLeft;
        private System.Windows.Forms.Splitter splitterRight;
        private System.Windows.Forms.MenuItem menuItemFitToWindowWidth;
        private System.Windows.Forms.MenuItem menuItemFitToWindowHeight;
        private System.Windows.Forms.MenuItem menuItemFitToWindowLargeOnly;
        private System.Windows.Forms.MenuItem menuItemZoomIn;
        private System.Windows.Forms.MenuItem menuItemZoomOut;
        private System.Windows.Forms.MenuItem menuItemPerspective;
        private System.Windows.Forms.MenuItem menuItemThumbnailBrowsing;
        private System.Windows.Forms.MenuItem menuItemFolderBrowsing;
        private System.Windows.Forms.MenuItem menuItemImageBrowsing;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemZoom100Percent;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.MenuItem menuItemPrint;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog;
        private System.Windows.Forms.MenuItem menuItemPrintPageSetup;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItemPrintPreview;
        private System.Windows.Forms.MenuItem menuItemBurnToCD;
        private System.Windows.Forms.MenuItem menuItemSlideshow;
        private System.Windows.Forms.MenuItem menuItemImageEditing;
#endregion

#region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
            this.menuMain = new System.Windows.Forms.MainMenu();
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.menuItemSep7 = new System.Windows.Forms.MenuItem();
            this.menuItemPrevious = new System.Windows.Forms.MenuItem();
            this.menuItemNext = new System.Windows.Forms.MenuItem();
            this.menuItemFirst = new System.Windows.Forms.MenuItem();
            this.menuItemLast = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItemPrintPageSetup = new System.Windows.Forms.MenuItem();
            this.menuItemPrintPreview = new System.Windows.Forms.MenuItem();
            this.menuItemPrint = new System.Windows.Forms.MenuItem();
            this.menuItemSep5 = new System.Windows.Forms.MenuItem();
            this.menuItemRecentPictures = new System.Windows.Forms.MenuItem();
            this.menuItemSep3 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemEdit = new System.Windows.Forms.MenuItem();
            this.menuItemDelete = new System.Windows.Forms.MenuItem();
            this.menuItemCopy = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemRotate = new System.Windows.Forms.MenuItem();
            this.menuItemRotate90R = new System.Windows.Forms.MenuItem();
            this.menuItemRotate90L = new System.Windows.Forms.MenuItem();
            this.menuItemRotate180 = new System.Windows.Forms.MenuItem();
            this.menuItemFlip = new System.Windows.Forms.MenuItem();
            this.menuItemFlipHorizontal = new System.Windows.Forms.MenuItem();
            this.menuItemFlipVertical = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemAsciiArt = new System.Windows.Forms.MenuItem();
            this.menuItemView = new System.Windows.Forms.MenuItem();
            this.menuItemPerspective = new System.Windows.Forms.MenuItem();
            this.menuItemThumbnailBrowsing = new System.Windows.Forms.MenuItem();
            this.menuItemImageBrowsing = new System.Windows.Forms.MenuItem();
            this.menuItemFolderBrowsing = new System.Windows.Forms.MenuItem();
            this.menuItemImageEditing = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemFitToWindow = new System.Windows.Forms.MenuItem();
            this.menuItemFitToWindowLargeOnly = new System.Windows.Forms.MenuItem();
            this.menuItemFitToWindowWidth = new System.Windows.Forms.MenuItem();
            this.menuItemFitToWindowHeight = new System.Windows.Forms.MenuItem();
            this.menuItemShowNativeSize = new System.Windows.Forms.MenuItem();
            this.menuItemStretchToWindow = new System.Windows.Forms.MenuItem();
            this.menuItemSep8 = new System.Windows.Forms.MenuItem();
            this.menuItemZoomIn = new System.Windows.Forms.MenuItem();
            this.menuItemZoom100Percent = new System.Windows.Forms.MenuItem();
            this.menuItemZoomOut = new System.Windows.Forms.MenuItem();
            this.menuItemSep = new System.Windows.Forms.MenuItem();
            this.menuItemInterpolationMode = new System.Windows.Forms.MenuItem();
            this.menuItemNearestNeighbor = new System.Windows.Forms.MenuItem();
            this.menuItemLow = new System.Windows.Forms.MenuItem();
            this.menuItemBilinear = new System.Windows.Forms.MenuItem();
            this.menuItemBicubic = new System.Windows.Forms.MenuItem();
            this.menuItemHigh = new System.Windows.Forms.MenuItem();
            this.menuItemHighQualityBilinear = new System.Windows.Forms.MenuItem();
            this.menuItemHighQualityBicubic = new System.Windows.Forms.MenuItem();
            this.menuItemSep4 = new System.Windows.Forms.MenuItem();
            this.menuItemFullScreen = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemSep6 = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.menuItemDebug = new System.Windows.Forms.MenuItem();
            this.menuItemWalkObjectGraph = new System.Windows.Forms.MenuItem();
            this.menuItemGcCollect = new System.Windows.Forms.MenuItem();
            this.menuItemBurnToCD = new System.Windows.Forms.MenuItem();
            this.statusBar = new PViewer.ImageStatusBar();
            this.toolBar = new System.Windows.Forms.ToolBar();
            this.toolBarButtonOpen = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonRefresh = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonPrevious = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonNext = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonZoomIn = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonZoomOut = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonOptions = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonAbout = new System.Windows.Forms.ToolBarButton();
            this.imageListToolbar = new System.Windows.Forms.ImageList(this.components);
            this.paneThumbnail = new PViewer.ThumbnailPane();
            this.paneEffects = new PViewer.EffectsPane();
            this.paneExplorer = new PViewer.ExplorerPane();
            this.paneImage = new PViewer.ImagePane();
            this.splitterLeft = new System.Windows.Forms.Splitter();
            this.splitterRight = new System.Windows.Forms.Splitter();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.menuItemSlideshow = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuItemFile,
                                                                                     this.menuItemEdit,
                                                                                     this.menuItemView,
                                                                                     this.menuItemHelp,
                                                                                     this.menuItemDebug});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.menuItemOpen,
                                                                                         this.menuItemSep7,
                                                                                         this.menuItemPrevious,
                                                                                         this.menuItemNext,
                                                                                         this.menuItemFirst,
                                                                                         this.menuItemLast,
                                                                                         this.menuItem6,
                                                                                         this.menuItemPrintPageSetup,
                                                                                         this.menuItemPrintPreview,
                                                                                         this.menuItemPrint,
                                                                                         this.menuItemSep5,
                                                                                         this.menuItemRecentPictures,
                                                                                         this.menuItemSep3,
                                                                                         this.menuItemExit});
            this.menuItemFile.Text = "&File";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index = 0;
            this.menuItemOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.menuItemOpen.Text = "&Open File...";
            this.menuItemOpen.Click += new System.EventHandler(this.OnOpenImage);
            // 
            // menuItemSep7
            // 
            this.menuItemSep7.Index = 1;
            this.menuItemSep7.Text = "-";
            // 
            // menuItemPrevious
            // 
            this.menuItemPrevious.Index = 2;
            this.menuItemPrevious.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.menuItemPrevious.Text = "&Previous";
            this.menuItemPrevious.Click += new System.EventHandler(this.OnPrevious);
            // 
            // menuItemNext
            // 
            this.menuItemNext.Index = 3;
            this.menuItemNext.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.menuItemNext.Text = "&Next";
            this.menuItemNext.Click += new System.EventHandler(this.OnNext);
            // 
            // menuItemFirst
            // 
            this.menuItemFirst.Index = 4;
            this.menuItemFirst.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
            this.menuItemFirst.Text = "&First";
            this.menuItemFirst.Click += new System.EventHandler(this.OnFirstImage);
            // 
            // menuItemLast
            // 
            this.menuItemLast.Index = 5;
            this.menuItemLast.Shortcut = System.Windows.Forms.Shortcut.CtrlL;
            this.menuItemLast.Text = "&Last";
            this.menuItemLast.Click += new System.EventHandler(this.OnLastImage);
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 6;
            this.menuItem6.Text = "-";
            // 
            // menuItemPrintPageSetup
            // 
            this.menuItemPrintPageSetup.Index = 7;
            this.menuItemPrintPageSetup.Text = "Print Page Setup...";
            this.menuItemPrintPageSetup.Click += new System.EventHandler(this.OnPrintPageSetup);
            // 
            // menuItemPrintPreview
            // 
            this.menuItemPrintPreview.Index = 8;
            this.menuItemPrintPreview.Text = "Print Preview...";
            this.menuItemPrintPreview.Click += new System.EventHandler(this.OnPrintPreview);
            // 
            // menuItemPrint
            // 
            this.menuItemPrint.Index = 9;
            this.menuItemPrint.Text = "Print...";
            this.menuItemPrint.Click += new System.EventHandler(this.OnPrint);
            // 
            // menuItemSep5
            // 
            this.menuItemSep5.Index = 10;
            this.menuItemSep5.Text = "-";
            // 
            // menuItemRecentPictures
            // 
            this.menuItemRecentPictures.Index = 11;
            this.menuItemRecentPictures.Text = "Recent Files";
            // 
            // menuItemSep3
            // 
            this.menuItemSep3.Index = 12;
            this.menuItemSep3.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 13;
            this.menuItemExit.Shortcut = System.Windows.Forms.Shortcut.CtrlX;
            this.menuItemExit.Text = "&Exit";
            this.menuItemExit.Click += new System.EventHandler(this.OnFileExit);
            // 
            // menuItemEdit
            // 
            this.menuItemEdit.Index = 1;
            this.menuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.menuItemDelete,
                                                                                         this.menuItemCopy,
                                                                                         this.menuItem1,
                                                                                         this.menuItemRotate,
                                                                                         this.menuItemFlip,
                                                                                         this.menuItem3,
                                                                                         this.menuItemAsciiArt});
            this.menuItemEdit.Text = "&Edit";
            // 
            // menuItemDelete
            // 
            this.menuItemDelete.Index = 0;
            this.menuItemDelete.Shortcut = System.Windows.Forms.Shortcut.Del;
            this.menuItemDelete.Text = "&Delete";
            this.menuItemDelete.Click += new System.EventHandler(this.OnDeleteImage);
            // 
            // menuItemCopy
            // 
            this.menuItemCopy.Index = 1;
            this.menuItemCopy.Shortcut = System.Windows.Forms.Shortcut.CtrlC;
            this.menuItemCopy.Text = "&Copy";
            this.menuItemCopy.Click += new System.EventHandler(this.OnCopyImage);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "-";
            // 
            // menuItemRotate
            // 
            this.menuItemRotate.Index = 3;
            this.menuItemRotate.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                           this.menuItemRotate90R,
                                                                                           this.menuItemRotate90L,
                                                                                           this.menuItemRotate180});
            this.menuItemRotate.Text = "Rotate";
            // 
            // menuItemRotate90R
            // 
            this.menuItemRotate90R.Index = 0;
            this.menuItemRotate90R.Text = "90 Degree &Right";
            this.menuItemRotate90R.Click += new System.EventHandler(this.OnRotate90R);
            // 
            // menuItemRotate90L
            // 
            this.menuItemRotate90L.Index = 1;
            this.menuItemRotate90L.Text = "90 Degree &Left";
            this.menuItemRotate90L.Click += new System.EventHandler(this.OnRotate90L);
            // 
            // menuItemRotate180
            // 
            this.menuItemRotate180.Index = 2;
            this.menuItemRotate180.Text = "1&80 Degree";
            this.menuItemRotate180.Click += new System.EventHandler(this.OnRotate180);
            // 
            // menuItemFlip
            // 
            this.menuItemFlip.Index = 4;
            this.menuItemFlip.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.menuItemFlipHorizontal,
                                                                                         this.menuItemFlipVertical});
            this.menuItemFlip.Text = "Flip";
            // 
            // menuItemFlipHorizontal
            // 
            this.menuItemFlipHorizontal.Index = 0;
            this.menuItemFlipHorizontal.Text = "&Horizontal";
            this.menuItemFlipHorizontal.Click += new System.EventHandler(this.OnFlipHorizontal);
            // 
            // menuItemFlipVertical
            // 
            this.menuItemFlipVertical.Index = 1;
            this.menuItemFlipVertical.Text = "&Vertical";
            this.menuItemFlipVertical.Click += new System.EventHandler(this.OnFlipVertical);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 5;
            this.menuItem3.Text = "-";
            // 
            // menuItemAsciiArt
            // 
            this.menuItemAsciiArt.Index = 6;
            this.menuItemAsciiArt.Text = "&ASCII Art";
            this.menuItemAsciiArt.Click += new System.EventHandler(this.OnAsciiArt);
            // 
            // menuItemView
            // 
            this.menuItemView.Index = 2;
            this.menuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.menuItemPerspective,
                                                                                         this.menuItem4,
                                                                                         this.menuItemFitToWindow,
                                                                                         this.menuItemFitToWindowLargeOnly,
                                                                                         this.menuItemFitToWindowWidth,
                                                                                         this.menuItemFitToWindowHeight,
                                                                                         this.menuItemShowNativeSize,
                                                                                         this.menuItemStretchToWindow,
                                                                                         this.menuItemSep8,
                                                                                         this.menuItemZoomIn,
                                                                                         this.menuItemZoom100Percent,
                                                                                         this.menuItemZoomOut,
                                                                                         this.menuItemSep,
                                                                                         this.menuItemInterpolationMode,
                                                                                         this.menuItemSep4,
                                                                                         this.menuItemFullScreen,
                                                                                         this.menuItemSlideshow});
            this.menuItemView.Text = "&View";
            this.menuItemView.Popup += new System.EventHandler(this.OnPopupMainMenu);
            // 
            // menuItemPerspective
            // 
            this.menuItemPerspective.Index = 0;
            this.menuItemPerspective.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                this.menuItemThumbnailBrowsing,
                                                                                                this.menuItemImageBrowsing,
                                                                                                this.menuItemFolderBrowsing,
                                                                                                this.menuItemImageEditing});
            this.menuItemPerspective.Text = "&Perspective";
            // 
            // menuItemThumbnailBrowsing
            // 
            this.menuItemThumbnailBrowsing.Index = 0;
            this.menuItemThumbnailBrowsing.Shortcut = System.Windows.Forms.Shortcut.F7;
            this.menuItemThumbnailBrowsing.Text = "&Thumbnail Browsing";
            this.menuItemThumbnailBrowsing.Click += new System.EventHandler(this.OnThumbnailBrowsing);
            // 
            // menuItemImageBrowsing
            // 
            this.menuItemImageBrowsing.Index = 1;
            this.menuItemImageBrowsing.Shortcut = System.Windows.Forms.Shortcut.F8;
            this.menuItemImageBrowsing.Text = "&Image Browsing";
            this.menuItemImageBrowsing.Click += new System.EventHandler(this.OnImageBrowsing);
            // 
            // menuItemFolderBrowsing
            // 
            this.menuItemFolderBrowsing.Index = 2;
            this.menuItemFolderBrowsing.Shortcut = System.Windows.Forms.Shortcut.F9;
            this.menuItemFolderBrowsing.Text = "&Folder Browsing";
            this.menuItemFolderBrowsing.Click += new System.EventHandler(this.OnFolderBrowsing);
            // 
            // menuItemImageEditing
            // 
            this.menuItemImageEditing.Index = 3;
            this.menuItemImageEditing.Shortcut = System.Windows.Forms.Shortcut.F10;
            this.menuItemImageEditing.Text = "Image &Editing";
            this.menuItemImageEditing.Click += new System.EventHandler(this.OnImageEditing);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 1;
            this.menuItem4.Text = "-";
            // 
            // menuItemFitToWindow
            // 
            this.menuItemFitToWindow.Index = 2;
            this.menuItemFitToWindow.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItemFitToWindow.Text = "Fit to &Window";
            this.menuItemFitToWindow.Click += new System.EventHandler(this.OnFitToWindow);
            // 
            // menuItemFitToWindowLargeOnly
            // 
            this.menuItemFitToWindowLargeOnly.Index = 3;
            this.menuItemFitToWindowLargeOnly.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.menuItemFitToWindowLargeOnly.Text = "Fit to Window, &Large Only";
            this.menuItemFitToWindowLargeOnly.Click += new System.EventHandler(this.OnFitToWindowLargeOnly);
            // 
            // menuItemFitToWindowWidth
            // 
            this.menuItemFitToWindowWidth.Index = 4;
            this.menuItemFitToWindowWidth.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.menuItemFitToWindowWidth.Text = "Fit to Window &Width";
            this.menuItemFitToWindowWidth.Click += new System.EventHandler(this.OnFitToWindowWidth);
            // 
            // menuItemFitToWindowHeight
            // 
            this.menuItemFitToWindowHeight.Index = 5;
            this.menuItemFitToWindowHeight.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.menuItemFitToWindowHeight.Text = "Fit to Window &Height";
            this.menuItemFitToWindowHeight.Click += new System.EventHandler(this.OnFitToWindowHeight);
            // 
            // menuItemShowNativeSize
            // 
            this.menuItemShowNativeSize.Index = 6;
            this.menuItemShowNativeSize.Shortcut = System.Windows.Forms.Shortcut.F5;
            this.menuItemShowNativeSize.Text = "Show &Native Size";
            this.menuItemShowNativeSize.Click += new System.EventHandler(this.OnNativeSize);
            // 
            // menuItemStretchToWindow
            // 
            this.menuItemStretchToWindow.Index = 7;
            this.menuItemStretchToWindow.Shortcut = System.Windows.Forms.Shortcut.F6;
            this.menuItemStretchToWindow.Text = "&Stretch to Window";
            this.menuItemStretchToWindow.Click += new System.EventHandler(this.OnStretchToWindow);
            // 
            // menuItemSep8
            // 
            this.menuItemSep8.Index = 8;
            this.menuItemSep8.Text = "-";
            // 
            // menuItemZoomIn
            // 
            this.menuItemZoomIn.Index = 9;
            this.menuItemZoomIn.Text = "Zoom +\t+";
            this.menuItemZoomIn.Click += new System.EventHandler(this.OnZoomIn);
            // 
            // menuItemZoom100Percent
            // 
            this.menuItemZoom100Percent.Index = 10;
            this.menuItemZoom100Percent.Text = "Zoom 100%";
            this.menuItemZoom100Percent.Click += new System.EventHandler(this.OnZoom100Percent);
            // 
            // menuItemZoomOut
            // 
            this.menuItemZoomOut.Index = 11;
            this.menuItemZoomOut.Text = "Zoom -\t-";
            this.menuItemZoomOut.Click += new System.EventHandler(this.OnZoomOut);
            // 
            // menuItemSep
            // 
            this.menuItemSep.Index = 12;
            this.menuItemSep.Text = "-";
            // 
            // menuItemInterpolationMode
            // 
            this.menuItemInterpolationMode.Index = 13;
            this.menuItemInterpolationMode.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                      this.menuItemNearestNeighbor,
                                                                                                      this.menuItemLow,
                                                                                                      this.menuItemBilinear,
                                                                                                      this.menuItemBicubic,
                                                                                                      this.menuItemHigh,
                                                                                                      this.menuItemHighQualityBilinear,
                                                                                                      this.menuItemHighQualityBicubic});
            this.menuItemInterpolationMode.Text = "Interpolation Mode";
            // 
            // menuItemNearestNeighbor
            // 
            this.menuItemNearestNeighbor.Index = 0;
            this.menuItemNearestNeighbor.Text = "NearestNeighbor";
            this.menuItemNearestNeighbor.Click += new System.EventHandler(this.OnInterpolationNearestNeighbor);
            // 
            // menuItemLow
            // 
            this.menuItemLow.Index = 1;
            this.menuItemLow.Text = "Low";
            this.menuItemLow.Click += new System.EventHandler(this.OnInterpolationLow);
            // 
            // menuItemBilinear
            // 
            this.menuItemBilinear.Index = 2;
            this.menuItemBilinear.Text = "Bilinear";
            this.menuItemBilinear.Click += new System.EventHandler(this.OnInterpolationBilinear);
            // 
            // menuItemBicubic
            // 
            this.menuItemBicubic.Index = 3;
            this.menuItemBicubic.Text = "Bicubic";
            this.menuItemBicubic.Click += new System.EventHandler(this.OnInterpolationBicubic);
            // 
            // menuItemHigh
            // 
            this.menuItemHigh.Index = 4;
            this.menuItemHigh.Text = "High";
            this.menuItemHigh.Click += new System.EventHandler(this.OnInterpolationHighQuality);
            // 
            // menuItemHighQualityBilinear
            // 
            this.menuItemHighQualityBilinear.Index = 5;
            this.menuItemHighQualityBilinear.Text = "HighQualityBilinear";
            this.menuItemHighQualityBilinear.Click += new System.EventHandler(this.OnInterpolationHighQualityBilinear);
            // 
            // menuItemHighQualityBicubic
            // 
            this.menuItemHighQualityBicubic.Index = 6;
            this.menuItemHighQualityBicubic.Text = "HighQualityBicubic";
            this.menuItemHighQualityBicubic.Click += new System.EventHandler(this.OnInterpolationHighQualityBicubic);
            // 
            // menuItemSep4
            // 
            this.menuItemSep4.Index = 14;
            this.menuItemSep4.Text = "-";
            // 
            // menuItemFullScreen
            // 
            this.menuItemFullScreen.Index = 15;
            this.menuItemFullScreen.Shortcut = System.Windows.Forms.Shortcut.F11;
            this.menuItemFullScreen.Text = "&Full Screen";
            this.menuItemFullScreen.Click += new System.EventHandler(this.OnFullScreen);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 3;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                         this.menuItem2,
                                                                                         this.menuItemSep6,
                                                                                         this.menuItemAbout});
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 0;
            this.menuItem2.Text = "Content";
            // 
            // menuItemSep6
            // 
            this.menuItemSep6.Index = 1;
            this.menuItemSep6.Text = "-";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 2;
            this.menuItemAbout.Text = "&About";
            this.menuItemAbout.Click += new System.EventHandler(this.OnHelpAbout);
            // 
            // menuItemDebug
            // 
            this.menuItemDebug.Index = 4;
            this.menuItemDebug.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                          this.menuItemWalkObjectGraph,
                                                                                          this.menuItemGcCollect,
                                                                                          this.menuItemBurnToCD});
            this.menuItemDebug.Text = "Debug";
            // 
            // menuItemWalkObjectGraph
            // 
            this.menuItemWalkObjectGraph.Index = 0;
            this.menuItemWalkObjectGraph.Text = "Walk Object Graph to c:\\walk.txt";
            this.menuItemWalkObjectGraph.Click += new System.EventHandler(this.OnWalkObjectGraph);
            // 
            // menuItemGcCollect
            // 
            this.menuItemGcCollect.Index = 1;
            this.menuItemGcCollect.Text = "GC.Collect";
            this.menuItemGcCollect.Click += new System.EventHandler(this.OnGcCollect);
            // 
            // menuItemBurnToCD
            // 
            this.menuItemBurnToCD.Index = 2;
            this.menuItemBurnToCD.Text = "Burn to CD";
            this.menuItemBurnToCD.Click += new System.EventHandler(this.OnBurnToCD);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(6, 547);
            this.statusBar.Name = "statusBar";
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(708, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 0;
            // 
            // toolBar
            // 
            this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
                                                                                       this.toolBarButtonOpen,
                                                                                       this.toolBarButtonRefresh,
                                                                                       this.toolBarButtonPrevious,
                                                                                       this.toolBarButtonNext,
                                                                                       this.toolBarButtonZoomIn,
                                                                                       this.toolBarButtonZoomOut,
                                                                                       this.toolBarButtonOptions,
                                                                                       this.toolBarButtonAbout});
            this.toolBar.DropDownArrows = true;
            this.toolBar.ImageList = this.imageListToolbar;
            this.toolBar.Location = new System.Drawing.Point(6, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.ShowToolTips = true;
            this.toolBar.Size = new System.Drawing.Size(708, 60);
            this.toolBar.TabIndex = 1;
            this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.OnToolbarButton);
            // 
            // toolBarButtonOpen
            // 
            this.toolBarButtonOpen.ImageIndex = 0;
            this.toolBarButtonOpen.Text = "Open";
            this.toolBarButtonOpen.ToolTipText = "Open";
            // 
            // toolBarButtonRefresh
            // 
            this.toolBarButtonRefresh.ImageIndex = 1;
            this.toolBarButtonRefresh.Text = "Refresh";
            this.toolBarButtonRefresh.ToolTipText = "Refresh";
            // 
            // toolBarButtonPrevious
            // 
            this.toolBarButtonPrevious.ImageIndex = 2;
            this.toolBarButtonPrevious.Text = "Previous";
            this.toolBarButtonPrevious.ToolTipText = "Previous";
            // 
            // toolBarButtonNext
            // 
            this.toolBarButtonNext.ImageIndex = 3;
            this.toolBarButtonNext.Text = "Next";
            this.toolBarButtonNext.ToolTipText = "Next";
            // 
            // toolBarButtonZoomIn
            // 
            this.toolBarButtonZoomIn.ImageIndex = 5;
            this.toolBarButtonZoomIn.Text = "Zoom In";
            this.toolBarButtonZoomIn.ToolTipText = "Zoom In";
            // 
            // toolBarButtonZoomOut
            // 
            this.toolBarButtonZoomOut.ImageIndex = 4;
            this.toolBarButtonZoomOut.Text = "Zoom Out";
            this.toolBarButtonZoomOut.ToolTipText = "Zoom Out";
            // 
            // toolBarButtonOptions
            // 
            this.toolBarButtonOptions.ImageIndex = 6;
            this.toolBarButtonOptions.Text = "Options";
            this.toolBarButtonOptions.ToolTipText = "Options";
            this.toolBarButtonOptions.Visible = false;
            // 
            // toolBarButtonAbout
            // 
            this.toolBarButtonAbout.ImageIndex = 7;
            this.toolBarButtonAbout.Text = "About";
            this.toolBarButtonAbout.ToolTipText = "About";
            // 
            // imageListToolbar
            // 
            this.imageListToolbar.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
            this.imageListToolbar.ImageSize = new System.Drawing.Size(32, 32);
            this.imageListToolbar.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListToolbar.ImageStream")));
            this.imageListToolbar.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // paneThumbnail
            // 
            this.paneThumbnail.BackColor = System.Drawing.Color.DarkGray;
            this.paneThumbnail.CaptionText = "Thumbnails";
            this.paneThumbnail.Dock = System.Windows.Forms.DockStyle.Left;
            this.paneThumbnail.DockPadding.Bottom = 1;
            this.paneThumbnail.DockPadding.Left = 1;
            this.paneThumbnail.DockPadding.Right = 1;
            this.paneThumbnail.DockPadding.Top = 1;
            this.paneThumbnail.Location = new System.Drawing.Point(222, 60);
            this.paneThumbnail.Name = "paneThumbnail";
            this.paneThumbnail.Size = new System.Drawing.Size(200, 487);
            this.paneThumbnail.TabIndex = 6;
            // 
            // paneEffects
            // 
            this.paneEffects.CaptionText = "Effects";
            this.paneEffects.Dock = System.Windows.Forms.DockStyle.Right;
            this.paneEffects.DockPadding.All = 1;
            this.paneEffects.Location = new System.Drawing.Point(514, 60);
            this.paneEffects.Name = "paneEffects";
            this.paneEffects.Size = new System.Drawing.Size(200, 487);
            this.paneEffects.TabIndex = 9;
            // 
            // paneExplorer
            // 
            this.paneExplorer.CaptionText = "Folders";
            this.paneExplorer.Dock = System.Windows.Forms.DockStyle.Left;
            this.paneExplorer.DockPadding.All = 1;
            this.paneExplorer.Location = new System.Drawing.Point(6, 60);
            this.paneExplorer.Name = "paneExplorer";
            this.paneExplorer.Size = new System.Drawing.Size(216, 487);
            this.paneExplorer.TabIndex = 7;
            this.paneExplorer.Load += new System.EventHandler(this.OnExplorerPaneLoaded);
            // 
            // paneImage
            // 
            this.paneImage.CaptionText = "Picture";
            this.paneImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paneImage.DockPadding.Bottom = 1;
            this.paneImage.DockPadding.Left = 5;
            this.paneImage.DockPadding.Right = 5;
            this.paneImage.DockPadding.Top = 1;
            this.paneImage.Location = new System.Drawing.Point(422, 60);
            this.paneImage.Name = "paneImage";
            this.paneImage.Size = new System.Drawing.Size(92, 487);
            this.paneImage.TabIndex = 5;
            // 
            // splitterLeft
            // 
            this.splitterLeft.Location = new System.Drawing.Point(422, 60);
            this.splitterLeft.Name = "splitterLeft";
            this.splitterLeft.Size = new System.Drawing.Size(5, 487);
            this.splitterLeft.TabIndex = 7;
            this.splitterLeft.TabStop = false;
            // 
            // splitterRight
            // 
            this.splitterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterRight.Location = new System.Drawing.Point(509, 60);
            this.splitterRight.Name = "splitterRight";
            this.splitterRight.Size = new System.Drawing.Size(5, 487);
            this.splitterRight.TabIndex = 10;
            this.splitterRight.TabStop = false;
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Location = new System.Drawing.Point(484, 44);
            this.printPreviewDialog.MinimumSize = new System.Drawing.Size(375, 250);
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.TransparencyKey = System.Drawing.Color.Empty;
            this.printPreviewDialog.Visible = false;
            // 
            // menuItemSlideshow
            // 
            this.menuItemSlideshow.Index = 16;
            this.menuItemSlideshow.Text = "Slideshow";
            this.menuItemSlideshow.Click += new System.EventHandler(this.OnSlideshow);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(720, 569);
            this.Controls.Add(this.splitterRight);
            this.Controls.Add(this.splitterLeft);
            this.Controls.Add(this.paneImage);
            this.Controls.Add(this.paneEffects);
            this.Controls.Add(this.paneThumbnail);
            this.Controls.Add(this.paneExplorer);
            this.Controls.Add(this.toolBar);
            this.Controls.Add(this.statusBar);
            this.DockPadding.Left = 6;
            this.DockPadding.Right = 6;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.Menu = this.menuMain;
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PViewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }
#endregion

        public MainForm(string[] args)
        {
            // Save command-line arguments for later use.
            _commandLineArgs = args;

            WindowSerializer windowSerializer = new WindowSerializer(this);

            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            Images = new DirectoryImageCollection();
            Navigator = new ImageNavigator();

            LoadPreference();
        }

        public DirectoryImageCollection Images
        {
            get
            {
                return _images;
            }

            set
            {
                if (_images != null)
                {
                    _images.ImageCollected -= new EventHandler(this.OnImageFilesCollected);
                    _images.ImageAdded -= new EventHandler(this.OnImageAdded);
                    _images.ImageRemoved -= new EventHandler(this.OnImageRemoved);
                }

                if (value == null)
                {
                    throw new ArgumentNullException("Images can not be null");
                }

                _images = value;
                _images.ImageCollected += new EventHandler(this.OnImageFilesCollected);
                _images.ImageAdded += new EventHandler(this.OnImageAdded);
                _images.ImageRemoved += new EventHandler(this.OnImageRemoved);

                OnImageFilesCollected(this, EventArgs.Empty);
            }
        }

        public ImageNavigator Navigator
        {
            get
            {
                return _nav;
            }

            set
            {
                if (_nav != null)
                {
                    _nav.NavigationEvent -= new EventHandler(paneEffects.OnImageNavigation);
                    _nav.NavigationEvent -= new EventHandler(this.OnImageNavigation);
                }

                _nav = value;
                if (_nav != null)
                {
                    _nav.Collection = _images;
                    _nav.NavigationEvent += new EventHandler(this.OnImageNavigation);
                    _nav.NavigationEvent += new EventHandler(paneEffects.OnImageNavigation);
                }
            }
        }

        private void OnMRUClick(object sender, string fileName)
        {
            _images.Collect(fileName);
            _nav.Goto(fileName);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        private string ResourceString(string name)
        {
            return _rm.GetString(name, System.Globalization.CultureInfo.CurrentUICulture);
        }

        private void OnLoad(object sender, System.EventArgs e)
        {
            _mru = new MRU();
            _mru.Style = MRUStyle.InSubMenu;
            _mru.Parent = menuItemRecentPictures;
            _mru.MRUClick += new MRU.onMRUClick(OnMRUClick);

            paneThumbnail.List.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnListKeyDown);
            paneThumbnail.List.SelectedIndexChanged += new System.EventHandler(this.OnSelectedIndexChanged);

            paneImage.Box.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            paneImage.Box.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            paneImage.Box.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            paneImage.Box.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            paneImage.Box.MouseDown += new MouseEventHandler(this.OnMouseDown);

            statusBar.AddPanels();

            Update();

            try
            {
                if (_commandLineArgs.Length > 0)
                {
                    _images.Collect(_commandLineArgs[0]);
                    _nav.Goto(_commandLineArgs[0]);
                }
                else
                {
                    LoadLastImage();
                }
            }
            catch (ArgumentException ex)
            {
                // Ignore exceptions when loading last image.
                Debug.WriteLine("Exception: " + ex);
            }

            // Force to focus on image pane (it is always visible in all perspectives) 
            // after loading.
            paneImage.Box.Focus();
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SavePreference();

            paneThumbnail.List.Items.Clear();
            Navigator = null;
        }

        /// <summary>
        /// Load viewer preference.
        /// </summary>
        private void LoadPreference()
        {
            Genghis.Preferences prefReader = Genghis.Preferences.GetUserNode(GetType());

            paneImage.Box.SizeMode = (SizeMode) Enum.Parse(typeof(SizeMode), 
                prefReader.GetString("SizeMode", SizeMode.ShowNativeSize.ToString()));
            paneImage.Box.InterpolationMode = (InterpolationMode) Enum.Parse(typeof(InterpolationMode), 
                prefReader.GetString("InterpolationMode", InterpolationMode.High.ToString()));

            Perspective = (Perspectives) Enum.Parse(typeof(Perspectives), 
                prefReader.GetString("Perspective", Perspectives.ThumbnailBrowsing.ToString()));

            prefReader.Close();
        }

        private void LoadLastImage()
        {
            Genghis.Preferences prefReader = Genghis.Preferences.GetUserNode(GetType());

            string lastImage = prefReader.GetString("LastImage", "");
            if (lastImage.Length > 0)
            {
                _images.Collect(lastImage);
                _nav.Goto(lastImage);
            }

            prefReader.Close();
        }

        /// <summary>
        /// Save viewer preference.
        /// </summary>
        private void SavePreference()
        {
            Genghis.Preferences prefWriter = Genghis.Preferences.GetUserNode(GetType());
            prefWriter.SetProperty("SizeMode", paneImage.Box.SizeMode);
            prefWriter.SetProperty("InterpolationMode", paneImage.Box.InterpolationMode);
            prefWriter.SetProperty("Perspective", Perspective);

            if (_nav.CurrentImage != null)
            {
                prefWriter.SetProperty("LastImage", _nav.CurrentImage.ImagePath);
            }
            prefWriter.Close();
        }

        private void UpdateImageStatus()
        {
            ImageInfo image = _nav.CurrentImage;

            if (image != null)
            {
                this.Text = image.ImagePath;

                statusBar.NavPanel.Text = String.Format(ResourceString("FileCount"), _nav.Current + 1, _images.Count);
                statusBar.DimensionPanel.Text = String.Format(ResourceString("ImageDimension"), image.Image.Width, image.Image.Height, image.Image.PixelFormat);
                statusBar.SizePanel.Text = String.Format("{0} {1}", image.SizeString, image.CompressionRatio.ToString("P"));
                statusBar.DatePanel.Text = image.LastWriteTime.ToString();
                statusBar.ZoomPanel.Text = paneImage.Box.ZoomRatio.ToString("P");
            }
            else
            {
                this.Text = String.Empty;

                statusBar.NavPanel.Text = String.Empty;
                statusBar.DimensionPanel.Text = String.Empty;
                statusBar.SizePanel.Text = String.Empty;
                statusBar.DatePanel.Text = String.Empty;
                statusBar.ZoomPanel.Text = String.Empty;
                statusBar.CommentPanel.Text = String.Empty;
            }
        }

        private void OnImageAdded(object sender, EventArgs e)
        {}

        private void OnImageRemoved(object sender, EventArgs eventArgs)
        {
            ImageCollection.ImageRemovedEventArgs e = (ImageCollection.ImageRemovedEventArgs) eventArgs;
            paneThumbnail.List.Items.RemoveAt(e._index);
        }

        private void OnExplorerPaneLoaded(object sender, System.EventArgs e)
        {
            paneExplorer.Tree.AfterSelect -= new TreeViewEventHandler(this.AfterSelectDirectory);
            if (_images.Directory != null)
            {
                paneExplorer.SelectPath(_images.Directory);
            }
            paneExplorer.Tree.AfterSelect += new TreeViewEventHandler(this.AfterSelectDirectory);
        }

        /// <summary>
        /// Show image comment on status bar.
        /// </summary>
        /// <param name="comment">the comment</param>
        private void ShowImageComment(string comment)
        {
            statusBar.CommentPanel.Text = comment;
        }

        private void OnImageFilesCollected(object sender, EventArgs e)
        {
            if (_nav != null)
            {
                _nav.Collection = _images;

                paneThumbnail.List.RefreshThumbnails(_images);

                if (paneExplorer.Created)
                {
                    OnExplorerPaneLoaded(this, null);
                }
            }
        }

        /// <summary>
        /// effects: load the next image in the array. switch to next directory if necessary.
        /// </summary>
        private void ShowNextImage()
        {
            if (_nav.AtLast())
            {
                DirectoryImageCollection images = _images.FindNextCollection(1);

                if (images != null &&
                        MessageBox.Show(
                            String.Format(ResourceString("ContinueWithNextDirectory"), images.Directory),
                            ResourceString("AppTitle"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.None) == DialogResult.Yes)
                {
                    Images = images;
                }

                _nav.GotoFirst();
            }
            else
            {
                _nav.GotoNext();
            }
        }

        private void ShowPreviousImage()
        {
            if (_nav.AtFirst())
            {
                DirectoryImageCollection images = _images.FindNextCollection(-1);

                if (images != null &&
                        MessageBox.Show(
                            String.Format(ResourceString("ContinueWithNextDirectory"), images.Directory),
                            ResourceString("AppTitle"),
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.None) == DialogResult.Yes)
                {
                    Images = images;
                }

                _nav.GotoLast();
            }
            else
            {
                _nav.GotoPrevious();
            }
        }

        private void AfterSelectDirectory(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            try
            {
                DirectoryInfo di;

                // If drive, get directories from drives
                if (e.Node.SelectedImageIndex < 11)
                {
                    di = new DirectoryInfo(e.Node.FullPath + "\\");
                }
                //  Else, get directories from directories
                else
                {
                    di = new DirectoryInfo(e.Node.FullPath);
                }

                if (di.FullName != _images.Directory)
                {
                    _images.Collect(di);
                    if (_images.Count > 0)
                    {
                        _nav.Current = 0;
                        paneImage.Box.Focus();
                    }
                }
            }
            catch (SystemException ex)
            {
                // Throw Exception when accessing directory: C:\System Volume Information	 // do nothing
                Debug.WriteLine("Exception: " + ex);
            }
        }

        public static OpenFileDialog OpenImageFileDialog(int filterIndex)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
                "Image Files (JPEG, GIF, BMP, etc.)|*.jpg;*.jpeg;*.gif;*.bmp;*.tif;*.tiff;*.png|" +
                "JPEG files (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "GIF Files (*.gif)|*.gif|" +
                "BMP Files (*.bmp)|*.bmp|" +
                "TIFF Files (*.tif;*.tiff)|*.tif;*.tiff|" +
                "PNG Files (*.png)|*.png|" +
                "All files (*.*)|*.*";

            dialog.FilterIndex = filterIndex;

            return dialog;
        }

        // Handler for the Open command
        private void OnOpenImage(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = OpenImageFileDialog(_filterIndex))
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fileName = ofd.FileName;
                    if (fileName.Length != 0)
                    {
                        // Remember current filter index.
                        _filterIndex = ofd.FilterIndex;

                        // Load this image.
                        _images.Collect(fileName);
                        _nav.Goto(fileName);
                    }
                }
            }
        }

        private void SetSizeMode(SizeMode SizeMode)
        {
            paneImage.Box.SizeMode = SizeMode;

            // Reset zoom level.
            ZoomLevel = DefaultZoomLevel;

            UpdateImageStatus();
        }

        private void OnFitToWindow(object sender, EventArgs e)
        {
            SetSizeMode(SizeMode.FitToWindow);
        }

        private void OnFitToWindowLargeOnly(object sender, System.EventArgs e)
        {
            SetSizeMode(SizeMode.FitToWindowLargeOnly);
        }

        private void OnFitToWindowWidth(object sender, System.EventArgs e)
        {
            SetSizeMode(SizeMode.FitToWindowWidth);
        }

        private void OnFitToWindowHeight(object sender, System.EventArgs e)
        {
            SetSizeMode(SizeMode.FitToWindowHeight);
        }

        void OnNativeSize(object sender, EventArgs e)
        {
            SetSizeMode(SizeMode.ShowNativeSize);
        }

        private void OnStretchToWindow(object sender, System.EventArgs e)
        {
            SetSizeMode(SizeMode.StretchToWindow);
        }

        protected void OnMouseDown(Object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.XButton1)
            {
                _nav.GotoNext();
            }
            else if (e.Button == MouseButtons.XButton2)
            {
                _nav.GotoPrevious();
            }
        }

        protected override void OnMouseWheel(MouseEventArgs args)
        {
            int nScroll = args.Delta * SystemInformation.MouseWheelScrollLines / 120;
            if (nScroll < 0)
            {
                ShowNextImage();
            }
            else if (nScroll > 0)
            {
                ShowPreviousImage();
            }
        }

        private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
            case Keys.PageDown:
            case Keys.Space:
            case Keys.Right:
            case Keys.Down:
                ShowNextImage();
                break;

            case Keys.PageUp:
            case Keys.Up:
            case Keys.Left:
                ShowPreviousImage();
                break;

            case Keys.Home:
                _nav.GotoFirst();
                break;

            case Keys.End:
                _nav.GotoLast();
                break;

            case Keys.Escape:

                if (Perspective == Perspectives.FullScreenImageBrowsing)
                {
                    Perspective = _savedFormState.Perspective;
                }
                else
                {
                    // Close the form and exit.
                    Close();
                }
                break;

            case Keys.Delete:

                TrashImage();
                break;

            case Keys.Z:
                OnRotate90L(this, EventArgs.Empty);
                break;

            case Keys.X:
                OnRotate90R(this, EventArgs.Empty);
                break;

            case Keys.Add:
                OnZoomIn(this, EventArgs.Empty);
                break;

            case Keys.Subtract:
                OnZoomOut(this, EventArgs.Empty);
                break;
            }
        }

        private void OnRotate90R(object sender, System.EventArgs e)
        {
            paneImage.Box.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }

        private void OnRotate90L(object sender, System.EventArgs e)
        {
            paneImage.Box.RotateFlip(RotateFlipType.Rotate270FlipNone);
        }

        private void OnRotate180(object sender, System.EventArgs e)
        {
            paneImage.Box.RotateFlip(RotateFlipType.Rotate180FlipNone);
        }

        private void OnFlipHorizontal(object sender, System.EventArgs e)
        {
            paneImage.Box.RotateFlip(RotateFlipType.RotateNoneFlipX);
        }

        private void OnFlipVertical(object sender, System.EventArgs e)
        {
            paneImage.Box.RotateFlip(RotateFlipType.RotateNoneFlipY);
        }

        private void OnHelpAbout(object sender, System.EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.ShowDialog(this);
        }

        private void OnPrevious(object sender, System.EventArgs e)
        {
            ShowPreviousImage();
        }

        private void OnNext(object sender, System.EventArgs e)
        {
            ShowNextImage();
        }

        private void OnImageNavigation(object sender, EventArgs e)
        {
            try
            {
                using (CursorChanger cursorChanger = new CursorChanger(Cursors.WaitCursor))
                {
                    paneImage.Box.ImageInfo = _nav.CurrentImage;
                    if (_nav.CurrentImage != null)
                    {
                        ShowImageComment(String.Format(ResourceString("LoadTime"), _nav.CurrentImage.LoadingTime));

                        foreach (ListViewItem item in paneThumbnail.List.Items) 
                        {
                            item.Selected = false;
                        }
                        paneThumbnail.List.EnsureVisible(_nav.Current);
                        paneThumbnail.List.Items[_nav.Current].Selected = true;

                        // Reset zoom level.
                        ZoomLevel = DefaultZoomLevel;

                        _mru.Add(_nav.CurrentImage.ImagePath);
                    }

                    UpdateImageStatus();

                    if (_slideshow != null)
                    {
                        // restart the timer when navigating to new item
                        _slideshow.Restart();
                    }
                }
            }
            catch (ArgumentException ex)
            {
                statusBar.NavPanel.Text = String.Format(ResourceString("InvalidImage"), _nav.CurrentImage.ImagePath) + ": " + ex.Message;
            }
        }

        private void OnPopupMainMenu(object sender, System.EventArgs e)
        {
            menuItemShowNativeSize.Checked = (paneImage.Box.SizeMode == SizeMode.ShowNativeSize);
            // disable fit mode if it is not compatible with slideshow.
            menuItemShowNativeSize.Enabled = (_slideshow == null || SizeModeBehavior.SupportsTransition(SizeMode.ShowNativeSize));
            menuItemFitToWindow.Checked = (paneImage.Box.SizeMode == SizeMode.FitToWindow);
            menuItemFitToWindow.Enabled = (_slideshow == null || SizeModeBehavior.SupportsTransition(SizeMode.FitToWindow));
            menuItemFitToWindowLargeOnly.Checked = (paneImage.Box.SizeMode == SizeMode.FitToWindowLargeOnly);
            menuItemFitToWindowLargeOnly.Enabled = (_slideshow == null || SizeModeBehavior.SupportsTransition(SizeMode.FitToWindowLargeOnly));
            menuItemFitToWindowWidth.Checked = (paneImage.Box.SizeMode == SizeMode.FitToWindowWidth);
            menuItemFitToWindowWidth.Enabled = (_slideshow == null || SizeModeBehavior.SupportsTransition(SizeMode.FitToWindowWidth));
            menuItemFitToWindowHeight.Checked = (paneImage.Box.SizeMode == SizeMode.FitToWindowHeight);
            menuItemFitToWindowHeight.Enabled = (_slideshow == null || SizeModeBehavior.SupportsTransition(SizeMode.FitToWindowHeight));
            menuItemStretchToWindow.Checked = (paneImage.Box.SizeMode == SizeMode.StretchToWindow);
            menuItemStretchToWindow.Enabled = (_slideshow == null || SizeModeBehavior.SupportsTransition(SizeMode.StretchToWindow));
            
            menuItemBicubic.Checked = (paneImage.Box.InterpolationMode == InterpolationMode.Bicubic);
            menuItemBilinear.Checked = (paneImage.Box.InterpolationMode == InterpolationMode.Bilinear);
            menuItemHigh.Checked = (paneImage.Box.InterpolationMode == InterpolationMode.High);
            menuItemHighQualityBicubic.Checked = (paneImage.Box.InterpolationMode == InterpolationMode.HighQualityBicubic);
            menuItemHighQualityBilinear.Checked = (paneImage.Box.InterpolationMode == InterpolationMode.HighQualityBilinear);
            menuItemLow.Checked = (paneImage.Box.InterpolationMode == InterpolationMode.Low);
            menuItemNearestNeighbor.Checked = (paneImage.Box.InterpolationMode == InterpolationMode.NearestNeighbor);
            menuItemFullScreen.Checked = _pref.FullScreen;

            menuItemThumbnailBrowsing.Checked = (_perspective == Perspectives.ThumbnailBrowsing);
            menuItemImageBrowsing.Checked = (_perspective == Perspectives.ImageBrowsing);
            menuItemFolderBrowsing.Checked = (_perspective == Perspectives.FolderBrowsing);
            menuItemImageEditing.Checked = (_perspective == Perspectives.ImageEditing);

            menuItemZoomIn.Enabled = (paneImage.Box.ImageInfo != null);
            menuItemZoomOut.Enabled = (paneImage.Box.ImageInfo != null);
            menuItemZoom100Percent.Enabled = (paneImage.Box.ImageInfo != null);

            menuItemDelete.Enabled = (paneImage.Box.ImageInfo != null);
            menuItemCopy.Enabled = (paneImage.Box.ImageInfo != null);
            menuItemRotate.Enabled = (paneImage.Box.ImageInfo != null);
            menuItemFlip.Enabled = (paneImage.Box.ImageInfo != null);
            menuItemAsciiArt.Enabled = (paneImage.Box.ImageInfo != null);

            menuItemPrevious.Enabled = (paneImage.Box.ImageInfo != null);
            menuItemNext.Enabled = (paneImage.Box.ImageInfo != null);
            menuItemFirst.Enabled = (paneImage.Box.ImageInfo != null);
            menuItemLast.Enabled = (paneImage.Box.ImageInfo != null);

            menuItemSlideshow.Checked = (_slideshow != null);
            menuItemSlideshow.Enabled = SizeModeBehavior.SupportsTransition(paneImage.Box.SizeMode);
        }

        private void OnDoubleClick(object sender, System.EventArgs e)
        {
            OnOpenImage(null, null);
        }

        // Handler for the Exit command
        void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnDragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            string[] files = (string[]) e.Data.GetData(DataFormats.FileDrop);

            _images.Collect(files[0]);
            _nav.Goto(files[0]);
        }

        private void OnDragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void OnInterpolationBicubic(object sender, System.EventArgs e)
        {
            paneImage.Box.InterpolationMode = InterpolationMode.Bicubic;
        }

        private void OnInterpolationBilinear(object sender, System.EventArgs e)
        {
            paneImage.Box.InterpolationMode = InterpolationMode.Bilinear;
        }

        private void OnInterpolationHighQuality(object sender, System.EventArgs e)
        {
            paneImage.Box.InterpolationMode = InterpolationMode.High;
        }

        private void OnInterpolationHighQualityBicubic(object sender, System.EventArgs e)
        {
            paneImage.Box.InterpolationMode = InterpolationMode.HighQualityBicubic;
        }

        private void OnInterpolationHighQualityBilinear(object sender, System.EventArgs e)
        {
            paneImage.Box.InterpolationMode = InterpolationMode.HighQualityBilinear;
        }

        private void OnInterpolationLow(object sender, System.EventArgs e)
        {
            paneImage.Box.InterpolationMode = InterpolationMode.Low;
        }

        private void OnInterpolationNearestNeighbor(object sender, System.EventArgs e)
        {
            paneImage.Box.InterpolationMode = InterpolationMode.NearestNeighbor;
        }

        private void OnFullScreen(object sender, System.EventArgs e)
        {
            // save states before full-screen
            _savedFormState.Perspective = Perspective;
            _savedFormState.WindowState = WindowState;
            Perspective = Perspectives.FullScreenImageBrowsing;
        }

        private void OnSelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Show selected image in the list view.
            if (paneThumbnail.List.SelectedIndices.Count > 0)
            {
                _nav.Current = paneThumbnail.List.SelectedIndices[0];
            }
        }

        private void CopyImageToClipboard()
        {
            Image image = paneImage.Box.GetSelectedImage();
            if (image != null)
            {
                Clipboard.SetDataObject(image);
            }
        }

        /// <summary>
        /// Trash current image.
        /// </summary>
        private void TrashImage()
        {
            if (MessageBox.Show(String.Format(ResourceString("DeleteImage"), _nav.CurrentImage.ImagePath),
                                 ResourceString("AppTitle"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                _nav.CurrentImage.Trash();
            }
        }

        private void OnListKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
            case Keys.Delete:

                TrashImage();
                break;
            }
        }

        private void OnDeleteImage(object sender, System.EventArgs e)
        {
            TrashImage();
        }

        private void OnCopyImage(object sender, System.EventArgs e)
        {
            CopyImageToClipboard();
        }

        private void OnFirstImage(object sender, System.EventArgs e)
        {
            _nav.GotoFirst();
        }

        private void OnLastImage(object sender, System.EventArgs e)
        {
            _nav.GotoLast();
        }

        private int ZoomLevel
        {
            get
            {
                return _zoomLevel;
            }

            set
            {
                if (value < 0 || value >= _zoomLevels.Length)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _zoomLevel = value;
                paneImage.Box.Zoom = _zoomLevels[_zoomLevel];
                UpdateImageStatus();
            }
        }

        private void OnZoomIn(object sender, System.EventArgs e)
        {
            if (_zoomLevel < _zoomLevels.Length - 1)
            {
                ZoomLevel = _zoomLevel + 1;
            }
        }

        private void OnZoomOut(object sender, System.EventArgs e)
        {
            if (_zoomLevel > 0)
            {
                ZoomLevel = _zoomLevel - 1;
            }
        }

        private void OnZoom100Percent(object sender, System.EventArgs e)
        {
            ZoomLevel = DefaultZoomLevel;
        }

        private void OnToolbarButton(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
        {
            if (e.Button == toolBarButtonZoomIn)
            {
                OnZoomIn(sender, e);
            }
            else if (e.Button == toolBarButtonZoomOut)
            {
                OnZoomOut(sender, e);
            }
            else if (e.Button == toolBarButtonAbout)
            {
                OnHelpAbout(sender, e);
            }
            else if (e.Button == toolBarButtonNext)
            {
                OnNext(sender, e);
            }
            else if (e.Button == toolBarButtonPrevious)
            {
                OnPrevious(sender, e);
            }
            else if (e.Button == toolBarButtonOpen)
            {
                OnOpenImage(sender, e);
            }
        }

        private void OnWalkObjectGraph(object sender, System.EventArgs e)
        {
#if DEBUG
            Utility.WalkGraph(this);        
#endif
		}

        private void OnGcCollect(object sender, System.EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private void ConvertToAsciiArt(string imageFile)
        {
            using (FileStream imageStream = File.Open(imageFile, FileMode.Open))
            {
                String asciiArt = StaticDust.AsciiArt.ConvertImage(imageStream);
                String txtFile = Path.ChangeExtension(Path.GetTempFileName(), "txt");
                using (StreamWriter w = File.CreateText(txtFile))
                {
                    w.Write(asciiArt);
                }

                System.Diagnostics.Process.Start(txtFile);
            }
        }

        private void OnAsciiArt(object sender, System.EventArgs e)
        {
            ConvertToAsciiArt(_nav.CurrentImage.ImagePath);
        }

        private void OnThumbnailBrowsing(object sender, System.EventArgs e)
        {
            Perspective = Perspectives.ThumbnailBrowsing;
        }

        private void OnImageBrowsing(object sender, System.EventArgs e)
        {
            Perspective = Perspectives.ImageBrowsing;
        }

        private void OnFolderBrowsing(object sender, System.EventArgs e)
        {
            Perspective = Perspectives.FolderBrowsing;
        }

        private void OnImageEditing(object sender, System.EventArgs e)
        {
            Perspective = Perspectives.ImageEditing;
        }

        private ResourceManager _rm = new ResourceManager("PViewer.PViewer", Assembly.GetExecutingAssembly());

        private DirectoryImageCollection _images;
        private ImageNavigator _nav;

        private int _filterIndex = -1;

        private ViewerPreference _pref = new ViewerPreference();

        private MRU _mru;
        private static string[] _commandLineArgs;

        private static double[] _zoomLevels = { .07, 0.1, 0.15, 0.2, 0.25, 0.3, 0.5, 0.7, 1, 1.5, 2, 3, 4, 5, 6, 7, 8, 12, 16 };
        public static readonly int DefaultZoomLevel = 8;
        private int _zoomLevel = DefaultZoomLevel;

        enum Perspectives
        {
            ThumbnailBrowsing,
            ImageBrowsing,
            FullScreenImageBrowsing,
            FolderBrowsing,
            ImageEditing,
        }

        private Perspectives _perspective;

        struct SavedFormState
        {
            public Perspectives Perspective;
            public FormWindowState WindowState;
        }
        SavedFormState _savedFormState;

        private Perspectives Perspective
        {
            get { return _perspective; }
            set
            {
                SuspendLayout();

                if (value == Perspectives.FullScreenImageBrowsing)
                {
                    Menu = null;
                    FormBorderStyle = FormBorderStyle.None;
                    WindowState = FormWindowState.Normal;
                    statusBar.Visible = false;
                    toolBar.Visible = false;
                    paneImage.CaptionControl.Visible = false;
                    Bounds = Screen.PrimaryScreen.Bounds;

                    paneImage.Box.Focus();
                }
                else
                {
                    Menu = menuMain;
                    FormBorderStyle = FormBorderStyle.Sizable;
                    if (_perspective == Perspectives.FullScreenImageBrowsing)
                    {
                        WindowState = _savedFormState.WindowState;
                    }
                    statusBar.Visible = true;
                    toolBar.Visible = true;
                    paneImage.CaptionControl.Visible = true;
                }

                switch (value)
                {
                case Perspectives.ThumbnailBrowsing:
                    paneExplorer.Visible = false;
                    paneEffects.Visible = false;
                    splitterRight.Visible = false;
                    paneThumbnail.Visible = true;
                    splitterLeft.Visible = true;
                    break;

                case Perspectives.ImageBrowsing:
                case Perspectives.FullScreenImageBrowsing:
                    paneExplorer.Visible = false;
                    paneEffects.Visible = false;
                    splitterRight.Visible = false;
                    paneThumbnail.Visible = false;
                    splitterLeft.Visible = false;
                    break;

                case Perspectives.FolderBrowsing:
                    paneExplorer.Visible = true;
                    paneEffects.Visible = false;
                    splitterRight.Visible = false;
                    paneThumbnail.Visible = true;
                    splitterLeft.Visible = true;
                    break;

                case Perspectives.ImageEditing:
                    paneExplorer.Visible = false;
                    paneEffects.Visible = true;
                    splitterRight.Visible = true;
                    paneThumbnail.Visible = false;
                    splitterLeft.Visible = false;
                    break;
                }
                
                paneImage.Visible = true;
                paneImage.Box.Focus();

                _perspective = value;

                ResumeLayout();
            }
        }

#region Printing support
        
        private PageSettings pageSettings = new PageSettings();

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            // calculate the rectangle to print image
            Image image = paneImage.Box.ImageInfo.Image;
            Rectangle imageRect = FitSize.FitToWindow(e.MarginBounds.Size, image.Size);
            imageRect.Offset(pageSettings.Margins.Left, pageSettings.Margins.Top);

            e.Graphics.DrawImage(image, imageRect);

            e.HasMorePages = false;
        }

        private void OnPrintPageSetup(object sender, System.EventArgs e)
        {
            pageSetupDialog.PageSettings = pageSettings;
            pageSetupDialog.ShowDialog();
        }

        private void OnPrintPreview(object sender, System.EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings = pageSettings;
            pd.PrintPage += new PrintPageEventHandler(PrintPage);

            printPreviewDialog.Document = pd;
            printPreviewDialog.ShowDialog();
        }

        private void OnPrint(object sender, System.EventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.DefaultPageSettings = pageSettings;
            pd.PrintPage += new PrintPageEventHandler(PrintPage);

            printDialog.Document = pd;
            printDialog.PrinterSettings = new PrinterSettings();
            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                pd.PrinterSettings = printDialog.PrinterSettings;
                pd.Print();
            }
        }
#endregion

#region Burning support

        private void OnBurnToCD(object sender, System.EventArgs e)
        {
            BurningForm burningForm = new BurningForm();
            burningForm.Show();
            burningForm.BurnToCD(_images);
        }

#endregion

#region Slideshow
        class Slideshow
        {
            private Timer _timer;
            private ImageNavigator _nav;

            public Slideshow(int interval, ImageNavigator nav)
            {
                // Setup a timer to show transition.
                _timer = new Timer();
                _timer.Interval = interval;
                _timer.Tick += new EventHandler(OnSlideshowTimer);

                _nav = nav;
            }

            public void Start()
            {
                _timer.Start();
            }

            public void Stop()
            {
                _timer.Stop();
            }

            public void Restart()
            {
                Stop();
                Start();
            }

            private void OnSlideshowTimer(object sender, EventArgs e)
            {
                Stop();
                _nav.GotoNext();
            }
        }

        Slideshow _slideshow;

        private int _msSlideshowInterval = 5000;

        private void OnSlideshow(object sender, System.EventArgs e)
        {
            if (_slideshow != null)
            {
                // stop slideshow
                _slideshow.Stop();
                _slideshow = null;
                paneImage.Box.UseTransition = false;
            }
            else
            {
                // start slideshow
                paneImage.Box.UseTransition = true;
                _slideshow = new Slideshow(_msSlideshowInterval, _nav);
                _slideshow.Start();
            }
        }
    }
#endregion
}
