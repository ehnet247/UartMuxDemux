using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace UartMuxDemux
{
    public partial class MainForm : Form
    {
        public const int MAX_NB_OF_PORT = 16; 
        protected UartLogger[] uartLoggers = new UartLogger[6];
        private bool bFrameStarted = false;
        public bool bFrameTimeout = false;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        private string strDataReceivedDate;
        private string strDataReceivedTime;
        //private UartLogger uartLogger1;
        protected SerialPortsDescription[] serialPortsDesc;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPortsDesc = new SerialPortsDescription[MAX_NB_OF_PORT];
            for (int i=0;i< MAX_NB_OF_PORT; i++)
            {
                serialPortsDesc[i] = new SerialPortsDescription();
            }
            // Load saved settings
            LoadSettings();
            // Configure datagrid view
            SetupDataGrid();
        }

        private void SetupDataGrid()
        {
            dataGridViewFrames.ColumnCount = 3;
            dataGridViewFrames.Columns[0].Name = "PortName";
            dataGridViewFrames.Columns[1].Name = "Open";
            dataGridViewFrames.Columns[2].Name = "FrameCount";
        }

        private void LoadSettings()
        {
            //UartLogger.startByte = Properties.Settings.Default.nbOfDemuxPorts;
            //UartLogger.eofByte = Properties.Settings.Default.eofByte;
            //numericUpDownEoF.Value = Properties.Settings.Default.eofByte;
            //UartLogger.specialByte = Properties.Settings.Default.specialByte;
        }

        private void OpenClosePort(int portNumber, bool actionOpen)
        {

            if ((actionOpen == true) && (serialPortsDesc[portNumber].serialPort.IsOpen == false))
            {
                string fileName = "log_" + serialPortsDesc[portNumber].serialPort.PortName + ".csv";
                // Create an instance of UartLogger
                uartLoggers[portNumber] = new UartLogger(serialPortsDesc[portNumber].serialPort, fileName);
                try
                {
                    serialPortsDesc[portNumber].serialPort.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                UpdateCheboxesText();
            }
            else if((actionOpen == false) && (serialPortsDesc[portNumber].serialPort.IsOpen == true))
            {
                try
                {
                    if (serialPortsDesc[portNumber].serialPort.IsOpen)
                    {
                        serialPortsDesc[portNumber].serialPort.Close();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                UpdateCheboxesText();
            }
        }

        private void UpdateCheboxesText()
        {
        }

        private void RecordCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            string strPortNumber = ((CheckBox)sender).Name;
            strPortNumber = strPortNumber.Substring(strPortNumber.Length - 1, 1);
            int portNumber = Convert.ToInt32(strPortNumber);
            bool openClose = ((CheckBox)sender).Checked;
            if (openClose) // If Opening
            {

                // Set the End of Frame Byte
                //UartLogger.eofByte = (byte)numericUpDownEoF.Value;

                switch (portNumber)
                {
                    case 0:
                        
                        break;
                    case 1:
                        
                        break;
                    case 2:
                        
                        break;
                }
            }
            else // If Closing
            {

                if (serialPortsDesc[portNumber].serialPort.IsOpen == false)
                {
                }
            }
            OpenClosePort(portNumber, openClose);
        }


        private void RefreshCheckboxes()
        {
        }

        private void RefreshCounters()
        {
        }

        private void buttonOpenAll_Click(object sender, EventArgs e)
        {
            //b
        }

        private void CheckBoxConnect_CheckedChanged(object sender, EventArgs e)
        {
            OpenClosePort(1, ((CheckBox)sender).Checked);
        }

        private void timerRefreshDisplay_Tick(object sender, EventArgs e)
        {
            // Refresh the checkboxes
            RefreshCheckboxes();
            // Refresh the Counters
            RefreshCounters();
        }

        private void timerFrameTimeout_Tick(object sender, EventArgs e)
        {

        }

        private void OpenCloseCheckboxSource_CheckedChanged(object sender, EventArgs e)
        {
            bool CloseToOpen = ((CheckBox)sender).Checked;
            if (CloseToOpen)
            {
                // If Opening
                try
                {
                    serialPortSource.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Check if port successfully opened
                if(serialPortSource.IsOpen)
                {
                    OpenCloseCheckboxSource.Text = "Close";
                    // start the background listener
                    backgroundWorkerSource.RunWorkerAsync();
                }
            }
            else
            {
                // If Closing
                // Stop the background listener
                if(backgroundWorkerSource.IsBusy)
                {
                    try
                    {
                        backgroundWorkerSource.CancelAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                // Close the port
                try
                {
                    serialPortSource.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                // Refresh the text
                if(serialPortSource.IsOpen == false)
                {
                    OpenCloseCheckboxSource.Text = "Open";
                }
            }
        }

        private void backgroundWorkerSource_DoWork(object sender, DoWorkEventArgs e)
        {
            while(serialPortSource.IsOpen)
            {
                if (bFrameStarted == false)
                {
                    // Get the current date
                    strDataReceivedDate = UartLogger.GetDateString();
                    // Get the current time
                    strDataReceivedTime = UartLogger.GetTimeString();
                }
                if ((serialPortSource.IsOpen) && (serialPortSource.BytesToRead >= 5))
                {
                    // Read the available bytes
                    byte[] readBuffer = new byte[serialPortSource.BytesToRead];
                    serialPortSource.Read(readBuffer, 0, serialPortSource.BytesToRead);
                    // Write the received bytes in every open ports
                    foreach (SerialPortsDescription spDesc in serialPortsDesc)
                    {
                        if (spDesc.serialPort.IsOpen)
                        {
                            try
                            {
                                spDesc.serialPort.Write(readBuffer, 0, readBuffer.Length);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void timerRefreshCounters_Tick(object sender, EventArgs e)
        {

            // Refresh the checkboxes
            RefreshCheckboxes();
            // Refresh the Counters
            RefreshCounters();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PortsConfigForm portsConfigForm = new PortsConfigForm(serialPortsDesc);
            portsConfigForm.ShowDialog();
        }
    }
}
