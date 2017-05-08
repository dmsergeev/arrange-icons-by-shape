namespace ArrangeIconsByShape.DesktopIconsManager.Windows.Core
{
    using System;

    internal struct Point
    {
        public int x;
        public int y;
    }

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

    internal struct Rectangle
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    internal struct IconPoint
    {
        public int X;
        public int Y;
    }

    internal struct MonitorInfo
    {
        public int cbSize;
        public Rectangle rcMonitor;
        public Rectangle rcWork;
        public MONITORINFOF dwFlags;
    }
}
