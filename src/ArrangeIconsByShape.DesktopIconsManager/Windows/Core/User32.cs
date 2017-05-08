using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ArrangeIconsByShape.DesktopIconsManager.Windows.Core
{
    /// <summary>
    /// user32.dll wrapper
    /// </summary>
    internal class User32
    {
        public const uint LVM_FIRST = 0x1000;
        public const uint LVM_GETITEMCOUNT = LVM_FIRST + 4;
        public const uint LVM_SETITEMPOSITION = LVM_FIRST + 15;
        public const uint LVM_GETITEMPOSITION = LVM_FIRST + 16;
        public const int LVM_ARRANGE = (0x1000 + 22);

        public const int GWL_STYLE = -16;
        public const int WM_COMMAND = 0x0111;

        private const string User32Dll = "user32.dll";

        internal delegate bool EnumWindowsProc(IntPtr wnd, int param);
        internal delegate bool EnumDisplayMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rectangle lprcMonitor, IntPtr dwData);

        public static IntPtr MakeLParam(int wLow, int wHigh) => (IntPtr)(((short)wHigh << 16) | (wLow & 0xffff));

        [DllImport(User32Dll)]
        internal static extern bool EnumWindows(EnumWindowsProc enum_func, int param);

        [DllImport(User32Dll)]
        internal static extern IntPtr GetShellWindow();

        [DllImport(User32Dll)]
        internal static extern IntPtr FindWindowEx(IntPtr wnd, IntPtr wnd_child_after, string @class, string window);

        [DllImport(User32Dll)]
        internal static extern int GetClassName(IntPtr wnd, StringBuilder class_name, int max_count);

        [DllImport(User32Dll)]
        internal static extern IntPtr SendMessage(IntPtr wnd, uint msg, int w_param, IntPtr l_param);

        [DllImport(User32Dll)]
        internal static extern uint GetWindowThreadProcessId(IntPtr wnd, out uint process_id);

        [DllImport(User32Dll)]
        internal static extern bool GetMonitorInfo([In] IntPtr hMonitor, [Out] out MonitorInfo lpmi);

        [DllImport(User32Dll)]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport(User32Dll)]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport(User32Dll)]
        internal static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip,
            EnumDisplayMonitorsDelegate lpfnEnum, IntPtr dwData);
    }
}
