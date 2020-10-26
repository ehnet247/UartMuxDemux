/********************************************************/
/*                        MUX                           */
/* Receives data on slave ports and forward it          */
/* to Master port                                       */
/********************************************************/

using System;
using System.CodeDom;
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
        public string strData;
    }
    public class Mux
    {
        public string linkType;
        private readonly BackgroundWorker uploadPacketBackgroundWorker;
        private MasterPort masterPort;
        private List<string> lPacketsToUpload;

        public Mux(MasterPort masterPort)
        {
            this.masterPort = masterPort;
            uploadPacketBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            uploadPacketBackgroundWorker.WorkerSupportsCancellation = true;
            uploadPacketBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uploadPacketBackgroundWorker_DoWork);
            uploadPacketBackgroundWorker.RunWorkerAsync();
            lPacketsToUpload = new List<string>();
        }

        private void uploadPacketBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                if(masterPort.serialPort.IsOpen && (lPacketsToUpload.Count > 0))
                {
                    // Send the packet
                    masterPort.serialPort.Write(lPacketsToUpload[0]);
                   // Remove the packet from the list
                    lPacketsToUpload.RemoveAt(0);
                }
                else if(masterPort.serialPort.IsOpen == false)
                {
                    // Pause thread for 100ms to breath
                    System.Threading.Thread.Sleep(100);
                }
                else if (lPacketsToUpload.Count == 0)
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
            strPacketToUpload += CustomDefs.strMuxFieldSeparator;
            // Add the data bytes
            strPacketToUpload += packet.strData;
            // Add the end-of-packet byte
            strPacketToUpload += CustomDefs.strMuxEndOfLine;

        }
}
}
