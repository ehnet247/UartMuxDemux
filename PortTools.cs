using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public static class PortTools
    {



        public static int GetPortNumber(string portname)
        {
            string strPortNumber = string.Empty;
            int iPortNumber = 0;
            try
            {
                strPortNumber = portname.Substring(3, (portname.Length - 3));
                iPortNumber = Convert.ToInt32(strPortNumber);
            }
            catch (Exception ex)
            {
                TraceLogger.ErrorTrace(ex.Message);
            }
            return iPortNumber;
        }

        public static string GetTimeString()
        {
            string strTime = DateTime.Now.Hour.ToString("00") + ":" +
                DateTime.Now.Minute.ToString("00") + ":" +
                DateTime.Now.Second.ToString("00") + "." +
                DateTime.Now.Millisecond.ToString("000");
            return strTime;
        }
    }
}
