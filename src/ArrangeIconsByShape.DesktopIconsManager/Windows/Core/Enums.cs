using System;

namespace ArrangeIconsByShape.DesktopIconsManager.Windows.Core
{
    internal enum MONITORINFOF : uint
    {
        PRIMARY = 1
    }

    internal enum ListViewStyles : short
    {
        LVS_OWNERDATA = 0x1000,
        LVS_SORTASCENDING = 0x0010,
        LVS_SORTDESCENDING = 0x0020,
        LVS_SHAREIMAGELISTS = 0x0040,
        LVS_NOLABELWRAP = 0x0080,
        LVS_AUTOARRANGE = 0x0100
    }

    internal enum ListViewAlignment
    {
        LVA_DEFAULT = 0x0000,
        LVA_ALIGNLEFT = 0x0001,
        LVA_ALIGNTOP = 0x0002,
        LVA_SNAPTOGRID = 0x0005
    }

    [Flags]
    internal enum SetWindowPositionOptions : uint
    {
        NoSize = 0x00000001,
        NoMove = 0x00000002,
        NoZOrder = 0x00000004,
        FrameChanged = 0x00000020,
        DockFrame = 0x00100000
    }


    [Flags]
    internal enum AllocationType
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
    internal enum MemoryProtection
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
    internal enum FreeType
    {
        Decommit = 0x4000,
        Release = 0x8000,
    }

    [Flags]
    internal enum ProcessAccess
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
