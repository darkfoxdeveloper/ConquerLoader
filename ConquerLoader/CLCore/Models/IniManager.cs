using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CLCore.Models
{
    public class IniManager
    {
        private readonly string FileName;
        private readonly string FileSection;
        public IniManager(string _FileName, string section = "data")
        {
            this.FileName = _FileName;
            this.FileSection = section;
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int GetPrivateProfileStringA(string Section, string Key, string _Default, StringBuilder Buffer, int BufferSize, string FileName);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int WritePrivateProfileStringA(string Section, string Key, string Arg, string FileName);
        public object this[object key, object _default = null]
        {
            get
            {
                if (FileSection == null) return null;
                return ReadString(FileSection, key.ToString(), _default.ToString(), 1024);
            }
            set
            {
                if (FileSection == null) return;
                Write(FileSection, key.ToString(), value);
            }
        }
        public byte ReadByte(string Section, string Key, byte _Default)
        {
            byte.TryParse(this.ReadString(Section, Key, _Default.ToString(), 6), out byte buf);
            return buf;
        }
        public short ReadInt16(string Section, string Key, short _Default)
        {
            short.TryParse(this.ReadString(Section, Key, _Default.ToString(), 9), out short buf);
            return buf;
        }
        public int ReadInt32(string Section, string Key, int _Default)
        {
            int.TryParse(this.ReadString(Section, Key, _Default.ToString(), 15), out int buf);
            return buf;
        }
        public sbyte ReadSByte(string Section, string Key, byte _Default)
        {
            sbyte.TryParse(this.ReadString(Section, Key, _Default.ToString(), 6), out sbyte buf);
            return buf;
        }
        public string ReadString(string Section, string Key)
        {
            return this.ReadString(Section, Key, "", 400);
        }

        public string ReadString(string Section, string Key, string _Default, int BufSize = 400)
        {
            StringBuilder Buffer = new StringBuilder(BufSize);
            GetPrivateProfileStringA(Section, Key, _Default, Buffer, BufSize, this.FileName);
            return Buffer.ToString();
        }
        public ushort ReadUInt16(string Section, string Key)
        {
            ushort.TryParse(this.ReadString(Section, Key, 0.ToString(), 9), out ushort buf);
            return buf;
        }
        public uint ReadUInt32(string Section, string Key)
        {
            uint.TryParse(this.ReadString(Section, Key, 0.ToString(), 15), out uint buf);
            return buf;
        }
        public void Write(string Section, string Key, object Value)
        {
            WritePrivateProfileStringA(Section, Key, Value.ToString(), this.FileName);
        }
        public void Write(string Section, string Key, string Value)
        {
            WritePrivateProfileStringA(Section, Key, Value, this.FileName);
        }
    }
}
