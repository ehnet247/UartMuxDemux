namespace UartMuxDemux
{
    partial class ConfigForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.groupBoxMasterPort = new System.Windows.Forms.GroupBox();
            this.numericUpDownMasterPortBaudrate = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxMasterPortName = new System.Windows.Forms.ComboBox();
            this.groupBoxSlavePorts = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.comboBoxSlavePortEdit = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.numericUpDownSlavePortPacketLength = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownSlavePortBaudrate = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownSlavePortTimeout = new System.Windows.Forms.NumericUpDown();
            this.comboBoxSlaveLinkType = new System.Windows.Forms.ComboBox();
            this.textBoxSlavePortName = new System.Windows.Forms.TextBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxSlavePortEofDetection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxSlavePorts = new System.Windows.Forms.ListBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxMasterPort.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMasterPortBaudrate)).BeginInit();
            this.groupBoxSlavePorts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlavePortPacketLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlavePortBaudrate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlavePortTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxMasterPort
            // 
            this.groupBoxMasterPort.Controls.Add(this.numericUpDownMasterPortBaudrate);
            this.groupBoxMasterPort.Controls.Add(this.label8);
            this.groupBoxMasterPort.Controls.Add(this.label5);
            this.groupBoxMasterPort.Controls.Add(this.comboBoxMasterPortName);
            this.groupBoxMasterPort.Location = new System.Drawing.Point(13, 13);
            this.groupBoxMasterPort.Name = "groupBoxMasterPort";
            this.groupBoxMasterPort.Size = new System.Drawing.Size(346, 388);
            this.groupBoxMasterPort.TabIndex = 0;
            this.groupBoxMasterPort.TabStop = false;
            this.groupBoxMasterPort.Text = "Master port";
            // 
            // numericUpDownMasterPortBaudrate
            // 
            this.numericUpDownMasterPortBaudrate.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownMasterPortBaudrate.Location = new System.Drawing.Point(9, 100);
            this.numericUpDownMasterPortBaudrate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownMasterPortBaudrate.Minimum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numericUpDownMasterPortBaudrate.Name = "numericUpDownMasterPortBaudrate";
            this.numericUpDownMasterPortBaudrate.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownMasterPortBaudrate.TabIndex = 14;
            this.numericUpDownMasterPortBaudrate.Value = new decimal(new int[] {
            9600,
            0,
            0,
            0});
            this.numericUpDownMasterPortBaudrate.ValueChanged += new System.EventHandler(this.numericUpDownMuxBaudrate_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 17);
            this.label8.TabIndex = 13;
            this.label8.Text = "Baudrate:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 11;
            this.label5.Text = "Port name:";
            // 
            // comboBoxMasterPortName
            // 
            this.comboBoxMasterPortName.FormattingEnabled = true;
            this.comboBoxMasterPortName.Location = new System.Drawing.Point(9, 51);
            this.comboBoxMasterPortName.Name = "comboBoxMasterPortName";
            this.comboBoxMasterPortName.Size = new System.Drawing.Size(121, 24);
            this.comboBoxMasterPortName.TabIndex = 0;
            this.comboBoxMasterPortName.SelectedIndexChanged += new System.EventHandler(this.comboBoxMuxPortName_SelectedIndexChanged);
            // 
            // groupBoxSlavePorts
            // 
            this.groupBoxSlavePorts.Controls.Add(this.label9);
            this.groupBoxSlavePorts.Controls.Add(this.comboBoxSlavePortEdit);
            this.groupBoxSlavePorts.Controls.Add(this.label7);
            this.groupBoxSlavePorts.Controls.Add(this.numericUpDownSlavePortPacketLength);
            this.groupBoxSlavePorts.Controls.Add(this.numericUpDownSlavePortBaudrate);
            this.groupBoxSlavePorts.Controls.Add(this.label6);
            this.groupBoxSlavePorts.Controls.Add(this.numericUpDownSlavePortTimeout);
            this.groupBoxSlavePorts.Controls.Add(this.comboBoxSlaveLinkType);
            this.groupBoxSlavePorts.Controls.Add(this.textBoxSlavePortName);
            this.groupBoxSlavePorts.Controls.Add(this.buttonRemove);
            this.groupBoxSlavePorts.Controls.Add(this.buttonAdd);
            this.groupBoxSlavePorts.Controls.Add(this.comboBoxSlavePortEofDetection);
            this.groupBoxSlavePorts.Controls.Add(this.label4);
            this.groupBoxSlavePorts.Controls.Add(this.label3);
            this.groupBoxSlavePorts.Controls.Add(this.label2);
            this.groupBoxSlavePorts.Controls.Add(this.label1);
            this.groupBoxSlavePorts.Controls.Add(this.listBoxSlavePorts);
            this.groupBoxSlavePorts.Location = new System.Drawing.Point(407, 13);
            this.groupBoxSlavePorts.Name = "groupBoxSlavePorts";
            this.groupBoxSlavePorts.Size = new System.Drawing.Size(381, 388);
            this.groupBoxSlavePorts.TabIndex = 1;
            this.groupBoxSlavePorts.TabStop = false;
            this.groupBoxSlavePorts.Text = "Slave ports";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(188, 28);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 17);
            this.label9.TabIndex = 16;
            this.label9.Text = "Edit Port:";
            // 
            // comboBoxSlavePortEdit
            // 
            this.comboBoxSlavePortEdit.FormattingEnabled = true;
            this.comboBoxSlavePortEdit.Location = new System.Drawing.Point(191, 51);
            this.comboBoxSlavePortEdit.Name = "comboBoxSlavePortEdit";
            this.comboBoxSlavePortEdit.Size = new System.Drawing.Size(121, 24);
            this.comboBoxSlavePortEdit.TabIndex = 15;
            this.comboBoxSlavePortEdit.SelectedIndexChanged += new System.EventHandler(this.comboBoxPortEdit_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(188, 318);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Packet length (bytes):";
            // 
            // numericUpDownSlavePortPacketLength
            // 
            this.numericUpDownSlavePortPacketLength.Location = new System.Drawing.Point(188, 338);
            this.numericUpDownSlavePortPacketLength.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownSlavePortPacketLength.Name = "numericUpDownSlavePortPacketLength";
            this.numericUpDownSlavePortPacketLength.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownSlavePortPacketLength.TabIndex = 13;
            this.numericUpDownSlavePortPacketLength.ValueChanged += new System.EventHandler(this.numericUpDownPacketLength_ValueChanged);
            // 
            // numericUpDownSlavePortBaudrate
            // 
            this.numericUpDownSlavePortBaudrate.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownSlavePortBaudrate.Location = new System.Drawing.Point(188, 191);
            this.numericUpDownSlavePortBaudrate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownSlavePortBaudrate.Minimum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numericUpDownSlavePortBaudrate.Name = "numericUpDownSlavePortBaudrate";
            this.numericUpDownSlavePortBaudrate.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownSlavePortBaudrate.TabIndex = 12;
            this.numericUpDownSlavePortBaudrate.Value = new decimal(new int[] {
            9600,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(188, 171);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Baudrate:";
            // 
            // numericUpDownSlavePortTimeout
            // 
            this.numericUpDownSlavePortTimeout.Location = new System.Drawing.Point(191, 284);
            this.numericUpDownSlavePortTimeout.Name = "numericUpDownSlavePortTimeout";
            this.numericUpDownSlavePortTimeout.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownSlavePortTimeout.TabIndex = 10;
            this.numericUpDownSlavePortTimeout.ValueChanged += new System.EventHandler(this.numericUpDownTimeout_ValueChanged);
            // 
            // comboBoxSlaveLinkType
            // 
            this.comboBoxSlaveLinkType.FormattingEnabled = true;
            this.comboBoxSlaveLinkType.Items.AddRange(new object[] {
            "Fixed size",
            "First byte defines size",
            "Unknown"});
            this.comboBoxSlaveLinkType.Location = new System.Drawing.Point(187, 144);
            this.comboBoxSlaveLinkType.Name = "comboBoxSlaveLinkType";
            this.comboBoxSlaveLinkType.Size = new System.Drawing.Size(121, 24);
            this.comboBoxSlaveLinkType.TabIndex = 9;
            this.comboBoxSlaveLinkType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDemuxLinkType_SelectedIndexChanged);
            // 
            // textBoxSlavePortName
            // 
            this.textBoxSlavePortName.Location = new System.Drawing.Point(187, 99);
            this.textBoxSlavePortName.Name = "textBoxSlavePortName";
            this.textBoxSlavePortName.Size = new System.Drawing.Size(100, 22);
            this.textBoxSlavePortName.TabIndex = 8;
            this.textBoxSlavePortName.TextChanged += new System.EventHandler(this.textBoxSlavePortName_TextChanged);
            this.textBoxSlavePortName.Leave += new System.EventHandler(this.textBoxSlavePortName_Leave);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(318, 54);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(26, 23);
            this.buttonRemove.TabIndex = 7;
            this.buttonRemove.Text = "-";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(159, 52);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(26, 23);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "+";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // comboBoxSlavePortEofDetection
            // 
            this.comboBoxSlavePortEofDetection.FormattingEnabled = true;
            this.comboBoxSlavePortEofDetection.Items.AddRange(new object[] {
            "Fixed size",
            "First byte defines size",
            "Unknown"});
            this.comboBoxSlavePortEofDetection.Location = new System.Drawing.Point(191, 236);
            this.comboBoxSlavePortEofDetection.Name = "comboBoxSlavePortEofDetection";
            this.comboBoxSlavePortEofDetection.Size = new System.Drawing.Size(152, 24);
            this.comboBoxSlavePortEofDetection.TabIndex = 5;
            this.comboBoxSlavePortEofDetection.SelectedIndexChanged += new System.EventHandler(this.comboBoxEofDetection_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "End of frame detection:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(188, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Timeout (ms):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(185, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Link type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port name:";
            // 
            // listBoxSlavePorts
            // 
            this.listBoxSlavePorts.FormattingEnabled = true;
            this.listBoxSlavePorts.ItemHeight = 16;
            this.listBoxSlavePorts.Location = new System.Drawing.Point(7, 54);
            this.listBoxSlavePorts.Name = "listBoxSlavePorts";
            this.listBoxSlavePorts.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxSlavePorts.Size = new System.Drawing.Size(120, 212);
            this.listBoxSlavePorts.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(331, 407);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(413, 407);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.groupBoxSlavePorts);
            this.Controls.Add(this.groupBoxMasterPort);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupBoxMasterPort.ResumeLayout(false);
            this.groupBoxMasterPort.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMasterPortBaudrate)).EndInit();
            this.groupBoxSlavePorts.ResumeLayout(false);
            this.groupBoxSlavePorts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlavePortPacketLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlavePortBaudrate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSlavePortTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMasterPort;
        private System.Windows.Forms.GroupBox groupBoxSlavePorts;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxSlavePortEofDetection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxSlavePorts;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxSlavePortName;
        private System.Windows.Forms.ComboBox comboBoxSlaveLinkType;
        private System.Windows.Forms.NumericUpDown numericUpDownSlavePortTimeout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMasterPortName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownSlavePortBaudrate;
        private System.Windows.Forms.NumericUpDown numericUpDownMasterPortBaudrate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numericUpDownSlavePortPacketLength;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox comboBoxSlavePortEdit;
    }
}