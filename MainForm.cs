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
        private bool bPortsStateChanged = false;
        public bool bFrameTimeout = false;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        protected List<SlavePort> slavePortsList = new List<SlavePort>();
        protected Mux mux;
        protected Demux demux;
        protected MasterPort masterPort;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Instanciate master port
            masterPort = new MasterPort();
            // Instanciate the DEMUX
            demux = new Demux(masterPort);
            // Share the Demux instance with MasterPort
            masterPort.SetDemux(demux);
            // Instanciate the MUX
            mux = new Mux(masterPort);
            // Restore the saved settings
            try
            {
                Settings.Default.Reload();
            }
            catch (Exception ex)
            {
                TraceLogger.ErrorTrace(ex.Message);
            }
            // Load master port saved config
            LoadMasterPort();
            // Load the saved slave port list
            LoadSlavePortsList();
            // Share the  slave port list with the demux
            demux.SetSlavePortList(slavePortsList);

            // Display the ports status
            SetupPortsListBoxes();

        }

        private void LoadMasterPort()
        {
            if (Settings.Default.strMasterPortName != string.Empty)
            {
                masterPort.serialPort.PortName = Settings.Default.strMasterPortName;
            }
            if (Settings.Default.iMasterPortBaudrate > 0)
            {
                masterPort.serialPort.BaudRate = Settings.Default.iMasterPortBaudrate;
            }
        }

        private void LoadSlavePortsList()
        {
            slavePortsList = new List<SlavePort>();
            for(int iPortIndex = 0; iPortIndex < CustomDefs.MAX_NB_OF_SLAVE_PORTS - 1; iPortIndex++)
            {
                string strStoredPortName = Settings.Default.aSlavePortsNames[iPortIndex];
                if (strStoredPortName.StartsWith("COM"))
                {
                    SlavePort NewSp = new SlavePort(mux);
                    NewSp.serialPort.PortName = Settings.Default.aSlavePortsNames[iPortIndex];
                    NewSp.SetLinkType(Settings.Default.aSlavePortsLinkType[iPortIndex]);
                    NewSp.eofDetection = Settings.Default.aSlavePortsEoFDetectionMode[iPortIndex];
                    NewSp.SetPacketTimeoutValue(Convert.ToInt32(Settings.Default.aSlavePortsTimeout[iPortIndex]));
                    NewSp.iPacketLength = Convert.ToInt32(Settings.Default.aSlavePortsPacketLength[iPortIndex]);

                    slavePortsList.Add(NewSp);
                }
            }
            TraceLogger.EventTrace("SlavePortsList loaded");
        }

        private void comPortName1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strPortNumber = ((ComboBox)sender).Name;
            strPortNumber = strPortNumber.Substring(strPortNumber.Length - 1, 1);
            int portNumber = Convert.ToInt32(strPortNumber);

            slavePortsList[portNumber].serialPort.PortName = ((ComboBox)sender).Text;
        }

        private void buttonConfig_Click(object sender, EventArgs e)
        {
            ConfigForm configForm = new ConfigForm(mux, demux, masterPort, slavePortsList);
            DialogResult result = configForm.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Store the ports configuration in non volatile memory
                StoreSettings();
                // Force the refresh of slave ports
                bPortsStateChanged = true;
                RefreshCheckedListBoxes();
            }
        }

        private void StoreSettings()
        {
            /////// MASTER PORT   //////////////////////
            Settings.Default.strMasterPortName = masterPort.serialPort.PortName;
            Settings.Default.iMasterPortBaudrate = masterPort.serialPort.BaudRate;
            /////// SLAVE PORTS   //////////////////////
            for (int iPortIndex = 0; iPortIndex < CustomDefs.MAX_NB_OF_SLAVE_PORTS - 1; iPortIndex++)
            {
                if (slavePortsList.Count > iPortIndex)
                {
                    Settings.Default.aSlavePortsNames[iPortIndex] = slavePortsList[iPortIndex].serialPort.PortName;
                    Settings.Default.aSlavePortsLinkType[iPortIndex] = slavePortsList[iPortIndex].GetLinkType();
                    Settings.Default.aSlavePortsEoFDetectionMode[iPortIndex] = slavePortsList[iPortIndex].eofDetection;
                    Settings.Default.aSlavePortsTimeout[iPortIndex] = slavePortsList[iPortIndex].GetPacketTimeoutValue().ToString();
                    Settings.Default.aSlavePortsPacketLength[iPortIndex] = slavePortsList[iPortIndex].iPacketLength.ToString();
                }
                else
                {
                    Settings.Default.aSlavePortsNames[iPortIndex] = string.Empty;
                    Settings.Default.aSlavePortsLinkType[iPortIndex] = string.Empty;
                    Settings.Default.aSlavePortsEoFDetectionMode[iPortIndex] = string.Empty;
                    Settings.Default.aSlavePortsTimeout[iPortIndex] = string.Empty;
                    Settings.Default.aSlavePortsPacketLength[iPortIndex] = string.Empty;
                }
            }
            // Now store the settings in non volatile memory
            try
            {
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                TraceLogger.ErrorTrace(ex.Message);
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
                if ((slavePort.serialPort != null) && (slavePort.serialPort.IsOpen == true))
                {
                    slavePort.ClosePort();
                    // If state has changed, notify it to refresh the port list
                    if (slavePort.serialPort.IsOpen == false)
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

        private void SetupPortsListBoxes()
        {
            /////// MASTER /////////////////////////
            if (masterPort != null)
            {
                checkBoxMasterPort.Text = masterPort.serialPort.PortName;
            }
            /////// SLAVES //////////////////////
            // Clear the list
            checkedListBoxSlavePorts.Items.Clear();
            // Add the slave ports
            for(int iPortIndex = 0; iPortIndex < slavePortsList.Count; iPortIndex++)
            {
                if (slavePortsList[iPortIndex].serialPort.IsOpen)
                {
                    checkedListBoxSlavePorts.Items.Add(slavePortsList[iPortIndex].serialPort.PortName, CheckState.Checked);
                }
                else if (true /*System.IO.Ports.SerialPort.GetPortNames().Contains(slavePortsList[iPortIndex].serialPort.PortName)*/)
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
            /////// MASTER PORT   //////////////////////
            // Set the name of the master port
            checkBoxMasterPort.Text = masterPort.serialPort.PortName;
            // Set the state of the master port
            checkBoxMasterPort.Checked = masterPort.serialPort.IsOpen;

            /////// SLAVE PORTS   //////////////////////
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
                    SetupPortsListBoxes();
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
