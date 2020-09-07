using System;
using System.Collections.Generic;
using System.IO;

namespace ConquerLoader.Models
{
    public class DebugWriter
    {
        private string LogPath { get; set; }
        private List<string> Log { get; set; }

        public DebugWriter(string _LogPath)
        {
            LogPath = _LogPath;
            Log = new List<string>();
        }

        private void WriteHeader()
        {
            Log.Add("[ConquerLoader]");
        }

        private void WriteFooter()
        {
            Log.Add("-------------------");
        }

        private void WriteBody(string Content)
        {
            string cont = "[" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "] " + Content;
            Log.Add(cont);
        }

        public void Write(string Content)
        {
            WriteHeader();
            WriteBody(Content);
            WriteFooter();
            SaveLog();
        }

        public void SaveLog()
        {
            File.WriteAllLines(LogPath, Log);
        }
    }
}
