﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using Binarysharp.MemoryManagement.Memory;

namespace Anathema
{
    public partial class GUILabelerChangeCounter : DockContent, ILabelerChangeCounterView
    {
        LabelerChangeCounterPresenter LabelerChangeCounterPresenter;

        public GUILabelerChangeCounter()
        {
            InitializeComponent();

            LabelerChangeCounterPresenter = new LabelerChangeCounterPresenter(this, new LabelerChangeCounter());

            SetMinChanges();
            SetMaxChanges();
            SetVariableSize();
        }

        public void EventLabelerFinished(List<RemoteRegion> MemoryRegions)
        {
            throw new NotImplementedException();
        }
        
        private void HandleResize()
        {
            MinChangesTrackBar.Width = (this.Width - MinChangesTrackBar.Location.X) / 2;
            MaxChangesTrackBar.Location = new Point(MinChangesTrackBar.Location.X + MinChangesTrackBar.Width, MaxChangesTrackBar.Location.Y);
            MaxChangesTrackBar.Width = MinChangesTrackBar.Width;

            VariableSizeTrackBar.Width = (this.Width - VariableSizeTrackBar.Location.X) / 2;
        }

        private void SetMinChanges()
        {
            Int32 MinChanges = MinChangesTrackBar.Value;
            MinChangesValueLabel.Text = MinChanges.ToString();
            LabelerChangeCounterPresenter.SetMinChanges(MinChanges);
        }

        private void SetMaxChanges()
        {
            Int32 MaxChanges = MaxChangesTrackBar.Value;
            String MaxChangesString = MaxChanges.ToString();

            if (MaxChanges == MaxChangesTrackBar.Maximum)
            {
                MaxChanges = Int32.MaxValue;
                MaxChangesString = "Inf";
            }

            MaxChangesValueLabel.Text = MaxChangesString;
            LabelerChangeCounterPresenter.SetMaxChanges(MaxChanges);
        }

        private void SetVariableSize()
        {
            Int32 VariableSize = (Int32)Math.Pow(2, VariableSizeTrackBar.Value);
            VariableSizeValueLabel.Text = VariableSize.ToString();
            LabelerChangeCounterPresenter.SetMaxChanges(VariableSize);
        }

        #region Events

        private void MinChangesTrackBar_Scroll(Object Sender, EventArgs E)
        {
            if (MinChangesTrackBar.Value > MaxChangesTrackBar.Value)
            {
                MaxChangesTrackBar.Value = MinChangesTrackBar.Value;
                SetMaxChanges();
            }

            SetMinChanges();
        }

        private void MaxChangesTrackBar_Scroll(Object Sender, EventArgs E)
        {
            if (MaxChangesTrackBar.Value < MinChangesTrackBar.Value)
            {
                MinChangesTrackBar.Value = MaxChangesTrackBar.Value;
                SetMinChanges();
            }
            SetMaxChanges();
        }

        private void VariableSizeTrackBar_Scroll(Object Sender, EventArgs E)
        {
            SetVariableSize();
        }

        private void GUILabelerChangeCounter_Resize(Object Sender, EventArgs E)
        {
            HandleResize();
        }

        private void StartScanButton_Click(object sender, EventArgs e)
        {
            LabelerChangeCounterPresenter.BeginLabeler();
        }

        private void StopScanButton_Click(object sender, EventArgs e)
        {
            Snapshot Result = LabelerChangeCounterPresenter.EndLabeler();

            SnapshotManager.GetSnapshotManagerInstance().SaveSnapshot(Result);
        }

        #endregion
    }
}
