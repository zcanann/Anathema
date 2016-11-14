﻿namespace Ana.Source.Scanners.BackgroundScans
{
    using Engine;
    using Snapshots;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UserSettings;
    using Utils;
    using Utils.DataStructures;

    /// <summary>
    /// Class to collect all pointers in the running process.
    /// </summary>
    internal class PointerCollector : RepeatedTask
    {
        /// <summary>
        /// Time in milliseconds between scans
        /// </summary>
        private const Int32 RescanTime = 128;

        /// <summary>
        /// The maximum number of regions we parse per scan
        /// </summary>
        private const Int32 RegionLimit = 8;

        /// <summary>
        /// Gets or sets the number of regions processed by this prefilter
        /// </summary>
        private Int64 processedCount;

        /// <summary>
        /// Singleton instance of the <see cref="PointerCollector"/> class.
        /// </summary>
        private static Lazy<PointerCollector> pointerCollectorInstance = new Lazy<PointerCollector>(
            () => { return new PointerCollector(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="PointerCollector" /> class from being created.
        /// </summary>
        private PointerCollector()
        {
            this.FoundPointers = new HashSet<IntPtr>();
            this.ConstructingSet = new HashSet<IntPtr>();
        }

        /// <summary>
        /// The pointers found in the target process
        /// </summary>
        private HashSet<IntPtr> FoundPointers { get; set; }

        /// <summary>
        /// The new found pointers being constructed, which will replace the found pointers upon snapshot parse completion.
        /// </summary>
        private HashSet<IntPtr> ConstructingSet { get; set; }

        /// <summary>
        /// The current snapshot being parsed. A new one is collected after the current one is parsed.
        /// </summary>
        private Snapshot CurrentSnapshot { get; set; }

        public static PointerCollector GetInstance()
        {
            return PointerCollector.pointerCollectorInstance.Value;
        }

        /// <summary>
        /// Gets all found pointers in the external process
        /// </summary>
        /// <returns>A set of all found pointers</returns>
        public HashSet<IntPtr> GetFoundPointers()
        {
            return this.FoundPointers;
        }

        public override void Begin()
        {
            this.UpdateInterval = RescanTime;
            base.Begin();
        }

        protected override void OnUpdate()
        {
            this.GatherPointers();
        }

        /// <summary>
        /// Called when the repeated task completes
        /// </summary>
        protected override void OnEnd()
        {
            base.OnEnd();
        }

        /// <summary>
        /// Gradually gathers pointers in the running process
        /// </summary>
        private void GatherPointers()
        {
            Boolean isOpenedProcess32Bit = EngineCore.GetInstance().Processes.IsOpenedProcess32Bit();
            dynamic invalidPointerMin = isOpenedProcess32Bit ? (UInt32)UInt16.MaxValue : (UInt64)UInt16.MaxValue;
            dynamic invalidPointerMax = isOpenedProcess32Bit ? Int32.MaxValue : Int64.MaxValue;
            ConcurrentHashSet<IntPtr> foundPointers = new ConcurrentHashSet<IntPtr>();

            // Test for conditions where we set the final found set and take a new snapshot to parse
            if (this.CurrentSnapshot == null || this.CurrentSnapshot.GetRegionCount() <= 0 || this.processedCount >= this.CurrentSnapshot.GetRegionCount())
            {
                this.processedCount = 0;
                this.CurrentSnapshot = SnapshotManager.GetInstance().CollectSnapshot(useSettings: false, usePrefilter: false).Clone();
                this.FoundPointers = this.ConstructingSet;
                this.ConstructingSet = new HashSet<IntPtr>();
            }

            List<SnapshotRegion> sortedRegions = new List<SnapshotRegion>(this.CurrentSnapshot.GetSnapshotRegions().OrderBy(x => x.TimeSinceLastRead));

            // Process the allowed amount of chunks from the priority queue
            Parallel.For(
                0,
                Math.Min(sortedRegions.Count, PointerCollector.RegionLimit),
                SettingsViewModel.GetInstance().ParallelSettings,
                (index) =>
            {
                Interlocked.Increment(ref this.processedCount);

                SnapshotRegion region = sortedRegions[index];
                Boolean success;

                // Set to type of a pointer
                region.SetElementType(EngineCore.GetInstance().Processes.IsOpenedProcess32Bit() ? typeof(UInt32) : typeof(UInt64));

                // Enforce 4-byte alignment of pointers
                region.SetAlignment(sizeof(Int32));

                // Read current page data for chunk
                region.ReadAllRegionMemory(out success);

                // Read failed; Deallocated page
                if (!success)
                {
                    return;
                }

                if (!region.HasValues())
                {
                    return;
                }

                foreach (SnapshotElement element in region)
                {
                    // Enforce user mode memory pointers
                    if (element.LessThanValue(invalidPointerMin) || element.GreaterThanValue(invalidPointerMax))
                    {
                        continue;
                    }

                    // Enforce 4-byte alignment of destination
                    if (element.GetCurrentValue() % 4 != 0)
                    {
                        continue;
                    }

                    IntPtr Value = new IntPtr(element.GetCurrentValue());

                    // Check if it is possible that this pointer is valid, if so keep it
                    if (this.CurrentSnapshot.ContainsAddress(Value))
                    {
                        foundPointers.Add(Value);
                    }
                }

                // Clear the saved values, we do not need them now
                region.SetCurrentValues(null);
            });

            IEnumerable<IntPtr> pointers = foundPointers.ToList();
            this.ConstructingSet.UnionWith(pointers);
            this.FoundPointers.UnionWith(pointers);
        }
    }
    //// End class
}
//// End namespace