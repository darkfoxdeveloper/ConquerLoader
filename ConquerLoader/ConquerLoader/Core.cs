using ConquerLoader.Models;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace ConquerLoader
{
    public static class Core
    {
        public static LoaderConfig GetLoaderConfig()
        {
            LoaderConfig lConfig = null;
            if (File.Exists("config.json"))
            {
                lConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<LoaderConfig>(File.ReadAllText("config.json"));
            }
            return lConfig;
        }
        public static void SaveLoaderConfig(LoaderConfig LoaderConfig)
        {
            File.WriteAllText("config.json", Newtonsoft.Json.JsonConvert.SerializeObject(LoaderConfig, Newtonsoft.Json.Formatting.Indented));
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public int bInheritHandle;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct STARTUPINFO
        {
            public int cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public int dwX;
            public int dwY;
            public int dwXSize;
            public int dwYSize;
            public int dwXCountChars;
            public int dwYCountChars;
            public int dwFillAttribute;
            public int dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [DllImport("kernel32.dll")]
        public static extern bool CreateProcess(string lpApplicationName, string lpCommandLine, ref SECURITY_ATTRIBUTES lpProcessAttributes, ref SECURITY_ATTRIBUTES lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, string lpBuffer, UIntPtr nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);[DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, UIntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);
        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern int WaitForSingleObject(IntPtr handle, int milliseconds);
        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);

        public static bool InjectDLL(IntPtr hProcess, string strDLLName, BackgroundWorker worker)
        {
            IntPtr ptr;
            string str;
            int num = strDLLName.Length + 1;
            IntPtr lpBaseAddress = VirtualAllocEx(hProcess, IntPtr.Zero, (uint)num, 0x1000, 0x40);
            worker.ReportProgress(20);
            if ((lpBaseAddress == IntPtr.Zero) && (lpBaseAddress == IntPtr.Zero))
            {
                str = "Unable to allocate memory to target process.\n";
                str = str + "Error code: " + Marshal.GetLastWin32Error();
                return false;
            }
            WriteProcessMemory(hProcess, lpBaseAddress, strDLLName, (UIntPtr)num, out ptr);
            worker.ReportProgress(50);
            if (Marshal.GetLastWin32Error() != 0x514)
            {
                str = "Please execute with Administration Permissions";
                str = str + "Error code: " + Marshal.GetLastWin32Error();
            }
            else if (Marshal.GetLastWin32Error() != 0)
            {
                str = "Unable to write memory to process.";
                str = str + "Error code: " + Marshal.GetLastWin32Error();
                return false;
            }
            UIntPtr procAddress = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            worker.ReportProgress(60);
            if (procAddress == ((UIntPtr)0))
            {
                str = "Unable to find address of \"LoadLibraryA\".\n";
                MessageBox.Show(str + "Error code: " + Marshal.GetLastWin32Error());
                return false;
            }
            IntPtr handle = CreateRemoteThread(hProcess, IntPtr.Zero, 0, procAddress, lpBaseAddress, 0, out ptr);
            worker.ReportProgress(80);
            if (handle == IntPtr.Zero)
            {
                str = "Unable to load dll into memory.";
                str = str + "Error code: " + Marshal.GetLastWin32Error();
                return false;
            }
            long num2 = WaitForSingleObject(handle, 0x2710);
            worker.ReportProgress(90);
            switch (num2)
            {
                case 0x80L:
                case 0x102L:
                case 0xffffffffL:
                    CloseHandle(handle);
                    return false;
            }
            Thread.Sleep(0x3e8);
            VirtualFreeEx(hProcess, lpBaseAddress, (UIntPtr)0, 0x8000);
            worker.ReportProgress(100);
            CloseHandle(handle);
            return true;
        }
    }
}
