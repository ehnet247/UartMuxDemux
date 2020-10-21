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
        private readonly BackgroundWorker readingbackgroundWorker;
        private readonly BackgroundWorker writingbackgroundWorker;
        private bool bKeepListening = true;
        private bool bFrameStarted = false;
        public bool bFrameTimeout = false;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        public string linkType;
        public string eofDetection;
        public byte startByte;
        private List<byte> lReadBuffer;
        private List<byte> lWriteBuffer;
        private string strDataReceivedDate;
        private string strDataReceivedTime;
        private MuxPort muxPort;

        public DemuxPort(MuxPort muxPort)
        {
            this.serialPort = new SerialPort();
            this.muxPort = muxPort;
            this.linkType = LinkType.Ascii;
            readingbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            readingbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReadingBackgroundWorker_DoWork);
            writingbackgroundWorker = new System.ComponentModel.BackgroundWorker();
            writingbackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WritingBackgroundWorker_DoWork);
            // Start the reading thread
            readingbackgroundWorker.RunWorkerAsync();
            // Start the writing thread
            writingbackgroundWorker.RunWorkerAsync();
        }

        public void Write(byte[] aWriteBuffer)
        {
            lWriteBuffer.AddRange(aWriteBuffer);
        }

        private void WritingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bKeepListening)
            {
                // Method 1: write the whole buffer
                if ((serialPort.IsOpen) && (lWriteBuffer.Count > 0))
                {
                    serialPort.Write(lWriteBuffer.ToArray(), 0, lWriteBuffer.Count);
                }

                // Method 2: byte by byte
                /*while(lWriteBuffer.Count > 0)
                {
                    byte[] aWriteBuffer = new byte[1];
                    aWriteBuffer[0] = lWriteBuffer[0];
                    serialPort.Write(aWriteBuffer, 0, 1);
                    lWriteBuffer.RemoveAt(0);
                }*/
            }
        }

        private void ReadingBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (bKeepListening)
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
