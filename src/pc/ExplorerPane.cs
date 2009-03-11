using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PViewer
{
	public class ExplorerPane : PViewer.BasePane
	{
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TreeView treeView;

		public ExplorerPane()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ExplorerPane));
            this.treeView = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView.HideSelection = false;
            this.treeView.ImageList = this.imageList;
            this.treeView.Location = new System.Drawing.Point(1, 21);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(214, 226);
            this.treeView.TabIndex = 0;
            this.treeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.AfterSelectTreeNode);
            // 
            // imageList
            // 
            this.imageList.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ExplorerPane
            // 
            this.Controls.Add(this.treeView);
            this.DockPadding.All = 1;
            this.Name = "ExplorerPane";
            this.Load += new System.EventHandler(this.OnLoad);
            this.Controls.SetChildIndex(this.treeView, 0);
            this.ResumeLayout(false);

        }
		#endregion

        private void OnLoad(object sender, System.EventArgs e)
        {
            PopulateTree();
        }

        private void PopulateTree()
        {
            // This routine adds all computer drives to the root nodes of treeView control
            string[] aDrives = Environment.GetLogicalDrives();

            treeView.BeginUpdate();

            foreach (string strDrive in aDrives)
            {
                TreeNode dnMyDrives = new TreeNode(strDrive.Remove(2, 1));

                switch (strDrive)
                {
                case "A:\\":
                case "B:\\":
                    dnMyDrives.SelectedImageIndex = 0;
                    dnMyDrives.ImageIndex = 0;
                    break;

                case "C:\\":

                    // The next statement causes the AfterSelectTreeNode Event to fire once on startup.
                    // This effect can be seen just after intial program load. C:\ node is selected
                    // Automatically on program load, expanding the C:\ treeView node.
                    treeView.SelectedNode = dnMyDrives;
                    dnMyDrives.SelectedImageIndex = 1;
                    dnMyDrives.ImageIndex = 1;

                    break;

                default:
                    dnMyDrives.SelectedImageIndex = 1;
                    dnMyDrives.ImageIndex = 1;
                    break;
                }

                treeView.Nodes.Add(dnMyDrives);
            }
            treeView.EndUpdate();
        }

        private void AfterSelectTreeNode(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            // Get subdirectories from disk, add to treeView control
            AddDirectories(e.Node);

            // if node is collapsed, expand it. This allows single click to open folders.
            treeView.SelectedNode.Expand();
        }

        private void AddDirectories(TreeNode tnSubNode)
        {
            // This method is used to get directories (from disks, or from other directories)

            treeView.BeginUpdate();

            try
            {
                DirectoryInfo diRoot;

                // If drive, get directories from drives
                if (tnSubNode.SelectedImageIndex < 11)
                {
                    diRoot = new DirectoryInfo(tnSubNode.FullPath + "\\");
                }
                    //  Else, get directories from directories
                else
                {
                    diRoot = new DirectoryInfo(tnSubNode.FullPath);
                }
                DirectoryInfo[] dirs = diRoot.GetDirectories();

                // Must clear this first, else the directories will get duplicated in treeview
                tnSubNode.Nodes.Clear();

                // Add the sub directories to the treeView
                foreach (DirectoryInfo dir in dirs)
                {
                    TreeNode subNode = new TreeNode(dir.Name);
                    subNode.ImageIndex = 11;
                    subNode.SelectedImageIndex = 12;
                    tnSubNode.Nodes.Add(subNode);
                }
            }
            catch (Exception)
            {
                ; // Throw Exception when accessing directory: C:\System Volume Information	 // do nothing
            }

            treeView.EndUpdate();
        }

        public TreeView Tree 
        {
            get
            {
                return treeView;
            }
        }

        public void SelectPath(string path)
        {
            SelectPath( Tree.Nodes, path);
        }

        private void SelectPath( TreeNodeCollection nodes, string path)
        {
            if (Tree.SelectedNode != null &&
                String.Compare( Tree.SelectedNode.FullPath, path, true) == 0)
            {
                return ;
            }

            foreach (TreeNode node in nodes)
            {
                if (String.Compare( node.FullPath, path, true) == 0)
                {
                    Tree.SelectedNode = node;

                    node.EnsureVisible();
                    Tree.Focus();
                    return ;
                }
                else if (path.StartsWith(node.FullPath))
                {
                    AddDirectories(node);
                    SelectPath( node.Nodes, path);
                    return ;
                }
            }
        }
	}
}

