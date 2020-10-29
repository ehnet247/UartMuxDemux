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
        protected List<SlavePort> slavePortsList = new List<SlavePort>();
        protected Mux mux;
        protected Demux demux;
        protected MasterPort masterPort;
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Instanciate master port
            masterPort = new MasterPort(demux);
            // Instanciate the DEMUX
            demux = new Demux(masterPort);
            // Instanciate the MUX
            mux = new Mux(masterPort);
            // Load the saved slave port list
            LoadSlavePortsList();
            // Load saved settings
            LoadSettings();
        }

        private void LoadSlavePortsList()
        {
            slavePortsList = new List<SlavePort>();
            foreach(string portName in Settings.Default.aSlavePortsNames)
            {
                SlavePort NewSp = new SlavePort(mux);
                NewSp.serialPort.PortName = portName;
                slavePortsList.Add(NewSp);
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

            slavePortsList[portNumber].serialPort.PortName = ((ComboBox)sender).Text;
        }

        private void timerFrameTimeout_Tick(object sender, EventArgs e)
        {

        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm(mux, demux, masterPort, slavePortsList);
            DialogResult result = configForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Force the refresh of slave ports
                bPortsStateChanged = true;
                RefreshCheckedListBoxes();
            }
        }

        private void buttonOpenPorts_Click(object sender, EventArgs e)
        {
            // Open master port
            OpenMasterPort();
            if (masterPort.serialPort.IsOpen)
            {
                // Open slave ports
                OpenSlavePorts();
                // Start the timer
                timerDisplayRefresh.Start();
            }
        }

        private void OpenMasterPort()
        {
            if(masterPort != null)
            {
                bool bPreviousState = masterPort.serialPort.IsOpen;
                masterPort.OpenPort();
                // If state has changed, notify it to refresh the port list
                if(masterPort.serialPort.IsOpen != bPreviousState)
                {
                    bPortsStateChanged = true;
                }
            }
        }

        private void CloseMasterPort()
        {
            if ((masterPort.serialPort != null) && (masterPort.serialPort.IsOpen))
            {
                masterPort.ClosePort();
                if(masterPort.serialPort.IsOpen)
                {
                    TraceLogger.ErrorTrace("Master port couldn't be closed");
                }
            }
        }

        private void OpenSlavePorts()
        {
            foreach(SlavePort sp in slavePortsList)
            {
                bool bPreviousState = sp.serialPort.IsOpen;
                if ((sp.serialPort != null) && (sp.serialPort.IsOpen == false))
                try
                {
                        sp.OpenPort();
                }
                catch(Exception ex)
                {
                        TraceLogger.ErrorTrace(ex.Message);
                }
                // If state has changed, notify it to refresh the port list
                if (sp.serialPort.IsOpen != bPreviousState)
                {
                    bPortsStateChanged = true;
                }
            }
        }

        private void CloseSlavePorts()
        {
            foreach (SlavePort slavePort in slavePortsList)
            {
                bool bPreviousState = slavePort.serialPort.IsOpen;
                if ((slavePort.serialPort != null) && (slavePort.serialPort.IsOpen == true))
                {
                    slavePort.ClosePort();
                    // If state has changed, notify it to refresh the port list
                    if (slavePort.serialPort.IsOpen != bPreviousState)
                    {
                        bPortsStateChanged = true;
                    }
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
            /////// SLAVES   //////////////////////
            // Clear the list
            checkedListBoxSlavePorts.Items.Clear();
            // Add the slave ports
            for(int iPortIndex = 0; iPortIndex < slavePortsList.Count; iPortIndex++)
            {
                if (slavePortsList[iPortIndex].serialPort.IsOpen)
                {
                    checkedListBoxSlavePorts.Items.Add(slavePortsList[iPortIndex].serialPort.PortName, CheckState.Checked);
                }
                else if (System.IO.Ports.SerialPort.GetPortNames().Contains(slavePortsList[iPortIndex].serialPort.PortName))
                {
                    checkedListBoxSlavePorts.Items.Add(slavePortsList[iPortIndex].serialPort.PortName, CheckState.Unchecked);
                }
                else
                {
                    checkedListBoxSlavePorts.Items.Add(slavePortsList[iPortIndex].serialPort.PortName, CheckState.Indeterminate);
                }
            }

        }

        private void RefreshCheckedListBoxes()
        {
            /////// MASTER   //////////////////////
            // Set the name of the master port
            checkBoxMasterPort.Text = masterPort.serialPort.PortName;
            // Set the state of the master port
            checkBoxMasterPort.Checked = masterPort.serialPort.IsOpen;

            /////// SLAVES   //////////////////////
            // Instanciate the slave open ports table
            bool[] aOpenPorts = new bool[slavePortsList.Count];
            if (bPortsStateChanged)
            {
                // Clear the list
                checkedListBoxSlavePorts.Items.Clear();
            }
                // Get the state of each slave port
                for (int iPortIndex = 0;iPortIndex < slavePortsList.Count;iPortIndex++)
            {
                //
                if(slavePortsList[iPortIndex].serialPort.IsOpen)
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
            for (int iPortIndex = 0; iPortIndex < slavePortsList.Count + 1; iPortIndex++)
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
            // Close master port
            CloseMasterPort();
            // Close slave ports
            CloseSlavePorts();
        }
    }
}
