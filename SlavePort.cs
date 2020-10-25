using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public class SlavePort
    {
        public byte startByte;
        public int iPacketLength = 0;
        public string eofDetection;
        public SerialPort serialPort;
        private System.Windows.Forms.Timer timerPacketTimeout;
        private readonly BackgroundWorker dataReceptionBackgroundWorker;
        private bool bPacketStarted = false;
        private bool bPacketTimeout = false;
        private byte u8PacketTimeoutValue = 0;
        private string linkType;
        private Mux mux;

        public SlavePort(Mux mux)
        {
            this.mux = mux;
            serialPort = new SerialPort();
            this.linkType = LinkType.Ascii;
            dataReceptionBackgroundWorker = new BackgroundWorker();
            dataReceptionBackgroundWorker.WorkerSupportsCancellation = true;
            dataReceptionBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.dataReceptionBackgroundWorker_DoWork);
            this.timerPacketTimeout = new System.Windows.Forms.Timer();
            this.timerPacketTimeout.Tick += new System.EventHandler(this.timerPacketTimeout_Tick);
        }

        public void SendPacket()
        {
            //
        }

        private void UploadDataToMux(string portSource, string timeCode, byte[] data)
        {
            Packet packet = new Packet();
            packet.strPortSource = portSource;
            packet.strTime = timeCode;
            packet.aData = data;
            //
            mux.AddPacketToUpload(packet);
        }

        private void dataReceptionBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                if ((serialPort.IsOpen) && (serialPort.BytesToRead > 0))
                {
                    string strReceptionTime = PortTools.GetTimeString();
                    int iBytesToRead = serialPort.BytesToRead;
                    // Create a reading buffer
                    byte[] rxBuffer = new byte[iBytesToRead];
                    if (linkType == LinkType.Binary)
                    {
                        //
                        serialPort.Read(rxBuffer, 0, iBytesToRead);
                    }
                    else if(linkType == LinkType.Ascii)
                    {
                        //
                    }
                    // Upload the received data to MUX
                    UploadDataToMux(serialPort.PortName, strReceptionTime, rxBuffer);
                }
                else
                {
                    // Pause thread for 100ms to breath
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        private void timerPacketTimeout_Tick(object sender, EventArgs e)
        {
            bPacketTimeout = true;
            timerPacketTimeout.Stop();
        }

        public byte GetPacketTimeoutValue()
        {
            return u8PacketTimeoutValue;
        }
        public void OpenPort()
        {
            if(SerialPort.GetPortNames().Contains(serialPort.PortName))
            {
                try
                {
                    serialPort.Open();
                }
                catch(Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
                }
                if(serialPort.IsOpen)
                {
                    TraceLogger.EventTrace("Port " + serialPort.PortName + " open.");
                    dataReceptionBackgroundWorker.RunWorkerAsync();
                }
            }
        }

        public void SetPacketTimeoutValue(int iTimeoutValue)
        {
            timerPacketTimeout.Interval = iTimeoutValue;
            u8PacketTimeoutValue = (byte)iTimeoutValue;
        }
        public string GetLinkType()
        {
            return linkType;
        }

        public void SetLinkType(string strLinkType)
        {
            this.linkType = strLinkType;
            if (strLinkType == LinkType.Ascii)
            {
                serialPort.NewLine = CustomDefs.strNewLineDef;
            }
        }
    }
}
