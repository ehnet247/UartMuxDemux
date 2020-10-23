/********************************************************/
/*                        DEMUX                         */
/* Receives data on master port and forward them        */
/* to slave ports                                       */
/********************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public class Demux
    {
        public string strNewLineDef = "\n";
        private readonly BackgroundWorker demuxBackgroundWorker;
        private string strCurrentFrameDate = String.Empty;
        private string strCurrentFrameTime = String.Empty;
        private List<byte> lReadBuffer;
        private List<byte> lWriteBuffer;
        private string strDataReceivedDate;
        private string strDataReceivedTime;
        private Mux muxPort;

        public Demux(Mux muxPort)
        {
            this.muxPort = muxPort;
            this.lReadBuffer = new List<byte>();
            this.lWriteBuffer = new List<byte>();
            demuxBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            demuxBackgroundWorker.WorkerSupportsCancellation = true;
            demuxBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.DemuxBackgroundWorker_DoWork);
            // Start the demux thread
            demuxBackgroundWorker.RunWorkerAsync();
        }

        public void Write(byte[] aWriteBuffer)
        {
            lWriteBuffer.AddRange(aWriteBuffer);
        }

        private void DemuxBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            while ((worker.CancellationPending == false) && (masterPort.IsOpen) && (masterPort.BytesToRead > 0))
            {
            }
            worker.CancelAsync();
        }

        private void WriteFrameToSlaves()
        {
        }
    }
}
