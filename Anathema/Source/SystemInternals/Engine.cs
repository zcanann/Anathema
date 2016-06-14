﻿using Anathema.Source.Graphics;
using Anathema.Source.SystemInternals.Architecture.Assembler;
using Anathema.Source.SystemInternals.Architecture.Disassembler;
using Anathema.Source.SystemInternals.Graphics;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Anathema.Source.SystemInternals.OperatingSystems
{
    /// <summary>
    /// Abstraction of the OS, providing access to assembly functions and target process functions
    /// </summary>
    public class Engine
    {
        public IOperatingSystem Memory { get; private set; }

        public IAssembler Assembler { get; private set; }

        public IDisassembler Disassembler { get; private set; }

        public IGraphicsConnector GraphicsConnector { get; private set; }

        public Engine(Process TargetProcess)
        {
            Memory = OperatingSystemFactory.GetOperatingSystem(TargetProcess);
            Assembler = AssemblerFactory.GetAssembler();
            Disassembler = DisassemblerFactory.GetDisassembler();
            GraphicsConnector = GraphicsConnectorFactory.GetGraphicsConnector(TargetProcess);
        }

        /// <summary>
        /// Determines if the OS is a 32 bit OS
        /// </summary>
        /// <returns></returns>
        public static Boolean IsOS32Bit()
        {
            return !Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines if the OS is a 64 bit OS
        /// </summary>
        /// <returns></returns>
        public static Boolean IsOS64Bit()
        {
            return Environment.Is64BitOperatingSystem;
        }

        /// <summary>
        /// Determines if Anathema is running as 32 bit
        /// </summary>
        /// <returns></returns>
        public static Boolean IsAnathema32Bit()
        {
            return !Environment.Is64BitProcess;
        }

        /// <summary>
        /// Determines if Anathema is running as 64 bit
        /// </summary>
        /// <returns></returns>
        public static Boolean IsAnathema64Bit()
        {
            return Environment.Is64BitProcess;
        }

        /// <summary>
        /// Determines if Anathema is running as 32 bit or 64 bit
        /// </summary>
        /// <returns></returns>
        public static Boolean IsAnthema64Bit()
        {
            return Environment.Is64BitProcess;
        }

        /// <summary>
        /// Determines if a specified process is 32 bit
        /// </summary>
        /// <param name="ProcessHandle"></param>
        /// <returns></returns>
        public static Boolean IsProcess32Bit(IntPtr ProcessHandle)
        {
            // First do the simple check if seeing if the OS is 32 bit, in which case the process wont be 64 bit
            if (!Environment.Is64BitOperatingSystem)
                return true;

            Boolean IsWow64;
            if (!IsWow64Process(ProcessHandle, out IsWow64))
                return false; // Error

            return IsWow64;
        }

        /// <summary>
        /// Determines if a specified process is 64 bit
        /// </summary>
        /// <param name="ProcessHandle"></param>
        /// <returns></returns>
        public static Boolean IsProcess64bit(IntPtr ProcessHandle)
        {
            return !IsProcess32Bit(ProcessHandle);
        }

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsWow64Process([In] IntPtr ProcessHandle, [Out, MarshalAs(UnmanagedType.Bool)] out bool Wow64Process);

    } // End interface

} // End namespace