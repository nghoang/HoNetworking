using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HoNetworking
{
    enum ServerStatus {Running,Stopped};
    public class HoServer
    {
        Socket connection;
        TcpListener server;
        int port = 9322;
        Thread updateThread;
        Thread listenThread;
        ServerStatus status = ServerStatus.Stopped;

        public HoServer(int _port)
        {
            port = _port;
        }

        public void StartServer()
        {
            if (listenThread == null)
            {
                server = new TcpListener(IPAddress.Any, port);
                listenThread = new Thread(new ThreadStart(NetworkListen));
                listenThread.Start();
            }
        }

        public void StopServer()
        {
            if (server != null)
            {
                listenThread.Abort();
                server.Stop();
                status = ServerStatus.Stopped;
            }
        }

        public void NetworkListen()
        {
            server.Start();
            status = ServerStatus.Running;
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
            }
        }
    }
}
