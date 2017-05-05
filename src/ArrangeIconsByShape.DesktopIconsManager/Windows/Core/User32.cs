using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ArrangeIconsByShape.DesktopIconsManager.Windows.Core
{
    internal class User32
    {
        public const uint LVM_FIRST = 0x1000;
        public const uint LVM_GETITEMCOUNT = LVM_FIRST + 4;
        public const uint LVM_SETITEMPOSITION = LVM_FIRST + 15;
        public const uint LVM_GETITEMPOSITION = LVM_FIRST + 16;
        public const int GWL_STYLE = -16;
        public const int WM_COMMAND = 0x0111;

        internal delegate bool EnumWindowsProc(IntPtr wnd, int param);
        internal delegate bool EnumDisplayMonitorsDelegate(IntPtr hMonitor, IntPtr hdcMonitor, ref Rectangle lprcMonitor, IntPtr dwData);

        public static IntPtr MakeLParam(int wLow, int wHigh) => (IntPtr)(((short)wHigh << 16) | (wLow & 0xffff));

        [DllImport("user32.DLL")]
        internal static extern bool EnumWindows(EnumWindowsProc enum_func, int param);

        [DllImport("user32.DLL")]
        internal static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindowEx(IntPtr wnd, IntPtr wnd_child_after, string @class, string window);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern int GetClassName(IntPtr wnd, StringBuilder class_name, int max_count);

        [DllImport("user32.dll")]
        internal static extern IntPtr SendMessage(IntPtr wnd, uint msg, int w_param, IntPtr l_param);

        [DllImport("user32.dll")]
        internal static extern uint GetWindowThreadProcessId(IntPtr wnd, out uint process_id);

        [DllImport("user32.dll")]
        internal static extern bool GetMonitorInfo([In] IntPtr hMonitor, [Out] out MonitorInfo lpmi);

        [DllImport("user32.dll")]
        internal static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32")]
        internal static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        internal static extern bool EnumDisplayMonitors(IntPtr hdc, IntPtr lprcClip,
            EnumDisplayMonitorsDelegate lpfnEnum, IntPtr dwData);
    }
}
