using System;
using System.Runtime.InteropServices;

namespace ArrangeIconsByShape.DesktopIconsManager.Windows.Core
{
    /// <summary>
    /// kernel32.dll wrapper
    /// </summary>
    internal static class Kernel32
    {
        private const string Kernel32Dll = "kernel32.dll";

        [DllImport(Kernel32Dll)]
        internal static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, bool inherit_handle, uint pid);

        [DllImport(Kernel32Dll)]
        internal static extern bool CloseHandle(IntPtr handle);

        [DllImport(Kernel32Dll)]
        internal static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport(Kernel32Dll)]
        internal static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, FreeType dwFreeType);

        [DllImport(Kernel32Dll)]
        internal static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint lpNumberOfBytesWritten);

        [DllImport(Kernel32Dll)]
        internal static extern int ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] IntPtr buffer, int size, ref uint lpNumberOfBytesRead);
    }
}
