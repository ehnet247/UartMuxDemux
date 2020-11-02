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
        private List<byte> RxCurrentPacket = null;
        private string strCurrentPacketTimecode;
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

        public void SendPacket(string strPacket)
        {
            //
            serialPort.Write(strPacket);
        }

        private void UploadDataToMux(string portSource, string timeCode, byte[] data)
        {
            Packet packet = new Packet();
            packet.strPortSource = portSource;
            packet.strTime = "(" + timeCode + ")";
            packet.aData = data;
            if (this.linkType == LinkType.Binary)
            {
                // Convert bytes in hex string values
                packet.strData = string.Empty;
                foreach (byte dataByte in data)
                {
                    packet.strData += dataByte.ToString("X2");
                    packet.strData += CustomDefs.strMuxBinaryByteSeparator;
                }
            }
            else if (this.linkType == LinkType.Ascii)
            {
                packet.strData = string.Empty;
                foreach (byte dataByte in data)
                {
                    packet.strData += Convert.ToChar(dataByte).ToString();
                    packet.strData += CustomDefs.strMuxAsciiByteSeparator;
                }
            }
                // Add the packet to the list of packets to be uploaded
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
                    // Are we already receiving a packet?
                    if ((RxCurrentPacket == null) || (RxCurrentPacket.Count == 0))
                    {
                        // No: instanciate a new packet
                        RxCurrentPacket = new List<byte>();
                        // Get the timecode
                        strCurrentPacketTimecode = PortTools.GetTimeString();
                        // Start the timer if necessary
                        if (eofDetection == EofDetection.Unknown)
                        {
                            timerPacketTimeout.Interval = this.GetPacketTimeoutValue();
                            timerPacketTimeout.Start();
                        }
                    }
                    else
                    {
                        // Yes, a packet is currently being received
                    }
                    // Now read the received bytes 
                    // Create a temporary reading buffer
                    byte[] rxBuffer = new byte[iBytesToRead];
                    // Read the received bytes 
                    serialPort.Read(rxBuffer, 0, iBytesToRead);
                    // Copy the received bytes to the current packet
                    RxCurrentPacket.AddRange(rxBuffer);
                    // Check if the packet is complete
                    switch (eofDetection)
                    {
                        case EofDetection.FixedSize:
                            if (RxCurrentPacket.Count >= this.iPacketLength)
                            {
                                // Upload the packet
                                UploadDataToMux(serialPort.PortName,
                                    strCurrentPacketTimecode,
                                    RxCurrentPacket.ToArray());
                                // Free the rx buffer
                                RxCurrentPacket.Clear();
                            }
                            break;
                        case EofDetection.FirstByte:
                            // Get the packet size
                            int iPacketSize = (int)RxCurrentPacket[0];
                            if (RxCurrentPacket.Count >= iPacketSize)
                            {
                                // Upload the packet
                                UploadDataToMux(serialPort.PortName,
                                    strCurrentPacketTimecode,
                                    RxCurrentPacket.ToArray());
                            }
                            break;
                    }
                }
                else
                {
                    if (serialPort.IsOpen == false)
                    {
                        dataReceptionBackgroundWorker.CancelAsync();
                    }
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

        public void ClosePort()
        {
            if(serialPort.IsOpen)
            {
                try
                {
                    serialPort.Close();
                }
                catch (Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
                }
                if (serialPort.IsOpen)
                {
                    TraceLogger.ErrorTrace("Could not close port " + serialPort.PortName);
                }
                else
                {
                    TraceLogger.EventTrace("Port " + serialPort.PortName + "closed");
                    dataReceptionBackgroundWorker.CancelAsync();
                }
            }
        }

        public void SetPacketTimeoutValue(int iTimeoutValue)
        {
            if (iTimeoutValue > 0)
            {
                timerPacketTimeout.Interval = iTimeoutValue;
                u8PacketTimeoutValue = (byte)iTimeoutValue;
            }
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
