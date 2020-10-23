using System;
using System.Collections.Generic;
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
    public class SlavePort
    {
        private System.Windows.Forms.Timer timerPacketTimeout;
        private bool bPacketStarted = false;
        private bool bPacketTimeout = false;
        private byte u8PacketTimeoutValue = 0;
        public string eofDetection;
        private string linkType;
        public byte startByte;
        public int iPacketLength = 0;

        public SerialPort serialPort;

        public SlavePort()
        {
            serialPort = new SerialPort();
            this.linkType = LinkType.Ascii;
            this.timerPacketTimeout = new System.Windows.Forms.Timer();
            this.timerPacketTimeout.Tick += new System.EventHandler(this.timerPacketTimeout_Tick);
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
            if (strLinkType == LinkType.Ascii)
            {
                serialPort.NewLine = this.strNewLineDef;
            }
        }
    }
}
