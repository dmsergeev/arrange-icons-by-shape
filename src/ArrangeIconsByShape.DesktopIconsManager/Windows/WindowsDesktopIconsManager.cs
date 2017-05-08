namespace ArrangeIconsByShape.DesktopIconsManager.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using Contracts.Interfaces;
    using Contracts.Models;
    using Core;

    public class WindowsDesktopIconsManager : IDesktopIconsManager
    {
        public int GetNumberOfIcons()
            => (int) User32.SendMessage(GetDesktopWindow(), User32.LVM_GETITEMCOUNT, 0, IntPtr.Zero);

        public IEnumerable<(int x, int y)> GetIconsPositions()
        {
            var wnd = GetDesktopWindow();
            User32.GetWindowThreadProcessId(wnd, out uint pid);

            var desktopProcessHandle = IntPtr.Zero;
            var sharedMemoryPointer = IntPtr.Zero;
            try
            {
                desktopProcessHandle = Kernel32.OpenProcess(ProcessAccess.VmOperation
                                                            | ProcessAccess.VmRead
                                                            | ProcessAccess.VmWrite, false, pid);
                sharedMemoryPointer = Kernel32.VirtualAllocEx(desktopProcessHandle, IntPtr.Zero, 4096,
                    AllocationType.Reserve | AllocationType.Commit, MemoryProtection.ReadWrite);

                var iconsCount = GetNumberOfIcons();
                var icons = new List<(int x, int y)>();

                for (var index = 0; index < iconsCount; index++)
                {
                    uint numberOfBytes = 0;
                    var points = new IconPoint[1];

                    Kernel32.WriteProcessMemory(desktopProcessHandle, sharedMemoryPointer,
                        Marshal.UnsafeAddrOfPinnedArrayElement(points, 0), Marshal.SizeOf(typeof(IconPoint)),
                        ref numberOfBytes);

                    User32.SendMessage(wnd, User32.LVM_GETITEMPOSITION, index, sharedMemoryPointer);

                    Kernel32.ReadProcessMemory(desktopProcessHandle, sharedMemoryPointer,
                        Marshal.UnsafeAddrOfPinnedArrayElement(points, 0), Marshal.SizeOf(typeof(IconPoint)),
                        ref numberOfBytes);

                    icons.Add((x: points[0].X, y: points[0].Y));
                }

                return icons;
            }
            finally
            {
                if (desktopProcessHandle != IntPtr.Zero)
                    Kernel32.CloseHandle(desktopProcessHandle);
                if (sharedMemoryPointer != IntPtr.Zero)
                    Kernel32.VirtualFreeEx(desktopProcessHandle, sharedMemoryPointer, 0, FreeType.Release);
            }
        }

        public IEnumerable<Monitor> GetDisplayMonitors()
        {
            var monitors = new List<Monitor>();

            User32.EnumDisplayMonitors(IntPtr.Zero, IntPtr.Zero,
                (IntPtr hMonitor, IntPtr hdcMonitor, ref Rectangle lprcMonitor, IntPtr dwData) =>
                {
                    monitors.Add(GetMonitor(hMonitor));
                    return true;
                }, IntPtr.Zero);

            NormalizeCoordinates(monitors);
            return monitors;
        }

        public void RandomizeIconsPosition(Monitor monitor)
        {
            var wnd = GetDesktopWindow();
            var iconsCount = GetNumberOfIcons();
            var random = new Random();

            DisableAutoArrangeIcons(wnd);
            DisableSnapToGrid(wnd);

            for (var index = 0; index < iconsCount; index++)
            {
                var randomCoordinatesWithinMonitor = GetRandomCoordinates(monitor, random);
                User32.SendMessage(wnd, User32.LVM_SETITEMPOSITION, index, 
                    User32.MakeLParam(randomCoordinatesWithinMonitor.x, randomCoordinatesWithinMonitor.y));
            }
        }
        /// <summary>
        /// Not working
        /// </summary>
        /// <param name="wnd"></param>
        private static void DisableSnapToGrid(IntPtr wnd) => User32.SendMessage(wnd, User32.LVM_ARRANGE, (int)ListViewAlignment.LVA_DEFAULT, IntPtr.Zero);

        private static void DisableAutoArrangeIcons(IntPtr wnd)
        {
            var styleHandle = User32.GetWindowLong(wnd, User32.GWL_STYLE);
            if ((styleHandle & (int) ListViewStyles.LVS_AUTOARRANGE) == (int) ListViewStyles.LVS_AUTOARRANGE)
                User32.SetWindowLong(wnd, User32.GWL_STYLE, styleHandle & ~(int) ListViewStyles.LVS_AUTOARRANGE);
        }

        private static (int x, int y) GetRandomCoordinates(Monitor monitor, Random random)
        {
            var randomX = random.Next(monitor.UpperLeftCornerCoordinates.X, monitor.LowerRightCornerCoordinates.X);
            var randomY = random.Next(monitor.UpperLeftCornerCoordinates.Y, monitor.LowerRightCornerCoordinates.Y);
            Debug.WriteLine($"{randomX}, {randomY}");
            return (randomX, randomY);
        }

        /// <summary>
        /// Moves coordinate plane so that the leftmost monitor is at (0;0)
        /// </summary>
        /// <param name="monitors"></param>
        private static void NormalizeCoordinates(List<Monitor> monitors)
        {
            if (monitors == null || !monitors.Any()) return;
            var leftmostMonitor = monitors.OrderBy(monitor => monitor.UpperLeftCornerCoordinates.X).FirstOrDefault();
            var (x, y) = leftmostMonitor.UpperLeftCornerCoordinates;
            
            //leftmost monitor is at 0;0
            if (x == 0 && y == 0) return;
            
            foreach (var monitor in monitors)
            {
                monitor.LowerRightCornerCoordinates.X -= x;
                monitor.LowerRightCornerCoordinates.Y -= y;
                monitor.UpperLeftCornerCoordinates.X -= x;
                monitor.UpperLeftCornerCoordinates.Y -= y;
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

        private static Monitor GetMonitor(IntPtr hMonitor)
        {
            var success = false;

            var monitorInfo = new MonitorInfo
            {
                cbSize = Marshal.SizeOf(typeof(MonitorInfo))
            };

            if (hMonitor != IntPtr.Zero)
            {
                success = User32.GetMonitorInfo(hMonitor, out monitorInfo);
            }

            return success
                ? new Monitor
                {
                    UpperLeftCornerCoordinates = (monitorInfo.rcWork.Left, monitorInfo.rcWork.Top),
                    LowerRightCornerCoordinates = (monitorInfo.rcWork.Right, monitorInfo.rcWork.Bottom)
                }
                : null;
        }
    }
}
