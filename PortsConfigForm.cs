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
        // Serial ports description table
        protected SerialPortsDescription[] serialPortsDescription;
        private decimal previousConfigPortNumber = 0;
        public PortsConfigForm(SerialPortsDescription[] serialPortsDescription)
        {
            this.serialPortsDescription = serialPortsDescription;
            InitializeComponent();
        }

        private void PortsConfigForm_Load(object sender, EventArgs e)
        {
            // Get the number of ports
            numericUpDownPortNumber.Value = (decimal)Settings.Default.nbOfDemuxPorts;
            numericUpDownPortConfig.Maximum = numericUpDownPortNumber.Value;
            propertyGridPorts.SelectedObject = serialPortsDescription[(int)numericUpDownPortNumber.Value].serialPort;
            // Display the properties of the selected port
            UpdatePortProperties();
        }

        private void numericUpDownPortNumber_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownPortConfig.Maximum = numericUpDownPortNumber.Value;
            // Display the properties of the selected port
            UpdatePortProperties();
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

        private void SaveSettings(decimal portNum)
        {
            switch (portNum)
            {
                case 1:

                    // Save the serialPort instance
                    Settings.Default.demuxPort1 = serialPortsDescription[0].serialPort;
                    // Save the link type
                    Settings.Default.linkType1 = serialPortsDescription[0].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter1 = serialPortsDescription[0].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize1 = (uint)serialPortsDescription[0].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte1 = serialPortsDescription[0].u8EoFByte;
                    break;
                case 2:
                    // Save the serialPort instance
                    Settings.Default.demuxPort2 = serialPortsDescription[1].serialPort;
                    // Save the link type
                    Settings.Default.linkType2 = serialPortsDescription[1].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter2 = serialPortsDescription[1].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize2 = (uint)serialPortsDescription[1].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte2 = serialPortsDescription[1].u8EoFByte;
                    break;
                case 3:
                    // Save the serialPort instance
                    Settings.Default.demuxPort3 = serialPortsDescription[2].serialPort;
                    // Save the link type
                    Settings.Default.linkType3 = serialPortsDescription[2].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter3 = serialPortsDescription[2].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize3 = (uint)serialPortsDescription[2].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte3 = serialPortsDescription[2].u8EoFByte;
                    break;
                case 4:
                    // Save the serialPort instance
                    Settings.Default.demuxPort4 = serialPortsDescription[3].serialPort;
                    // Save the link type
                    Settings.Default.linkType4 = serialPortsDescription[3].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter4 = serialPortsDescription[3].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize4 = (uint)serialPortsDescription[3].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte4 = serialPortsDescription[3].u8EoFByte;
                    break;
                case 5:
                    // Save the serialPort instance
                    Settings.Default.demuxPort5 = serialPortsDescription[4].serialPort;
                    // Save the link type
                    Settings.Default.linkType5 = serialPortsDescription[4].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter5 = serialPortsDescription[4].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize5 = (uint)serialPortsDescription[4].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte5 = serialPortsDescription[4].u8EoFByte;
                    break;
                case 6:
                    // Save the serialPort instance
                    Settings.Default.demuxPort6 = serialPortsDescription[5].serialPort;
                    // Save the link type
                    Settings.Default.linkType6 = serialPortsDescription[5].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter6 = serialPortsDescription[5].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize6 = (uint)serialPortsDescription[5].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte6 = serialPortsDescription[5].u8EoFByte;
                    break;
                case 7:
                    // Save the serialPort instance
                    Settings.Default.demuxPort7 = serialPortsDescription[6].serialPort;
                    // Save the link type
                    Settings.Default.linkType7 = serialPortsDescription[6].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter7 = serialPortsDescription[6].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize7 = (uint)serialPortsDescription[6].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte7 = serialPortsDescription[6].u8EoFByte;
                    break;
                case 8:
                    // Save the serialPort instance
                    Settings.Default.demuxPort8 = serialPortsDescription[7].serialPort;
                    // Save the link type
                    Settings.Default.linkType8 = serialPortsDescription[7].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter8 = serialPortsDescription[7].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize8 = (uint)serialPortsDescription[7].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte8 = serialPortsDescription[7].u8EoFByte;
                    break;
                case 9:
                    // Save the serialPort instance
                    Settings.Default.demuxPort9 = serialPortsDescription[8].serialPort;
                    // Save the link type
                    Settings.Default.linkType9 = serialPortsDescription[8].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter9 = serialPortsDescription[8].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize9 = (uint)serialPortsDescription[8].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte9 = serialPortsDescription[8].u8EoFByte;
                    break;
                case 10:
                    // Save the serialPort instance
                    Settings.Default.demuxPort10 = serialPortsDescription[9].serialPort;
                    // Save the link type
                    Settings.Default.linkType10 = serialPortsDescription[9].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter10 = serialPortsDescription[9].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize10 = (uint)serialPortsDescription[9].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte10 = serialPortsDescription[9].u8EoFByte;
                    break;
                case 11:
                    // Save the serialPort instance
                    Settings.Default.demuxPort11 = serialPortsDescription[10].serialPort;
                    // Save the link type
                    Settings.Default.linkType11 = serialPortsDescription[10].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter11 = serialPortsDescription[10].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize11 = (uint)serialPortsDescription[10].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte11 = serialPortsDescription[10].u8EoFByte;
                    break;
                case 12:
                    // Save the serialPort instance
                    Settings.Default.demuxPort12 = serialPortsDescription[11].serialPort;
                    // Save the link type
                    Settings.Default.linkType12 = serialPortsDescription[11].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter12 = serialPortsDescription[11].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize12 = (uint)serialPortsDescription[11].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte12 = serialPortsDescription[11].u8EoFByte;
                    break;
                case 13:
                    // Save the serialPort instance
                    Settings.Default.demuxPort13 = serialPortsDescription[12].serialPort;
                    // Save the link type
                    Settings.Default.linkType13 = serialPortsDescription[12].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter13 = serialPortsDescription[12].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize13 = (uint)serialPortsDescription[12].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte13 = serialPortsDescription[12].u8EoFByte;
                    break;
                case 14:
                    // Save the serialPort instance
                    Settings.Default.demuxPort14 = serialPortsDescription[13].serialPort;
                    // Save the link type
                    Settings.Default.linkType14 = serialPortsDescription[13].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter14 = serialPortsDescription[13].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize14 = (uint)serialPortsDescription[13].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte14 = serialPortsDescription[13].u8EoFByte;
                    break;
                case 15:
                    // Save the serialPort instance
                    Settings.Default.demuxPort15 = serialPortsDescription[14].serialPort;
                    // Save the link type
                    Settings.Default.linkType15 = serialPortsDescription[14].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter15 = serialPortsDescription[14].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize15 = (uint)serialPortsDescription[14].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte15 = serialPortsDescription[14].u8EoFByte;
                    break;
                case 16:
                    // Save the serialPort instance
                    Settings.Default.demuxPort16 = serialPortsDescription[15].serialPort;
                    // Save the link type
                    Settings.Default.linkType16 = serialPortsDescription[15].strLinkType;
                    // Save the frame delimiter
                    Settings.Default.frameDelimiter16 = serialPortsDescription[15].strFrameDelimiter;
                    // Save the frame size
                    Settings.Default.frameSize16 = (uint)serialPortsDescription[15].iFrameSize;
                    // Save the EoF byte
                    Settings.Default.eoFByte16 = serialPortsDescription[15].u8EoFByte;
                    break;
                default:
                    //
                    break;
            }
        }

        private void UpdatePortProperties()
        {
            try
            {
                if (serialPortsDescription[(int)numericUpDownPortConfig.Value].serialPort == null)
                {
                    // Fill-in the properties table
                    serialPortsDescription[(int)numericUpDownPortConfig.Value].serialPort = new SerialPort();
                    // Set the link type comboBox value for the new port number
                    comboBoxLinkType.Text = serialPortsDescription[(int)numericUpDownPortConfig.Value].strLinkType;
                    // Set the frame size comboBox value for the new port number
                    comboBoxFrameDelimiter.Text = serialPortsDescription[(int)numericUpDownPortConfig.Value].strFrameDelimiter;
                    // Set the end of frame byte numericUpDown value for the new port number
                    numericUpDownEoFByte.Value = serialPortsDescription[(int)numericUpDownPortConfig.Value].u8EoFByte;
                }
                // Set the new port as input of the serial port properties grid
                propertyGridPorts.SelectedObject = serialPortsDescription[(int)numericUpDownPortConfig.Value].serialPort;
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
            UpdatePortProperties();
            // Store the config port number to further save the settings of the correct port
            previousConfigPortNumber = numericUpDownPortConfig.Value;
            // Set the link type comboBox value for the new port number
            comboBoxLinkType.Text = serialPortsDescription[(int)numericUpDownPortConfig.Value].strLinkType;
            // Set the frame delimiter comboBox value for the new port number
            comboBoxFrameDelimiter.Text = serialPortsDescription[(int)numericUpDownPortConfig.Value].strFrameDelimiter;
            DisplayCorrectEoFFields();
            // Set the end of frame byte numericUpDown value for the new port number
            numericUpDownEoFByte.Value = serialPortsDescription[(int)numericUpDownPortConfig.Value].u8EoFByte;
        }

        private void DisplayCorrectEoFFields()
        {
            if (comboBoxFrameDelimiter.Text == "Fixed")
            {
                labelFrameLength.Visible = true;
                numericUpDownFrameLength.Visible = true;
                labelEoFByte.Visible = false;
                numericUpDownEoFByte.Visible = false;
            }
            else if (comboBoxFrameDelimiter.Text == "Defined by first byte")
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
            serialPortsDescription[(int)numericUpDownPortConfig.Value].strLinkType = comboBoxLinkType.Text;
        }

        private void comboBoxFrameDelimiter_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new value to the current serialPortsDescription item
            serialPortsDescription[(int)numericUpDownPortConfig.Value].strFrameDelimiter = comboBoxFrameDelimiter.Text;
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
            serialPortsDescription[(int)numericUpDownPortConfig.Value].iFrameSize = (int)numericUpDownFrameLength.Value;
        }
    }
}
