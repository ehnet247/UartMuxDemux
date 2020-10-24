using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
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

        public void SendPacket()
        {
            //
        }

        private void UploadPacketToMux()
        {
            //
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
                serialPort.NewLine = CustomDefs.strNewLineDef;
            }
        }
    }
}
