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
                catch(Exception ex)
                {
                    TraceLogger.ErrorTrace(ex.Message);
                }
            }
            // Load demux ports
            foreach (SlavePort slavePort in slavePortsList)
            {
                listBoxDemuxPorts.Items.Add(slavePort.serialPort.PortName);
            }
            // Sort them
            SortItemsInListBox();
            // Select the first demux port if any
            if (listBoxDemuxPorts.Items.Count > 0)
            {
                listBoxDemuxPorts.SelectedIndex = 0;
            }
        }

        private void SortItemsInListBox()
        {
            List<string> portNameList = new List<string>();

            // Clear the listbox
            listBoxDemuxPorts.Items.Clear();
                // Constitute a list of the ports names
                foreach(SlavePort slavePort in slavePortsList)
                {
                    portNameList.Add(slavePort.serialPort.PortName);
                    portNameList.Sort();
                }
                listBoxDemuxPorts.Items.AddRange(portNameList.ToArray());
        }
        private int GetPortIndexByNumber(int iPortNum)
        {
            int iPortIndex = 0;
            while(iPortIndex < slavePortsList.Count)
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
            if(slavePortsList.Count < CustomDefs.MAX_NB_OF_DEMUX_PORT)
            {
                SlavePort slavePort = new SlavePort(mux);
                slavePort.serialPort.PortName = "COM" + ((int)(1 + GetLastPortNumberInList())).ToString();
                slavePortsList.Add(slavePort);
                SortItemsInListBox();
            }
        }

        private int GetLastPortNumberInList()
        {
            // Get last index of list
            int iLastIndex = listBoxDemuxPorts.Items.Count - 1;
            int iLastPortNumber = 0;
            // Get the last port name
            string strLastPortName = listBoxDemuxPorts.Items[iLastIndex].ToString();
            // Get the port number as a string
            string strLastPortNumber = strLastPortName.Substring(3);
            // try to convert it in an int
            try
            {
                iLastPortNumber = Convert.ToInt32(strLastPortNumber);
            }
            catch(Exception ex)
            {
                TraceLogger.ErrorTrace(ex.Message);
            }
            return iLastPortNumber;
        }

        private void listBoxDemuxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            strSelectedPort = listBoxDemuxPorts.SelectedItem.ToString();
            // Display port characteristics
            DisplayPortCharacteristics();
        }

        private void DisplayPortCharacteristics()
        {
            if (strSelectedPort != String.Empty)
            {
                SlavePort slavePort = slavePortsList[GetPortIndexByName(strSelectedPort)];
                textBoxDemuxPortName.Text = slavePort.serialPort.PortName;
                comboBoxDemuxLinkType.Text = slavePort.GetLinkType();
            }
            else
            {
                TraceLogger.ErrorTrace("strSelectedPort is empty");
            }
        }

        private void textBoxPortName_TextChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            int iPortIndex = GetPortIndexByName(strSelectedPort);
            if(iPortIndex >= 0)
            {
                if(slavePortsList[iPortIndex].serialPort.PortName != textBoxDemuxPortName.Text)
                    slavePortsList[iPortIndex].serialPort.PortName = textBoxDemuxPortName.Text;
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
                if (slavePortsList[iPortIndex].GetLinkType() != comboBoxDemuxLinkType.Text)
                {
                    slavePortsList[iPortIndex].SetLinkType(comboBoxDemuxLinkType.Text);
                    if(Settings.Default.aSlavePortsLinkTypes[iPortIndex] != slavePortsList[iPortIndex].GetLinkType())
                    {
                        Settings.Default.aSlavePortsLinkTypes[iPortIndex] = slavePortsList[iPortIndex].GetLinkType();
                    }
                    // Save the new value
                    Settings.Default.aSlavePortsLinkTypes[iPortIndex] = comboBoxDemuxLinkType.Text;
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
            // Save the settings in the external config file
            Settings.Default.Save();
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
                    Settings.Default.aSlavePortsLinkTypes[iPortIndex] = comboBoxDemuxLinkType.Text;
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
    }
}
