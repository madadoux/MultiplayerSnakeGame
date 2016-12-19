using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace MultiplayerSnakeGame
{
    class server
    {
        public void StartServer()
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 8000);
            Thread newthread = new Thread(new ParameterizedThreadStart(HandleConnection));
            newthread.Start(serverSocket);

        }
        public void HandleConnection(object o)
        { 
            
       
        
        }
    }
}
