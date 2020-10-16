using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public class DemuxPort
    {
        public SerialPort serialPort;
        public string linkType;

        public DemuxPort()
        {
            this.serialPort = new SerialPort();
        }
    }
}
