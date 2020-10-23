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
    public class Packet
    {
        public string strTime;
        public string strPortSource;
        public byte[] aData;
    }
    public class MuxPort
    {
        public const string strTxHeader = "Tx";
        public SerialPort serialPort;
        public string linkType;
        public bool bKeepListening;
        private System.ComponentModel.BackgroundWorker writingBackgroundWorker;
        private List<DemuxPort> demuxPorts;
        private List<Packet> lPackets;

        public MuxPort(List<DemuxPort> demuxPorts)
        {
            this.serialPort = new SerialPort();
            this.lPackets = new List<Packet>();
            this.bKeepListening = true;
            this.demuxPorts = demuxPorts;
            writingBackgroundWorker = new BackgroundWorker();
            this.writingBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WritingBackgroundWorker_DoWork);
            this.writingBackgroundWorker.RunWorkerAsync();
        }

        private void WritingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if((this.bKeepListening) && (serialPort.IsOpen) && (lPackets.Count > 0))
                {
                    // Create a string initialized with a header
                    string strPacketToSend = strTxHeader;
                    // Add the COM port number to the string to send
                    strPacketToSend += lPackets[0].strPortSource;
                    // Convert the packet data in ASCII chars
                    foreach (byte dataByte in lPackets[0].aData)
                    {
                        strPacketToSend += dataByte.ToString("X2") + ";";
                    }
                    // Send the packet
                    serialPort.Write(strPacketToSend);
                }
            }
        }

        public void SendPacket(Packet packetToSend)
        {
            lPackets.Add(packetToSend);
        }
    }
}
