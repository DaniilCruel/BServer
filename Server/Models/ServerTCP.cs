using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Data;
using GOST_34_12_2015;

namespace Server
{
    // Модель сервера

    class ServerTCP
    {
        private readonly Dispatcher dispatcher;  // Для изменения ObservableCollection из других потоков
        private readonly IPEndPoint ep;  // Адрес и порт сервера
        private readonly TcpListener server; // Слушает входящие соединения
        private readonly CancellationTokenSource listenToken; // Отмена прослушивания 

        public ObservableCollection<User> ClientList { get; set; } // Список клиентов

        public ObservableCollection<FileInfo> FileList { get; set; }         // Список файлов 

        public event EventHandler<string> AddToLog; // Событие записи в лог
        public event EventHandler<string> KeyChanged;

        private const int block = 1024; // Размер буффера при передаче
        private const string filePath = "files\\";
        public Kuznechik Crypt = new Kuznechik();
        // Инициализация настроек сервера

        public ServerTCP(string ipAddress, int port, Kuznechik Kuz)
        {
            dispatcher = Dispatcher.CurrentDispatcher;
            ep = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            server = new TcpListener(ep);
            listenToken = new CancellationTokenSource();
            ClientList = new ObservableCollection<User>();
            FileList = new ObservableCollection<FileInfo>();
            Crypt = Kuz;
        }

        // Запуск прослушивания входящих соединений
        public void Start()
        {
            server.Start();
            Task.Run(() => ListeningIncomingConnetions());
            Directory.CreateDirectory(filePath);
            SearchFiles();
    
            byte[] buff = Crypt.masterKey;
            int bytes = 255;
            string str2= Encoding.Unicode.GetString(buff, 0, bytes);

            KeyChanged(this, str2);
            AddToLog(this, "Server is running");
        }

        // Остановка сервера 
        public Task Stop()
        {
            return Task.Run(() =>
            {
                listenToken.Cancel();
                foreach (var client in ClientList)
                {
                    CloseClient(client);
                }
                ClientList.Clear();
                FileList.Clear();
                server.Stop();
            });
        }

        // Отсылает сообщение клиенту
        /// <param name="client">Клиент, которому отправляется сообщение</param>
        /// <param name="message">Текст сообщения</param>
        private void SendMessage2(TcpClient client, string message)
        {
            byte[] buff = Encoding.Unicode.GetBytes(message);
            client.GetStream().Write(buff, 0, buff.Length);
        }

        /// <summary>
        /// Принимает сообщение от клиента
        /// </summary>
        /// <param name="client">Клиент, сообщение от которого принимается</param>
        private string ReceiveMessage2(TcpClient client)
        {
            StringBuilder str = new StringBuilder();
            if (client.Client.Poll(1000000, SelectMode.SelectRead)) // Изменение на сокете
            {
                byte[] buff = new byte[block];
                int bytes = 0;
                while (client.GetStream().DataAvailable)
                {
                    bytes = client.GetStream().Read(buff, 0, buff.Length);
                    str.Append(Encoding.Default.GetString(buff, 0, bytes));
                }
            }
            return str.ToString() != "" ? str.ToString() : "-1";
        }

        /// <summary>
        /// Принимает сообщение в виде набора байтов от клиента
        /// </summary>
        /// <param name="client">Клиент, сообщение от которого принимается</param>
        private byte[] ReceiveBytes(TcpClient client)
        {
            int buffLength = int.Parse(ReceiveMessage2(client));
            SendMessage2(client, "0");

            byte[] buff = new byte[buffLength];
            int bytes = client.GetStream().Read(buff, 0, buffLength);
            return buff;
        }

        /// <summary>
        /// Принимает файл от клиента
        /// </summary>
        /// <param name="client">Клиент, файл от которого принимается</param>
        private void ReceiveFile(User client)
        {
            SendMessage2(client.TcpClient, "0");
            string fileName = ReceiveMessage2(client.TcpClient);
            SendMessage2(client.TcpClient, "0");
            int fileLength = int.Parse(ReceiveMessage2(client.TcpClient));
            SendMessage2(client.TcpClient, "0");

            Directory.CreateDirectory(filePath);
            using (var writer = new FileStream($"{filePath}{fileName}", FileMode.Create, FileAccess.Write))
            {
                int bytes = 0;
                byte[] buff = new byte[block];
                while (bytes < fileLength)
                {
                    buff = ReceiveBytes(client.TcpClient);
                    writer.Write(buff, 0, buff.Length);
                    SendMessage2(client.TcpClient, "0");
                    bytes += block;
                }
            }

            SearchFiles();
            AddToLog(this, $"Receive file: {fileName} {fileLength} bytes");
        }


