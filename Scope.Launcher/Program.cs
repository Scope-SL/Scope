// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Scope SL">
// Copyright (c) Scope SL. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Scope.Launcher
{
    using System;
    using System.Runtime.InteropServices;
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            [DllImport("UnityPlayer.dll", CharSet = CharSet.Unicode)]
            static extern IntPtr UnityMain(IntPtr instance, IntPtr previous_instance, string[] command_line, int show_command_line);

            UnityMain(Marshal.GetHINSTANCE(typeof(Program).Module), IntPtr.Zero, args, 1);
        }
    }
}