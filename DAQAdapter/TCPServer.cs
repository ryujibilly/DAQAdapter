using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace DAQAdapter
{
    public class TCPServer
    {
        private int TcpPort = 5001;
        private Socket ServerSocket = null;
        private Thread RecThread = null;
        private Socket CurClientSocket = null;
        private object ClientSocketAsynObj = new object();

        TCPServer(int tcpPort)
        {
            this.TcpPort = tcpPort;
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
