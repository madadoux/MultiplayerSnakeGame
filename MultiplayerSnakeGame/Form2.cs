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

        Thread my_thread;
        private void Form2_Load(object sender, EventArgs e)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
          my_thread = new Thread(new ParameterizedThreadStart(Handle_connection));
            my_thread.Start(sock);

        }
      
        public void Handle_connection(Object obj)
    {
       
    }
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Form3 f = new Form3();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}