        /// <summary>
        /// Прослушивание входящих соединений
        /// </summary>
        private void ListeningIncomingConnetions()
        {
            TcpClient incomingConnection;
            while (!listenToken.Token.IsCancellationRequested)
            {
                Thread.Sleep(50);
                if (!server.Pending()) continue; // Попытка соединения
                List<string> ClientListM = new List<string>();
                foreach (User user in ClientList)
                {
                    ClientListM.Add(user.Login);
                }

                incomingConnection = server.AcceptTcpClient();
                Message messadd = new Message();
                string clientName = messadd.RessiveMessege(Crypt,incomingConnection).UserSend; // Получаем имя клиента и добавляем его в список

                if (CheckUsername(clientName))
                {
                    Message Error = new Message() { ServerMessage = ServerMessage.WrongUsername, Reciever = incomingConnection.GetStream() , messege = clientName };
                    Error.SendMessage(Crypt,Error);
                    continue;
                }

                User client = new User(1,incomingConnection, clientName, new CancellationTokenSource());

                Message Collection = new Message() { ServerMessage = ServerMessage.UsersCollection, Reciever= incomingConnection.GetStream(), Users = ClientListM };
                Collection.SendMessage(Crypt,Collection);
               

                dispatcher.BeginInvoke(new Action(() => ClientList.Add(client)));
                Task.Run(() => ClientMessaging(client));

                AddToLog(this, $"{client.Login} successful connected");
                Message mess = new Message() { ServerMessage = ServerMessage.AddUser, UserSend = client.Login };
                Broadcast(mess);

            }
        }
        public bool CheckUsername(string username)
        {
            return ClientList.Select(x => x.Login).Contains(username);
        }

        /// <summary>
        /// Обработка сообщений от клиента
        /// </summary>
        /// <param name="client">Клиент, сообщения которого обрабатываются</param>
        private void ClientMessaging(User client)
        {
            Message readMes = new Message();
            while (!client.ClientToken.Token.IsCancellationRequested) // Обрабатываем, пока клиент не отключится
            {
                if (client.TcpClient.Client.Poll(1000000, SelectMode.SelectRead)) // Изменение на сокете
                {
                    if (client.TcpClient.GetStream().DataAvailable) // Есть какие-то данные
                    {
                        readMes = readMes.RessiveMessege(Crypt,client.TcpClient); // None,   RemoveUser,    Message,   Broadcast
                        switch ((int)readMes.ServerMessage)
                        {
                            case 1: // none
                                AddToLog(this, $"None:{client.Login}: {readMes}");
                                ClientDisconnection(client);
                                break;
                            case 4:  //RemoveUser
                                
                                ClientList.Remove(ClientList.Where(x => x.Login == readMes.UserSend).First());
                                Broadcast(new Message() { ServerMessage = ServerMessage.RemoveUser, UserSend = readMes.UserSend } );
                                //  message.Sender.TcpClient.GetStream().Close();
                                //  message.Sender.TcpClient.Close();
                                dispatcher.BeginInvoke(new Action(() => ClientList.Remove(ClientList.Where(x => x.Login == readMes.UserSend).First())));
                                AddToLog(this, $"{client.Login}: disconnected");
                                break;
                            case 5:  // Message, 
                                
                                NetworkStream nwStream = ClientList.Where(x => x.Login == readMes.UserResiv).First().TcpClient.GetStream();
                                NetworkStream nwStreamSender = ClientList.Where(x => x.Login == readMes.UserSend).First().TcpClient.GetStream();

                                Message Res = new Message() { Reciever = nwStream, UserSend= readMes.UserSend, messege = readMes.messege, ServerMessage = ServerMessage.Message };
                                Message Send = new Message() { Reciever = nwStreamSender, UserSend = readMes.UserSend, messege = readMes.messege, ServerMessage = ServerMessage.Message };

                                Res.SendMessage(Crypt,Res);
                                Send.SendMessage(Crypt,Send);

                                AddToLog(this, $"Message:{client.Login}: {readMes.messege}");
                                break;
                            case 7: //Broadcast
                                Message mess = new Message() { ServerMessage = ServerMessage.Message, messege = readMes.messege, UserSend = readMes.UserSend };
                                Broadcast(mess);
                                AddToLog(this, $"Broadcast: {client.Login}: {readMes}");
                                break;
                            default:
                                AddToLog(this, $"{client.Login}: {readMes}");
                                break;
                        }
                    }
                    else // Если нет, то произошел обрыв связи
                    {
                        ClientDisconnection(client);
                    }
                }
                Thread.Sleep(50);
            }
        }

        /// <summary>
        /// Закрытие соединения с клиентом по инициативе клиента
        /// </summary>
        /// <param name="client">Клиент, соединение с которым закрывается</param>
        private void ClientDisconnection(User client)
        {
            client.TcpClient.GetStream().Close();
            client.TcpClient.Close();
            client.ClientToken.Cancel();
            dispatcher.BeginInvoke(new Action(() => ClientList.Remove(client)));
            AddToLog(this, $"{client.Login}: disconnected");
        }

        /// <summary>
        /// Закрытие соединения с клиентом по инициативе сервера
        /// </summary>
        /// <param name="client">Клиент, соединение с которым закрывается</param>
        private void CloseClient(User client)
        {
            client.ClientToken.Cancel();
            SendMessage2(client.TcpClient, "stop_server");
            client.TcpClient.GetStream().Close();
            client.TcpClient.Close();
            dispatcher.BeginInvoke(new Action(() => ClientList.Remove(client)));
            AddToLog(this, $"{client.Login}: disconnected");
        }

        /// <summary>
        /// Поиск загруженных файлов при старте сервера
        /// </summary>
        private void SearchFiles()
        {
            dispatcher.BeginInvoke(new Action(() => FileList.Clear()));
            foreach (var i in Directory.GetFiles(filePath))
            {
                dispatcher.BeginInvoke(new Action(() => FileList.Add(new FileInfo($"{filePath}{i}"))));
            }
        }
        public void Broadcast(Message message)
        {
            foreach (var client in ClientList)
            {

                    NetworkStream nwStream = client.TcpClient.GetStream();
                    message.UserResiv = client.Login;
                    message.Reciever = nwStream;
                    message.SendMessage(Crypt,message);
              
            }
        }
    }
}
