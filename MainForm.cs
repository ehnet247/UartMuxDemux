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
        private bool bPortsStateChanged = false;
        public bool bFrameTimeout = false;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        private string strDataReceivedDate;
        private string strDataReceivedTime;
        protected List<DemuxPort> demuxPortsList = new List<DemuxPort>();
        protected MuxPort muxPort;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Instanciate muxPort
            muxPort = new MuxPort(demuxPortsList);
            LoadDemuxPortsList();
            // Load saved settings
            LoadSettings();
        }

        private void LoadDemuxPortsList()
        {
            demuxPortsList = new List<DemuxPort>();
            foreach(string portName in Settings.Default.aDemuxPortsNames)
            {
                DemuxPort NewDp = new DemuxPort(this.muxPort);
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

        private void timerFrameTimeout_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm(muxPort, demuxPortsList);
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
        }

        private void OpenMuxPort()
        {
            if(muxPort.serialPort != null)
            {
                bool bPreviousState = muxPort.serialPort.IsOpen;
                try
                {
                    muxPort.serialPort.Open();
                }
                catch(Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
                }
                // If state has changed, notify it to refresh the port list
                if(muxPort.serialPort.IsOpen != bPreviousState)
                {
                    bPortsStateChanged = true;
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
                if(muxPort.serialPort.IsOpen)
                {
                    TraceLogger.ErrorTrace("Mux port couldn't be closed");
                }
            }
        }

        private void OpenDeMuxPorts()
        {
            foreach(DemuxPort dp in demuxPortsList)
            {
                bool bPreviousState = dp.serialPort.IsOpen;
                if ((dp.serialPort != null) && (dp.serialPort.IsOpen == false))
                try
                {
                    dp.serialPort.Open();
                }
                catch(Exception ex)
                {
                        TraceLogger.ErrorTrace(ex.Message);
                }
                // If state has changed, notify it to refresh the port list
                if (dp.serialPort.IsOpen != bPreviousState)
                {
                    bPortsStateChanged = true;
                }
            }
        }

        private void CloseDeMuxPorts()
        {
            foreach (DemuxPort dp in demuxPortsList)
            {
                bool bPreviousState = dp.serialPort.IsOpen;
                if ((dp.serialPort != null) && (dp.serialPort.IsOpen == true))
                    try
                    {
                        dp.serialPort.Close();
                    }
                    catch (Exception ex)
                    {
                        TraceLogger.ErrorTrace(ex.Message);
                    }
                // If state has changed, notify it to refresh the port list
                if (dp.serialPort.IsOpen != bPreviousState)
                {
                    bPortsStateChanged = true;
                }
            }
        }

        private void timerDisplayRefresh_Tick_(object sender, EventArgs e)
        {
            RefreshCheckedListBoxes();
        }

        private void buttonClosePorts_Click(object sender, EventArgs e)
        {
            // Close the open ports
            CloseOpenPorts();
        }

        private void SetupListBoxPorts()
        {
            // Clear the list
            checkedListBoxDemuxPorts.Items.Clear();
            // Add the demux ports
            for(int iPortIndex = 0; iPortIndex < demuxPortsList.Count; iPortIndex++)
            {
                if (demuxPortsList[iPortIndex].serialPort.IsOpen)
                {
                    checkedListBoxDemuxPorts.Items.Add(demuxPortsList[iPortIndex].serialPort.PortName, CheckState.Checked);
                }
                else if (System.IO.Ports.SerialPort.GetPortNames().Contains(demuxPortsList[iPortIndex].serialPort.PortName))
                {
                    checkedListBoxDemuxPorts.Items.Add(demuxPortsList[iPortIndex].serialPort.PortName, CheckState.Unchecked);
                }
                else
                {
                    checkedListBoxDemuxPorts.Items.Add(demuxPortsList[iPortIndex].serialPort.PortName, CheckState.Indeterminate);
                }
            }
        }

        private void RefreshCheckedListBoxes()
        {
            // MUX
            // Set the name of the mux port
            checkBoxMuxPort.Text = muxPort.serialPort.PortName;
            // Set the state of the mux port
            checkBoxMuxPort.Checked = muxPort.serialPort.IsOpen;

            // DEMUX
            // Instanciate the demux open ports table
            bool[] aOpenPorts = new bool[demuxPortsList.Count];
            if (bPortsStateChanged)
            {
                // Clear the list
                checkedListBoxDemuxPorts.Items.Clear();
            }
                // Get the state of each demux port
                for (int iPortIndex = 0;iPortIndex < demuxPortsList.Count;iPortIndex++)
            {
                //
                if(demuxPortsList[iPortIndex].serialPort.IsOpen)
                {
                    aOpenPorts[iPortIndex] = true;
                }
                else
                {
                    aOpenPorts[iPortIndex] = false;
                }
            }
            // Check if the state of the mux port has changed
            // For each demux port, check if the state has changed
            for (int iPortIndex = 0; iPortIndex < demuxPortsList.Count + 1; iPortIndex++)
            {
                if (bPortsStateChanged)
                {
                    // Rebuild the list
                    SetupListBoxPorts();
                    bPortsStateChanged = false;
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
