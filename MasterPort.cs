﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UartMuxDemux
{
    public class MasterPort
    {

        public SerialPort serialPort;
        private Demux demux;

        public MasterPort(Demux demux)
        {
            this.demux = demux;
            serialPort = new SerialPort();
        }
    }
}
