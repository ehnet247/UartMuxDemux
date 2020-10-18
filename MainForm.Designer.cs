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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.baudrateComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxBaudrate = new System.Windows.Forms.ComboBox();
            this.timerDisplayRefresh = new System.Windows.Forms.Timer(this.components);
            this.groupBoxLogger = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.OpenCloseCheckboxSource = new System.Windows.Forms.CheckBox();
            this.comPortNameSource = new System.Windows.Forms.ComboBox();
            this.serialPortSource = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorkerSource = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonOpenPorts = new System.Windows.Forms.Button();
            this.buttonClosePorts = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
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
            // timerDisplayRefresh
            // 
            this.timerDisplayRefresh.Tick += new System.EventHandler(this.timerDisplayRefresh_Tick_);
            // 
            // groupBoxLogger
            // 
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(42, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 29);
            this.button1.TabIndex = 26;
            this.button1.Text = "Config";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonOpenPorts
            // 
            this.buttonOpenPorts.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonOpenPorts.Location = new System.Drawing.Point(121, 275);
            this.buttonOpenPorts.Name = "buttonOpenPorts";
            this.buttonOpenPorts.Size = new System.Drawing.Size(113, 29);
            this.buttonOpenPorts.TabIndex = 27;
            this.buttonOpenPorts.Text = "Open ports";
            this.buttonOpenPorts.UseVisualStyleBackColor = false;
            this.buttonOpenPorts.Click += new System.EventHandler(this.buttonOpenPorts_Click);
            // 
            // buttonClosePorts
            // 
            this.buttonClosePorts.BackColor = System.Drawing.Color.Red;
            this.buttonClosePorts.Location = new System.Drawing.Point(245, 275);
            this.buttonClosePorts.Name = "buttonClosePorts";
            this.buttonClosePorts.Size = new System.Drawing.Size(113, 29);
            this.buttonClosePorts.TabIndex = 28;
            this.buttonClosePorts.Text = "Close ports";
            this.buttonClosePorts.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(876, 558);
            this.Controls.Add(this.buttonClosePorts);
            this.Controls.Add(this.buttonOpenPorts);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBoxLogger);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "UartMuxDemux";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox baudrateComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxBaudrate;
        private System.Windows.Forms.Timer timerDisplayRefresh;
        private System.Windows.Forms.GroupBox groupBoxLogger;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox comPortNameSource;
        private System.Windows.Forms.CheckBox OpenCloseCheckboxSource;
        private System.IO.Ports.SerialPort serialPortSource;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonOpenPorts;
        private System.Windows.Forms.Button buttonClosePorts;
    }
}

