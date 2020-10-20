using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public static class LinkType
    {
        public const string Ascii = "Ascii";
        public const string Binary = "Binary";
        public static readonly string[] LinkTypes = { Ascii, Binary };
    }

    public static class EofDetection
    {
    public const string FixedSize = "Fixed size";
    public const string FirstByte = "First byte defines size";
        public const string Unknown = "Unknown";
        public static readonly string[] EofDetections = { FixedSize, FirstByte, Unknown };
    }
    public class DemuxPort
    {
        public SerialPort serialPort;
        private BackgroundWorker backgroundWorker;
        private bool bKeepwatching = true;
        private bool bFrameStarted = false;
        public bool bFrameTimeout = false;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        public string linkType;
        public string eofDetection;
        public byte startByte;
        private List<byte> lReadBuffer;
        private int iByteCount;
        private string strDataReceivedDate;
        private string strDataReceivedTime;
        private MuxPort muxPort;

        public DemuxPort(MuxPort muxPort)
        {
            this.serialPort = new SerialPort();
            this.muxPort = muxPort;
            this.linkType = LinkType.Ascii;
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bKeepwatching)
            {
                if (serialPort.IsOpen)
                {
                    if (bFrameStarted == false)
                    {
                        // Get the current date
                        strDataReceivedDate = GetDateString();
                        // Get the current time
                        strDataReceivedTime = GetTimeString();
                    }
                    if (serialPort.BytesToRead >= 5)
                    {
                        // Read the available bytes
                        //int iNbBytesToRead = serialPort.BytesToRead;
                        //byte[] aReadBuffer = new byte[iNbBytesToRead];
                        byte[] aReadBuffer = new byte[5];
                        //int iReadByte = serialPort.Read(aReadBuffer, 0, iNbBytesToRead);
                        int iReadByte = serialPort.Read(aReadBuffer, 0, 5);
                        if (iReadByte > 0)
                        {
                            if (aReadBuffer.Contains(startByte) && (bFrameStarted == false))
                            {
                                bFrameStarted = true;
                                // Record the date and time of the frame
                                strCurrentFrameDate = strDataReceivedDate;
                                strCurrentFrameTime = strDataReceivedTime;
                            }
                            // Copy the buffer into the list
                            lReadBuffer.AddRange(aReadBuffer);
                            if (lReadBuffer.Count >= 5)
                            {

                                // Write it into a file
                                //WriteRawData();
                            }
                            int iListLength = lReadBuffer.Count;
                            int iStartPosition = lReadBuffer.IndexOf(startByte);
                            int iFrameLength = -1;
                            if (iStartPosition >= 0)
                            {
                                iFrameLength = lReadBuffer.Count - iStartPosition;
                            }
                            if (iFrameLength >= 5)
                            {
                                if (bFrameStarted)
                                {
                                    // Add the received frame to the FIFO
                                    WriteFrameToMux();
                                    // Reset Frame started flag
                                    bFrameStarted = false;
                                    // Clear the buffer list
                                    lReadBuffer.Clear();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void WriteLogLine(string strDate, string strTime, string strCounter, string strEvent, string strDecodedEvent, string strParam)
        {
        }

        private void timerFrameTimeout_Tick(object sender, EventArgs e)
        {
            bFrameTimeout = true;
        }

        public static string GetDateString()
        {
            string strDate = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year;
            return strDate;
        }

        public static string GetTimeString()
        {
            string strTime = DateTime.Now.Hour.ToString("00") + ":" +
                DateTime.Now.Minute.ToString("00") + ":" +
                DateTime.Now.Second.ToString("00") + ":" +
                DateTime.Now.Millisecond.ToString("000");
            return strTime;
        }

        private void WriteFrameToMux()
        {
            //
            if((muxPort.serialPort != null) && (muxPort.serialPort.IsOpen))
            { }
        }
    }
}
