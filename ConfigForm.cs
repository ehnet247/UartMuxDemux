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
        public const int MAX_NB_OF_DEMUX_PORT = 16;
        protected List<DemuxPort> demuxPortsList;
        protected MuxPort muxPort;
        private string strSelectedPort = string.Empty;
        public ConfigForm(MuxPort muxPort, List<DemuxPort> dPorts)
        {
            this.demuxPortsList = dPorts;
            this.muxPort = muxPort;
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // Select the saved setting
            if ((Settings.Default.MuxPortName != String.Empty) &&
                (SerialPort.GetPortNames().Contains(Settings.Default.MuxPortName)))
            {
                comboBoxMuxPortName.SelectedItem = Settings.Default.MuxPortName;
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
            foreach (DemuxPort demuxPort in demuxPortsList)
            {
                listBoxDemuxPorts.Items.Add(demuxPort.serialPort.PortName);
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
                foreach(DemuxPort dp in demuxPortsList)
                {
                    portNameList.Add(dp.serialPort.PortName);
                    portNameList.Sort();
                }
                listBoxDemuxPorts.Items.AddRange(portNameList.ToArray());
        }

        private int GetPortNumber(string portname)
        {
            string strPortNumber = string.Empty;
            int iPortNumber = 0;
            try
            {
                strPortNumber = portname.Substring(3, (portname.Length - 3));
                iPortNumber = Convert.ToInt32(strPortNumber);
            }
            catch (Exception ex)
            {
                TraceLogger.ErrorTrace(ex.Message);
            }
            return iPortNumber;
        }
        private int GetPortIndexByNumber(int iPortNum)
        {
            int iPortIndex = 0;
            while(iPortIndex < demuxPortsList.Count)
            {
                if (GetPortNumber(demuxPortsList[iPortIndex].serialPort.PortName) == iPortNum)
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
            while (iPortIndex < demuxPortsList.Count)
            {
                if (demuxPortsList[iPortIndex].serialPort.PortName == portname)
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
            if(demuxPortsList.Count < MAX_NB_OF_DEMUX_PORT)
            {
                DemuxPort dp = new DemuxPort(this.muxPort);
                dp.serialPort.PortName = "COM" + ((int)(1 + GetLastPortNumberInList())).ToString();
                demuxPortsList.Add(dp);
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
                DemuxPort dp = demuxPortsList[GetPortIndexByName(strSelectedPort)];
                textBoxDemuxPortName.Text = dp.serialPort.PortName;
                comboBoxDemuxLinkType.Text = dp.linkType;
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
                if(demuxPortsList[iPortIndex].serialPort.PortName != textBoxDemuxPortName.Text)
                demuxPortsList[iPortIndex].serialPort.PortName = textBoxDemuxPortName.Text;
                // Refresh the list
                SortItemsInListBox();

            }
        }

        private void comboBoxDemuxLinkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            int iPortIndex = GetPortIndexByName(strSelectedPort);
            if ((iPortIndex >= 0) && (iPortIndex < MAX_NB_OF_DEMUX_PORT))
            {
                if (demuxPortsList[iPortIndex].linkType != comboBoxDemuxLinkType.Text)
                {
                    demuxPortsList[iPortIndex].linkType = comboBoxDemuxLinkType.Text;
                    if(Settings.Default.aDemuxPortsLinkTypes[iPortIndex] != demuxPortsList[iPortIndex].linkType)
                    {
                        Settings.Default.aDemuxPortsLinkTypes[iPortIndex] = demuxPortsList[iPortIndex].linkType;
                    }
                    // Save the new value
                    Settings.Default.aDemuxPortsLinkTypes[iPortIndex] = comboBoxDemuxLinkType.Text;
                }

            }
        }

        private void comboBoxMuxPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            muxPort.serialPort.PortName = comboBoxMuxPortName.Text;
        }

        private void numericUpDownMuxBaudrate_ValueChanged(object sender, EventArgs e)
        {
            muxPort.serialPort.BaudRate = (int)numericUpDownMuxBaudrate.Value;
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
            if ((iPortIndex >= 0) && (iPortIndex < MAX_NB_OF_DEMUX_PORT))
            {
                if (demuxPortsList[iPortIndex].eofDetection != comboBoxEofDetection.Text)
                {
                    demuxPortsList[iPortIndex].eofDetection = comboBoxEofDetection.Text;
                    if (Settings.Default.aDemuxPortsEoFDetectionModes[iPortIndex] != demuxPortsList[iPortIndex].linkType)
                    {
                        Settings.Default.aDemuxPortsEoFDetectionModes[iPortIndex] = demuxPortsList[iPortIndex].linkType;
                    }
                    // Save the new value
                    Settings.Default.aDemuxPortsLinkTypes[iPortIndex] = comboBoxDemuxLinkType.Text;
                }

            }
        }

        private void numericUpDownTimeout_ValueChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            int iPortIndex = GetPortIndexByName(strSelectedPort);
            if ((iPortIndex >= 0) && (iPortIndex < MAX_NB_OF_DEMUX_PORT))
            {
                if (demuxPortsList[iPortIndex].GetPacketTimeout() != numericUpDownTimeout.Value)
                {
                    demuxPortsList[iPortIndex].SetPacketTimeout((int)numericUpDownTimeout.Value);
                    // Save the setting if necessary
                    if (Convert.ToByte(Settings.Default.aDemuxPortsTimeout[iPortIndex]) != demuxPortsList[iPortIndex].GetPacketTimeout())
                    {
                        Settings.Default.aDemuxPortsTimeout[iPortIndex] = numericUpDownTimeout.Value.ToString();
                    }
                    
                }

            }
        }

        private void numericUpDownPacketLength_ValueChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            int iPortIndex = GetPortIndexByName(strSelectedPort);
            if ((iPortIndex >= 0) && (iPortIndex < MAX_NB_OF_DEMUX_PORT))
            {
                if (demuxPortsList[iPortIndex].iPacketLength != numericUpDownPacketLength.Value)
                {
                    demuxPortsList[iPortIndex].iPacketLength = (int)numericUpDownPacketLength.Value;
                    // Save the setting if necessary
                    if (Convert.ToByte(Settings.Default.aDemuxPortsPacketLength[iPortIndex]) != demuxPortsList[iPortIndex].iPacketLength)
                    {
                        Settings.Default.aDemuxPortsPacketLength[iPortIndex] = numericUpDownPacketLength.Value.ToString();
                    }

                }

            }
        }
    }
}
