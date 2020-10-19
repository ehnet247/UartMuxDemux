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
            this.groupBoxLogger = new System.Windows.Forms.GroupBox();
            this.checkedListBoxDemuxPorts = new System.Windows.Forms.CheckedListBox();
            this.serialPortSource = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorkerSource = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonOpenPorts = new System.Windows.Forms.Button();
            this.buttonClosePorts = new System.Windows.Forms.Button();
            this.groupBoxLogger.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Location = new System.Drawing.Point(176, -470);
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
            // groupBoxLogger
            // 
            this.groupBoxLogger.Controls.Add(this.checkedListBoxDemuxPorts);
            this.groupBoxLogger.Location = new System.Drawing.Point(12, 12);
            this.groupBoxLogger.Name = "groupBoxLogger";
            this.groupBoxLogger.Size = new System.Drawing.Size(346, 302);
            this.groupBoxLogger.TabIndex = 23;
            this.groupBoxLogger.TabStop = false;
            this.groupBoxLogger.Text = "DEMUX";
            // 
            // checkedListBoxDemuxPorts
            // 
            this.checkedListBoxDemuxPorts.FormattingEnabled = true;
            this.checkedListBoxDemuxPorts.Location = new System.Drawing.Point(6, 21);
            this.checkedListBoxDemuxPorts.Name = "checkedListBoxDemuxPorts";
            this.checkedListBoxDemuxPorts.Size = new System.Drawing.Size(323, 276);
            this.checkedListBoxDemuxPorts.TabIndex = 0;
            // 
            // backgroundWorkerSource
            // 
            this.backgroundWorkerSource.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerSource_DoWork);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 320);
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
            this.buttonOpenPorts.Location = new System.Drawing.Point(95, 320);
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
            this.buttonClosePorts.Location = new System.Drawing.Point(214, 320);
            this.buttonClosePorts.Name = "buttonClosePorts";
            this.buttonClosePorts.Size = new System.Drawing.Size(113, 29);
            this.buttonClosePorts.TabIndex = 28;
            this.buttonClosePorts.Text = "Close ports";
            this.buttonClosePorts.UseVisualStyleBackColor = false;
            this.buttonClosePorts.Click += new System.EventHandler(this.buttonClosePorts_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(388, 418);
            this.Controls.Add(this.buttonClosePorts);
            this.Controls.Add(this.buttonOpenPorts);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBoxLogger);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "UartMuxDemux";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxLogger.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox baudrateComboBox;
        private System.Windows.Forms.Timer timerDisplayRefresh;
        private System.Windows.Forms.GroupBox groupBoxLogger;
        private System.IO.Ports.SerialPort serialPortSource;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSource;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonOpenPorts;
        private System.Windows.Forms.Button buttonClosePorts;
        private System.Windows.Forms.CheckedListBox checkedListBoxDemuxPorts;
    }
}

