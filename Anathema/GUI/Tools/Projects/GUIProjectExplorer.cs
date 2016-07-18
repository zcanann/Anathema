﻿using Aga.Controls.Tree;
using Aga.Controls.Tree.NodeControls;
using Anathema.Source.Project;
using Anathema.Source.Project.ProjectItems;
using Anathema.Source.Utils;
using Anathema.Source.Utils.Caches;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.MVP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Anathema.GUI
{
    public partial class GUIProjectExplorer : DockContent, IProjectExplorerView
    {
        private ProjectExplorerPresenter ProjectExplorerPresenter;
        private BiDictionary<ProjectItem, ProjectNode> NodeCache;
        private TreeModel ProjectTree;
        private Object AccessLock;

        public GUIProjectExplorer()
        {
            InitializeComponent();

            EntryCheckBox.IsEditEnabledValueNeeded += CheckIndex;

            NodeCache = new BiDictionary<ProjectItem, ProjectNode>();
            ProjectTree = new TreeModel();
            AccessLock = new Object();

            ProjectExplorerTreeView.Model = ProjectTree;

            ProjectExplorerPresenter = new ProjectExplorerPresenter(this, ProjectExplorer.GetInstance());
        }

        void CheckIndex(Object Sender, NodeControlValueEventArgs E)
        {
            E.Value = true;
        }

        public void RefreshStructure()
        {
            if (ProjectExplorerPresenter == null)
                return;

            using (TimedLock.Lock(AccessLock))
            {
                ControlThreadingHelper.InvokeControlAction(ProjectExplorerTreeView, () =>
                {
                    ProjectExplorerTreeView.BeginUpdate();
                    ProjectTree.Nodes.Clear();
                    NodeCache.Clear();

                    BuildNodes(ProjectExplorerPresenter.GetProjectRoot());

                    ProjectExplorerTreeView.EndUpdate();
                });
            }
        }

        private void BuildNodes(ProjectItem ProjectItem)
        {
            if (ProjectItem == null)
                return;

            Image Image = null;

            if (ProjectItem is AddressItem)
            {
                Image = new Bitmap(Properties.Resources.CollectValues);
            }
            else if (ProjectItem is FolderItem)
            {
                Image = new Bitmap(Properties.Resources.Open);
            }

            ProjectNode ProjectNode = new ProjectNode(ProjectItem.Description);
            ProjectNode.ProjectItem = ProjectItem;
            ProjectNode.EntryIcon = Image;

            ProjectTree.Nodes.Add(ProjectNode);
            NodeCache.Add(ProjectItem, ProjectNode);

            foreach (ProjectItem Child in ProjectItem)
                BuildNodes(Child);
        }

        private ProjectItem GetProjectItemFromNode(TreeNodeAdv TreeNodeAdv)
        {
            Node Node = ProjectTree.FindNode(ProjectExplorerTreeView.GetPath(TreeNodeAdv));

            if (Node == null || !typeof(ProjectNode).IsAssignableFrom(Node.GetType()))
                return null;

            ProjectNode ProjectNode = (ProjectNode)Node;
            ProjectItem ProjectItem = ProjectNode.ProjectItem;

            return ProjectItem;
        }

        #region Events

        private void ProjectExplorerTreeView_NodeMouseDoubleClick(Object Sender, TreeNodeAdvMouseEventArgs E)
        {
            ProjectItem ProjectItem = GetProjectItemFromNode(E?.Node);

            if (ProjectItem == null)
                return;

            if (ProjectItem is AddressItem)
            {
                EditAddressEntry(null, 0);
            }
            else if (ProjectItem is FolderItem)
            {

            }
            else if (ProjectItem is ScriptItem)
            {

            }
        }


        private void AddressToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewAddressItem();
        }

        private void FolderToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewFolderItem();
        }

        #endregion

        /// <summary>
        /// DEPRECATED: Will replace property editors with a standalone property editor class
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="ColumnIndex"></param>
        private void EditAddressEntry(ProjectNode Target, Int32 ColumnIndex)
        {
            GUIAddressEditor GUIAddressEditor;

            using (TimedLock.Lock(AccessLock))
            {
                List<Int32> Indicies = new List<Int32>();

                foreach (TreeNodeAdv Index in ProjectExplorerTreeView.SelectedNodes)
                    Indicies.Add(Index.Index);

                if (Indicies.Count == 0)
                    return;

                GUIAddressEditor = new GUIAddressEditor(Indicies[0], Indicies);
            }

            // Create editor for this entry
            GUIAddressEditor.ShowDialog(this);
        }

        public void AddNewAddressItem()
        {
            ProjectExplorerPresenter.AddNewAddressItem(GetSelectedItem());
        }

        public void AddNewFolderItem()
        {
            ProjectExplorerPresenter.AddNewFolderItem(GetSelectedItem());
        }

        private ProjectItem GetSelectedItem()
        {
            Node SelectedNode = ProjectTree.FindNode(ProjectExplorerTreeView.GetPath(ProjectExplorerTreeView.SelectedNode));
            ProjectItem SelectedItem = null;

            if (SelectedNode != null && typeof(ProjectNode).IsAssignableFrom(SelectedNode.GetType()))
            {
                if (NodeCache.Reverse.ContainsKey((ProjectNode)SelectedNode))
                    SelectedItem = NodeCache.Reverse[(ProjectNode)SelectedNode];
            }

            return SelectedItem;
        }

        private void DeleteAddressTableEntries(Int32 StartIndex, Int32 EndIndex)
        {
            using (TimedLock.Lock(AccessLock))
            {
                if (ProjectExplorerTreeView.SelectedNodes == null || ProjectExplorerTreeView.SelectedNodes.Count <= 0)
                    return;

                List<Int32> Nodes = new List<Int32>();
                ProjectExplorerTreeView.SelectedNodes.ForEach(X => Nodes.Add(X.Index));
                ProjectExplorerPresenter.DeleteProjectItems(Nodes);
            }
        }

        #region Events

        private void AddAddressButton_Click(Object Sender, EventArgs E)
        {
            AddNewAddressItem();
        }

        private Point LastRightClickLocation = Point.Empty;

        private void AddressTableListView_MouseClick(Object Sender, MouseEventArgs E)
        {
            if (E.Button == MouseButtons.Right)
                LastRightClickLocation = E.Location;

            TreeNodeAdv ListViewItem = ProjectExplorerTreeView.GetNodeAt(E.Location);

            if (ListViewItem == null)
                return;

            // if (E.X < (ListViewItem.Bounds.Left + 16))
            //     AddressTablePresenter.SetAddressFrozen(ListViewItem.Index, !ListViewItem.Checked);  // (Has to be negated, click happens before check change)
        }

        private void AddressTableListView_KeyPress(Object Sender, KeyPressEventArgs E)
        {
            if (E.KeyChar != ' ')
                return;

            /*Boolean FreezeState = AddressTableListView.SelectedIndices == null ? false : !AddressTableListView.Items[AddressTableListView.SelectedIndices[0]].Checked;
            foreach (Int32 Index in AddressTableListView.SelectedIndices)
                AddressTablePresenter.SetAddressFrozen(Index, FreezeState);*/
        }

        private void ToggleFreezeToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            /*Boolean FreezeState = AddressTableListView.SelectedIndices == null ? false : !AddressTableListView.Items[AddressTableListView.SelectedIndices[0]].Checked;
            foreach (Int32 Index in AddressTableListView.SelectedIndices)
                AddressTablePresenter.SetAddressFrozen(Index, FreezeState);*/
        }

        private void AddressTableListView_MouseDoubleClick(Object Sender, MouseEventArgs E)
        {
            /*
            ListViewItem SelectedItem;
            Int32 ColumnIndex;

            ListViewHitTestInfo HitTest = AddressTableTreeView.HitTest(E.Location);
            SelectedItem = HitTest.Item;
            ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            // Do not bring up edit menu on double clicks to checkbox
            if (ColumnIndex == AddressTableListView.Columns.IndexOf(FrozenHeader))
                return;

            if (SelectedItem == null)
                return;

            EditAddressTableEntry(SelectedItem.Index, ColumnIndex);
            */
        }

        private void EditAddressEntryToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            /*
                ListViewItem SelectedItem;
                Int32 ColumnIndex;

                ListViewHitTestInfo HitTest = AddressTableListView.HitTest(LastRightClickLocation);
                SelectedItem = HitTest.Item;
                ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

                if (SelectedItem == null)
                    return;

                EditAddressTableEntry(SelectedItem.Index, ColumnIndex);
            */
        }

        private void DeleteSelectionToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            /*
            ListViewItem SelectedItem;
            Int32 ColumnIndex;

            ListViewHitTestInfo HitTest = AddressTableListView.HitTest(LastRightClickLocation);
            SelectedItem = HitTest.Item;
            ColumnIndex = HitTest.Item.SubItems.IndexOf(HitTest.SubItem);

            if (SelectedItem == null)
                return;

            DeleteAddressTableEntries(SelectedItem.Index, SelectedItem.Index);

            AddressTableListView.SelectedIndices.Clear();
            */
        }

        private void AddNewAddressToolStripMenuItem_Click(Object Sender, EventArgs E)
        {
            AddNewAddressItem();
        }

        private void AddressTableContextMenuStrip_Opening(Object Sender, CancelEventArgs E)
        {
            // using (TimedLock.Lock(AccessLock))
            {
                /*
                ListViewHitTestInfo HitTest = AddressTableListView.HitTest(AddressTableListView.PointToClient(MousePosition));
                ListViewItem SelectedItem = HitTest.Item;

                if (SelectedItem == null)
                {
                    ToggleFreezeToolStripMenuItem.Enabled = false;
                    EditAddressEntryToolStripMenuItem.Enabled = false;
                    DeleteSelectionToolStripMenuItem.Enabled = false;
                    AddNewAddressToolStripMenuItem.Enabled = true;
                }
                else
                */
                {
                    ToggleFreezeToolStripMenuItem.Enabled = true;
                    EditAddressEntryToolStripMenuItem.Enabled = true;
                    DeleteSelectionToolStripMenuItem.Enabled = true;
                    AddNewAddressToolStripMenuItem.Enabled = true;
                }
            }
        }

        private ListViewItem DraggedItem;
        private void AddressTableListView_ItemDrag(Object Sender, ItemDragEventArgs E)
        {
            DraggedItem = (ListViewItem)E.Item;
            DoDragDrop(E.Item, DragDropEffects.All);
        }

        private void AddressTableListView_DragOver(Object Sender, DragEventArgs E)
        {
            E.Effect = DragDropEffects.All;
        }

        private void AddressTableListView_DragDrop(Object Sender, DragEventArgs E)
        {
            // using (TimedLock.Lock(AccessLock))
            {
                /*
                ListViewHitTestInfo HitTest = AddressTableListView.HitTest(AddressTableListView.PointToClient(new Point(E.X, E.Y)));
                ListViewItem SelectedItem = HitTest.Item;

                if (DraggedItem == null || DraggedItem == SelectedItem)
                    return;

                if ((SelectedItem != null && SelectedItem.GetType() != typeof(ListViewItem)) || DraggedItem.GetType() != typeof(ListViewItem))
                    return;

                AddressTablePresenter.ReorderItem(DraggedItem.Index, SelectedItem == null ? AddressTableListView.Items.Count : SelectedItem.Index);
                */
            }
        }

        #endregion

    } // End class

} // End namespace