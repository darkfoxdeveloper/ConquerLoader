using CLCore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ConquerLoader.Models
{
    public class ServersDatGenerator
    {
        private readonly string _Template = "";
        private BindingList<ServerConfiguration> _Servers { get; set; }
        private LoaderConfig Config { get; set; }
        private ServerConfiguration SelectedServer { get; set; }

        public ServersDatGenerator(BindingList<ServerConfiguration> Servers)
        {
            // In this system for server.dat (V6600+) only can use port 5816 for gameserver
            _Servers = Servers;
            _Template = Properties.Resources.ServersXML;
            XDocument doc = XDocument.Parse(_Template);
            int initialId = 1;
            Config = Core.GetLoaderConfig();
            XElement tableRowsBase = doc.Element("mysqldump").Element("database").Element("table_data");
            XElement elGroup = tableRowsBase.Elements("row").Where(x => x.Elements("field").Where(y => y.Attribute("name").Value == "id" && y.Value == "0").Count() > 0).FirstOrDefault();
            List<ServerDatGroup> Groups = Config.GetGroups();
            elGroup.Elements("field").Where(x => x.Attribute("name").Value == "Child").FirstOrDefault().Value = Groups.Count.ToString();
            uint GroupId = 1;
            List<XElement> groupElements = new List<XElement>();
            List<XElement> serverElements = new List<XElement>();
            foreach (ServerDatGroup group in Groups)
            {
                XElement rootElement = new XElement("row");
                try
                {
                    rootElement.Add(new XElement("field",
                        new XAttribute("name", "id"), GroupId
                    ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "ServerName"), ""
                    ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "ServerIP"), ""
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "ServerPort"), 0
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "FlashName"), group.GroupName
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "FlashIcon"), group.GroupIcon
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "FlashHint")
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "Child"), group.Servers.Count
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "PicServerIP"), 0
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "PicServerPort"), 0
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "BindServerIP"), 0
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "BindServerPort"), 0
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "Charges"), 0
                   ));
                   //tableRowsBase.Add(rootElement);
                   groupElements.Add(rootElement);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
                GroupId++;
                foreach (ServerConfiguration serv in group.Servers)
                {
                    if (Config.DefaultServer.ServerName == serv.ServerName && Config.DefaultServer.ServerVersion == serv.ServerVersion) // Its the selected server
                    {
                        uint GroupIndex = GroupId-2;
                        uint ServerIndex = (uint)initialId-1;
                        SetSelectedServer(GroupIndex, ServerIndex);
                    }
                    XElement rowElement = new XElement("row");
                    try
                    {
                        rowElement.Add(new XElement("field",
                            new XAttribute("name", "id"), (GroupId-1) + (initialId.ToString().PadLeft(2, '0'))
                        )); ;
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "ServerName"), serv.ServerName
                        ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "ServerIP"), serv.LoginHost
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "ServerPort"), serv.LoginPort
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "FlashName"), serv.ServerName
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "FlashIcon"), serv.ServerIcon
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "FlashHint")
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "Child"), 0
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "PicServerIP"), 0
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "PicServerPort"), 0
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "BindServerIP"), 0
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "BindServerPort"), 0
                       ));
                        rowElement.Add(new XElement("field",
                             new XAttribute("name", "Charges"), 0
                       ));
                       serverElements.Add(rowElement);
                    }
                    catch (Exception e)
                    {
                        System.Windows.Forms.MessageBox.Show(e.Message);
                    }
                    initialId++;
                }
                initialId = 1;
            }
            foreach (XElement xElGroup in groupElements)
            {
                tableRowsBase.Add(xElGroup);
            }
            foreach (XElement xElServ in serverElements)
            {
                tableRowsBase.Add(xElServ);
            }
            MemoryStream memStream = new MemoryStream();
            XmlWriterSettings xws = new XmlWriterSettings { OmitXmlDeclaration = true, Indent = true, Encoding = new UTF8Encoding(false) };
            XmlWriter writer = XmlWriter.Create(memStream, xws);
            doc.Save(writer);
            writer.Close();
            memStream.Position = 0;
            using (FileStream compressedFileStream = File.Create("Servers.dat"))
            {
                using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                {
                    memStream.CopyTo(compressionStream);
                }
            }
        }

        public void SetSelectedServer(uint GroupIndex, uint ServerIndex)
        {
            string SetupIniPath = Path.Combine(Directory.GetCurrentDirectory(), "ini", "GameSetup.ini");
            IniManager parser = new IniManager(SetupIniPath, "ScreenMode");
            parser.Write("Group", "GroupRecord", GroupIndex);
            parser.Write("Server", "ServerRecord", ServerIndex);
        }
    }
}
