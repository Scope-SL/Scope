using System;
using System.Runtime.InteropServices;

namespace Scope.Launcher
{
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