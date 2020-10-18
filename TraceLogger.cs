using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    class TraceLogger
    {
        private static StreamWriter textWriter = null;
        public static string strLogFileName = "Log.txt";

        public static void ErrorTrace(string strMessage)
        {
            // Get the current date
            string strDate = GetDateString();
            // Get the current time
            string strTime = GetTimeString();
            try
            {
                if (File.Exists(strLogFileName))
                {
                    textWriter = File.AppendText(strLogFileName);
                }
                else
                {
                    textWriter = File.CreateText(strLogFileName);
                }
                textWriter.WriteLine(strDate + "@" + strTime + " ERROR: " + strMessage);
                textWriter.Close();
                textWriter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void EventTrace(string strMessage)
        {
            // Get the current date
            string strDate = GetDateString();
            // Get the current time
            string strTime = GetTimeString();
            try
            {
                if (File.Exists(strLogFileName))
                {
                    textWriter = File.AppendText(strLogFileName);
                }
                else
                {
                    textWriter = File.CreateText(strLogFileName);
                }
                textWriter.WriteLine(strDate + "@" + strTime + " EVENT: " + strMessage);
                textWriter.Close();
                textWriter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
    }
}
