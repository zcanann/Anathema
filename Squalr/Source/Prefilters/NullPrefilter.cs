﻿namespace Squalr.Source.Prefilters
{
    using Snapshots;
    using SqualrCore.Source.ActionScheduler;
    using SqualrCore.Source.Engine.Processes;
    using System;
    using System.Threading;

    /// <summary>
    /// A debugging prefilter that does not perform any filtering.
    /// </summary>
    internal class NullPrefilter : ScheduledTask, ISnapshotPrefilter
    {
        /// <summary>
        /// Singleton instance of the <see cref="NullPrefilter"/> class.
        /// </summary>
        private static Lazy<ISnapshotPrefilter> snapshotPrefilterInstance = new Lazy<ISnapshotPrefilter>(
            () => { return new NullPrefilter(); },
            LazyThreadSafetyMode.ExecutionAndPublication);

        /// <summary>
        /// Prevents a default instance of the <see cref="NullPrefilter" /> class from being created.
        /// </summary>
        private NullPrefilter() : base("Prefilter", isRepeated: true, trackProgress: true)
        {
        }

        /// <summary>
        /// Gets a singleton instance of the <see cref="NullPrefilter"/> class.
        /// </summary>
        /// <returns>A singleton instance of the class.</returns>
        public static ISnapshotPrefilter GetInstance()
        {
            return NullPrefilter.snapshotPrefilterInstance.Value;
        }

        /// <summary>
        /// Starts the update cycle for this prefilter.
        /// </summary>
        public void BeginPrefilter()
        {
            this.Start();
        }

        /// <summary>
        /// Gets the snapshot generated by the prefilter.
        /// </summary>
        /// <returns>The snapshot generated by the prefilter.</returns>
        public Snapshot GetPrefilteredSnapshot()
        {
            return SnapshotManager.GetInstance().CreateSnapshotFromUsermodeMemory();
        }

        /// <summary>
        /// Recieves a process update.
        /// </summary>
        /// <param name="process">The newly selected process.</param>>
        public void Update(NormalizedProcess process)
        {
        }

        /// <summary>
        /// Starts the prefilter.
        /// </summary>
        protected override void OnBegin()
        {
            base.OnBegin();
        }

        /// <summary>
        /// Updates the prefilter.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token for handling canceled tasks.</param>
        protected override void OnUpdate(CancellationToken cancellationToken)
        {
            this.UpdateProgress(ScheduledTask.MaximumProgress);

            base.OnUpdate(cancellationToken);
        }

        protected override void OnEnd()
        {
            base.OnEnd();
        }
    }
    //// End class
}
//// End namespace