using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ComManager
{
    class XmlReader
    {
        string path;
        public XmlReader(string _path)
        {
            this.path = _path;
        }

        public Dictionary<int, ComEntity> ReadXmlFile()
        {
            List<ComEntity> RS232conf = new List<ComEntity>();
            Dictionary<int, ComEntity> RS232confD = new Dictionary<int, ComEntity>();
            try
            {
                XmlTextReader reader = new XmlTextReader(System.IO.Path.Combine(path, "MyXml.xml"));
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load((System.IO.Path.Combine(path, "MyXml.xml")));
                XmlNodeList elemList = xmlDoc.GetElementsByTagName("ComId");

                for (int i = 0; i < elemList.Count; i++)
                {
                    XmlNode ch = elemList[i]["ComType"];
                    string id = elemList[i].Attributes["Id"].Value;
                    string type = elemList[i]["ComType"].GetAttribute("Type");
                    switch (type)
                    {
                        case "RS232":
                            ComRS232 rs232 = new ComRS232();
                            {
                                rs232.ComId = Convert.ToInt32(id);
                                CreateConf(ch, rs232);
                                RS232conf.Add(rs232);
                                RS232confD.Add(Convert.ToInt32(id), rs232);
                            }
                            break;
                        case "TCP":
                            //TODO TCP connection
                            break;
                        default:
                            break;
                    }
                }

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

            return RS232confD;
        }

        private void CreateConf(XmlNode ch, ComRS232 rs232)
        {
            foreach (XmlNode child in ch.ChildNodes)
            {
                switch (child.Name)
                {
                    case "PortName":
                        rs232.PortName = child.InnerText;
                        break;
                    case "BaudRate":
                        rs232.BaudRate = Convert.ToInt32(child.InnerText);
                        break;
                    case "StopBits":
                        rs232.stopBits = child.InnerText;
                        break;
                    case "HandShake":
                        rs232.handshake = child.InnerText;
                        break;
                    case "Parity":
                        rs232.parity = child.InnerText;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
