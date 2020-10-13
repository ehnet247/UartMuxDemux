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
        private List<byte> aCurrentFrame;
        private string strLogFileName = String.Empty;
        private bool bFrameStarted = false;
        public bool bFrameTimeout = false;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        private System.Windows.Forms.Timer timerFrameTimeout;
        private BackgroundWorker backgroundWorker;
        private bool bKeepwatching = true;
        protected List<byte> lReadBuffer;
        protected int iByteCount;
        public UartLogger(SerialPort serialPort, string fileName)
        {
            // Save the log file name
            strLogFileName = fileName;
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
            // Instanciate the timer callback
            timerFrameTimeout.Tick += new System.EventHandler(this.timerFrameTimeout_Tick);
            // By default, the length of a frame is 5 bytes
            iFrameLength = 5;
            iFrameRxTimeout = 100;
            // By default, the frame reception timeout is 100ms
            // create a file
            try
            {
                textWriter = File.CreateText(fileName);
                textWriter.WriteLine("Date;Time;Counter;Event;Decoded Event;Param");
                textWriter.Close();
                textWriter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
                    if (serialPort.BytesToRead > 0)
                    {
                        // Read the available bytes
                        int iNbBytesToRead = serialPort.BytesToRead;
                        byte[] aReadBuffer = new byte[iNbBytesToRead];
                        int iReadByte = serialPort.Read(aReadBuffer, 0, iNbBytesToRead);
                        if (iReadByte > 0)
                        {
                            // Copy the buffer into the list
                            lReadBuffer.AddRange(aReadBuffer);
                            // Update the progress bar
                            iByteCount = lReadBuffer.Count;

                        }
                        // Test
                        Console.WriteLine(iReadByte + " bytes received");
                    }
                }
                else
                {
                    bKeepwatching = false;
                }
            }
        }

        private void serialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string strDataReceivedDate;
            string strDataReceivedTime;
            string strEvent = String.Empty;

            // Get the current date
            strDataReceivedDate = getDateString();
            // Get the current time
            strDataReceivedTime = getTimeString();

            // Read existing data
            try
            {
                while ((serialPort.BytesToRead > 0) && (bFrameTimeout == false))
                {
                    int iReadData = -1;
                    try
                    {
                        iReadData = serialPort.ReadByte();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    if (iReadData != -1)
                    {
                        // Convert iReadData into byte
                        byte readByte = (byte)iReadData;
                        if (bFrameStarted)
                        {
                            // Add read data to buffer
                            aCurrentFrame.Add(readByte);
                            // End the frame if EOF byte received
                            if (readByte == eofByte)
                            {
                                // Increase the frame counter
                                iFrameCount++;
                                // Reset the frame started flag
                                bFrameStarted = false;
                                // Interprets the frame contained in buffer and write it in the log file
                                InterpretAndWriteFrame();
                                // Reset the buffer
                                aCurrentFrame.Clear();
                            }
                        }
                        else
                        {
                            if (readByte == startByte)
                            {
                                // Start a new frame
                                aCurrentFrame = new List<byte>(iFrameLength);
                                bFrameStarted = true;
                                bFrameTimeout = false;
                                timerFrameTimeout.Start();
                                // Record the date and time of the frame
                                strCurrentFrameDate = strDataReceivedDate;
                                strCurrentFrameTime = strDataReceivedTime;
                                // Add read data to buffer
                                aCurrentFrame.Add(readByte);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                startIndex = aCurrentFrame.IndexOf(UartLogger.startByte);
                int iCounter = aCurrentFrame[startIndex + 1];
                strCounter = iCounter.ToString("X2");
                iEvent = (int)aCurrentFrame[startIndex + 2];
                iParam = (int)aCurrentFrame[startIndex + 3];
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

        private string getDateString()
        {
            string strDate = DateTime.Now.Day.ToString("00") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Year;
            return strDate;
        }

        private string getTimeString()
        {
            string strTime = DateTime.Now.Hour.ToString("00") + ":" +
                DateTime.Now.Minute.ToString("00") + ":" +
                DateTime.Now.Second.ToString("00") + ":" +
                DateTime.Now.Millisecond.ToString("000");
            return strTime;
        }
    }
    }





////////////////////////////////////////////////////////////////
// Other tried methods of uart reception                      //
////////////////////////////////////////////////////////////////
/*try
{
    while (serialPort.BytesToRead > 0)
    {
        // Get the received data
        int iBytesToRead = serialPort.BytesToRead;
        aReadBuffer = new byte[iBytesToRead];
        int readByte = serialPort.ReadByte();
        //check if the read byte is valid
        if (readByte >= 0)
        {
            if (bFrameStarted == false)
            {
                // Check if the received byte is a start byte
                if ((readByte == UartLogger.startByte) && (bSpecialByteReceived == false) && (bFrameStarted == false))
                {
                    currentFrame = new List<byte>();
                    bFrameStarted = true;
                }
                else
                {
                    // Nothing to do while we didn't receive a start of frame byte
                }
            }
            else
            {
                // Frame is started
                // Check if the received byte is a special byte
                if (readByte == UartLogger.specialByte)
                {
                    bSpecialByteReceived = true;
                }
                else if (bSpecialByteReceived == true)
                {
                    bSpecialByteReceived = false;
                }
                // Process bytes different from EoF byte
                if (readByte != UartLogger.eofByte)
                {
                    // Store the byte
                    currentFrame.Add((byte)readByte);
                    // Set the corresponding frame content
                    if (posInFrame == 0)
                    {
                        iEvent = (byte)readByte;
                    }
                    else if (posInFrame == 1)
                    {
                        iParam = (byte)readByte;
                    }
                    // Increase the index
                    posInFrame++;
                    // Max 4 bytes per frame
                    if (posInFrame > 3)
                    {
                        // Discard the current frame
                        bFrameStarted = false;
                        posInFrame = 0;
                        iParam = 0;
                        iEvent = 0;
                    }

                }
                // Check if the received End of Frame byte is not preceded by special byte
                else if (bSpecialByteReceived == false)
                {
                    iFrameCount++;
                    // Convert the event into a decoded string
                    if (iEvent <= Properties.Settings.Default.aMessageConversion.Count)
                    {
                        strEvent = Properties.Settings.Default.aMessageConversion[iEvent];
                    }
                    else
                    {
                        strEvent = "?"; // unknown
                    }//Write the data in textWriter
                    WriteLogLine(strDate, strTime, iEvent, strEvent, iParam.ToString());

                    // End the current frame

                    bFrameStarted = false;
                    posInFrame = 0;
                    iParam = 0;
                    iEvent = 0;
                }
            }
        }
    } // End of while (serialPort.BytesToRead > 0)
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
*/

////////////////////////////////////////////////////////////////

/*try
{
        int iBytesToRead = serialPort.BytesToRead;
        aReadBuffer = new byte[iBytesToRead];
        iReadBytes = serialPort.Read(aReadBuffer, 0, iBytesToRead);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }

    if (bFrameStarted == false)
    {
        // Convert array into current frame list
        List<byte> aFrameList = new List<byte>(aReadBuffer.Length);
        aFrameList = aReadBuffer.ToList();
        if (aReadBuffer.Contains(UartLogger.startByte))
        {
            // Remove all bytes before startByte
            aFrameList.RemoveRange(0, aFrameList.IndexOf(UartLogger.startByte));
            // Indicate a frame has began
            bFrameStarted = true;
        }
    }
if (bFrameStarted)
{
    // Add array to the current frame list
    List<byte> aReadBufferList = new List<byte>(aReadBuffer.Length);
    // Check if received list contains End of Frame byte
    if (aReadBufferList.Contains(UartLogger.eofByte))
    {
        // Remove all bytes after End ofFrame bybyte
        int index = aReadBufferList.IndexOf(UartLogger.eofByte);
        int count = aReadBufferList.Count - index;
        aReadBufferList.RemoveRange(index, count);
        // Add the new list to the current frame List
        aFrameList.Concat(aReadBufferList);
        // Launch the frame interpretation
        InterpretFrame();
        bFrameStarted = false;
    }
    else
    {
        // Do nothing: frame not started and no Start Byte received
    }

}
*/
