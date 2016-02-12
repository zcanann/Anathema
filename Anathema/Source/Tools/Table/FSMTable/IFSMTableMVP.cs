﻿using Anathema.MemoryManagement;
using Anathema.MemoryManagement.Memory;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Anathema
{
    delegate void FSMTableEventHandler(Object Sender, TableEventArgs Args);
    class FSMTableEventArgs : EventArgs
    {
        public Int32 ItemCount = 0;
        public Int32 ClearCacheIndex = -1;
    }

    interface IFSMTableView : IView
    {
        // Methods invoked by the presenter (upstream)
        void UpdateFSMTableItemCount(Int32 ItemCount);
    }

    interface IFSMTableModel : IModel, IProcessObserver
    {
        // Events triggered by the model (upstream)
        event FSMTableEventHandler EventClearFSMCacheItem;
        event FSMTableEventHandler EventClearFSMCache;

        // Functions invoked by presenter (downstream)
        Boolean SaveTable(String Path);
        Boolean LoadTable(String Path);

        void OpenFSM(Int32 Index);
        
        FiniteStateMachine GetFSMItemAt(Int32 Index);
    }

    class FSMTablePresenter : Presenter<IFSMTableView, IFSMTableModel>
    {
        protected new IFSMTableView View { get; set; }
        protected new IFSMTableModel Model { get; set; }

        private ListViewCache FSMTableCache;

        public FSMTablePresenter(IFSMTableView View, IFSMTableModel Model) : base(View, Model)
        {
            this.View = View;
            this.Model = Model;

            FSMTableCache = new ListViewCache();

            // Bind events triggered by the model
            Model.EventClearFSMCacheItem += EventClearFSMCacheItem;
            Model.EventClearFSMCache += EventClearFSMCache;
        }

        #region Method definitions called by the view (downstream)

        public Boolean SaveTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.SaveTable(Path);
        }

        public Boolean LoadTable(String Path)
        {
            if (Path == String.Empty)
                return false;

            return Model.LoadTable(Path);
        }

        public ListViewItem GetFSMTableItemAt(Int32 Index)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event definitions for events triggered by the model (upstream)

        private void EventClearFSMCacheItem(Object Sender, TableEventArgs E)
        {
            FSMTableCache.Delete((UInt64)E.ClearCacheIndex);
            View.UpdateFSMTableItemCount(E.ItemCount);
        }

        private void EventClearFSMCache(Object Sender, TableEventArgs E)
        {
            FSMTableCache.FlushCache();
            View.UpdateFSMTableItemCount(E.ItemCount);
        }

        #endregion
    }
}