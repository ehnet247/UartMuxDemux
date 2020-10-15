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
using UartMuxDemux.Properties;

namespace UartMuxDemux
{
    public partial class PortsConfigForm : Form
    {
        public const int MAX_NB_OF_PORT = 16;
        // Serial ports description table
        protected SerialPortDescription[] serialPortsDescription;
        private int previousConfigPortNumber = 0;
        public PortsConfigForm(SerialPortDescription[] serialPortsDescription)
        {
            this.serialPortsDescription = serialPortsDescription;
            InitializeComponent();
        }

        private void PortsConfigForm_Load(object sender, EventArgs e)
        {
            // Get the number of ports
            try
            {
            numericUpDownPortNumber.Value = (decimal)Settings.Default.nbOfDemuxPorts;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            numericUpDownPortConfig.Maximum = numericUpDownPortNumber.Value;
            UpdateListboxPortConfig();
            // Restore the settings 
            for(int iPortNumber = 0; iPortNumber < Settings.Default.nbOfDemuxPorts; iPortNumber++)
            {
                LoadSettings(iPortNumber);
            }
            // Select the first port in the port list
            if (Settings.Default.nbOfDemuxPorts > 0)
            {
                listBoxPortConfig.SelectedIndex = 0;
            // Display the properties of the selected port
                UpdatePortProperties(0);
            }
        }

        private void UpdateListboxPortConfig()
        {
            int iPortNum = 0;
            while (iPortNum < numericUpDownPortNumber.Value)
            {
                if ((serialPortsDescription[iPortNum] != null) && (serialPortsDescription[iPortNum].serialPort != null))
                {
                    listBoxPortConfig.Items.Add(serialPortsDescription[iPortNum].serialPort.PortName);
                }
                iPortNum++;
            }
        }
            

        private void numericUpDownPortNumber_ValueChanged(object sender, EventArgs e)
        {
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            // Save the config
            SaveConfig();
            // Save the settings in external file
            try
            {
                Settings.Default.Save();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // Close the form
            this.Close();
        }

        private void SaveConfig()
        {
            Settings.Default.nbOfDemuxPorts = (int)numericUpDownPortNumber.Value;
            // Save ports config
            for (int portNum = 1; portNum < numericUpDownPortNumber.Value; portNum++)
            {
                SaveSettings(portNum);
            }
        }

        private void SaveSettings(int portNum)
        {
            Settings.Default.demuxPortsName[portNum] = serialPortsDescription[portNum].serialPort.PortName;
            Settings.Default.demuxPortsBaudrate[portNum] = serialPortsDescription[portNum].serialPort.BaudRate.ToString();
            Settings.Default.demuxPortsLinkType[portNum] = serialPortsDescription[portNum].strLinkType;
            Settings.Default.demuxPortsRxTimeout[portNum] = serialPortsDescription[portNum].iRxTimeout.ToString();
            Settings.Default.demuxPortsFrameDelimiting[portNum] = serialPortsDescription[portNum].strFrameDelimiting;
            Settings.Default.demuxPortsEOFByte[portNum] = serialPortsDescription[portNum].u8EoFByte.ToString();
            Settings.Default.demuxPortsFrameLength[portNum] = serialPortsDescription[portNum].iFrameLength.ToString();
        }

        private void LoadSettings(int portNum)
            {
            serialPortsDescription[portNum].serialPort.PortName = Settings.Default.demuxPortsName[portNum];
            serialPortsDescription[portNum].serialPort.BaudRate = Convert.ToInt32(Settings.Default.demuxPortsBaudrate[portNum]);
            serialPortsDescription[portNum].iRxTimeout = Convert.ToInt32(Settings.Default.demuxPortsRxTimeout[portNum]);
            serialPortsDescription[portNum].strLinkType = Settings.Default.demuxPortsLinkType[portNum];
            serialPortsDescription[portNum].strFrameDelimiting = Settings.Default.demuxPortsFrameDelimiting[portNum];
            serialPortsDescription[portNum].u8EoFByte = Convert.ToByte(Settings.Default.demuxPortsEOFByte[portNum]);
            serialPortsDescription[portNum].iFrameLength = Convert.ToInt32(Settings.Default.demuxPortsFrameLength);
        }

        private void UpdatePortProperties(int iPortIndex)
        {
            try
            {
                if (serialPortsDescription[iPortIndex].serialPort != null)
                {
                    // Fill-in the port name
                    textBoxPortName.Text = serialPortsDescription[iPortIndex].serialPort.PortName;
                    // Fill-in the port baudrate
                    numericUpDownBaudrate.Value = serialPortsDescription[iPortIndex].serialPort.BaudRate;
                    // Fill-in the port link type
                    comboBoxLinkType.Text = serialPortsDescription[iPortIndex].strLinkType;
                    // Fill-in the port frame delimiting method
                    comboBoxFrameDelimiting.Text = serialPortsDescription[iPortIndex].strFrameDelimiting;
                    // Fill-in the port end of frame byte
                    numericUpDownEoFByte.Value = Convert.ToByte(serialPortsDescription[iPortIndex].u8EoFByte);
                    // Fill-in the port end of frame length
                    numericUpDownFrameLength.Value = serialPortsDescription[iPortIndex].iFrameLength;
                    // Set the link type comboBox value for the new port number
                    comboBoxLinkType.Text = serialPortsDescription[iPortIndex].strLinkType;
                    // Set the end of frame byte numericUpDown value for the new port number
                    numericUpDownEoFByte.Value = serialPortsDescription[iPortIndex].u8EoFByte;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void numericUpDownPortConfig_ValueChanged(object sender, EventArgs e)
        {
            // Save the settings of previous port
            SaveSettings(previousConfigPortNumber);
            // Display the properties of the selected port
            UpdatePortProperties((int)numericUpDownPortConfig.Value - 1);
        }

        private void DisplayCorrectEoFFields()
        {
            if (comboBoxFrameDelimiting.Text == "Fixed")
            {
                labelFrameLength.Visible = true;
                numericUpDownFrameLength.Visible = true;
                labelEoFByte.Visible = false;
                numericUpDownEoFByte.Visible = false;
            }
            else if (comboBoxFrameDelimiting.Text == "Defined by first byte")
            {
                labelEoFByte.Visible = false;
                numericUpDownEoFByte.Visible = false;
                labelFrameLength.Visible = false;
                numericUpDownFrameLength.Visible = false;
            }
            else
            {
                labelEoFByte.Visible = true;
                numericUpDownEoFByte.Visible = true;
            }
        }

        private void comboBoxLinkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new value to the current serialPortsDescription item
            serialPortsDescription[(int)numericUpDownPortConfig.Value - 1].strLinkType = comboBoxLinkType.Text;
        }

        private void comboBoxFrameDelimiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new value to the current serialPortsDescription item
            serialPortsDescription[(int)numericUpDownPortConfig.Value].strFrameDelimiting = comboBoxFrameDelimiting.Text;
            DisplayCorrectEoFFields();
        }

        private void numericUpDownEoFByte_ValueChanged(object sender, EventArgs e)
        {
            // Set the new value to the current serialPortsDescription item
            serialPortsDescription[(int)numericUpDownPortConfig.Value].u8EoFByte = (byte)numericUpDownEoFByte.Value;
        }

        private void numericUpDownFrameLength_ValueChanged(object sender, EventArgs e)
        {
            // Set the new value to the current serialPortsDescription item
            serialPortsDescription[(int)numericUpDownPortConfig.Value].iFrameLength = (int)numericUpDownFrameLength.Value;
        }

        private void textBoxPortName_TextChanged(object sender, EventArgs e)
        {
            serialPortsDescription[(int)numericUpDownPortConfig.Value - 1].serialPort.PortName = textBoxPortName.Text;
        }

        private void numericUpDownBaudrate_ValueChanged(object sender, EventArgs e)
        {
            serialPortsDescription[(int)numericUpDownPortConfig.Value - 1].serialPort.BaudRate = (int)numericUpDownBaudrate.Value;
        }

        private void listBoxPortConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Save the settings of previous port
            SaveSettings(previousConfigPortNumber);
            // Display the properties of the selected port
            UpdatePortProperties(listBoxPortConfig.SelectedIndex);
            previousConfigPortNumber = listBoxPortConfig.SelectedIndex;
        }
    }
}
