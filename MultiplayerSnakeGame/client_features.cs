using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
namespace MultiplayerSnakeGame
{
    class client_features
    {
       public IPAddress ip_address;
       public int rank;
       public bool current_player;
       public Socket client_socket;
       public client_features(String IpAddress)
       {
          this.ip_address = IPAddress.Parse(IpAddress);
          client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
       }
    }
}
