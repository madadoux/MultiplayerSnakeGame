using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace MultiplayerSnakeGame
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        EndPoint end_point;
        IPAddress ip_add;
        Thread new_thread;
        List<IPAddress> my_list = new List<IPAddress>();
        private void Form3_Load(object sender, EventArgs e)
        {
            String strHostName = Dns.GetHostName();
            IPHostEntry iphostentry = Dns.GetHostByName(strHostName);
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                ip_add = ipaddress;
                break;
            }
            string network_id;
            string[] arr = ip_add.ToString().Split('.');
            int num_of_class = int.Parse(arr[0]);
            /////////////////////////////////////////// class (A)
            if (num_of_class >= 1 && num_of_class <= 126)
            {
                network_id = arr[0]+".";

                for (int i = 0; i < 254; i++)
                {
                    for (int j = 0; j < 254; j++)
                    {
                        for (int k = 0; k < 254; k++)
                        {
                            my_list.Add(IPAddress.Parse(network_id+"."+i.ToString()+"."+j.ToString()+"."+k.ToString()));
                        }
                    }
                }

            }
            else if (num_of_class >= 128 && num_of_class <= 191)
            {
                network_id = arr[0] + "." + arr[1] + ".";
                for (int i = 0; i < 254; i++)
                {
                    for (int j = 0; j < 254; j++)
                    {
                        my_list.Add(IPAddress.Parse(network_id +"."+ i.ToString() + "." + j.ToString()));
                    }
                }

            }
            else if (num_of_class >= 192 && num_of_class <= 223)
            {
                network_id = arr[0] + "." + arr[1] + "." + arr[2] + ".";
                for (int i = 0; i < 254; i++)
                {
                    my_list.Add(IPAddress.Parse(network_id + i.ToString()));

                }
            }

            //////////////////////////////////////////////////////////////////////////////////
            timer1.Start();
            /////////////////////////////////////////////////////////////////////////
            Socket Server_Sokcet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            new_thread = new Thread(new ParameterizedThreadStart(Handle_connection));
            new_thread.Start(Server_Sokcet);   

        }
        int count;
        void Handle_connection(Object o)
        {
            Socket Server_Sokcet = (Socket)o;
            IPEndPoint Iep = new IPEndPoint(IPAddress.Any, 8000);
            Socket ClientSokcet;
            Server_Sokcet.Bind(Iep);
            Server_Sokcet.Listen(5);
            while (true)
            {
                ClientSokcet = Server_Sokcet.Accept();
                string s = ClientSokcet.RemoteEndPoint.ToString();
                string[] arr = s.Split('.');
                listBox1.Items.Add(arr[0]);
                listBox1.Refresh();
                new_count++;
                if (new_count == 5)
                {
                    button1.Enabled = true;
                    new_thread.Abort();
                }

            }

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
        }
        int new_count=10000;
        private void timer1_Tick(object sender, EventArgs e)
        {
        
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            Thread th=new Thread(new ParameterizedThreadStart(handle_function));
            th.Start(sock);
            new_count--;
            
            if (new_count == 0)
            {
                timer1.Stop();

                MessageBox.Show("Time finished");
            }
           
        }
        int counter2 = 8000;


        void handle_function(Object o)
        {
            Socket sock = (Socket)o;
            for (int i = 0; i < my_list.Count; i++)
            {
               EndPoint iep = new IPEndPoint(my_list[i], 8000);
                sock.SendTo(ASCIIEncoding.ASCII.GetBytes(ip_add.ToString()), iep);
            }
            sock.Close();
        }
        }
    }

