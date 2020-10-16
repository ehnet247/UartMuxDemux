namespace UartMuxDemux
{
    partial class MainForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.comPortName0 = new System.Windows.Forms.ComboBox();
            this.RecordCheckbox0 = new System.Windows.Forms.CheckBox();
            this.RecordCheckbox1 = new System.Windows.Forms.CheckBox();
            this.RecordCheckbox2 = new System.Windows.Forms.CheckBox();
            this.comPortName1 = new System.Windows.Forms.ComboBox();
            this.comPortName2 = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.baudrateComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown0 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButtonNbBytes = new System.Windows.Forms.RadioButton();
            this.radioButtonEoFByte = new System.Windows.Forms.RadioButton();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownEoF = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxBaudrate = new System.Windows.Forms.ComboBox();
            this.timerRefreshDisplay = new System.Windows.Forms.Timer(this.components);
            this.groupBoxLogger = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OpenCloseCheckboxSource = new System.Windows.Forms.CheckBox();
            this.comPortNameSource = new System.Windows.Forms.ComboBox();
            this.serialPortSource = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorkerSource = new System.ComponentModel.BackgroundWorker();
            this.timerRefreshCounters = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEoF)).BeginInit();
            this.groupBoxLogger.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // comPortName0
            // 
            this.comPortName0.FormattingEnabled = true;
            this.comPortName0.Location = new System.Drawing.Point(6, 56);
            this.comPortName0.Name = "comPortName0";
            this.comPortName0.Size = new System.Drawing.Size(121, 24);
            this.comPortName0.TabIndex = 0;
            this.comPortName0.SelectedIndexChanged += new System.EventHandler(this.comPortName1_SelectedIndexChanged);
            // 
            // RecordCheckbox0
            // 
            this.RecordCheckbox0.Appearance = System.Windows.Forms.Appearance.Button;
            this.RecordCheckbox0.AutoSize = true;
            this.RecordCheckbox0.Location = new System.Drawing.Point(161, 49);
            this.RecordCheckbox0.Name = "RecordCheckbox0";
            this.RecordCheckbox0.Size = new System.Drawing.Size(53, 27);
            this.RecordCheckbox0.TabIndex = 1;
            this.RecordCheckbox0.Text = "Open";
            this.RecordCheckbox0.UseVisualStyleBackColor = true;
            this.RecordCheckbox0.CheckedChanged += new System.EventHandler(this.RecordCheckbox_CheckedChanged);
            // 
            // RecordCheckbox1
            // 
            this.RecordCheckbox1.Appearance = System.Windows.Forms.Appearance.Button;
            this.RecordCheckbox1.AutoSize = true;
            this.RecordCheckbox1.Location = new System.Drawing.Point(161, 82);
            this.RecordCheckbox1.Name = "RecordCheckbox1";
            this.RecordCheckbox1.Size = new System.Drawing.Size(53, 27);
            this.RecordCheckbox1.TabIndex = 2;
            this.RecordCheckbox1.Text = "Open";
            this.RecordCheckbox1.UseVisualStyleBackColor = true;
            this.RecordCheckbox1.CheckedChanged += new System.EventHandler(this.RecordCheckbox_CheckedChanged);
            // 
            // RecordCheckbox2
            // 
            this.RecordCheckbox2.Appearance = System.Windows.Forms.Appearance.Button;
            this.RecordCheckbox2.AutoSize = true;
            this.RecordCheckbox2.Location = new System.Drawing.Point(161, 115);
            this.RecordCheckbox2.Name = "RecordCheckbox2";
            this.RecordCheckbox2.Size = new System.Drawing.Size(53, 27);
            this.RecordCheckbox2.TabIndex = 3;
            this.RecordCheckbox2.Text = "Open";
            this.RecordCheckbox2.UseVisualStyleBackColor = true;
            this.RecordCheckbox2.CheckedChanged += new System.EventHandler(this.RecordCheckbox_CheckedChanged);
            // 
            // comPortName1
            // 
            this.comPortName1.FormattingEnabled = true;
            this.comPortName1.Location = new System.Drawing.Point(6, 88);
            this.comPortName1.Name = "comPortName1";
            this.comPortName1.Size = new System.Drawing.Size(121, 24);
            this.comPortName1.TabIndex = 7;
            // 
            // comPortName2
            // 
            this.comPortName2.FormattingEnabled = true;
            this.comPortName2.Location = new System.Drawing.Point(6, 120);
            this.comPortName2.Name = "comPortName2";
            this.comPortName2.Size = new System.Drawing.Size(121, 24);
            this.comPortName2.TabIndex = 8;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(420, -330);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(112, 25);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // baudrateComboBox
            // 
            this.baudrateComboBox.Items.AddRange(new object[] {
            "9600",
            "19200",
            "115200"});
            this.baudrateComboBox.Name = "baudrateComboBox";
            this.baudrateComboBox.Size = new System.Drawing.Size(121, 28);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(216, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Received frames :";
            // 
            // numericUpDown0
            // 
            this.numericUpDown0.Location = new System.Drawing.Point(220, 52);
            this.numericUpDown0.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown0.Name = "numericUpDown0";
            this.numericUpDown0.Size = new System.Drawing.Size(82, 22);
            this.numericUpDown0.TabIndex = 14;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(219, 85);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(83, 22);
            this.numericUpDown1.TabIndex = 15;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(219, 118);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(83, 22);
            this.numericUpDown2.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.radioButtonNbBytes);
            this.groupBox1.Controls.Add(this.radioButtonEoFByte);
            this.groupBox1.Controls.Add(this.numericUpDown6);
            this.groupBox1.Controls.Add(this.numericUpDownEoF);
            this.groupBox1.Location = new System.Drawing.Point(518, 324);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(346, 222);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DEMUX options";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 17);
            this.label3.TabIndex = 30;
            this.label3.Text = "Frame detection:";
            // 
            // radioButtonNbBytes
            // 
            this.radioButtonNbBytes.AutoSize = true;
            this.radioButtonNbBytes.Location = new System.Drawing.Point(10, 116);
            this.radioButtonNbBytes.Name = "radioButtonNbBytes";
            this.radioButtonNbBytes.Size = new System.Drawing.Size(137, 21);
            this.radioButtonNbBytes.TabIndex = 29;
            this.radioButtonNbBytes.Text = "Number of bytes:";
            this.radioButtonNbBytes.UseVisualStyleBackColor = true;
            // 
            // radioButtonEoFByte
            // 
            this.radioButtonEoFByte.AutoSize = true;
            this.radioButtonEoFByte.Checked = true;
            this.radioButtonEoFByte.Location = new System.Drawing.Point(10, 88);
            this.radioButtonEoFByte.Name = "radioButtonEoFByte";
            this.radioButtonEoFByte.Size = new System.Drawing.Size(145, 21);
            this.radioButtonEoFByte.TabIndex = 28;
            this.radioButtonEoFByte.TabStop = true;
            this.radioButtonEoFByte.Text = "End of frame byte:";
            this.radioButtonEoFByte.UseVisualStyleBackColor = true;
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(150, 115);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(98, 22);
            this.numericUpDown6.TabIndex = 27;
            this.numericUpDown6.Value = new decimal(new int[] {
            32,
            0,
            0,
            0});
            // 
            // numericUpDownEoF
            // 
            this.numericUpDownEoF.Hexadecimal = true;
            this.numericUpDownEoF.Location = new System.Drawing.Point(161, 87);
            this.numericUpDownEoF.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDownEoF.Name = "numericUpDownEoF";
            this.numericUpDownEoF.Size = new System.Drawing.Size(87, 22);
            this.numericUpDownEoF.TabIndex = 23;
            this.numericUpDownEoF.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 17);
            this.label5.TabIndex = 29;
            this.label5.Text = "Baudrate:";
            // 
            // comboBoxBaudrate
            // 
            this.comboBoxBaudrate.FormattingEnabled = true;
            this.comboBoxBaudrate.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "128000",
            "256000"});
            this.comboBoxBaudrate.Location = new System.Drawing.Point(83, 21);
            this.comboBoxBaudrate.Name = "comboBoxBaudrate";
            this.comboBoxBaudrate.Size = new System.Drawing.Size(98, 24);
            this.comboBoxBaudrate.TabIndex = 28;
            this.comboBoxBaudrate.SelectedIndexChanged += new System.EventHandler(this.comboBoxBaudrate_SelectedIndexChanged);
            // 
            // timerRefreshDisplay
            // 
            this.timerRefreshDisplay.Enabled = true;
            this.timerRefreshDisplay.Tick += new System.EventHandler(this.timerRefreshDisplay_Tick);
            // 
            // groupBoxLogger
            // 
            this.groupBoxLogger.Controls.Add(this.label1);
            this.groupBoxLogger.Controls.Add(this.comPortName0);
            this.groupBoxLogger.Controls.Add(this.RecordCheckbox0);
            this.groupBoxLogger.Controls.Add(this.RecordCheckbox1);
            this.groupBoxLogger.Controls.Add(this.RecordCheckbox2);
            this.groupBoxLogger.Controls.Add(this.numericUpDown2);
            this.groupBoxLogger.Controls.Add(this.numericUpDown1);
            this.groupBoxLogger.Controls.Add(this.numericUpDown0);
            this.groupBoxLogger.Controls.Add(this.comPortName1);
            this.groupBoxLogger.Controls.Add(this.comPortName2);
            this.groupBoxLogger.Location = new System.Drawing.Point(518, 82);
            this.groupBoxLogger.Name = "groupBoxLogger";
            this.groupBoxLogger.Size = new System.Drawing.Size(346, 222);
            this.groupBoxLogger.TabIndex = 23;
            this.groupBoxLogger.TabStop = false;
            this.groupBoxLogger.Text = "DEMUX";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.comboBoxBaudrate);
            this.groupBox2.Controls.Add(this.OpenCloseCheckboxSource);
            this.groupBox2.Controls.Add(this.comPortNameSource);
            this.groupBox2.Location = new System.Drawing.Point(12, 82);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(346, 101);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "MUX";
            // 
            // OpenCloseCheckboxSource
            // 
            this.OpenCloseCheckboxSource.Appearance = System.Windows.Forms.Appearance.Button;
            this.OpenCloseCheckboxSource.AutoSize = true;
            this.OpenCloseCheckboxSource.Location = new System.Drawing.Point(150, 51);
            this.OpenCloseCheckboxSource.Name = "OpenCloseCheckboxSource";
            this.OpenCloseCheckboxSource.Size = new System.Drawing.Size(53, 27);
            this.OpenCloseCheckboxSource.TabIndex = 2;
            this.OpenCloseCheckboxSource.Text = "Open";
            this.OpenCloseCheckboxSource.UseVisualStyleBackColor = true;
            this.OpenCloseCheckboxSource.CheckedChanged += new System.EventHandler(this.OpenCloseCheckboxSource_CheckedChanged);
            // 
            // comPortNameSource
            // 
            this.comPortNameSource.FormattingEnabled = true;
            this.comPortNameSource.Location = new System.Drawing.Point(10, 51);
            this.comPortNameSource.Name = "comPortNameSource";
            this.comPortNameSource.Size = new System.Drawing.Size(121, 24);
            this.comPortNameSource.TabIndex = 1;
            // 
            // backgroundWorkerSource
            // 
            this.backgroundWorkerSource.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSource_DoWork);
            // 
            // timerRefreshCounters
            // 
            this.timerRefreshCounters.Enabled = true;
            this.timerRefreshCounters.Tick += new System.EventHandler(this.timerRefreshCounters_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Location = new System.Drawing.Point(22, 324);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(336, 100);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "MUX options";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(249, 489);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 29);
            this.button1.TabIndex = 26;
            this.button1.Text = "Config";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(876, 558);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxLogger);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "UartMuxDemux";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEoF)).EndInit();
            this.groupBoxLogger.ResumeLayout(false);
            this.groupBoxLogger.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comPortName0;
        private System.Windows.Forms.CheckBox RecordCheckbox0;
        private System.Windows.Forms.CheckBox RecordCheckbox1;
        private System.Windows.Forms.CheckBox RecordCheckbox2;
        private System.Windows.Forms.ComboBox comPortName1;
        private System.Windows.Forms.ComboBox comPortName2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox baudrateComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown0;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDownEoF;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxBaudrate;
        private System.Windows.Forms.Timer timerRefreshDisplay;
        private System.Windows.Forms.GroupBox groupBoxLogger;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comPortNameSource;
        private System.Windows.Forms.CheckBox OpenCloseCheckboxSource;
        private System.IO.Ports.SerialPort serialPortSource;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSource;
        private System.Windows.Forms.RadioButton radioButtonNbBytes;
        private System.Windows.Forms.RadioButton radioButtonEoFByte;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerRefreshCounters;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button1;
    }
}

