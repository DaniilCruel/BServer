using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GOST_34_12_2015;

namespace Data
{

    public class Message     // ServerMessage //: Sender //: Reciever //: Messege //: List //: |
    {
        public ServerMessage ServerMessage { set; get; } = ServerMessage.None;
        public string UserSend { set; get; }
        public string UserResiv { set; get; }
        public TcpClient Sender { set; get; }
        public NetworkStream Reciever { set; get; }
        public string messege { set; get; }
        public List<string> Users { set; get; } = new List<string>();
        public Message()
        {
            ServerMessage = ServerMessage.None;
            UserSend = null;
            UserResiv = null;
            Reciever = null;
            messege = null;
            Users = null;
        }

        public void SendMessage(Kuznechik Crypt, Message mess) 
        {
            string Users = null;
            if (mess.Users != null)
            foreach (string user in mess.Users)
                Users +=  user+ "-" ;

            string str = ((int)mess.ServerMessage).ToString() + "//:" + mess.UserSend + "//:" + mess.UserResiv + "//:" + mess.messege + "//:" + Users + "|END";
            
            System.Console.WriteLine("Send: before  " + str);
            byte[] buff = Encoding.Default.GetBytes(str);

            //Crypt.Encrypt(buff);
            System.Console.WriteLine("Send: after  " + buff);
            
            mess.Reciever.Write(buff, 0, buff.Length);
            mess.Reciever.Flush();
        }



        public Message RessiveMessege(Kuznechik Crypt, TcpClient client) 
        {
            StringBuilder str = new StringBuilder();
            if (client.Client.Poll(1000000, SelectMode.SelectRead)) // Изменение на сокете
            {
                byte[] buff = new byte[1024];
                int bytes = 0;
                while (client.GetStream().DataAvailable)
                {
                    bytes = client.GetStream().Read(buff, 0, buff.Length);
                    System.Console.WriteLine("Ressive1: before " + buff);

                    str.Append(Encoding.Default.GetString(buff, 0, bytes));
                }
            }
            string strM = str.ToString();
            System.Console.WriteLine("Ressive1: after" + strM);

            Message mess = new Message();
            int indexOfChar = 0;
            
            indexOfChar = strM.IndexOf("//:");
            mess.ServerMessage = (ServerMessage)Convert.ToInt32(strM.Substring(0, indexOfChar));
            strM = strM.Remove(0, indexOfChar + 3);

            indexOfChar = strM.IndexOf("//:");
            mess.UserSend = strM.Substring(0, indexOfChar);
            strM = strM.Remove(0, indexOfChar + 3);

            indexOfChar = strM.IndexOf("//:");
            mess.UserResiv = strM.Substring(0, indexOfChar);
            strM = strM.Remove(0, indexOfChar + 3);

            indexOfChar = strM.IndexOf("//:");
            mess.messege = strM.Substring(0, indexOfChar);
            strM = strM.Remove(0, indexOfChar + 3);

            indexOfChar = strM.IndexOf("|END");
            string Users = strM.Substring(0, indexOfChar);
            
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


