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
        private const int NB_OF_PORT = 3; 
        protected UartLogger[] uartLoggers = new UartLogger[6];
        private bool bFrameStarted = false;
        public bool bFrameTimeout = false;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        private string strDataReceivedDate;
        private string strDataReceivedTime;
        //private UartLogger uartLogger1;
        protected SerialPort[] serialPorts;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPorts = new SerialPort[NB_OF_PORT];
            for (int i=0;i<serialPorts.Length;i++)
            {
                serialPorts[i] = new SerialPort();
            }
            comPortNameSource.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            comPortName0.SelectedIndex = comPortName0.Items.Count - 1;
            comPortName0.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            comPortName0.SelectedIndex = comPortName0.Items.Count - 1;
            comPortName1.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            comPortName1.SelectedIndex = comPortName1.Items.Count - 1;
            comPortName2.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            comPortName2.SelectedIndex = comPortName2.Items.Count - 1;
            comboBoxBaudrate.SelectedIndex = 0;
            // Serial ports
            int settingsBaudrate = Properties.Settings.Default.serialBaudrate;
            comboBoxBaudrate.Text = settingsBaudrate.ToString();
            foreach (SerialPort serialPort in serialPorts)
            {
                serialPort.BaudRate = settingsBaudrate;
            }
            // Load saved settings
            LoadSettings();
        }

        private void LoadSettings()
        {
            UartLogger.startByte = Properties.Settings.Default.startByte;
            UartLogger.eofByte = Properties.Settings.Default.eofByte;
            numericUpDownEoF.Value = Properties.Settings.Default.eofByte;
            UartLogger.specialByte = Properties.Settings.Default.specialByte;
        }

        private void OpenClosePort(int portNumber, bool actionOpen)
        {

            if ((actionOpen == true) && (serialPorts[portNumber].IsOpen == false))
            {
                string fileName = "log_" + serialPorts[portNumber].PortName + ".csv";
                // Create an instance of UartLogger
                uartLoggers[portNumber] = new UartLogger(serialPorts[portNumber], fileName);
                try
                {
                    serialPorts[portNumber].Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                UpdateCheboxesText();
            }
            else if((actionOpen == false) && (serialPorts[portNumber].IsOpen == true))
            {
                try
                {
                    if (serialPorts[portNumber].IsOpen)
                    {
                        serialPorts[portNumber].Close();
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
            if (serialPorts[0].IsOpen)
            {
                RecordCheckbox0.Text = "Close";
            }
            else
            {
                RecordCheckbox0.Text = "Open";
            }

            if (serialPorts[1].IsOpen)
            {
                RecordCheckbox1.Text = "Close";
            }
            else
            {
                RecordCheckbox1.Text = "Open";
            }

            if (serialPorts[2].IsOpen)
            {
                RecordCheckbox2.Text = "Close";
            }
            else
            {
                RecordCheckbox2.Text = "Open";
            }
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
                UartLogger.eofByte = (byte)numericUpDownEoF.Value;

                switch (portNumber)
                {
                    case 0:
                        serialPorts[portNumber].PortName = comPortName0.Text;
                        break;
                    case 1:
                        serialPorts[portNumber].PortName = comPortName1.Text;
                        break;
                    case 2:
                        serialPorts[portNumber].PortName = comPortName2.Text;
                        break;
                }
            }
            else // If Closing
            {

                if (serialPorts[portNumber].IsOpen == false)
                {
                    switch (portNumber)
                    {
                        case 0:
                            serialPorts[portNumber].PortName = comPortName0.Text;
                            break;
                        case 1:
                            serialPorts[portNumber].PortName = comPortName1.Text;
                            break;
                        case 2:
                            serialPorts[portNumber].PortName = comPortName2.Text;
                            break;
                    }
                }
            }
            OpenClosePort(portNumber, openClose);
        }


        private void RefreshCheckboxes()
        {
            if (serialPorts[0].IsOpen)
            {
                RecordCheckbox0.Checked = true;
            }
            if (serialPorts[1].IsOpen)
            {
                RecordCheckbox1.Checked = true;
            }
            if (serialPorts[2].IsOpen)
            {
                RecordCheckbox1.Checked = true;
            }
        }

        private void RefreshCounters()
        {
            try
            {
                if (uartLoggers[0] != null)
                {
                    numericUpDown0.Value = uartLoggers[0].iFrameCount;
                    if (uartLoggers[0].IsPortOpen())
                    {
                        RecordCheckbox0.Text = "Close";
                    }
                    else
                    {
                        RecordCheckbox0.Text = "Open";
                    }
                }
                if (uartLoggers[1] != null)
                {
                    if (uartLoggers[1].IsPortOpen())
                    {
                        RecordCheckbox1.Text = "Close";
                    }
                    else
                    {
                        RecordCheckbox1.Text = "Open";
                    }
                }
                if (uartLoggers[2] != null)
                {
                    if (uartLoggers[2].IsPortOpen())
                    {
                        RecordCheckbox2.Text = "Close";
                    }
                    else
                    {
                        RecordCheckbox2.Text = "Open";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                if (uartLoggers[1] != null)
                {
                    numericUpDown1.Value = uartLoggers[1].iFrameCount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                if (uartLoggers[2] != null)
                {
                    numericUpDown2.Value = uartLoggers[2].iFrameCount;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void buttonOpenAll_Click(object sender, EventArgs e)
        {
            //b
        }

        private void comPortName1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strPortNumber = ((ComboBox)sender).Name;
            strPortNumber = strPortNumber.Substring(strPortNumber.Length - 1, 1);
            int portNumber = Convert.ToInt32(strPortNumber);

            serialPorts[portNumber].PortName = ((ComboBox)sender).Text;
        }

        private void CheckBoxConnect_CheckedChanged(object sender, EventArgs e)
        {
            OpenClosePort(1, ((CheckBox)sender).Checked);
        }

        private void comboBoxBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.serialBaudrate = Convert.ToInt32(comboBoxBaudrate.Text);
            Properties.Settings.Default.Save();
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
                // Configure the port
                serialPortSource.BaudRate = Properties.Settings.Default.serialBaudrate;
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
                    foreach (SerialPort sp in serialPorts)
                    {
                        if (sp.IsOpen)
                        {
                            try
                            {
                                sp.Write(readBuffer, 0, readBuffer.Length);
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
    }
}
