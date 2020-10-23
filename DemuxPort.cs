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
        public string strNewLineDef = "\n";
        public SerialPort serialPort;
        private readonly BackgroundWorker readingbackgroundWorker;
        private readonly BackgroundWorker writingbackgroundWorker;
        private System.Windows.Forms.Timer timerPacketTimeout;
        private bool bKeepListening = true;
        private bool bPacketStarted = false;
        private bool bPacketTimeout = false;
        private byte u8PacketTimeoutValue = 0;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        private string linkType;
        public string eofDetection;
        public byte startByte;
        public int iPacketLength = 0;
        private List<byte> lReadBuffer;
        private List<byte> lWriteBuffer;
        private string strDataReceivedDate;
        private string strDataReceivedTime;
        private MuxPort muxPort;

        public DemuxPort(MuxPort muxPort)
        {
            this.serialPort = new SerialPort();
            this.timerPacketTimeout = new System.Windows.Forms.Timer();
            this.timerPacketTimeout.Tick += new System.EventHandler(this.timerPacketTimeout_Tick);
            this.muxPort = muxPort;
            this.linkType = LinkType.Ascii;
            this.lReadBuffer = new List<byte>();
            this.lWriteBuffer = new List<byte>();
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
            while((bKeepListening) && (serialPort.IsOpen) && (serialPort.BytesToRead > 0))
            {
                byte[] aReadBuffer = new byte[serialPort.BytesToRead];
                // Start the timer to detect a packet timeout
                if(timerPacketTimeout.Enabled == false)
                {
                    timerPacketTimeout.Start();
                }
                // Read the available bytes and copy them into the buffer
                serialPort.Read(aReadBuffer, 0, serialPort.BytesToRead);
                lReadBuffer.AddRange(aReadBuffer);
                // Check if the frame reception is achieved
                switch(eofDetection)
                {
                    case EofDetection.FixedSize:
                        if(aReadBuffer.Length >= iPacketLength)
                        {
                            // Send Packet to MUX
                        }
                        break;
            case EofDetection.FirstByte:
                        if (aReadBuffer.Length >= (int)aReadBuffer[0])
                        {
                            // Build a packet
                            Packet packetToSend = new Packet();
                            packetToSend.strPortSource = serialPort.PortName;
                            packetToSend.strTime = GetTimeString();
                            packetToSend.aData = aReadBuffer;
                            // Send the Packet to MUX
                            muxPort.SendPacket(packetToSend);
                        }
                        break;
                    case EofDetection.Unknown:
                        // Check the timeout flag
                        if(bPacketTimeout)
                        {
                            // Build a packet
                            Packet packetToSend = new Packet();
                            packetToSend.strPortSource = serialPort.PortName;
                            packetToSend.strTime = GetTimeString();
                            packetToSend.aData = aReadBuffer;
                            // Send the Packet to MUX
                            muxPort.SendPacket(packetToSend);
                        }
                        break;
             default:
                        //
                        break;
                }
            }
        }

        public byte GetPacketTimeoutValue()
        {
            return u8PacketTimeoutValue;
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
            if(strLinkType == LinkType.Ascii)
            {
                serialPort.NewLine = this.strNewLineDef;
            }
        }

        private void timerPacketTimeout_Tick(object sender, EventArgs e)
        {
            bPacketTimeout = true;
            timerPacketTimeout.Stop();
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
