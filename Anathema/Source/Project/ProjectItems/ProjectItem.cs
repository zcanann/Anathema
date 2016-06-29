﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Runtime.Serialization;

namespace Anathema.Source.Project.ProjectItems
{
    [Obfuscation(ApplyToMembers = false)]
    [Obfuscation(Exclude = true)]
    [DataContract()]
    public class ProjectItem
    {
        [Obfuscation(Exclude = true)]
        [DataMember()]
        public ProjectItem Parent
        {
            [Obfuscation(Exclude = true)]
            get;
            [Obfuscation(Exclude = true)]
            set;
        }

        [Obfuscation(Exclude = true)]
        [DataMember()]
        public IEnumerable<ProjectItem> Children
        {
            [Obfuscation(Exclude = true)]
            get;
            [Obfuscation(Exclude = true)]
            set;
        }

        [Obfuscation(Exclude = true)]
        [DataMember()]
        public String Description { get; set; }

        [Obfuscation(Exclude = true)]
        [DataMember()]
        public Int32 TextColorARGB
        {
            [Obfuscation(Exclude = true)]
            get;
            [Obfuscation(Exclude = true)]
            set;
        }

        [Obfuscation(Exclude = true)]
        public Color TextColor
        {
            [Obfuscation(Exclude = true)]
            get { return Color.FromArgb(TextColorARGB); }
            [Obfuscation(Exclude = true)]
            set { TextColorARGB = value == null ? 0 : value.ToArgb(); }
        }

        [Obfuscation(Exclude = true)]
        protected Boolean Activated;

        public ProjectItem()
        {
            Parent = null;
            Children = null;
            TextColor = SystemColors.ControlText;
            Activated = false;
        }

        [Obfuscation(Exclude = true)]
        public virtual void SetActivationState(Boolean Activated)
        {
            this.Activated = Activated;
        }

        [Obfuscation(Exclude = true)]
        public Boolean GetActivationState()
        {
            return Activated;
        }

    } // End class

} // End namespace