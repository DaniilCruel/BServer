using System.Threading;
using System.Net.Sockets;
using System.Text;
using System;

namespace Data
{

    //Модель клиента
    [Serializable]
    public class User
    {
        public long id { get; set; }
        public string Login { get; set; }
        public TcpClient TcpClient { get; set; }
        public CancellationTokenSource ClientToken { get; set; }

        // Инициализая подключенного клиента

        public User()
        {
            id = 0;
            Login = null;
            TcpClient = null;
  
        }


        public User(long IdG, TcpClient tcpClient, string login, CancellationTokenSource token)
        {
            id = IdG;
            Login = (login);
            TcpClient = tcpClient;
            ClientToken = token;
        
        }
    }
}