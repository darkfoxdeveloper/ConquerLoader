using System;
using System.Collections.Generic;
using System.IO;

namespace CLCore.Models
{
    public class LogWritter
    {
        private string LogPath { get; set; }
        private List<string> Log { get; set; }

        public LogWritter(string _LogPath)
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

        public void Write(List<string> Contents)
        {
            WriteHeader();
            foreach(string line in Contents)
            {
                string cont = "[" + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss") + "] " + line;
                Log.Add(cont);
            }
            WriteFooter();
            SaveLog();
        }

        public void SaveLog()
        {
            File.WriteAllLines(LogPath, Log);
        }
    }
}
