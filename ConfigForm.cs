using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UartMuxDemux
{
    public partial class ConfigForm : Form
    {
        protected List<SlavePort> slavePortsList;
        protected MasterPort masterPort;
        protected Mux mux;
        protected Demux demux;
        private string strSelectedPort = string.Empty;
        private int iSelectedPortIndex = 0;
        public ConfigForm(Mux mux, Demux demux, MasterPort masterPort, List<SlavePort> slavePorts)
        {
            this.mux = mux;
            this.demux = demux;
            this.slavePortsList = slavePorts;
            this.masterPort = masterPort;
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // Select the saved setting
            if ((Settings.Default.MasterPortName != String.Empty) &&
                (SerialPort.GetPortNames().Contains(Settings.Default.MasterPortName)))
            {
                comboBoxMuxPortName.SelectedItem = Settings.Default.MasterPortName;
            }
            else
            {
                // Load the available com port in comboBoxMuxPortName
                try
                {
                    comboBoxMuxPortName.Items.AddRange(SerialPort.GetPortNames());
                }
                catch (Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
                }
            }
            // Load slave ports
            foreach (SlavePort slavePort in slavePortsList)
            {
                listBoxSlavePorts.Items.Add(slavePort.serialPort.PortName);
            }
            // Sort them
            SortItemsInListBox();
            // Select the first slave port if any
            if (listBoxSlavePorts.Items.Count > 0)
            {
                listBoxSlavePorts.SelectedIndex = 0;
            }
        }

        private void SortItemsInListBox()
        {
            // Clear the listbox
            listBoxSlavePorts.Items.Clear();
            // Create a list of index sorted
            List<int> lSortedIndexesList = new List<int>();
            foreach( SlavePort sp in slavePortsList)
            {
                if (sp.serialPort.PortName.Length > 3)
                {
                    lSortedIndexesList.Add(PortTools.GetPortNumber(sp.serialPort.PortName));
                }
            }
            lSortedIndexesList.Sort();
            // Create the same list in string type, with "COM" added at the beginning
            List<string> portNameList = new List<string>();
            
            foreach (int slavePort in lSortedIndexesList)
            {
                portNameList.Add("COM" + slavePort);
            }
            // Copy the list of sorted port names into listbox
            listBoxSlavePorts.Items.AddRange(portNameList.ToArray());
        }
        private int GetPortIndexByNumber(int iPortNum)
        {
            int iPortIndex = 0;
            while (iPortIndex < slavePortsList.Count)
            {
                if (PortTools.GetPortNumber(slavePortsList[iPortIndex].serialPort.PortName) == iPortNum)
                {
                    return iPortIndex;
                }
                iPortIndex++;
            }
            return -1;
        }

        private int GetPortIndexByName(string portname)
        {
            int iPortIndex = 0;
            while (iPortIndex < slavePortsList.Count)
            {
                if (slavePortsList[iPortIndex].serialPort.PortName == portname)
                {
                    return iPortIndex;
                }
                iPortIndex++;
            }
            return -1;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // We can add a port if there is one free
            if (slavePortsList.Count < CustomDefs.MAX_NB_OF_DEMUX_PORT)
            {
                SlavePort slavePort = new SlavePort(mux);
                int iPortNumber = 1 + GetLastPortNumberInList();
                slavePort.serialPort.PortName = "COM" + iPortNumber;
                slavePortsList.Add(slavePort);
                SortItemsInListBox();
                // Select the new port to edit it
                listBoxSlavePorts.SelectedIndex = listBoxSlavePorts.Items.Count - 1;
            }
        }

        private int GetLastPortNumberInList()
        {
            // Get last index of list
            int iLastIndex = listBoxSlavePorts.Items.Count - 1;
            int iLastPortNumber = 0;
            // Get the last port name
            string strLastPortName = listBoxSlavePorts.Items[iLastIndex].ToString();
            // Get the port number as a string
            string strLastPortNumber = strLastPortName.Substring(3);
            // try to convert it in an int
            try
            {
                iLastPortNumber = Convert.ToInt32(strLastPortNumber);
            }
            catch (Exception ex)
            {
                TraceLogger.ErrorTrace(ex.Message);
            }
            return iLastPortNumber;
        }

        private void listBoxSlavePorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSlavePorts.SelectedItem != null)
            {
                //MessageBox.Show("SelectedIndex: " + listBoxDemuxPorts.SelectedIndex);
                // Save the selected port name
                strSelectedPort = listBoxSlavePorts.SelectedItem.ToString();
                // Save the selected port index
                iSelectedPortIndex = listBoxSlavePorts.SelectedIndex;
                // Display port characteristics
                DisplayPortCharacteristics();
            }
        }

        private void DisplayPortCharacteristics()
        {
            if (strSelectedPort != String.Empty)
            {
                SlavePort slavePort = slavePortsList[GetPortIndexByName(strSelectedPort)];
                textBoxSlavePortName.Text = slavePort.serialPort.PortName;
                comboBoxSlaveLinkType.Text = slavePort.GetLinkType();
            }
            else
            {
                TraceLogger.ErrorTrace("strSelectedPort is empty");
            }
        }

        private void textBoxPortName_TextChanged(object sender, EventArgs e)
        {
            if (iSelectedPortIndex >= 0)
            {
                if (slavePortsList[iSelectedPortIndex].serialPort.PortName != textBoxSlavePortName.Text)
                    slavePortsList[iSelectedPortIndex].serialPort.PortName = textBoxSlavePortName.Text;
                // Refresh the list
                SortItemsInListBox();

            }
        }

        private void comboBoxDemuxLinkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            int iPortIndex = GetPortIndexByName(strSelectedPort);
            if ((iPortIndex >= 0) && (iPortIndex < CustomDefs.MAX_NB_OF_DEMUX_PORT))
            {
                if (slavePortsList[iPortIndex].GetLinkType() != comboBoxSlaveLinkType.Text)
                {
                    slavePortsList[iPortIndex].SetLinkType(comboBoxSlaveLinkType.Text);
                    if (Settings.Default.aSlavePortsLinkTypes[iPortIndex] != slavePortsList[iPortIndex].GetLinkType())
                    {
                        Settings.Default.aSlavePortsLinkTypes[iPortIndex] = slavePortsList[iPortIndex].GetLinkType();
                    }
                    // Save the new value
                    Settings.Default.aSlavePortsLinkTypes[iPortIndex] = comboBoxSlaveLinkType.Text;
                }

            }
        }

        private void comboBoxMuxPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            masterPort.serialPort.PortName = comboBoxMuxPortName.Text;
        }

        private void numericUpDownMuxBaudrate_ValueChanged(object sender, EventArgs e)
        {
            masterPort.serialPort.BaudRate = (int)numericUpDownMuxBaudrate.Value;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // Copy the selected port config in the structure
            BufferizeSettingsBeforeClose();
            // Save the settings in the external config file
            SaveSettings();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBoxEofDetection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            int iPortIndex = GetPortIndexByName(strSelectedPort);
            if ((iPortIndex >= 0) && (iPortIndex < CustomDefs.MAX_NB_OF_DEMUX_PORT))
            {
                if (slavePortsList[iPortIndex].eofDetection != comboBoxEofDetection.Text)
                {
                    slavePortsList[iPortIndex].eofDetection = comboBoxEofDetection.Text;
                    if (Settings.Default.aSlavePortsEoFDetectionModes[iPortIndex] != slavePortsList[iPortIndex].GetLinkType())
                    {
                        Settings.Default.aSlavePortsEoFDetectionModes[iPortIndex] = slavePortsList[iPortIndex].GetLinkType();
                    }
                    // Save the new value
                    Settings.Default.aSlavePortsLinkTypes[iPortIndex] = comboBoxSlaveLinkType.Text;
                }

            }
        }

        private void numericUpDownTimeout_ValueChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            int iPortIndex = GetPortIndexByName(strSelectedPort);
            if ((iPortIndex >= 0) && (iPortIndex < CustomDefs.MAX_NB_OF_DEMUX_PORT))
            {
                if (slavePortsList[iPortIndex].GetPacketTimeoutValue() != numericUpDownTimeout.Value)
                {
                    slavePortsList[iPortIndex].SetPacketTimeoutValue((int)numericUpDownTimeout.Value);
                    // Save the setting if necessary
                    if (Convert.ToByte(Settings.Default.aSlavePortsTimeout[iPortIndex]) != slavePortsList[iPortIndex].GetPacketTimeoutValue())
                    {
                        Settings.Default.aSlavePortsTimeout[iPortIndex] = numericUpDownTimeout.Value.ToString();
                    }

                }

            }
        }

        private void numericUpDownPacketLength_ValueChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            int iPortIndex = GetPortIndexByName(strSelectedPort);
            if ((iPortIndex >= 0) && (iPortIndex < CustomDefs.MAX_NB_OF_DEMUX_PORT))
            {
                if (slavePortsList[iPortIndex].iPacketLength != numericUpDownPacketLength.Value)
                {
                    slavePortsList[iPortIndex].iPacketLength = (int)numericUpDownPacketLength.Value;
                    // Save the setting if necessary
                    if (Convert.ToByte(Settings.Default.aSlavePortsPacketLength[iPortIndex]) != slavePortsList[iPortIndex].iPacketLength)
                    {
                        Settings.Default.aSlavePortsPacketLength[iPortIndex] = numericUpDownPacketLength.Value.ToString();
                    }

                }

            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            // Get the selected port index
            int iPortIndex = GetPortIndexByName(strSelectedPort);
            if (iPortIndex >= 0)
            {
                // Remove the port from the slavePortsList
                slavePortsList.RemoveAt(iPortIndex);
                // Refresh the list
                SortItemsInListBox();

            }
        }

        private void SaveSettings()
        {
            // Save the slave ports list
            for(int iPortIndex = 0; iPortIndex < slavePortsList.Count; iPortIndex++)
            {
                Settings.Default.aSlavePortsNames[iPortIndex] = slavePortsList[iPortIndex].serialPort.PortName;
            }
            // Store the settings in a file
            Settings.Default.Save();
        }

        private void BufferizeSettingsBeforeClose()
        {
            slavePortsList[iSelectedPortIndex].SetLinkType(comboBoxSlaveLinkType.Text);
            slavePortsList[iSelectedPortIndex].serialPort.BaudRate = (int)numericUpDownMuxBaudrate.Value;
            slavePortsList[iSelectedPortIndex].eofDetection = comboBoxEofDetection.Text;
            slavePortsList[iSelectedPortIndex].SetPacketTimeoutValue((int)numericUpDownTimeout.Value);
            slavePortsList[iSelectedPortIndex].iPacketLength = (int)numericUpDownPacketLength.Value;
        }
    }
}
