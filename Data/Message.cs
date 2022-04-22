﻿using System;
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
        public string User { set; get; }
        public TcpClient Sender { set; get; }
        public NetworkStream Reciever { set; get; }
        public string messege { set; get; }
        public List<string> Users { set; get; } = new List<string>();
        public Message()
        {
            ServerMessage = ServerMessage.None;
            User = null;
            Reciever = null;
            messege = null;
            Users = null;
        }

        public void SendMessage(Message mess) 
        {
            string Users = null;
            if (mess.Users != null)
            foreach (string user in mess.Users)
                Users +=  user+ "-" ;

            string str = ((int)mess.ServerMessage).ToString() + "//:" + mess.User + "//:" + mess.messege + "//:" + Users + "|END";
                       
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
            System.Console.WriteLine("ServerMess         :" + strM.Substring(0, indexOfChar));
            mess.ServerMessage = (ServerMessage)Convert.ToInt32(strM.Substring(0, indexOfChar));
            strM = strM.Remove(0, indexOfChar + 3);

            indexOfChar = strM.IndexOf("//:");
            mess.User = strM.Substring(0, indexOfChar);
            System.Console.WriteLine("SenderMess         :" + mess.User);
            strM = strM.Remove(0, indexOfChar + 3);

            indexOfChar = strM.IndexOf("//:");
            mess.messege = strM.Substring(0, indexOfChar);
            System.Console.WriteLine("Mess            :" + mess.messege);
            strM = strM.Remove(0, indexOfChar + 3);

            indexOfChar = strM.IndexOf("|END");
            string Users = strM.Substring(0, indexOfChar);
            System.Console.WriteLine("Users           :" + Users);
            
            string[] ArrUsers = Users.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            mess.Users = new List<string>();
            foreach (string s in ArrUsers)
                mess.Users.Add(s);
            strM = strM.Remove(0, indexOfChar);

            System.Console.WriteLine("Ressive2: " + strM );
            return (mess);

        }
    }
}


