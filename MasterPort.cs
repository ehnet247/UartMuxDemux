using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public class MasterPort
    {

        public SerialPort serialPort;
        private Demux demux;

        public MasterPort(Demux demux)
        {
            this.demux = demux;
            serialPort = new SerialPort();
        }

        public void OpenPort()
        {
            if (serialPort.IsOpen == false)
            {
                try
                {
                    serialPort.Open();
                }
                catch (Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
                }
            }
        }

        public void ClosePort()
        {
            if(serialPort.IsOpen)
            {
                try
                {
                    serialPort.Close();
                }
                catch(Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
}
            }
        }
    }
}
