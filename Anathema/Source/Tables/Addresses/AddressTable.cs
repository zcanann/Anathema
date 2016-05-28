﻿using Anathema.Source.OS;
using Anathema.Source.OS.Processes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Anathema.Source.Tables.Addresses
{
    /// <summary>
    /// Handles the displaying of results
    /// </summary>
    class AddressTable : IAddressTableModel, IProcessObserver
    {
        public enum TableColumnEnum
        {
            Frozen,
            Description,
            ValueType,
            Address,
            Value
        }

        // Singleton instance of address table
        private static Lazy<AddressTable> AddressTableInstance = new Lazy<AddressTable>(() => { return new AddressTable(); });

        private OSInterface OSInterface;

        private List<AddressItem> AddressItems;

        private Int32 StartReadIndex;
        private Int32 EndReadIndex;

        private AddressTable()
        {
            InitializeProcessObserver();
            AddressItems = new List<AddressItem>();

            Begin();
        }

        public override void OnGUIOpen()
        {
            UpdateAddressTableItemCount();
        }

        public static AddressTable GetInstance()
        {
            return AddressTableInstance.Value;
        }

        ~AddressTable()
        {
            TriggerEnd();
        }

        public void InitializeProcessObserver()
        {
            ProcessSelector.GetInstance().Subscribe(this);
        }

        public void UpdateOSInterface(OSInterface OSInterface)
        {
            this.OSInterface = OSInterface;
        }

        public override void UpdateReadBounds(Int32 StartReadIndex, Int32 EndReadIndex)
        {
            this.StartReadIndex = StartReadIndex;
            this.EndReadIndex = EndReadIndex;
        }

        public override void SetAddressFrozen(Int32 Index, Boolean Activated)
        {
            if (OSInterface == null)
            {
                // Allow disabling even if there is no valid process
                if (!Activated)
                    AddressItems[Index].SetActivationState(Activated);

                return;
            }

            if (Activated)
            {
                Boolean ReadSuccess;
                AddressItems[Index].ResolveAddress(OSInterface);
                AddressItems[Index].Value = OSInterface.Process.Read(AddressItems[Index].ElementType, AddressItems[Index].EffectiveAddress, out ReadSuccess);
            }

            AddressItems[Index].SetActivationState(Activated);
        }

        public override void AddAddressItem(IntPtr BaseAddress, Type ElementType, String Description, IEnumerable<Int32> Offsets = null, Boolean IsHex = false, String Value = null)
        {
            AddressItems.Add(new AddressItem(BaseAddress, ElementType, Description, Offsets, IsHex, Value));

            UpdateAddressTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override void AddAddressItem(AddressItem AddressItem)
        {
            AddressItems.Add(AddressItem);
            UpdateAddressTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override void DeleteTableItems(IEnumerable<Int32> Indicies)
        {
            foreach (Int32 Index in Indicies.OrderByDescending(X => X))
                AddressItems.RemoveAt(Index);

            UpdateAddressTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override AddressItem GetAddressItemAt(Int32 Index)
        {
            return AddressItems[Index];
        }

        public List<AddressItem> GetAddressItems()
        {
            return AddressItems;
        }

        public void SetAddressItems(List<AddressItem> AddressItems)
        {
            this.AddressItems = AddressItems;
            UpdateAddressTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override void SetAddressItemAt(Int32 Index, AddressItem AddressItem)
        {
            // Copy over attributes from the new item (such as to keep this item's color attributes)
            AddressItems[Index].Description = AddressItem.Description;
            AddressItems[Index].ElementType = AddressItem.ElementType;
            AddressItems[Index].BaseAddress = AddressItem.BaseAddress;
            AddressItems[Index].Offsets = AddressItem.Offsets;
            AddressItems[Index].IsHex = AddressItem.IsHex;

            // Force update of value, regardless if frozen or not
            AddressItems[Index].ForceUpdateValue(AddressItem.Value);

            // Write change to memory
            if (AddressItem.Value != null)
            {
                AddressItems[Index].ResolveAddress(OSInterface);
                if (OSInterface != null)
                    OSInterface.Process.Write(AddressItems[Index].ElementType, AddressItems[Index].EffectiveAddress, AddressItems[Index].Value);
            }

            UpdateAddressTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        public override void ReorderItem(Int32 SourceIndex, Int32 DestinationIndex)
        {
            // Bounds checking
            if (SourceIndex < 0 || SourceIndex > AddressItems.Count)
                return;

            // If an item is being removed before the destination, the destination must be shifted
            if (DestinationIndex > SourceIndex)
                DestinationIndex--;

            // Bounds checking
            if (DestinationIndex < 0 || DestinationIndex > AddressItems.Count)
                return;

            AddressItem Item = AddressItems[SourceIndex];
            AddressItems.RemoveAt(SourceIndex);
            AddressItems.Insert(DestinationIndex, Item);
            UpdateAddressTableItemCount();

            TableManager.GetInstance().TableChanged();
        }

        private void UpdateAddressTableItemCount()
        {
            AddressTableEventArgs AddressTableEventArgs = new AddressTableEventArgs();
            AddressTableEventArgs.ItemCount = AddressItems.Count;
            OnEventUpdateAddressTableItemCount(AddressTableEventArgs);
        }

        public override void Begin()
        {
            base.Begin();
        }

        protected override void Update()
        {
            // Freeze addresses
            foreach (AddressItem Item in AddressItems)
            {
                if (Item.GetActivationState())
                {
                    Item.ResolveAddress(OSInterface);

                    if (OSInterface != null && Item.Value != null)
                        OSInterface.Process.Write(Item.ElementType, Item.EffectiveAddress, Item.Value);
                }
            }

            for (Int32 Index = StartReadIndex; Index < EndReadIndex; Index++)
            {
                if (Index < 0 || Index >= AddressItems.Count)
                    continue;

                Boolean ReadSuccess;
                AddressItems[Index].ResolveAddress(OSInterface);

                if (OSInterface != null)
                    AddressItems[Index].Value = OSInterface.Process.Read(AddressItems[Index].ElementType, AddressItems[Index].EffectiveAddress, out ReadSuccess);
            }

            if (AddressItems.Count != 0)
                OnEventReadValues(new AddressTableEventArgs());
        }

        protected override void End() { }

    } // End class

} // End namespace