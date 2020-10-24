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
        private readonly BackgroundWorker demuxBackgroundWorker;
        private List<byte> lWriteBuffer;
        private MasterPort masterPort;

        public Demux(MasterPort masterPort)
        {
            this.masterPort = masterPort;
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
            while ((worker.CancellationPending == false) && (masterPort.serialPort.IsOpen) && (masterPort.serialPort.BytesToRead > 0))
            {
            }
            worker.CancelAsync();
        }

        private void WriteFrameToSlaves()
        {
        }
    }
}
