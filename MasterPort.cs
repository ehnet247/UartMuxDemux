using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly BackgroundWorker packetToDLReceptionBackgroundWorker;
        private List<byte> RxCurrentPacket = null;
        private bool bKeepReceiving;
        private string strRxPacket;

        public MasterPort()
        {
            serialPort = new SerialPort();
            serialPort.NewLine = CustomDefs.cDemuxEndOfLine.ToString();
            bKeepReceiving = true;
            strRxPacket = string.Empty;
            packetToDLReceptionBackgroundWorker = new BackgroundWorker();
            packetToDLReceptionBackgroundWorker.WorkerSupportsCancellation = true;
            packetToDLReceptionBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.packetToDLReceptionBackgroundWorker_DoWork);
        }

        public void SetDemux(Demux demux)
        {
            this.demux = demux;
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
                // Start the thread if port successfully open
                if (serialPort.IsOpen)
                {
                    packetToDLReceptionBackgroundWorker.RunWorkerAsync();
                }
            }
        }

        private void packetToDLReceptionBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (worker.CancellationPending == false)
            {
                if (serialPort.IsOpen)
                {
                    while ((serialPort.BytesToRead > 0) && (bKeepReceiving))
                    {
                        int iReadChar = 0;
                        iReadChar = serialPort.ReadChar();
                        char cReadChar = Convert.ToChar(iReadChar);
                        if (cReadChar != CustomDefs.cDemuxEndOfLine)
                        {
                            strRxPacket += cReadChar.ToString();
                        }
                        else
                        {
                            //bKeepReceiving = false;
                            ProcessFrame(strRxPacket);
                            strRxPacket = string.Empty;
                        }
                    }
                }
                else
                {
                    // Serial port is closed, stop the thread
                    packetToDLReceptionBackgroundWorker.CancelAsync();
                }
                    // Pause thread for 100ms to breath
                    System.Threading.Thread.Sleep(100);
            }
        }

        private void ProcessFrame(string strFrame)
        {
            if (strFrame.StartsWith(CustomDefs.strDemuxHeader))
            {
                int iStartIndex = 0;
                int iLength = 0;
                string strPortNumber = string.Empty;
                // Get the port number
                iStartIndex = CustomDefs.strDemuxHeader.Length;
                iLength = strFrame.IndexOf(CustomDefs.strDeMuxFieldSeparator) - CustomDefs.strDemuxHeader.Length;
                strPortNumber = strFrame.Substring(iStartIndex, iLength);
                // Get the data
                iStartIndex = strFrame.IndexOf(CustomDefs.strDeMuxFieldSeparator)
                    + CustomDefs.strDeMuxFieldSeparator.Length;
                iLength = strFrame.Length - iStartIndex;
                string strData = strFrame.Substring(iStartIndex, iLength);
                int iDestPortNumber = 0;
                try
                {
                    iDestPortNumber = Convert.ToInt32(strPortNumber);
                }
                catch (Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
                }
                string strDestPortName = "COM";
                strDestPortName += iDestPortNumber.ToString();
                if ((iDestPortNumber != 0) && (SerialPort.GetPortNames().Contains(strDestPortName)))
                {
                    demux.WritePacketToSlave(strDestPortName, strData);
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
