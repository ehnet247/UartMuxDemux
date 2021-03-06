﻿/********************************************************/
/*                        DEMUX                         */
/* Receives data on master port and forward them        */
/* to slave ports                                       */
/********************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public class Demux
    {
        private readonly BackgroundWorker downloadPacketsBackgroundWorker;
        private readonly BackgroundWorker processReceivedPacketsBackgroundWorker;
        private List<Packet> lPacketsToDownload;
        private MasterPort masterPort;
        private List<SlavePort> slavePortsList;

        public Demux(MasterPort masterPort)
        {
            this.masterPort = masterPort;
            lPacketsToDownload = new List<Packet>();
            downloadPacketsBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            downloadPacketsBackgroundWorker.WorkerSupportsCancellation = true;
            processReceivedPacketsBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            processReceivedPacketsBackgroundWorker.WorkerSupportsCancellation = true;
            downloadPacketsBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.downloadPacketsBackgroundWorker_DoWork);
            processReceivedPacketsBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.processReceivedPacketsBackgroundWorker_DoWork);
            // Start the packets doxnloading thread
            downloadPacketsBackgroundWorker.RunWorkerAsync();
        }

        private void Download(Packet packet)
        {
            lPacketsToDownload.Add(packet);
        }

        private void downloadPacketsBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                if (masterPort.serialPort.IsOpen && (lPacketsToDownload.Count > 0))
                {
                    // Create a string initialized with a header
                    string strPacketToSend = CustomDefs.strMuxHeader;
                    // Add the COM port number to the string to send
                    strPacketToSend += PortTools.GetPortNumber(lPacketsToDownload[0].strPortSource).ToString();
                    // Add a field separator
                    strPacketToSend += CustomDefs.strMuxFieldSeparator;
                    // Add the time
                    strPacketToSend += lPacketsToDownload[0].strTime;
                    // Convert the packet data in ASCII chars
                    foreach (byte dataByte in lPacketsToDownload[0].aData)
                    {
                        strPacketToSend += dataByte.ToString("X2") + CustomDefs.strDemuxByteSeparator;
                    }
                    // Add an end of line char
                    strPacketToSend += CustomDefs.strMuxEndOfLine;
                    // Send the packet
                    masterPort.serialPort.Write(strPacketToSend);
                }
                else
                {
                    // Pause thread for 100ms to breath
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        private void processReceivedPacketsBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                if(lPacketsToDownload.Count > 0)
                {
                    // Create a string initialized with a header
                    string strPacketToSend = CustomDefs.strMuxHeader;
                    // Add the COM port number to the string to send
                    strPacketToSend += PortTools.GetPortNumber(lPacketsToDownload[0].strPortSource).ToString();
                    // Add a field separator
                    strPacketToSend += CustomDefs.strMuxFieldSeparator;
                    // Add the time
                    strPacketToSend += lPacketsToDownload[0].strTime;
                    // Convert the packet data in ASCII chars
                    foreach (byte dataByte in lPacketsToDownload[0].aData)
                    {
                        strPacketToSend += dataByte.ToString("X2") + CustomDefs.strDemuxByteSeparator;
                    }
                    // Add an end of line char
                    strPacketToSend += CustomDefs.strMuxEndOfLine;
                    // Send the packet
                    masterPort.serialPort.Write(strPacketToSend);
                }
                else
                {
                    // Pause thread for 100ms to breath
                    System.Threading.Thread.Sleep(100);
                }
            }
        }

        public void SetSlavePortList(List<SlavePort> slavePortsList)
        {
            this.slavePortsList = slavePortsList;
        }

        public void WritePacketToSlave(string strPortName, string strPacket)
        {
            int iPortIndex = -1;
            // Check the port number exists in slavePortsList
            for (int i = 0; i < slavePortsList.Count; i++)
            {
                if (slavePortsList[i].serialPort.PortName == strPortName)
                {
                    iPortIndex = i;
                    // Break the for loop
                    i = slavePortsList.Count;
                }
            }

            // If the port number exists in slavePortsList
            if (iPortIndex >= 0)
            {
                // Add the End of packet char as it has been
                // removed during string manipulation
                strPacket += CustomDefs.cDemuxEndOfLine.ToString();
                slavePortsList[iPortIndex].SendPacket(strPacket);
            }

        }
    }
}
