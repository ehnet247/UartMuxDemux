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
        public ConfigForm(List<DemuxPort> dPorts)
        {
            this.demuxPortsList = dPorts;
            InitializeComponent();
        }

        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // Load demux ports
            foreach(DemuxPort demuxPort in demuxPortsList)
            {
                listBoxDemuxPorts.Items.Add(demuxPort.serialPort.PortName);
            }
            // Sort them
            SortItemsInListBox();
        }

        private void SortItemsInListBox()
        {
            // Clear the listbox
            listBoxDemuxPorts.Items.Clear();
            // Get a table of COM number
            List<int> portNumberList = new List<int>();
            foreach(DemuxPort dp in demuxPortsList)
            {
                string strPortNumber = string.Empty;
                try
                {
                    strPortNumber = dp.serialPort.PortName.Substring(3, (dp.serialPort.PortName.Length - 3));
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                portNumberList.Add(GetPortNumber(dp.serialPort.PortName));
                portNumberList.Sort();
                listBoxDemuxPorts.Items.AddRange(portNumberList);
            }
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
                Console.WriteLine(ex.Message);
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
            int iPortIndex = -1;
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
                demuxPortsList.Add(dp);
            }
        }

        private void listBoxDemuxPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display port characteristics
            DisplayPortCharacteristics();
        }

        private void DisplayPortCharacteristics()
        {
            string strSelectedPort = listBoxDemuxPorts.SelectedItem.ToString();
            int iPortNumber = GetPortNumber(strSelectedPort);
            DemuxPort dp = demuxPortsList[GetPortIndexByNumber(iPortNumber)];
            textBoxPortName.Text = dp.serialPort.PortName;
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
    }
}
