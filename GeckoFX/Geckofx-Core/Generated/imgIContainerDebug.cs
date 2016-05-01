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
// Generated by IDLImporter from file imgIContainerDebug.idl
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
    /// This interface is used in debug builds (and only there) in
    /// order to let automatic tests running JavaScript access
    /// internal state of imgContainers. This lets us test
    /// things like animation.
    /// </summary>
    [ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("52cbb839-6e63-4a70-b21a-1db4ca706c49")]
	public interface imgIContainerDebug
	{
		
		/// <summary>
        /// The # of frames this imgContainer has been notified about.
        /// That is equal to the # of times the animation timer has
        /// fired, and is usually equal to the # of frames actually
        /// drawn (but actual drawing might be disabled).
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetFramesNotifiedAttribute();
	}
}
