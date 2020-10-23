/********************************************************/
/*                        MUX                           */
/* Receives data on slave ports and forward it          */
/* to Master port                                       */
/********************************************************/

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
    public class Mux
    {
        public const string strRxHeader = "Rx";
        public const string strEndOfLine = "\n";
        public const string strFieldSeparator = ":";
        public const string strByteSeparator = ":";
        public SerialPort serialPort;
        public string linkType;
        private readonly BackgroundWorker readingbackgroundWorker;
        private System.ComponentModel.BackgroundWorker writingBackgroundWorker;
        private List<Demux> demuxPorts;
        private List<Packet> lPackets;

        public Mux(List<Demux> demuxPorts)
        {
            this.serialPort = new SerialPort();
            this.lPackets = new List<Packet>();
            this.demuxPorts = demuxPorts;
            readingbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            readingbackgroundWorker.WorkerSupportsCancellation = true;
            readingbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReadingBackgroundWorker_DoWork);
            writingBackgroundWorker = new BackgroundWorker();
            writingBackgroundWorker.WorkerSupportsCancellation = true;
            this.writingBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WritingBackgroundWorker_DoWork);
            this.writingBackgroundWorker.RunWorkerAsync();
        }
        
        private void WritingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while ((worker.CancellationPending == false) && (serialPort.IsOpen) && (serialPort.BytesToRead > 0))
            {
            }
        }

        private void ReadingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                if(serialPort.IsOpen && (lPackets.Count > 0))
                {
                    // Create a string initialized with a header
                    string strPacketToSend = strRxHeader;
                    // Add the COM port number to the string to send
                    strPacketToSend += PortTools.GetPortNumber(lPackets[0].strPortSource).ToString();
                    // Add a field separator
                    strPacketToSend += strFieldSeparator;
                    // Add the time
                    strPacketToSend += lPackets[0].strTime;
                    // Convert the packet data in ASCII chars
                    foreach (byte dataByte in lPackets[0].aData)
                    {
                        strPacketToSend += dataByte.ToString("X2") + strByteSeparator;
                    }
                    // Add an end of line char
                    strPacketToSend += strEndOfLine;
                    // Send the packet
                    serialPort.Write(strPacketToSend);
                }
                else if(serialPort.IsOpen == false)
                {
                    // Pause thread for 100ms to breath
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        public void SendPacket(Packet packetToSend)
        {
            lPackets.Add(packetToSend);
        }
    }
}
