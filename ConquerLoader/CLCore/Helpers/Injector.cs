namespace CLCore
{
    #region References

    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    #endregion
    public static class Injector
    {
        #region Win32
        [DllImport("KERNEL32.DLL")]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("KERNEL32.DLL")]
        private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAdress, UIntPtr dwSize, uint flAllocationType, uint flProtect);

        [DllImport("KERNEL32.DLL")]
        private static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAdress, uint dwSize, uint dwFreeType);

        [DllImport("KERNEL32.DLL")]
        private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, int lpNumberOfBytesWritten);

        [DllImport("KERNEL32.DLL")]
        private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr se, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, uint lpThreadId);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi)]
        private extern static IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("KERNEL32.DLL", CharSet = CharSet.Ansi)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("KERNEL32.DLL")]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("KERNEL32.DLL")]
        private static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliSeconds);

        private const uint PROCESS_ALL_ACCESS = (uint)(0x0002 | 0x0400 | 0x0008 | 0x0010 | 0x0020);
        private const uint MEM_COMMIT = 0x1000;
        private const uint MEM_RELEASE = 0x8000;
        private const uint PAGE_EXECUTE_READWRITE = 0x40;
        private const uint WAIT_ABANDONED = 0x00000080;
        private const uint WAIT_OBJECT_0 = 0x00000000;
        private const uint WAIT_TIMEOUT = 0x00000102;
        private const uint WAIT_FAILED = 0xFFFFFFFF;
        #endregion
        public static InjectionStatus LastStatus { get; set; }

        /// <summary>
        /// Function to inject a Dll
        /// </summary>
        /// <param name="DllName">Name of dll for inject.</param>
        /// <param name="ProcessName">Nombre del proceso en el que sera injectada la Dll.</param>
        public static InjectionStatus StartInjection(string DllName, uint ProcessID, System.ComponentModel.BackgroundWorker Worker)
        {
            try
            {
                IntPtr hProcess = new IntPtr(0); //openprocess
                IntPtr hModule = new IntPtr(0); //vritualAllocex
                IntPtr Injector = new IntPtr(0); //getprocadress
                IntPtr hThread = new IntPtr(0); //createremotethread
                int LenWrite = DllName.Length + 1;
                hProcess = OpenProcess(PROCESS_ALL_ACCESS, false, ProcessID);
                if (hProcess != IntPtr.Zero)
                {
                    hModule = VirtualAllocEx(hProcess, IntPtr.Zero, (UIntPtr)LenWrite, MEM_COMMIT, PAGE_EXECUTE_READWRITE);
                    if (hModule != IntPtr.Zero)
                    {
                        Worker.ReportProgress(30);
                        ASCIIEncoding Encoder = new ASCIIEncoding();
                        int Written = 0;
                        if (WriteProcessMemory(hProcess, hModule, Encoder.GetBytes(DllName), LenWrite, Written))
                        {
                            Injector = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

                            Worker.ReportProgress(40);
                            if (Injector != IntPtr.Zero)
                            {
                                hThread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, Injector, hModule, 0, 0);

                                if (hThread != IntPtr.Zero)
                                {
                                    Worker.ReportProgress(50);
                                    uint Result = WaitForSingleObject(hThread, 10 * 1000);
                                    if (Result != WAIT_FAILED && Result != WAIT_ABANDONED && Result != WAIT_TIMEOUT) {
                                        if (VirtualFreeEx(hProcess, hModule, 0, MEM_RELEASE))
                                        {
                                            if (hThread != IntPtr.Zero)
                                            {
                                                Worker.ReportProgress(99);
                                                CloseHandle(hThread);
                                                return ReturnInjectionStatus(true, "Injection success");
                                            }
                                            else ReturnInjectionStatus(false, "Bad thread handle ... Injection failed");
                                        }
                                        else ReturnInjectionStatus(false, "Memory free problem ... Injection failed");
                                    }
                                    else ReturnInjectionStatus(false, "WaitForSingle failed: " + Result.ToString() + "... Injection failed");
                                }
                                else ReturnInjectionStatus(false, "Problem when starting the thread ... Injection failed");
                            }
                            else ReturnInjectionStatus(false, "LoadLibraryA address not found ... Injection failed");
                        }
                        else ReturnInjectionStatus(false, "Write error in process ... Injection failed");
                    }
                    else ReturnInjectionStatus(false, "Unallocated memory ... Injection failed");
                }
                else return ReturnInjectionStatus(false, "Unopened process ... Injection failed");
            }
            catch (Exception Exc) { return ReturnInjectionStatus(false, "Injection Error: " + Exc.ToString()); }
            return ReturnInjectionStatus(false, "Unhandled reason... Injection failed");
        }
        public static InjectionStatus ReturnInjectionStatus (bool Injected, string ResultMessage)
        {
            LastStatus = new InjectionStatus() { Injected = Injected, ResultMessage = ResultMessage };
            return LastStatus;
        }
    }
    public class InjectionStatus
    {
        public bool Injected { get; set; }
        public string ResultMessage { get; set; }
    }
}