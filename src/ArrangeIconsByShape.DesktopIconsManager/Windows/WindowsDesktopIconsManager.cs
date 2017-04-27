using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ArrangeIconsByShape.Contracts.Interfaces;
using ArrangeIconsByShape.DesktopIconsManager.Windows.Core;

namespace ArrangeIconsByShape.DesktopIconsManager.Windows
{
    public class WindowsDesktopIconsManager : IDesktopIconsManager
    {
        public int GetNumberOfIcons()
            => (int)User32.SendMessage(GetDesktopWindow(), User32.LVM_GETITEMCOUNT, 0, IntPtr.Zero);
        
        public IEnumerable<(int x, int y)> GetIconsPositions()
        {
            var wnd = GetDesktopWindow();
            User32.GetWindowThreadProcessId(wnd, out uint pid);

            var desktopProcessHandle = IntPtr.Zero;
            var sharedMemoryPointer = IntPtr.Zero;
            try
            {
                desktopProcessHandle = Kernel32.OpenProcess(Kernel32.ProcessAccess.VmOperation | Kernel32.ProcessAccess.VmRead | Kernel32.ProcessAccess.VmWrite, false, pid);
                sharedMemoryPointer = Kernel32.VirtualAllocEx(desktopProcessHandle, IntPtr.Zero, 4096, Kernel32.AllocationType.Reserve | Kernel32.AllocationType.Commit, Kernel32.MemoryProtection.ReadWrite);

                var iconsCount = GetNumberOfIcons();
                var icons = new List<(int x, int y)>();

                for (var index = 0; index < iconsCount; index++)
                {
                    uint numberOfBytes = 0;
                    var points = new User32.IconPoint[1];

                    Kernel32.WriteProcessMemory(desktopProcessHandle, sharedMemoryPointer, Marshal.UnsafeAddrOfPinnedArrayElement(points, 0), Marshal.SizeOf(typeof(User32.IconPoint)), ref numberOfBytes);
                    User32.SendMessage(wnd, User32.LVM_GETITEMPOSITION, index, sharedMemoryPointer);
                    Kernel32.ReadProcessMemory(desktopProcessHandle, sharedMemoryPointer, Marshal.UnsafeAddrOfPinnedArrayElement(points, 0), Marshal.SizeOf(typeof(User32.IconPoint)), ref numberOfBytes);

                    icons.Add((x: points[0].X, y: points[0].Y));
                }

                return icons;
            }
            finally
            {
                if (desktopProcessHandle != IntPtr.Zero)
                    Kernel32.CloseHandle(desktopProcessHandle);
                if (sharedMemoryPointer != IntPtr.Zero)
                    Kernel32.VirtualFreeEx(desktopProcessHandle, sharedMemoryPointer, 0, Kernel32.FreeType.Release);
            }
        }

        private static IntPtr GetDesktopWindow()
        {
            var shellWindow = User32.GetShellWindow();
            var shellDefaultView = User32.FindWindowEx(shellWindow, IntPtr.Zero, "SHELLDLL_DefView", null);
            var sysListview = User32.FindWindowEx(shellDefaultView, IntPtr.Zero, "SysListView32", "FolderView");

            if (shellDefaultView == IntPtr.Zero)
            {
                User32.EnumWindows((wnd, l_param) =>
                {
                    var sb = new StringBuilder(256);
                    User32.GetClassName(wnd, sb, sb.Capacity);

                    if (sb.ToString() != "WorkerW") return true;
                    var child = User32.FindWindowEx(wnd, IntPtr.Zero, "SHELLDLL_DefView", null);
                    if (child == IntPtr.Zero) return true;
                    shellDefaultView = child;
                    sysListview = User32.FindWindowEx(child, IntPtr.Zero, "SysListView32", "FolderView");
                    return false;
                }, 0);
            }
            return sysListview;
        }

    }
}
