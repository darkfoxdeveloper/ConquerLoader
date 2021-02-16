using CLCore.Models;
using System;
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
        private string _Template = "";
        private BindingList<ServerConfiguration> _Servers { get; set; }

        public ServersDatGenerator(BindingList<ServerConfiguration> Servers)
        {
            _Servers = Servers;
            _Template = Properties.Resources.ServersXML;
            XDocument doc = XDocument.Parse(_Template);
            int initialId = 101;
            LoaderConfig lConfig = Core.GetLoaderConfig();
            XElement elGroup = doc.Element("mysqldump").Element("database").Element("table_data").Elements("row").Where(x => x.Elements("field").Where(y => y.Attribute("name").Value == "id" && y.Value == "1").Count() > 0).FirstOrDefault();
            elGroup.Elements("field").Where(x => x.Attribute("name").Value == "Child").FirstOrDefault().Value = lConfig.Servers.Count.ToString();
            //if (lConfig.ServerListGroupName != null)
            //{
            //    elGroup.Elements("field").Where(x => x.Attribute("name").Value == "FlashName").FirstOrDefault().Value = lConfig.ServerListGroupName;
            //}
            //if (lConfig.ServerListGroupIcon != null)
            //{
            //    elGroup.Elements("field").Where(x => x.Attribute("name").Value == "FlashIcon").FirstOrDefault().Value = lConfig.ServerListGroupIcon;
            //}
            foreach (ServerConfiguration serv in _Servers)
            {
                XElement rootElement = new XElement("row");
                try
                {
                    rootElement.Add(new XElement("field",
                        new XAttribute("name", "id"), initialId.ToString()
                    ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "ServerName"), serv.ServerName
                    ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "ServerIP"), serv.LoginHost
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "ServerPort"), serv.LoginPort
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "FlashName"), serv.ServerName
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "FlashIcon"), serv.FlashIcon
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "FlashHint")
                   ));
                    rootElement.Add(new XElement("field",
                         new XAttribute("name", "Child"), 0
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
                    ((XElement)((XContainer)doc.Root.LastNode).LastNode).Add(rootElement);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
                initialId++;
                // Tener en cuenta que en version 6600+ solo podran usar el puerto 5816
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
    }
}
