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
        private bool bFrameStarted = false;
        public bool bFrameTimeout = false;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        private string strDataReceivedDate;
        private string strDataReceivedTime;
        protected MuxPort muxPort = new MuxPort();
        protected List<DemuxPort> demuxPortsList = new List<DemuxPort>();
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDemuxPortsList();
            comPortNameSource.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            comboBoxBaudrate.SelectedIndex = 0;
            // Serial ports
            int settingsBaudrate = Properties.Settings.Default.serialBaudrate;
            comboBoxBaudrate.Text = settingsBaudrate.ToString();
            foreach (DemuxPort demuxPort in demuxPortsList)
            {
                demuxPort.serialPort.BaudRate = settingsBaudrate;
            }
            // Load saved settings
            LoadSettings();
        }

        private void LoadDemuxPortsList()
        {
            demuxPortsList = new List<DemuxPort>();
            foreach(string portName in Settings.Default.aDemuxPortsNames)
            {
                DemuxPort NewDp = new DemuxPort();
                NewDp.serialPort.PortName = portName;
                demuxPortsList.Add(NewDp);
            }
        }

        private void LoadSettings()
        {
        }

        private void comPortName1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strPortNumber = ((ComboBox)sender).Name;
            strPortNumber = strPortNumber.Substring(strPortNumber.Length - 1, 1);
            int portNumber = Convert.ToInt32(strPortNumber);

            demuxPortsList[portNumber].serialPort.PortName = ((ComboBox)sender).Text;
        }

        private void comboBoxBaudrate_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.serialBaudrate = Convert.ToInt32(comboBoxBaudrate.Text);
            Properties.Settings.Default.Save();
        }

        private void timerFrameTimeout_Tick(object sender, EventArgs e)
        {

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
                    foreach (DemuxPort dp in demuxPortsList)
                    {
                        if (dp.serialPort.IsOpen)
                        {
                            try
                            {
                                //sp.Write(readBuffer, 0, readBuffer.Length);
                            }
                            catch (Exception ex)
                            {
                                TraceLogger.ErrorTrace(ex.Message);
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm(demuxPortsList);
            DialogResult result = configForm.ShowDialog();
        }

        private void buttonOpenPorts_Click(object sender, EventArgs e)
        {
            // Open MUX port
            OpenMuxPort();
            if (muxPort.serialPort.IsOpen)
            {
                // Open demux ports
                OpenDeMuxPorts();
                // Start the timer
                timerDisplayRefresh.Start();
            }
            else
            {
                TraceLogger.ErrorTrace("Could not open MUX port");
                MessageBox.Show("Could not open MUX port!");
            }
        }

        private void OpenMuxPort()
        {
            if(muxPort.serialPort != null)
            {
                try
                {
                    muxPort.serialPort.Open();
                }
                catch(Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
                }
            }
        }

        private void CloseMuxPort()
        {
            if ((muxPort.serialPort != null) && (muxPort.serialPort.IsOpen))
            {
                try
                {
                    muxPort.serialPort.Close();
                }
                catch (Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
                }
            }
        }

        private void OpenDeMuxPorts()
        {
            foreach(DemuxPort dp in demuxPortsList)
            {
                if((dp.serialPort != null) && (dp.serialPort.IsOpen == false))
                try
                {
                    dp.serialPort.Open();
                }
                catch(Exception ex)
                {
                        TraceLogger.ErrorTrace(ex.Message);
                }
                // Start the display refresh timer
            }
        }

        private void CloseDeMuxPorts()
        {
            foreach (DemuxPort dp in demuxPortsList)
            {
                if ((dp.serialPort != null) && (dp.serialPort.IsOpen == true))
                    try
                    {
                        dp.serialPort.Close();
                    }
                    catch (Exception ex)
                    {
                        TraceLogger.ErrorTrace(ex.Message);
                    }
                // Start the display refresh timer
            }
        }

        private void timerDisplayRefresh_Tick_(object sender, EventArgs e)
        {
            RefreshCheckedListBoxDemuxPorts();
        }

        private void buttonClosePorts_Click(object sender, EventArgs e)
        {
            // Close the open ports
            CloseOpenPorts();
            //Stop the timer
            timerDisplayRefresh.Stop();
            // Refresh the checked list for a last time
            RefreshCheckedListBoxDemuxPorts();
        }

        private void RefreshCheckedListBoxDemuxPorts()
        {
            foreach (DemuxPort dp in demuxPortsList)
            {
                if (dp.serialPort.IsOpen)
                {
                    checkedListBoxDemuxPorts.Items.Add(dp.serialPort.PortName, CheckState.Checked);
                }
                else if (System.IO.Ports.SerialPort.GetPortNames().Contains(dp.serialPort.PortName))
                {
                    checkedListBoxDemuxPorts.Items.Add(dp.serialPort.PortName, CheckState.Unchecked);
                }
                else
                {
                    checkedListBoxDemuxPorts.Items.Add(dp.serialPort.PortName, CheckState.Indeterminate);
                }
            }
        }

        private void CloseOpenPorts()
        {
            // Close MUX port
            CloseMuxPort();
            // Close DEMUX ports
            CloseDeMuxPorts();
        }
    }
}
