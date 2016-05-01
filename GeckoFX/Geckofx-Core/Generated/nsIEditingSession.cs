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
// Generated by IDLImporter from file nsIEditingSession.idl
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
	[Guid("24f3f4da-18a4-448d-876d-7360fefac029")]
	public interface nsIEditingSession
	{
		
		/// <summary>
        /// Status after editor creation and document loading
        /// Value is one of the above error codes
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		uint GetEditorStatusAttribute();
		
		/// <summary>
        /// Make this window editable
        /// @param aWindow nsIDOMWindow, the window the embedder needs to make editable
        /// @param aEditorType string, "html" "htmlsimple" "text" "textsimple"
        /// @param aMakeWholeDocumentEditable if PR_TRUE make the whole document in
        /// aWindow editable, otherwise it's the
        /// embedder who should make the document
        /// (or part of it) editable.
        /// @param aInteractive if PR_FALSE turn off scripting and plugins
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void MakeWindowEditable([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow window, [MarshalAs(UnmanagedType.LPStr)] string aEditorType, [MarshalAs(UnmanagedType.U1)] bool doAfterUriLoad, [MarshalAs(UnmanagedType.U1)] bool aMakeWholeDocumentEditable, [MarshalAs(UnmanagedType.U1)] bool aInteractive);
		
		/// <summary>
        /// Test whether a specific window has had its editable flag set; it may have an editor
        /// now, or will get one after the uri load.
        ///
        /// Use this, passing the content root window, to test if we've set up editing
        /// for this content.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool WindowIsEditable([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow window);
		
		/// <summary>
        /// Get the editor for this window. May return null
        /// </summary>
		[return: MarshalAs(UnmanagedType.Interface)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		nsIEditor GetEditorForWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow window);
		
		/// <summary>
        /// Setup editor and related support objects
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetupEditorOnWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow window);
		
		/// <summary>
        /// Destroy editor and related support objects
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void TearDownEditorOnWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow window);
		
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void SetEditorOnControllers([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow, [MarshalAs(UnmanagedType.Interface)] nsIEditor aEditor);
		
		/// <summary>
        /// Disable scripts and plugins in aWindow.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void DisableJSAndPlugins([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow);
		
		/// <summary>
        /// Restore JS and plugins (enable/disable them) according to the state they
        /// were before the last call to disableJSAndPlugins.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void RestoreJSAndPlugins([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow);
		
		/// <summary>
        /// Removes all the editor's controllers/listeners etc and makes the window
        /// uneditable.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void DetachFromWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow);
		
		/// <summary>
        /// Undos detachFromWindow(), reattaches this editing session/editor
        /// to the window.
        /// </summary>
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		void ReattachToWindow([MarshalAs(UnmanagedType.Interface)] nsIDOMWindow aWindow);
		
		/// <summary>
        /// Whether this session has disabled JS and plugins.
        /// </summary>
		[return: MarshalAs(UnmanagedType.U1)]
		[MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
		bool GetJsAndPluginsDisabledAttribute();
	}
	
	/// <summary>nsIEditingSessionConsts </summary>
	public class nsIEditingSessionConsts
	{
		
		// <summary>
        // Error codes when we fail to create an editor
        // is placed in attribute editorStatus
        // </summary>
		public const long eEditorOK = 0;
		
		// 
		public const long eEditorCreationInProgress = 1;
		
		// 
		public const long eEditorErrorCantEditMimeType = 2;
		
		// 
		public const long eEditorErrorFileNotFound = 3;
		
		// 
		public const long eEditorErrorCantEditFramesets = 8;
		
		// 
		public const long eEditorErrorUnknown = 9;
	}
}
