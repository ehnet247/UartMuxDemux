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
            this.groupBoxMux = new System.Windows.Forms.GroupBox();
            this.numericUpDownMuxBaudrate = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxMuxPortName = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.numericUpDownDemuxBaudrate = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownTimeout = new System.Windows.Forms.NumericUpDown();
            this.comboBoxDemuxLinkType = new System.Windows.Forms.ComboBox();
            this.textBoxPortName = new System.Windows.Forms.TextBox();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.comboBoxEofDetection = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxDemuxPorts = new System.Windows.Forms.ListBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxMux.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMuxBaudrate)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDemuxBaudrate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxMux
            // 
            this.groupBoxMux.Controls.Add(this.numericUpDownMuxBaudrate);
            this.groupBoxMux.Controls.Add(this.label8);
            this.groupBoxMux.Controls.Add(this.label5);
            this.groupBoxMux.Controls.Add(this.comboBoxMuxPortName);
            this.groupBoxMux.Location = new System.Drawing.Point(13, 13);
            this.groupBoxMux.Name = "groupBoxMux";
            this.groupBoxMux.Size = new System.Drawing.Size(346, 388);
            this.groupBoxMux.TabIndex = 0;
            this.groupBoxMux.TabStop = false;
            this.groupBoxMux.Text = "Mux";
            // 
            // numericUpDownMuxBaudrate
            // 
            this.numericUpDownMuxBaudrate.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownMuxBaudrate.Location = new System.Drawing.Point(9, 100);
            this.numericUpDownMuxBaudrate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownMuxBaudrate.Minimum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numericUpDownMuxBaudrate.Name = "numericUpDownMuxBaudrate";
            this.numericUpDownMuxBaudrate.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownMuxBaudrate.TabIndex = 14;
            this.numericUpDownMuxBaudrate.Value = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numericUpDownMuxBaudrate.ValueChanged += new System.EventHandler(this.numericUpDownMuxBaudrate_ValueChanged);
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
            // comboBoxMuxPortName
            // 
            this.comboBoxMuxPortName.FormattingEnabled = true;
            this.comboBoxMuxPortName.Location = new System.Drawing.Point(9, 51);
            this.comboBoxMuxPortName.Name = "comboBoxMuxPortName";
            this.comboBoxMuxPortName.Size = new System.Drawing.Size(121, 24);
            this.comboBoxMuxPortName.TabIndex = 0;
            this.comboBoxMuxPortName.SelectedIndexChanged += new System.EventHandler(this.comboBoxMuxPortName_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.numericUpDownDemuxBaudrate);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.numericUpDownTimeout);
            this.groupBox2.Controls.Add(this.comboBoxDemuxLinkType);
            this.groupBox2.Controls.Add(this.textBoxPortName);
            this.groupBox2.Controls.Add(this.buttonRemove);
            this.groupBox2.Controls.Add(this.buttonAdd);
            this.groupBox2.Controls.Add(this.comboBoxEofDetection);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.listBoxDemuxPorts);
            this.groupBox2.Location = new System.Drawing.Point(407, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(381, 388);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Demux";
            // 
            // numericUpDownDemuxBaudrate
            // 
            this.numericUpDownDemuxBaudrate.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDemuxBaudrate.Location = new System.Drawing.Point(188, 167);
            this.numericUpDownDemuxBaudrate.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numericUpDownDemuxBaudrate.Minimum = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            this.numericUpDownDemuxBaudrate.Name = "numericUpDownDemuxBaudrate";
            this.numericUpDownDemuxBaudrate.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownDemuxBaudrate.TabIndex = 12;
            this.numericUpDownDemuxBaudrate.Value = new decimal(new int[] {
            1200,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(185, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Baudrate:";
            // 
            // numericUpDownTimeout
            // 
            this.numericUpDownTimeout.Location = new System.Drawing.Point(191, 284);
            this.numericUpDownTimeout.Name = "numericUpDownTimeout";
            this.numericUpDownTimeout.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownTimeout.TabIndex = 10;
            // 
            // comboBoxDemuxLinkType
            // 
            this.comboBoxDemuxLinkType.FormattingEnabled = true;
            this.comboBoxDemuxLinkType.Location = new System.Drawing.Point(188, 120);
            this.comboBoxDemuxLinkType.Name = "comboBoxDemuxLinkType";
            this.comboBoxDemuxLinkType.Size = new System.Drawing.Size(121, 24);
            this.comboBoxDemuxLinkType.TabIndex = 9;
            // 
            // textBoxPortName
            // 
            this.textBoxPortName.Location = new System.Drawing.Point(188, 75);
            this.textBoxPortName.Name = "textBoxPortName";
            this.textBoxPortName.Size = new System.Drawing.Size(100, 22);
            this.textBoxPortName.TabIndex = 8;
            this.textBoxPortName.TextChanged += new System.EventHandler(this.textBoxPortName_TextChanged);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(101, 283);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(26, 23);
            this.buttonRemove.TabIndex = 7;
            this.buttonRemove.Text = "-";
            this.buttonRemove.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(69, 283);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(26, 23);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "+";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // comboBoxEofDetection
            // 
            this.comboBoxEofDetection.FormattingEnabled = true;
            this.comboBoxEofDetection.Items.AddRange(new object[] {
            "Fixed size",
            "First bytedefines size",
            "Unknown"});
            this.comboBoxEofDetection.Location = new System.Drawing.Point(191, 226);
            this.comboBoxEofDetection.Name = "comboBoxEofDetection";
            this.comboBoxEofDetection.Size = new System.Drawing.Size(121, 24);
            this.comboBoxEofDetection.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(188, 206);
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
            this.label2.Location = new System.Drawing.Point(185, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Link type:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Port name:";
            // 
            // listBoxDemuxPorts
            // 
            this.listBoxDemuxPorts.FormattingEnabled = true;
            this.listBoxDemuxPorts.ItemHeight = 16;
            this.listBoxDemuxPorts.Location = new System.Drawing.Point(7, 54);
            this.listBoxDemuxPorts.Name = "listBoxDemuxPorts";
            this.listBoxDemuxPorts.Size = new System.Drawing.Size(120, 212);
            this.listBoxDemuxPorts.TabIndex = 0;
            this.listBoxDemuxPorts.SelectedIndexChanged += new System.EventHandler(this.listBoxDemuxPorts_SelectedIndexChanged);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(331, 407);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
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
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxMux);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Text = "ConfigForm";
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.groupBoxMux.ResumeLayout(false);
            this.groupBoxMux.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMuxBaudrate)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDemuxBaudrate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMux;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxEofDetection;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxDemuxPorts;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxPortName;
        private System.Windows.Forms.ComboBox comboBoxDemuxLinkType;
        private System.Windows.Forms.NumericUpDown numericUpDownTimeout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxMuxPortName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownDemuxBaudrate;
        private System.Windows.Forms.NumericUpDown numericUpDownMuxBaudrate;
        private System.Windows.Forms.Label label8;
    }
}