using System;
using System.Runtime.InteropServices;

namespace ArrangeIconsByShape.DesktopIconsManager.Windows.Core
{
    public static class Kernel32
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccess dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool inherit_handle, uint pid);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, MemoryProtection flProtect);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, FreeType dwFreeType);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] IntPtr buffer, int size, ref uint lpNumberOfBytesRead);

        [Flags]
        public enum AllocationType
        {
            Commit = 0x1000,
            Reserve = 0x2000,
            Decommit = 0x4000,
            Release = 0x8000,
            Reset = 0x80000,
            Physical = 0x400000,
            TopDown = 0x100000,
            WriteWatch = 0x200000,
            LargePages = 0x20000000
        }

        [Flags]
        public enum MemoryProtection
        {
            Execute = 0x10,
            ExecuteRead = 0x20,
            ExecuteReadWrite = 0x40,
            ExecuteWriteCopy = 0x80,
            NoAccess = 0x01,
            ReadOnly = 0x02,
            ReadWrite = 0x04,
            WriteCopy = 0x08,
            GuardModifierflag = 0x100,
            NoCacheModifierflag = 0x200,
            WriteCombineModifierflag = 0x400
        }

        [Flags]
        public enum FreeType
        {
            Decommit = 0x4000,
            Release = 0x8000,
        }

        [Flags]
        public enum ProcessAccess
        {
            CreateThread = 0x0002, // Required to create a thread.
            SetSessionId = 0x0004,
            VmOperation = 0x0008, // Required to perform an operation on the address space of a process
            VmRead = 0x0010, // Required to read memory in a process using ReadProcessMemory.
            VmWrite = 0x0020, // Required to write to memory in a process using WriteProcessMemory.
            DupHandle = 0x0040, // Required to duplicate a handle using DuplicateHandle.
            CreateProcess = 0x0080, // Required to create a process.
            SetQuota = 0x0100, // Required to set memory limits using SetProcessWorkingSetSize.
            SetInformation = 0x0200, // Required to set certain information about a process, such as its priority class (see SetPriorityClass).
            QueryInformation = 0x0400, // Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken).
            SuspendResume = 0x0800, // Required to suspend or resume a process.
            // Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass, IsProcessInJob, QueryFullProcessImageName).
            // A handle that has the PROCESS_QUERY_INFORMATION access right is automatically granted PROCESS_QUERY_LIMITED_INFORMATION.
            QueryLimitedInformation = 0x1000,
            Synchronize = 0x100000, // Required to wait for the process to terminate using the wait functions.
            Delete = 0x00010000, // Required to delete the object.
            // Required to read information in the security descriptor for the object, not including the information in the SACL.
            // To read or write the SACL, you must request the ACCESS_SYSTEM_SECURITY access right. For more information, see SACL Access Right.
            ReadControl = 0x00020000,
            WriteDac = 0x00040000, // Required to modify the DACL in the security descriptor for the object.
            WriteOwner = 0x00080000, // Required to change the owner in the security descriptor for the object.
            StandardRightsRequired = 0x000F0000,
            AllAccess = StandardRightsRequired | Synchronize | 0xFFFF // All possible access rights for a process object.
        }
    }
}
