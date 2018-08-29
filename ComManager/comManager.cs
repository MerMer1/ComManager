using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComManager
{
    class comManager
    {
      
        public Dictionary<int, ComEntity> comEntityDict = new Dictionary<int, ComEntity>();
        
        public comManager()
        {
            //init();
        }

        public void Attach(int comId, Observer observer)
        {
            try
            {
                ComEntity value;
                bool comEntityExsist = false;
                comEntityExsist = comEntityDict.TryGetValue(comId, out value);
                if (comEntityExsist)
                {
                    value.Attach(observer);
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public bool init()
        {
            XmlReader reader = new XmlReader(@"C:\Users\Administrator\Desktop");
            comEntityDict = reader.ReadXmlFile();
            comEntityDict.Add(111, new ComRS232() { ComId = 111, PortName = "COM5" });
            try
            {
                if (comEntityDict != null)
                {
                    foreach (var item in comEntityDict)
                    {
                        item.Value.init();
                    }                   
                }

       
                return true;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }            
        }

        public bool Send(int comId, byte[] msg)
        {
            try
            {
                ComEntity value;
                bool comEntityExsist = false;
                comEntityExsist = comEntityDict.TryGetValue(comId, out value);
                if (comEntityExsist)
                {
                    value.Send(comId, msg);
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }

        }
    }
}
