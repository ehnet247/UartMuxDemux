using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.IO;
using System.ComponentModel;

namespace UartMuxDemux
{
    public class UartLogger
    {
        public int iFrameCount { get; private set; }
        public static byte eofByte { get; set; }
        public static byte specialByte { get; set; }
        public static byte startByte { get; set; }
        public static int iFrameLength { get; set; }
        public static int iFrameRxTimeout { get; set; }
        private SerialPort serialPort;
        private StreamWriter textWriter = null;
        private StreamWriter rawTextWriter = null;
        private List<byte> aCurrentFrame;
        private string strLogFileName = String.Empty;
        private string strRawLogFileName = String.Empty;
        private bool bFrameStarted = false;
        public bool bFrameTimeout = false;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        private System.Windows.Forms.Timer timerFrameTimeout;
        private BackgroundWorker backgroundWorker;
        private bool bKeepwatching = true;
        private List<byte> lReadBuffer;
        private int iByteCount;
        private string strDataReceivedDate;
        private string strDataReceivedTime;
        public UartLogger(SerialPort serialPort, string fileName)
        {
            // Save the log file name
            strLogFileName = fileName;
            strRawLogFileName = "Raw_" + fileName;
            // Get the instance of serialPort
            this.serialPort = serialPort;
            // Instanciate the rx callback
            //serialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort_DataReceived);
            backgroundWorker = new System.ComponentModel.BackgroundWorker();
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // Instanciate the rx timeout timer
            timerFrameTimeout = new System.Windows.Forms.Timer();
            // Set an rx timeout value of 100ms
            timerFrameTimeout.Interval = 100;
            // Reset the frame counter
            iFrameCount = 0;
            // Reset the frame started flag
            bFrameStarted = false;
            // Instanciate the timer callback
            timerFrameTimeout.Tick += new System.EventHandler(this.timerFrameTimeout_Tick);
            // By default, the length of a frame is 5 bytes
            iFrameLength = 5;
            // By default, the frame reception timeout is 100ms
            iFrameRxTimeout = 100;
            // Instanciate a new list for reception buffer
            lReadBuffer = new List<byte>();
            // create a file
            try
            {
                textWriter = File.CreateText(strLogFileName);
                textWriter.WriteLine("Date;Time;Counter;Event;Decoded Event;Param");
                textWriter.Close();
                textWriter.Dispose();
                // Idem for rawTextWriter
                rawTextWriter = File.CreateText(strRawLogFileName);
                rawTextWriter.WriteLine("Date;Time;Data 0;Data 1;Data 2;Data 3;Data 4");
                rawTextWriter.Close();
                rawTextWriter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            StartRx();
        }

        public bool IsPortOpen()
        {
            return this.serialPort.IsOpen;
        }


        public void StartRx()
        {
            bKeepwatching = true;
            backgroundWorker.RunWorkerAsync();
        }


        public void Stop()
        {
            bKeepwatching = false;
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
                                WriteRawData();
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
                                    // Interprets the frame contained in buffer and write it in the log file
                                    InterpretAndWriteFrame();
                                    // Reset Frame started flag
                                    bFrameStarted = false;
                                    // Increase the frame counter
                                    iFrameCount++;
                                    // Clear the buffer list
                                    lReadBuffer.Clear();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void InterpretAndWriteFrame()
        {
            int startIndex = 0;
            int iEvent = 0;
            int iParam = 0;
            string strCounter = " ";
            string strDecodedEvent = " ";
            try
            {
                startIndex = lReadBuffer.IndexOf(UartLogger.startByte);
                int iCounter = lReadBuffer[startIndex + 1];
                strCounter = iCounter.ToString("X2");
                iEvent = (int)lReadBuffer[startIndex + 2];
                iParam = (int)lReadBuffer[startIndex + 3];
                // Convert the event into a decoded string
                if (iEvent <= Properties.Settings.Default.aMessageConversion.Count)
                {
                    strDecodedEvent = Properties.Settings.Default.aMessageConversion[iEvent];
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Write the data in textWriter
            WriteLogLine(strCurrentFrameDate, strCurrentFrameTime, strCounter, iEvent.ToString("X2"), strDecodedEvent, iParam.ToString("X2"));
        }

        private void WriteRawData()
        {
            string strRawdata = string.Empty;
            try
            {
                // Convert the list into a string
                foreach (byte u8frameByte in lReadBuffer)
                {
                    strRawdata += u8frameByte.ToString("X2") + ";";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Write the data in rawTextWriter
            WriteRawDataLine(strCurrentFrameDate, strCurrentFrameTime, strRawdata);
        }

        private void WriteRawDataLine(string strDate, string strTime, string strData)
        {
            if (rawTextWriter != null)
            {
                try
                {
                    string strToWrite = strDate + ";" + strTime + ";" + strData;
                    rawTextWriter = File.AppendText(strRawLogFileName);
                    rawTextWriter.WriteLine(strToWrite);
                    rawTextWriter.Close();
                    rawTextWriter.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void WriteLogLine(string strDate, string strTime, string strCounter, string strEvent, string strDecodedEvent, string strParam)
        {
            if (textWriter != null)
            {
                try
                {
                    string strToWrite = strDate + ";" + strTime + ";" + strCounter + ";" + strEvent + ";" + strDecodedEvent + ";" + strParam;
                    textWriter = File.AppendText(strLogFileName);
                    textWriter.WriteLine(strToWrite);
                    textWriter.Close();
                    textWriter.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
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
    }

    
}
