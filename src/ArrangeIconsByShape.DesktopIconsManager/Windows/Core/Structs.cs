namespace ArrangeIconsByShape.DesktopIconsManager.Windows.Core
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct Point
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowPosition
    {
        public IntPtr hwnd;
        public IntPtr hwndInsertAfter;
        public int x;
        public int y;
        public int cx;
        public int cy;
        public SetWindowPositionOptions flags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct Rectangle
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct IconPoint
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MonitorInfo
    {
        public int cbSize;
        public Rectangle rcMonitor;
        public Rectangle rcWork;
        public MONITORINFOF dwFlags;
    }
}
