using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KeyChat
{
    internal class SmsgServer
    {
        public event SmsgRecvEventHandler SmsgRecvEventHandler = null;
        public string IPStr
        {
            get;
            private set;
        }
        public int Port
        {
            get;
            private set;
        }
        public SmsgServer(string ipstr, int port)
        {
            IPStr = ipstr;
            Port = port;
        }

        Socket sock;
        public bool Start()
        {
            try
            {
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IPStr), Port);
                sock.Bind(iep);
                sock.Listen(5);
                AcceptLoopAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        delegate void AcceptDele();
        private void AcceptLoopAsync()
        {
            AcceptDele dele = AcceptLoop;
            dele.BeginInvoke(null, null);
        }
        private void AcceptLoop()
        {
            Socket dosock;
            while(true)
            {
                dosock = sock.Accept();
                DoIt(dosock);
            }
        }

        private void DoIt(Socket dosock)
        {
            IPEndPoint remote = dosock.RemoteEndPoint as IPEndPoint;
            byte[] packet = new byte[1024];
            dosock.Receive(packet);
            dosock.Close();
            MemoryStream ms = new MemoryStream(packet);
            BinaryReader br = new BinaryReader(ms);
            string msg = br.ReadString();
            br.Close();
            ms.Close();
            if (SmsgRecvEventHandler != null)
            {
                SmsgRecvEventHandler(this, new SmsgRecvEventArgs(remote, msg));
            }
        }
    }
}
