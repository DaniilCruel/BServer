using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


namespace Data
{

    public class Message     // ServerMessage -> Sender -> Reciever -> Messege -> List -> |
    {
        public ServerMessage ServerMessage { set; get; } = ServerMessage.None;
        public string Sender { set; get; }
        public NetworkStream Reciever { set; get; }
        public string messege { set; get; }
        public List<string> Users { set; get; } = new List<string>();
        public Message()
        {
            ServerMessage = ServerMessage.None;
            Sender = null;
            Reciever = null;
            messege = null;
            Users = null;
        }

        public void SendMessage(Message mess) 
        {
            string str = ((int)mess.ServerMessage).ToString() + "//:" + mess.Sender + "//:" + mess.messege + "//:" + mess.Users + "//:";
            System.Console.WriteLine("Send:  " + str);
            byte[] buff = Encoding.UTF8.GetBytes(str);
            mess.Reciever.Write(buff, 0, buff.Length);
            mess.Reciever.Flush();

        }
        public Message RessiveMessege(TcpClient client) 
        {
            string strM;
            StringBuilder str = new StringBuilder();
            if (client.Client.Poll(1000000, SelectMode.SelectRead)) // Изменение на сокете
            {
                byte[] buff = new byte[1024];
                int bytes = 0;
                while (client.GetStream().DataAvailable)
                {
                    bytes = client.GetStream().Read(buff, 0, buff.Length);
                    str.Append(Encoding.UTF8.GetString(buff, 0, bytes));
                }
            }
            strM = str.ToString();
            System.Console.WriteLine("Ressive1: " + strM);

            Message mess = new Message();

            int indexOfChar = strM.IndexOf("//:");
            System.Console.WriteLine("SerMess " + strM.Substring(0, indexOfChar));
            mess.ServerMessage = (ServerMessage)Convert.ToInt32(strM.Substring(0, indexOfChar));
            strM = strM.Remove(0, indexOfChar + 3);

            indexOfChar = strM.IndexOf("//:");
            mess.Sender = strM.Substring(0, indexOfChar);
            System.Console.WriteLine("SenderMess " + mess.Sender);
            strM = strM.Remove(0, indexOfChar + 3);

            indexOfChar = strM.IndexOf("//:");
            mess.messege = strM.Substring(0, indexOfChar);
            System.Console.WriteLine("Mess " + mess.messege);
            strM = strM.Remove(0, indexOfChar + 3);


            System.Console.WriteLine("Ressive2: " + strM);
            return (mess);

        }
    }
}


