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

        public delegate bool EnumWindowsProc(IntPtr wnd, int param);

        [DllImport("user32.DLL")]
        public static extern bool EnumWindows(EnumWindowsProc enum_func, int param);

        [DllImport("user32.DLL")]
        public static extern int GetWindowText(IntPtr wnd, StringBuilder sb, int max_count);

        [DllImport("user32.DLL")]
        public static extern int GetWindowTextLength(IntPtr wnd);

        [DllImport("user32.DLL")]
        public static extern bool IsWindowVisible(IntPtr wnd);

        [DllImport("user32.DLL")]
        public static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr wnd, IntPtr wnd_child_after, string @class, string window);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetClassName(IntPtr wnd, StringBuilder class_name, int max_count);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr wnd, UInt32 msg, int w_param, IntPtr l_param);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr wnd, out uint process_id);

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr wnd, int id, uint modifiers, uint vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr wnd, int id);

        [StructLayout(LayoutKind.Sequential)]
        public struct IconPoint
        {
            public int X;
            public int Y;
        }

        public enum ShowWindowCommands
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }

        public static IntPtr MakeLParam(int low, int high)
        {
            return (IntPtr)(((short)high << 16) | (low & 0xffff));
        }
    }
}
