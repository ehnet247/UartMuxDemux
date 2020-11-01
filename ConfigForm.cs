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
            // Load the available com port in comboBoxMasterPortName
            try
            {
                comboBoxMasterPortName.Items.AddRange(SerialPort.GetPortNames());
            }
            catch (Exception ex)
            {
                TraceLogger.ErrorTrace(ex.Message);
            }
            // Get the master port config
            if (true /*SerialPort.GetPortNames().Contains(masterPort.serialPort.PortName)*/)
            {
                comboBoxMasterPortName.Text = masterPort.serialPort.PortName;
                numericUpDownMasterPortBaudrate.Value = masterPort.serialPort.BaudRate;
            }
            // Get the slave ports config
            foreach (SlavePort slavePort in slavePortsList)
            {
                comboBoxSlavePortEdit.Items.Add(slavePort.serialPort.PortName);
            }
            // Sort them
            SortPortsInBoxes();
            // Select the first slave port if any
            if (comboBoxSlavePortEdit.Items.Count > 0)
            {
                comboBoxSlavePortEdit.SelectedIndex = 0;
            }
        }

        private void SortPortsInBoxes()
        {
            // Clear the ComboBox
            comboBoxSlavePortEdit.Items.Clear();
            // Clear the ListBox
            listBoxSlavePorts.Items.Clear();
            // Create a list of index sorted
            List<int> lSortedIndexesList = new List<int>();
            foreach (SlavePort sp in slavePortsList)
            {
                if (sp.serialPort.PortName.Length > 3)
                {
                    lSortedIndexesList.Add(PortTools.GetPortNumber(sp.serialPort.PortName));
                }
            }
            lSortedIndexesList.Sort();
            // Create the same list in string type, with "COM" added at the beginning
            List<string> slavePortsNameList = new List<string>();

            for (int iPortIndex = 0; iPortIndex < lSortedIndexesList.Count; iPortIndex++)
            {
                string strPortToAdd = "COM" + lSortedIndexesList[iPortIndex].ToString();
                slavePortsNameList.Add(strPortToAdd);
                listBoxSlavePorts.Items.Add(strPortToAdd);
                comboBoxSlavePortEdit.Items.Add(strPortToAdd);
            }
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
            if (slavePortsList.Count < CustomDefs.MAX_NB_OF_SLAVE_PORTS - 1)
            {
                SlavePort slavePort = new SlavePort(mux);
                int iPortNumber = 1 + GetLastPortNumberInList();
                slavePort.serialPort.PortName = "COM" + iPortNumber;
                slavePortsList.Add(slavePort);
                SortPortsInBoxes();
                // Select the new port to edit it
                comboBoxSlavePortEdit.SelectedIndex = comboBoxSlavePortEdit.Items.Count - 1;
            }
        }

        private int GetLastPortNumberInList()
        {
            // Get last index of list
            int iLastIndex = 0;
            if (comboBoxSlavePortEdit.Items.Count > 0)
            {
                iLastIndex = comboBoxSlavePortEdit.Items.Count - 1;
            }
            int iLastPortNumber = 0;
            if (comboBoxSlavePortEdit.Items.Count > 0)
            {
                // Get the last port name
                string strLastPortName = comboBoxSlavePortEdit.Items[iLastIndex].ToString();
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
            }
            return iLastPortNumber;
        }

        private void DisplayPortCharacteristics()
        {
            if (iSelectedPortIndex >= 0)
            {
                SlavePort slavePort = slavePortsList[iSelectedPortIndex];
                textBoxSlavePortName.Text = slavePort.serialPort.PortName;
                comboBoxSlaveLinkType.Text = slavePort.GetLinkType();
                numericUpDownSlavePortBaudrate.Value = slavePort.serialPort.BaudRate;
                comboBoxSlavePortEofDetection.Text = slavePort.eofDetection;
                numericUpDownSlavePortTimeout.Value = slavePort.GetPacketTimeoutValue();
                numericUpDownSlavePortPacketLength.Value = slavePort.iPacketLength;
            }
            else
            {
                TraceLogger.ErrorTrace("iSelectedPortIndex is empty");
            }
        }

        private void textBoxSlavePortName_TextChanged(object sender, EventArgs e)
        {
        }

        private void comboBoxDemuxLinkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((iSelectedPortIndex >= 0) && (iSelectedPortIndex < CustomDefs.MAX_NB_OF_SLAVE_PORTS - 1))
            {
                if (slavePortsList[iSelectedPortIndex].GetLinkType() != comboBoxSlaveLinkType.Text)
                {
                    slavePortsList[iSelectedPortIndex].SetLinkType(comboBoxSlaveLinkType.Text);
                }

            }
        }

        private void comboBoxMuxPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            masterPort.serialPort.PortName = comboBoxMasterPortName.Text;
        }

        private void numericUpDownMuxBaudrate_ValueChanged(object sender, EventArgs e)
        {
            masterPort.serialPort.BaudRate = (int)numericUpDownMasterPortBaudrate.Value;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // Copy the selected port config in the structure
            //BufferizeSettingsBeforeClose();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void comboBoxEofDetection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((iSelectedPortIndex >= 0) && (iSelectedPortIndex < CustomDefs.MAX_NB_OF_SLAVE_PORTS - 1))
            {
                if (slavePortsList[iSelectedPortIndex].eofDetection != comboBoxSlavePortEofDetection.Text)
                {
                    slavePortsList[iSelectedPortIndex].eofDetection = comboBoxSlavePortEofDetection.Text;
                }

            }
        }

        private void numericUpDownTimeout_ValueChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            if ((iSelectedPortIndex >= 0) && (iSelectedPortIndex < CustomDefs.MAX_NB_OF_SLAVE_PORTS - 1))
            {
                if (slavePortsList[iSelectedPortIndex].GetPacketTimeoutValue() != numericUpDownSlavePortTimeout.Value)
                {
                    slavePortsList[iSelectedPortIndex].SetPacketTimeoutValue((int)numericUpDownSlavePortTimeout.Value);

                }

            }
        }

        private void numericUpDownPacketLength_ValueChanged(object sender, EventArgs e)
        {
            if ((iSelectedPortIndex >= 0) && (iSelectedPortIndex < CustomDefs.MAX_NB_OF_SLAVE_PORTS - 1))
            {
                if (slavePortsList[iSelectedPortIndex].iPacketLength != numericUpDownSlavePortPacketLength.Value)
                {
                    slavePortsList[iSelectedPortIndex].iPacketLength = (int)numericUpDownSlavePortPacketLength.Value;

                }

            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (iSelectedPortIndex >= 0)
            {
                // Remove the port from the slavePortsList
                slavePortsList.RemoveAt(iSelectedPortIndex);
                // Refresh the lists
                SortPortsInBoxes();
                // Select the first slave port if any
                if (comboBoxSlavePortEdit.Items.Count > 0)
                {
                    comboBoxSlavePortEdit.SelectedIndex = 0;
                }

            }
        }

        private void BufferizeSettingsBeforeClose()
        {
            slavePortsList[iSelectedPortIndex].SetLinkType(comboBoxSlaveLinkType.Text);
            slavePortsList[iSelectedPortIndex].serialPort.BaudRate = (int)numericUpDownMasterPortBaudrate.Value;
            slavePortsList[iSelectedPortIndex].eofDetection = comboBoxSlavePortEofDetection.Text;
            slavePortsList[iSelectedPortIndex].SetPacketTimeoutValue((int)numericUpDownSlavePortTimeout.Value);
            slavePortsList[iSelectedPortIndex].iPacketLength = (int)numericUpDownSlavePortPacketLength.Value;
        }

        private void comboBoxPortEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSlavePortEdit.Text != string.Empty)
            {
                //MessageBox.Show("SelectedIndex: " + listBoxDemuxPorts.SelectedIndex);
                // Save the selected port index
                for (int iPortIndex = 0; iPortIndex < slavePortsList.Count; iPortIndex++)
                {
                    iSelectedPortIndex = iPortIndex;
                }
                // Display port characteristics
                DisplayPortCharacteristics();
            }
        }

        private void textBoxSlavePortName_Leave(object sender, EventArgs e)
        {
            if ((iSelectedPortIndex >= 0) && (slavePortsList.Count >= (iSelectedPortIndex + 1)))
            {
                if (slavePortsList[iSelectedPortIndex].serialPort.PortName != textBoxSlavePortName.Text)
                {
                    slavePortsList[iSelectedPortIndex].serialPort.PortName = textBoxSlavePortName.Text;
                    // Refresh the ComboBox
                    comboBoxSlavePortEdit.Text = textBoxSlavePortName.Text;
                    // Refresh the list
                    SortPortsInBoxes();
                }

            }
        }

        private void listBoxSlavePorts_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxSlavePorts.Text != string.Empty)
            {
                //MessageBox.Show("SelectedPort: " + listBoxSlavePorts.Text);
                // Get index of the selected port
                int iPortIndex = GetPortIndexByName(listBoxSlavePorts.Text);
                // Save the selected port index
                if (iPortIndex >= 0)
                {
                    iSelectedPortIndex = iPortIndex;
                }
                // Display port characteristics
                DisplayPortCharacteristics();
            }
        }

        private void numericUpDownSlavePortBaudrate_ValueChanged(object sender, EventArgs e)
        {
            if (iSelectedPortIndex >= 0)
            {
                slavePortsList[iSelectedPortIndex].serialPort.BaudRate = (int)numericUpDownSlavePortBaudrate.Value;
            }
        }
    }
}
