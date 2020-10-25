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
        public string linkType;
        private readonly BackgroundWorker uploadPacketBackgroundWorker;
        private MasterPort masterPort;
        private List<Packet> lPacketsToUpload;

        public Mux(MasterPort masterPort)
        {
            this.masterPort = masterPort;
            uploadPacketBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            uploadPacketBackgroundWorker.WorkerSupportsCancellation = true;
            uploadPacketBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uploadPacketBackgroundWorker_DoWork);
            uploadPacketBackgroundWorker.RunWorkerAsync();
        }

        private void uploadPacketBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                if(masterPort.serialPort.IsOpen && (lPacketsToUpload.Count > 0))
                {
                    // Create a string initialized with a header
                    string strPacketToSend = CustomDefs.strMuxHeader;
                    // Add the COM port number to the string to send
                    strPacketToSend += PortTools.GetPortNumber(lPacketsToUpload[0].strPortSource).ToString();
                    // Add a field separator
                    strPacketToSend += CustomDefs.strMuxFieldSeparator;
                    // Add the time
                    strPacketToSend += lPacketsToUpload[0].strTime;
                    // Convert the packet data in ASCII chars
                    foreach (byte dataByte in lPacketsToUpload[0].aData)
                    {
                        strPacketToSend += dataByte.ToString("X2") + CustomDefs.strByteSeparator;
                    }
                    // Add an end of line char
                    strPacketToSend += CustomDefs.strMuxEndOfLine;
                    // Send the packet
                    masterPort.serialPort.Write(strPacketToSend);
                }
                else if(masterPort.serialPort.IsOpen == false)
                {
                    // Pause thread for 100ms to breath
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        /**************************************************************/
        /* Method: UploadPacket                                       */
        /* Description: called by slave ports when they've received a */
        /* packet that should be uploaded to the master port          */
        /*                                                            */
        /**************************************************************/
        public void AddPacketToUpload(Packet packet)
        {
            // Create a string that will contain the packet
            string strPacketToUpload = CustomDefs.strMuxHeader;
            // Add the port number
            strPacketToUpload += packet.strPortSource;
            // Add the time code
            strPacketToUpload += packet.strTime;
            lPacketsToUpload.Add(packet);
        }
}
}
