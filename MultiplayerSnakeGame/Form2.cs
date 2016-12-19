using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace MultiplayerSnakeGame
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
         Thread   my_thread = new Thread(new ParameterizedThreadStart(Handle_connection));
            my_thread.Start(sock);

        }
        public void Handle_connection(Object obj)
    {
        Socket socket = (Socket)obj;
        EndPoint endpoint = new IPEndPoint(IPAddress.Any,8000);
        socket.Bind(endpoint);
            byte [] msg= new byte[1024];
            int recmsglen = socket.ReceiveFrom(msg,ref endpoint);
            string data = ASCIIEncoding.ASCII.GetString(msg);
            string length = data.Substring(0,recmsglen);
            button1.Enabled = true;
            socket.Close();
          
    }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
        }
    }
}
