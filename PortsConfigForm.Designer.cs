namespace UartMuxDemux
{
    partial class PortsConfigForm
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
            this.propertyGridPorts = new System.Windows.Forms.PropertyGrid();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericUpDownPortNumber = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownPortConfig = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelFrameLength = new System.Windows.Forms.Label();
            this.numericUpDownFrameLength = new System.Windows.Forms.NumericUpDown();
            this.labelFrameSize = new System.Windows.Forms.Label();
            this.numericUpDownEoFByte = new System.Windows.Forms.NumericUpDown();
            this.labelEoFByte = new System.Windows.Forms.Label();
            this.comboBoxLinkType = new System.Windows.Forms.ComboBox();
            this.comboBoxFrameDelimiter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortConfig)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrameLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEoFByte)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGridPorts
            // 
            this.propertyGridPorts.Location = new System.Drawing.Point(6, 21);
            this.propertyGridPorts.Name = "propertyGridPorts";
            this.propertyGridPorts.Size = new System.Drawing.Size(493, 306);
            this.propertyGridPorts.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(283, 464);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(373, 464);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // numericUpDownPortNumber
            // 
            this.numericUpDownPortNumber.Location = new System.Drawing.Point(12, 74);
            this.numericUpDownPortNumber.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownPortNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPortNumber.Name = "numericUpDownPortNumber";
            this.numericUpDownPortNumber.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownPortNumber.TabIndex = 3;
            this.numericUpDownPortNumber.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownPortNumber.ValueChanged += new System.EventHandler(this.numericUpDownPortNumber_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Number of ports:";
            // 
            // numericUpDownPortConfig
            // 
            this.numericUpDownPortConfig.Location = new System.Drawing.Point(31, 63);
            this.numericUpDownPortConfig.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownPortConfig.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPortConfig.Name = "numericUpDownPortConfig";
            this.numericUpDownPortConfig.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownPortConfig.TabIndex = 5;
            this.numericUpDownPortConfig.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownPortConfig.ValueChanged += new System.EventHandler(this.numericUpDownPortConfig_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numericUpDownPortConfig);
            this.groupBox1.Location = new System.Drawing.Point(138, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(650, 431);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Port configuration";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelFrameLength);
            this.groupBox2.Controls.Add(this.numericUpDownFrameLength);
            this.groupBox2.Controls.Add(this.labelFrameSize);
            this.groupBox2.Controls.Add(this.numericUpDownEoFByte);
            this.groupBox2.Controls.Add(this.labelEoFByte);
            this.groupBox2.Controls.Add(this.comboBoxLinkType);
            this.groupBox2.Controls.Add(this.comboBoxFrameDelimiter);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(385, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 334);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Specific";
            // 
            // labelFrameLength
            // 
            this.labelFrameLength.AutoSize = true;
            this.labelFrameLength.Location = new System.Drawing.Point(6, 199);
            this.labelFrameLength.Name = "labelFrameLength";
            this.labelFrameLength.Size = new System.Drawing.Size(95, 17);
            this.labelFrameLength.TabIndex = 14;
            this.labelFrameLength.Text = "Frame length:";
            // 
            // numericUpDownFrameLength
            // 
            this.numericUpDownFrameLength.Location = new System.Drawing.Point(9, 219);
            this.numericUpDownFrameLength.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownFrameLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFrameLength.Name = "numericUpDownFrameLength";
            this.numericUpDownFrameLength.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownFrameLength.TabIndex = 13;
            this.numericUpDownFrameLength.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDownFrameLength.ValueChanged += new System.EventHandler(this.numericUpDownFrameLength_ValueChanged);
            // 
            // labelFrameSize
            // 
            this.labelFrameSize.AutoSize = true;
            this.labelFrameSize.Location = new System.Drawing.Point(6, 90);
            this.labelFrameSize.Name = "labelFrameSize";
            this.labelFrameSize.Size = new System.Drawing.Size(109, 17);
            this.labelFrameSize.TabIndex = 10;
            this.labelFrameSize.Text = "Frame delimiter:";
            // 
            // numericUpDownEoFByte
            // 
            this.numericUpDownEoFByte.Hexadecimal = true;
            this.numericUpDownEoFByte.Location = new System.Drawing.Point(9, 163);
            this.numericUpDownEoFByte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownEoFByte.Name = "numericUpDownEoFByte";
            this.numericUpDownEoFByte.Size = new System.Drawing.Size(120, 22);
            this.numericUpDownEoFByte.TabIndex = 12;
            this.numericUpDownEoFByte.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownEoFByte.ValueChanged += new System.EventHandler(this.numericUpDownEoFByte_ValueChanged);
            // 
            // labelEoFByte
            // 
            this.labelEoFByte.AutoSize = true;
            this.labelEoFByte.Location = new System.Drawing.Point(6, 142);
            this.labelEoFByte.Name = "labelEoFByte";
            this.labelEoFByte.Size = new System.Drawing.Size(68, 17);
            this.labelEoFByte.TabIndex = 11;
            this.labelEoFByte.Text = "EoF byte:";
            // 
            // comboBoxLinkType
            // 
            this.comboBoxLinkType.FormattingEnabled = true;
            this.comboBoxLinkType.Items.AddRange(new object[] {
            "Binaire",
            "ASCII"});
            this.comboBoxLinkType.Location = new System.Drawing.Point(5, 53);
            this.comboBoxLinkType.Name = "comboBoxLinkType";
            this.comboBoxLinkType.Size = new System.Drawing.Size(124, 24);
            this.comboBoxLinkType.TabIndex = 7;
            this.comboBoxLinkType.SelectedIndexChanged += new System.EventHandler(this.comboBoxLinkType_SelectedIndexChanged);
            // 
            // comboBoxFrameDelimiter
            // 
            this.comboBoxFrameDelimiter.FormattingEnabled = true;
            this.comboBoxFrameDelimiter.Items.AddRange(new object[] {
            "Fixed",
            "Defined by first byte",
            "Unkown"});
            this.comboBoxFrameDelimiter.Location = new System.Drawing.Point(5, 110);
            this.comboBoxFrameDelimiter.Name = "comboBoxFrameDelimiter";
            this.comboBoxFrameDelimiter.Size = new System.Drawing.Size(153, 24);
            this.comboBoxFrameDelimiter.TabIndex = 8;
            this.comboBoxFrameDelimiter.SelectedIndexChanged += new System.EventHandler(this.comboBoxFrameDelimiter_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Link type:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.propertyGridPorts);
            this.groupBox3.Location = new System.Drawing.Point(15, 125);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(505, 333);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Generic";
            // 
            // PortsConfigForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(800, 499);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDownPortNumber);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PortsConfigForm";
            this.Text = "PortsConfigForm";
            this.Load += new System.EventHandler(this.PortsConfigForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPortConfig)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFrameLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEoFByte)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid propertyGridPorts;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownPortNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownPortConfig;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelEoFByte;
        private System.Windows.Forms.Label labelFrameSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxFrameDelimiter;
        private System.Windows.Forms.ComboBox comboBoxLinkType;
        private System.Windows.Forms.NumericUpDown numericUpDownEoFByte;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelFrameLength;
        private System.Windows.Forms.NumericUpDown numericUpDownFrameLength;
    }
}