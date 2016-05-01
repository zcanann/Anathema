// --------------------------------------------------------------------------------------------
// Version: MPL 1.1/GPL 2.0/LGPL 2.1
// 
// The contents of this file are subject to the Mozilla Public License Version
// 1.1 (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at
// http://www.mozilla.org/MPL/
// 
// Software distributed under the License is distributed on an "AS IS" basis,
// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License
// for the specific language governing rights and limitations under the
// License.
// 
// <remarks>
// Generated by IDLImporter from file nsITelemetry.idl
// 
// You should use these interfaces when you access the COM objects defined in the mentioned
// IDL/IDH file.
// </remarks>
// --------------------------------------------------------------------------------------------
namespace Gecko
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;


    /// <summary>
    ///This Source Code Form is subject to the terms of the Mozilla Public
    /// License, v. 2.0. If a copy of the MPL was not distributed with this
    /// file, You can obtain one at http://mozilla.org/MPL/2.0/. </summary>
    [ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("3d3b9075-5549-4244-9c08-b64fefa1dd60")]
	public interface nsIFetchTelemetryDataCallback
	{
		
		/// <summary>
        ///This Source Code Form is subject to the terms of the Mozilla Public
        /// License, v. 2.0. If a copy of the MPL was not distributed with this
        /// file, You can obtain one at http://mozilla.org/MPL/2.0/. </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Complete();
	}
	
	/// <summary>nsITelemetry </summary>
	[ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("4e4bfc35-dac6-4b28-ade4-7e45760051d5")]
	public interface nsITelemetry
	{
		
		/// <summary>
        /// An object containing a snapshot from all of the currently registered histograms.
        /// { name1: {data1}, name2:{data2}...}
        /// where data is consists of the following properties:
        /// min - Minimal bucket size
        /// max - Maximum bucket size
        /// histogram_type - HISTOGRAM_EXPONENTIAL, HISTOGRAM_LINEAR, or HISTOGRAM_BOOLEAN
        /// counts - array representing contents of the buckets in the histogram
        /// ranges -  an array with calculated bucket sizes
        /// sum - sum of the bucket contents
        /// static - true for histograms defined in TelemetryHistograms.h, false for ones defined with newHistogram
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetHistogramSnapshotsAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// The amount of time, in milliseconds, that the last session took
        /// to shutdown.  Reads as 0 to indicate failure.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetLastShutdownDurationAttribute();
		
		/// <summary>
        /// The number of failed profile lock attempts that have occurred prior to
        /// successfully locking the profile
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetFailedProfileLockCountAttribute();
		
		/// <summary>
        /// An object containing information about slow SQL statements.
        ///
        /// {
        /// mainThread: { "sqlString1": [<hit count>, <total time>], "sqlString2": [...], ... },
        /// otherThreads: { "sqlString3": [<hit count>, <total time>], "sqlString4": [...], ... }
        /// }
        ///
        /// where:
        /// mainThread: Slow statements that executed on the main thread
        /// otherThreads: Slow statements that executed on a non-main thread
        /// sqlString - String of the offending statement (see note)
        /// hit count - The number of times this statement required longer than the threshold time to execute
        /// total time - The sum of all execution times above the threshold time for this statement
        ///
        /// Note that dynamic SQL strings and SQL strings executed against addon DBs could contain private information.
        /// This property represents such SQL as aggregate database-level stats and the sqlString contains the database
        /// filename instead.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetSlowSQLAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// See slowSQL above.
        ///
        /// An object containing full strings of every slow SQL statement if toolkit.telemetry.debugSlowSql = true
        /// The returned SQL strings may contain private information and should not be reported to Telemetry.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetDebugSlowSQLAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// A number representing the highest number of concurrent threads
        /// reached during this session.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetMaximalNumberOfConcurrentThreadsAttribute();
		
		/// <summary>
        /// An array of chrome hang reports. Each element is a hang report represented
        /// as an object containing the hang duration, call stack PCs and information
        /// about modules in memory.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetChromeHangsAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// An array of thread hang stats,
        /// [<thread>, <thread>, ...]
        /// <thread> represents a single thread,
        /// {"name": "<name>",
        /// "activity": <time>,
        /// "hangs": [<hang>, <hang>, ...]}
        /// <time> represents a histogram of time intervals in milliseconds,
        /// with the same format as histogramSnapshots
        /// <hang> represents a particular hang,
        /// {"stack": <stack>, "histogram": <time>}
        /// <stack> represents the hang's stack,
        /// ["<frame_0>", "<frame_1>", ...]
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetThreadHangStatsAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// An object with two fields: memoryMap and stacks.
        /// * memoryMap is a list of loaded libraries.
        /// * stacks is a list of stacks. Each stack is a list of pairs of the form
        /// [moduleIndex, offset]. The moduleIndex is an index into the memoryMap and
        /// offset is an offset in the library at memoryMap[moduleIndex].
        /// This format is used to make it easier to send the stacks to the
        /// symbolication server.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetLateWritesAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// Returns an array whose values are the names of histograms defined
        /// in Histograms.json.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RegisteredHistograms(ref uint count, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=0)] ref string[] histograms);
		
		/// <summary>
        /// Create and return a histogram.  Parameters:
        ///
        /// @param name Unique histogram name
        /// @param expiration Expiration version
        /// @param min - Minimal bucket size
        /// @param max - Maximum bucket size
        /// @param bucket_count - number of buckets in the histogram.
        /// @param type - HISTOGRAM_EXPONENTIAL, HISTOGRAM_LINEAR or HISTOGRAM_BOOLEAN
        /// The returned object has the following functions:
        /// add(int) - Adds an int value to the appropriate bucket
        /// snapshot() - Returns a snapshot of the histogram with the same data fields as in histogramSnapshots()
        /// clear() - Zeros out the histogram's buckets and sum
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal NewHistogram([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase name, [MarshalAs(UnmanagedType.LPStruct)] nsACStringBase expiration, uint min, uint max, uint bucket_count, uint histogram_type, System.IntPtr jsContext);
		
		/// <summary>
        /// Create a histogram using the current state of an existing histogram.  The
        /// existing histogram must be registered in TelemetryHistograms.h.
        ///
        /// @param name Unique histogram name
        /// @param existing_name Existing histogram name
        /// The returned object has the same functions as a histogram returned from newHistogram.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal HistogramFrom([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase name, [MarshalAs(UnmanagedType.LPStruct)] nsACStringBase existing_name, System.IntPtr jsContext);
		
		/// <summary>
        /// Same as newHistogram above, but for histograms registered in TelemetryHistograms.h.
        ///
        /// @param id - unique identifier from TelemetryHistograms.h
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetHistogramById([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase id, System.IntPtr jsContext);
		
		/// <summary>
        /// Set this to false to disable gathering of telemetry statistics.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetCanRecordAttribute();
		
		/// <summary>
        /// Set this to false to disable gathering of telemetry statistics.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetCanRecordAttribute([MarshalAs(UnmanagedType.U1)] bool aCanRecord);
		
		/// <summary>
        /// A flag indicating whether Telemetry can submit official results.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetCanSendAttribute();
		
		/// <summary>
        /// Register a histogram for an addon.  Throws an error if the
        /// histogram name has been registered previously.
        ///
        /// @param addon_id - Unique ID of the addon
        /// @param name - Unique histogram name
        /// @param min - Minimal bucket size
        /// @param max - Maximum bucket size
        /// @param bucket_count - number of buckets in the histogram
        /// @param histogram_type - HISTOGRAM_EXPONENTIAL, HISTOGRAM_LINEAR, or
        /// HISTOGRAM_BOOLEAN
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RegisterAddonHistogram([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase addon_id, [MarshalAs(UnmanagedType.LPStruct)] nsACStringBase name, uint min, uint max, uint bucket_count, uint histogram_type);
		
		/// <summary>
        /// Return a histogram previously registered via
        /// registerAddonHistogram.  Throws an error if the id/name combo has
        /// not been registered via registerAddonHistogram.
        ///
        /// @param addon_id - Unique ID of the addon
        /// @param name - Registered histogram name
        ///
        /// The returned object has the same functions as a histogram returned
        /// from newHistogram.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetAddonHistogram([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase addon_id, [MarshalAs(UnmanagedType.LPStruct)] nsACStringBase name, System.IntPtr jsContext);
		
		/// <summary>
        /// Delete all histograms associated with the given addon id.
        ///
        /// @param addon_id - Unique ID of the addon
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void UnregisterAddonHistograms([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase addon_id);
		
		/// <summary>
        /// An object containing a snapshot from all of the currently
        /// registered addon histograms.
        /// { addon-id1 : data1, ... }
        ///
        /// where data is an object whose properties are the names of the
        /// addon's histograms and whose corresponding values are as in
        /// histogramSnapshots.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetAddonHistogramSnapshotsAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// Read data from the previous run. After the callback is called, the last
        /// shutdown time is available in lastShutdownDuration and any late
        /// writes in lateWrites.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void AsyncFetchTelemetryData([MarshalAs(UnmanagedType.Interface)] nsIFetchTelemetryDataCallback aCallback);
		
		/// <summary>
        /// Get statistics of file IO reports, null, if not recorded.
        ///
        /// The statistics are returned as an object whose propoerties are the names
        /// of the files that have been accessed and whose corresponding values are
        /// arrays of size three, representing startup, normal, and shutdown stages.
        /// Each stage's entry is either null or an array with the layout
        /// [total_time, #creates, #reads, #writes, #fsyncs, #stats]
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		Gecko.JsVal GetFileIOReportsAttribute(System.IntPtr jsContext);
		
		/// <summary>
        /// Return the number of seconds since process start using monotonic
        /// timestamps (unaffected by system clock changes).
        /// @throws NS_ERROR_NOT_AVAILABLE if TimeStamp doesn't have the data.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		double MsSinceProcessStart();
	}
	
	/// <summary>nsITelemetryConsts </summary>
	public class nsITelemetryConsts
	{
		
		// <summary>
        // Histogram types:
        // HISTOGRAM_EXPONENTIAL - buckets increase exponentially
        // HISTOGRAM_LINEAR - buckets increase linearly
        // HISTOGRAM_BOOLEAN - For storing 0/1 values
        // HISTOGRAM_FLAG - For storing a single value; its count is always == 1.
        // </summary>
		public const ulong HISTOGRAM_EXPONENTIAL = 0;
		
		// 
		public const ulong HISTOGRAM_LINEAR = 1;
		
		// 
		public const ulong HISTOGRAM_BOOLEAN = 2;
		
		// 
		public const ulong HISTOGRAM_FLAG = 3;
	}
}
