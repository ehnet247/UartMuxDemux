using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace UartMuxDemux
{
    public class SerialPortsDescription
    {
        public SerialPort serialPort;
        public string strLinkType;
        public string strFrameDelimiter;
        public int iFrameSize;
        public byte u8EoFByte;

        public SerialPortsDescription()
        {
            serialPort = new SerialPort();
        }
    }
}
