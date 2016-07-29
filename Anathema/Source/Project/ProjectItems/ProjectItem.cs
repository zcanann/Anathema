﻿using Anathema.Source.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = true)]
    [Obfuscation(Exclude = true)]
    [KnownType(typeof(ProjectItem))]
    [KnownType(typeof(FolderItem))]
    [KnownType(typeof(ScriptItem))]
    [KnownType(typeof(AddressItem))]
    [DataContract()]
    public abstract class ProjectItem
    {
        private ProjectItem _Parent;
        [Browsable(false)]
        public ProjectItem Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        private List<ProjectItem> _Children;
        [DataMember()]
        [Browsable(false)]
        public List<ProjectItem> Children
        {
            get { return _Children; }
            set { _Children = value; }
        }

        private String _Description;
        [DataMember()]
        [Category("Properties"), DisplayName("Description"), Description("Description to be shown for the Project Items")]
        public String Description
        {
            get { return _Description; }
            set { _Description = value; UpdateEntryVisual(); }
        }

        [DataMember()]
        [Browsable(false)]
        private UInt32 _TextColorARGB;

        [Category("Properties"), DisplayName("Text Color"), Description("Display Color")]
        public Color TextColor
        {
            get { return Color.FromArgb(unchecked((Int32)(_TextColorARGB))); }
            set { _TextColorARGB = value == null ? 0 : unchecked((UInt32)(value.ToArgb())); UpdateEntryVisual(); }
        }

        [Browsable(false)]
        protected Boolean Activated { get; set; }

        public ProjectItem() : this(String.Empty) { }
        public ProjectItem(String Description)
        {
            // Bypass setters/getters to avoid triggering any GUI updates in constructor
            this._Description = Description == null ? String.Empty : Description;
            this._Parent = null;
            this._Children = new List<ProjectItem>();
            this._TextColorARGB = unchecked((UInt32)SystemColors.ControlText.ToArgb());
            this.Activated = false;
        }

        public virtual void SetActivationState(Boolean Activated)
        {
            this.Activated = Activated;
        }

        public Boolean GetActivationState()
        {
            return Activated;
        }

        public void AddChild(ProjectItem ProjectItem)
        {
            ProjectItem.Parent = this;

            if (Children == null)
                Children = new List<ProjectItem>();

            Children.Add(ProjectItem);
        }

        public void AddSibling(ProjectItem ProjectItem, Boolean After)
        {
            ProjectItem.Parent = this.Parent;

            if (After)
                Parent?.Children?.Insert(Parent.Children.IndexOf(this) + 1, ProjectItem);
            else
                Parent?.Children?.Insert(Parent.Children.IndexOf(this), ProjectItem);

        }

        public void BuildParents(ProjectItem Parent = null)
        {
            this.Parent = Parent;

            foreach (ProjectItem Child in Children)
                Child.BuildParents(this);
        }

        public Boolean HasNode(ProjectItem ProjectItem)
        {
            if (Children.Contains(ProjectItem))
                return true;

            foreach (ProjectItem Child in Children)
            {
                if (Child.HasNode(ProjectItem))
                    return true;
            }

            return false;
        }

        public Boolean RemoveNode(ProjectItem ProjectItem)
        {
            if (ProjectItem == null)
                return false;

            if (Children.Contains(ProjectItem))
            {
                ProjectItem.Parent = null;
                Children.Remove(ProjectItem);
                return true;
            }
            else
            {
                foreach (ProjectItem Child in Children)
                {
                    if (Child.RemoveNode(ProjectItem))
                        return true;
                }
            }

            return false;
        }

        private void UpdateEntryVisual()
        {
            ProjectExplorer.GetInstance().RefreshProjectStructure();
        }

        public abstract void Update(EngineCore EngineCore);

    } // End class

} // End namespace