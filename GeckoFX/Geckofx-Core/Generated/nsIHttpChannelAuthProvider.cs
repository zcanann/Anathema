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
// Generated by IDLImporter from file nsIHttpChannelAuthProvider.idl
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
    /// nsIHttpChannelAuthProvider
    ///
    /// This interface is intended for providing authentication for http-style
    /// channels, like nsIHttpChannel and nsIWebSocket, which implement the
    /// nsIHttpAuthenticableChannel interface.
    ///
    /// When requesting pages AddAuthorizationHeaders MUST be called
    /// in order to get the http cached headers credentials. When the request is
    /// unsuccessful because of receiving either a 401 or 407 http response code
    /// ProcessAuthentication MUST be called and the page MUST be requested again
    /// with the new credentials that the user has provided. After a successful
    /// request, checkForSuperfluousAuth MAY be called, and disconnect MUST be
    /// called.
    /// </summary>
    [ComImport()]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("c68f3def-c7c8-4ee8-861c-eef49a48b702")]
	public interface nsIHttpChannelAuthProvider : nsICancelable
	{
		
		/// <summary>
        /// Call this method to request that this object abort whatever operation it
        /// may be performing.
        ///
        /// @param aReason
        /// Pass a failure code to indicate the reason why this operation is
        /// being canceled.  It is an error to pass a success code.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		new void Cancel(int aReason);
		
		/// <summary>
        /// Initializes the http authentication support for the channel.
        /// Implementations must hold a weak reference of the channel.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Init([MarshalAs(UnmanagedType.Interface)] nsIHttpAuthenticableChannel channel);
		
		/// <summary>
        /// Upon receipt of a server challenge, this function is called to determine
        /// the credentials to send.
        ///
        /// @param httpStatus
        /// the http status received.
        /// @param sslConnectFailed
        /// if the last ssl tunnel connection attempt was or not successful.
        /// @param callback
        /// the callback to be called when it returns NS_ERROR_IN_PROGRESS.
        /// The implementation must hold a weak reference.
        ///
        /// @returns NS_OK if the credentials were got and set successfully.
        /// NS_ERROR_IN_PROGRESS if the credentials are going to be asked to
        /// the user. The channel reference must be
        /// alive until the feedback from
        /// nsIHttpAuthenticableChannel's methods or
        /// until disconnect be called.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ProcessAuthentication(uint httpStatus, [MarshalAs(UnmanagedType.U1)] bool sslConnectFailed);
		
		/// <summary>
        /// Add credentials from the http auth cache.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void AddAuthorizationHeaders();
		
		/// <summary>
        /// Check if an unnecessary(and maybe malicious) url authentication has been
        /// provided.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void CheckForSuperfluousAuth();
		
		/// <summary>
        /// Cancel pending user auth prompts and release the callback and channel
        /// weak references.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void Disconnect(int status);
	}
}
