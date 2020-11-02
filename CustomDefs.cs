using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public static class CustomDefs
    {
        public const string strNewLineDef = "\n";
        public const int MAX_NB_OF_SLAVE_PORTS = 15;
        public const string strMuxHeader = "Rx";
        public const string strMuxFieldSeparator = " : ";
        public const string strDeMuxFieldSeparator = " : ";
        public const string strMuxTimecodePrefix = " ";
        public const string strMuxEndOfLine = "\n\r";
        public const char cDemuxEndOfLine = '\n';
        public const string strDemuxHeader = "Tx ";
        public const string strMuxAsciiByteSeparator = "";
        public const string strMuxBinaryByteSeparator = " ";
        public const string strDemuxByteSeparator = ":";
    }
    public static class LinkType
    {
        public const string Ascii = "ASCII";
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
}
