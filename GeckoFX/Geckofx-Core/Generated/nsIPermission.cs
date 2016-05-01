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
// Generated by IDLImporter from file nsIPermission.idl
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
    /// This interface defines a "permission" object,
    /// used to specify allowed/blocked objects from
    /// user-specified sites (cookies, images etc).
    /// </summary>
    [ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("cfb08e46-193c-4be7-a467-d7775fb2a31e")]
	public interface nsIPermission
	{
		
		/// <summary>
        /// The name of the host for which the permission is set
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetHostAttribute([MarshalAs(UnmanagedType.LPStruct)] nsAUTF8StringBase aHost);
		
		/// <summary>
        /// The id of the app for which the permission is set.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetAppIdAttribute();
		
		/// <summary>
        /// Whether the permission has been set to a page inside a browser element.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetIsInBrowserElementAttribute();
		
		/// <summary>
        /// a case-sensitive ASCII string, indicating the type of permission
        /// (e.g., "cookie", "image", etc).
        /// This string is specified by the consumer when adding a permission
        /// via nsIPermissionManager.
        /// @see nsIPermissionManager
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void GetTypeAttribute([MarshalAs(UnmanagedType.LPStruct)] nsACStringBase aType);
		
		/// <summary>
        /// The permission (see nsIPermissionManager.idl for allowed values)
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetCapabilityAttribute();
		
		/// <summary>
        /// The expiration type of the permission (session, time-based or none).
        /// Constants are EXPIRE_*, defined in nsIPermissionManager.
        /// @see nsIPermissionManager
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetExpireTypeAttribute();
		
		/// <summary>
        /// The expiration time of the permission (milliseconds since Jan 1 1970
        /// 0:00:00).
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		long GetExpireTimeAttribute();
	}
}
