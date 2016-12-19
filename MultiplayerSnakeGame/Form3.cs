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
            String Name = Dns.GetHostName();///////////////////////////// hat esmy
            IPHostEntry entery = Dns.GetHostEntry(Name);
            foreach (IPAddress ip in entery.AddressList)
            {
                ip_add = ip;//////////////////////hat awl ip address f el addresss list elly hwa el ip bta3y bs kda w atl3
                break;
            }

            string network_id;
            string[] arr = ip_add.ToString().Split('.');
            int num_of_class = int.Parse(arr[0]);
            /////////////////////////////////////////// class (A) network id =the first octet bs
            if (num_of_class >= 1 && num_of_class <= 126)
            {
                network_id = arr[0]+".";

                for (int i = 0; i < 254; i++)
                {
                    for (int j = 0; j < 254; j++)
                    {
                        for (int k = 0; k < 254; k++)
                        {
                            my_list.Add(IPAddress.Parse(network_id + "."+i.ToString()+"."+j.ToString()+"."+k.ToString()));
                        }
                    }
                }

            }
            else if (num_of_class >= 128 && num_of_class <= 191) ////////////class B network id awl 2 octet
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
            else if (num_of_class >= 192 && num_of_class <= 223)////////////////class C network id =awl 3 octet
            {
                network_id = arr[0] + "." + arr[1] + "." + arr[2] + ".";
                for (int i = 0; i < 254; i++)
                {
                    my_list.Add(IPAddress.Parse(network_id + i.ToString()));

                }
            }
            //////////////////////////////////////////////////////////////////////////////////
            sending();//ab3t el ip bta3 el server lkl el clients elly m3aya f nfs elnetwork
            /////////////////////////////////////////////////////////////////////////
            Socket new_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            new_thread = new Thread(new ParameterizedThreadStart(Handle_func));
            new_thread.Start(new_server);  
   
        }
        int counter=1;
        void Handle_func(Object coming)
        {
            Socket Server = (Socket)coming;
            IPEndPoint Iep = new IPEndPoint(IPAddress.Any, 8000);
            Socket New_client;
            Server.Bind(Iep);
            Server.Listen(5);
            while (true)
            {
                New_client = Server.Accept();
                ///////////////////////////////////// hatly el ip bta3 el client elly 3amally connect
                string str =New_client.RemoteEndPoint.ToString();
                string text ="client: "+str+"  has come";
                my_list_Box.Items.Add(text);/////////////////////////deef el ip bta3 el client el gded f el itemList
                my_list_Box.Refresh();

                counter++;

                if (counter == 5)
                {
                    button1.Enabled = true;////start the game
                    new_thread.Abort();
                }

            }

        }
        public void sending()
        {
            ////////////////////////////////// the server send its ip to all clients in the same network
            Socket Server_Sokcet =new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            new_thread = new Thread(new ParameterizedThreadStart(to_send));
            new_thread.Start(Server_Sokcet);
        }
        public void to_send(Object ob)
        { 
        Socket socket=(Socket)ob;
        for (int i = 0; i <= my_list.Count; i++)
        {
            IPEndPoint end = new IPEndPoint(my_list[i],8000);
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(ip_add.ToString());
            socket.SendTo(temp, end);
        }
        
        
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /////////////////////start the game
            Form1 fr = new Form1();
            fr.Show();
        }
        }
    }

