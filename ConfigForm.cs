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
        public ConfigForm(MuxPort muxPort, List<DemuxPort> dPorts)
        {
            this.demuxPortsList = dPorts;
            this.muxPort = muxPort;
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // Load the available com port in comboBoxMuxPortName
            comboBoxMuxPortName.Items.AddRange(SerialPort.GetPortNames());
            // Select the saved setting
            if ((Settings.Default.MuxPortName != String.Empty) &&
                (SerialPort.GetPortNames().Contains(Settings.Default.MuxPortName)))
            {
                comboBoxMuxPortName.SelectedItem = Settings.Default.MuxPortName;
            }
            else
            {
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
                DemuxPort dp = new DemuxPort();
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
            // Display port characteristics
            DisplayPortCharacteristics();
        }

        private void DisplayPortCharacteristics()
        {
            string strSelectedPort;
            if (listBoxDemuxPorts.SelectedItem != null)
            {
                strSelectedPort = listBoxDemuxPorts.SelectedItem.ToString();
                DemuxPort dp = demuxPortsList[GetPortIndexByName(strSelectedPort)];
                textBoxPortName.Text = dp.serialPort.PortName;
            }
        }

        private void textBoxPortName_TextChanged(object sender, EventArgs e)
        {
            // Get index of the selected port
            int iPortIndex = GetPortIndexByName(listBoxDemuxPorts.Text);
            if(iPortIndex >= 0)
            {
                demuxPortsList[iPortIndex].serialPort.PortName = textBoxPortName.Text;
                // Refresh the list
                SortItemsInListBox();

            }
        }

        private void comboBoxMuxPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
            muxPort.serialPort.PortName = comboBoxMuxPortName.Text;
        }

        private void comboBoxMuxLinkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            muxPort.linkType = comboBoxMuxLinkType.Text;
        }

        private void numericUpDownMuxBaudrate_ValueChanged(object sender, EventArgs e)
        {
            muxPort.serialPort.BaudRate = (int)numericUpDownMuxBaudrate.Value;
        }
    }
}
