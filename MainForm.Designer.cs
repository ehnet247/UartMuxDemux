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
            this.timerDisplayRefresh = new System.Windows.Forms.Timer(this.components);
            this.groupBoxSlavePorts = new System.Windows.Forms.GroupBox();
            this.checkedListBoxSlavePorts = new System.Windows.Forms.CheckedListBox();
            this.serialPortSource = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorkerSource = new System.ComponentModel.BackgroundWorker();
            this.buttonConfig = new System.Windows.Forms.Button();
            this.buttonOpenPorts = new System.Windows.Forms.Button();
            this.buttonClosePorts = new System.Windows.Forms.Button();
            this.groupBoxMasterPort = new System.Windows.Forms.GroupBox();
            this.checkBoxMasterPort = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxMuxPort = new System.Windows.Forms.CheckBox();
            this.groupBoxSlavePorts.SuspendLayout();
            this.groupBoxMasterPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(176, -404);
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
            // timerDisplayRefresh
            // 
            this.timerDisplayRefresh.Tick += new System.EventHandler(this.timerDisplayRefresh_Tick_);
            // 
            // groupBoxSlavePorts
            // 
            this.groupBoxSlavePorts.Controls.Add(this.checkedListBoxSlavePorts);
            this.groupBoxSlavePorts.Location = new System.Drawing.Point(12, 187);
            this.groupBoxSlavePorts.Name = "groupBoxSlavePorts";
            this.groupBoxSlavePorts.Size = new System.Drawing.Size(340, 222);
            this.groupBoxSlavePorts.TabIndex = 23;
            this.groupBoxSlavePorts.TabStop = false;
            this.groupBoxSlavePorts.Text = "Slaves";
            // 
            // checkedListBoxSlavePorts
            // 
            this.checkedListBoxSlavePorts.FormattingEnabled = true;
            this.checkedListBoxSlavePorts.Location = new System.Drawing.Point(6, 21);
            this.checkedListBoxSlavePorts.Name = "checkedListBoxSlavePorts";
            this.checkedListBoxSlavePorts.Size = new System.Drawing.Size(323, 174);
            this.checkedListBoxSlavePorts.TabIndex = 0;
            // 
            // buttonConfig
            // 
            this.buttonConfig.Location = new System.Drawing.Point(32, 443);
            this.buttonConfig.Name = "buttonConfig";
            this.buttonConfig.Size = new System.Drawing.Size(73, 29);
            this.buttonConfig.TabIndex = 26;
            this.buttonConfig.Text = "Config";
            this.buttonConfig.UseVisualStyleBackColor = true;
            this.buttonConfig.Click += new System.EventHandler(this.buttonConfig_Click);
            // 
            // buttonOpenPorts
            // 
            this.buttonOpenPorts.BackColor = System.Drawing.Color.LimeGreen;
            this.buttonOpenPorts.Location = new System.Drawing.Point(111, 443);
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
            this.buttonClosePorts.Location = new System.Drawing.Point(230, 443);
            this.buttonClosePorts.Name = "buttonClosePorts";
            this.buttonClosePorts.Size = new System.Drawing.Size(113, 29);
            this.buttonClosePorts.TabIndex = 28;
            this.buttonClosePorts.Text = "Close ports";
            this.buttonClosePorts.UseVisualStyleBackColor = false;
            this.buttonClosePorts.Click += new System.EventHandler(this.buttonClosePorts_Click);
            // 
            // groupBoxMasterPort
            // 
            this.groupBoxMasterPort.Controls.Add(this.checkBoxMasterPort);
            this.groupBoxMasterPort.Location = new System.Drawing.Point(12, 12);
            this.groupBoxMasterPort.Name = "groupBoxMasterPort";
            this.groupBoxMasterPort.Size = new System.Drawing.Size(340, 156);
            this.groupBoxMasterPort.TabIndex = 30;
            this.groupBoxMasterPort.TabStop = false;
            this.groupBoxMasterPort.Text = "Master";
            // 
            // checkBoxMasterPort
            // 
            this.checkBoxMasterPort.AutoSize = true;
            this.checkBoxMasterPort.Location = new System.Drawing.Point(7, 22);
            this.checkBoxMasterPort.Name = "checkBoxMasterPort";
            this.checkBoxMasterPort.Size = new System.Drawing.Size(102, 21);
            this.checkBoxMasterPort.TabIndex = 0;
            this.checkBoxMasterPort.Text = "Master port";
            this.checkBoxMasterPort.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // checkBoxMuxPort
            // 
            this.checkBoxMuxPort.AutoSize = true;
            this.checkBoxMuxPort.Location = new System.Drawing.Point(7, 22);
            this.checkBoxMuxPort.Name = "checkBoxMuxPort";
            this.checkBoxMuxPort.Size = new System.Drawing.Size(89, 21);
            this.checkBoxMuxPort.TabIndex = 0;
            this.checkBoxMuxPort.Text = "MUX port";
            this.checkBoxMuxPort.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(388, 484);
            this.Controls.Add(this.groupBoxMasterPort);
            this.Controls.Add(this.buttonClosePorts);
            this.Controls.Add(this.buttonOpenPorts);
            this.Controls.Add(this.buttonConfig);
            this.Controls.Add(this.groupBoxSlavePorts);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "UartMuxDemux";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxSlavePorts.ResumeLayout(false);
            this.groupBoxMasterPort.ResumeLayout(false);
            this.groupBoxMasterPort.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox baudrateComboBox;
        private System.Windows.Forms.Timer timerDisplayRefresh;
        private System.Windows.Forms.GroupBox groupBoxSlavePorts;
        private System.IO.Ports.SerialPort serialPortSource;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSource;
        private System.Windows.Forms.Button buttonConfig;
        private System.Windows.Forms.Button buttonOpenPorts;
        private System.Windows.Forms.Button buttonClosePorts;
        private System.Windows.Forms.CheckedListBox checkedListBoxSlavePorts;
        private System.Windows.Forms.GroupBox groupBoxMasterPort;
        private System.Windows.Forms.CheckBox checkBoxMasterPort;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxMuxPort;
    }
}

