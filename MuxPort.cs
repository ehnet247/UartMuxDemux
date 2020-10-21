using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public class MuxPort
    {
        public SerialPort serialPort;
        public string linkType;
        public bool bKeepListening;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private List<DemuxPort> demuxPorts;

        public MuxPort(List<DemuxPort> demuxPorts)
        {
            this.serialPort = new SerialPort();
            this.bKeepListening = true;
            this.demuxPorts = demuxPorts;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerAsync();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if((this.bKeepListening) && (serialPort.IsOpen))
                {
                    if(serialPort.BytesToRead > 0)
                    {
                        // Read the available bytes
                        int iNbBytesToRead = serialPort.BytesToRead;
                        byte[] aReadBuffer = new byte[iNbBytesToRead];
                        int iReadBytes = serialPort.Read(aReadBuffer, 0, iNbBytesToRead);

                        // Write the received data to every demux port
                        foreach(DemuxPort dp in demuxPorts)
                        {
                            try
                            {
                                if((dp != null) && (dp.serialPort.IsOpen))
                                {
                                    dp.Write(aReadBuffer);
                                }
                            }
                            catch(Exception ex)
                            {
                                TraceLogger.ErrorTrace(ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}
