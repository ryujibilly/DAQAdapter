using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace DAQAdapter
{
    public class Adapter: Component
    {
        /// <summary>
        /// Adapter's comport
        /// </summary>
        private SerialPort ComPort { get; set; }
        /// <summary>
        /// Adapter's sockets
        /// </summary>
        private Socket sockets { get; set; }
        /// <summary>
        /// Adapter's tcpserver
        /// </summary>
        private TcpServer tcpserver { get; set; }
        /// <summary>
        /// Adapter's tcpclient
        /// </summary>
        private TcpClient tcpclient { get; set; }

        /// <summary>
        /// Initialization
        /// </summary>
        /// <param name="comms">serialPorts</param>
        /// <param name="socks">sockets</param>
        public Adapter(SerialPort comms,TcpServer server)
        {
            this.ComPort = comms;
            this.tcpserver = server;
        }
        public Adapter()
        {

        }

        private void DataRecvCallBack(byte[] datalist)
        {

        }
    }
}