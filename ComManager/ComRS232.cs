using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ComManager
{
    class ComRS232 : ComEntity
    {
        SerialPort _serialPort;
        
        List<SerialPort> portList;
        
        public string PortName { get; set; }
        public int BaudRate { get; set; }
        
        public string stopBits { get; set; }
        public string handshake { get; set; }
        public string parity { get; set; }

        private string receivedMsg;

        // Gets or sets subject state

        public string ReceivedMsg
        {
            get { return receivedMsg; }
            set { receivedMsg = value; }
        }

       

        public override bool init()
        {
            try
            {
                portList = new List<SerialPort>();
                _serialPort = new SerialPort
                {
                    PortName = PortName,
                    BaudRate = BaudRate,
                    StopBits = StopBits.One,
                    Handshake = Handshake.None,
                    Parity = Parity.None
                };
                //this.Attach(ComId,new A())
                List<string> allPorts = SerialPort.GetPortNames().ToList();
                //ListenToAlaPorts(allPorts);

                if (allPorts != null && allPorts.Count > 0)
                {
                    bool portExist = allPorts.Contains(_serialPort.PortName);
                    if (portExist)
                    {
                        _serialPort.ReadTimeout = 500;
                        _serialPort.WriteTimeout = 500;
                        if (!_serialPort.IsOpen)
                        {
                            _serialPort.Open();
                        }                     
                        Console.WriteLine("port " + this.PortName + " opened");
                        _serialPort.DataReceived += _serialPort_DataReceived;

                    }
                    else
                    {
                        Console.WriteLine("cant find port " + this.PortName);
                    }                    
                }
                else
                {
                    Console.WriteLine("No Ports found . . .");
                }
            }
            catch (Exception e)
            {
                return false;
                throw new Exception(e.Message);
            }

            return true;
        }

        public void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] data;
            try
            {
                SerialPort spL = (SerialPort)sender;
                Console.WriteLine("DATA RECEIVED! on port " + spL.PortName);
                int count = spL.BytesToRead;
                data = new byte[count];
                spL.Read(data, 0, data.Length);
                Console.WriteLine(" the data is==> " + Encoding.UTF8.GetString(data) + " on port " + spL.PortName);
                Notify(data);
            }
            catch (Exception exp)
            {

                throw new Exception(exp.Message);
            }
            
        }


        public override bool Send(int comId, byte[] msg)
        {
            try
            {
                Console.WriteLine("sending to " + _serialPort.PortName + " the byte[] ==> " + Encoding.UTF8.GetString(msg));
                _serialPort.Write(msg, 0, msg.Length);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        //private void ListenToAlaPorts(List<string> listOfPorts)
        //{
        //    foreach (var port in listOfPorts)
        //    {
        //        if (port != this.PortName)
        //        {
        //            SerialPort portForListening = new SerialPort(port);
        //            portForListening.Open();
        //            portForListening.DataReceived += _serialPort_DataReceived;
        //            portList.Add(portForListening);
        //        }
                  
        //    }
        //}

        
    }
}
