using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TCPChat.Infrastructure;
using GOST_34_12_2015;

namespace TCPChat.ViewModels
{
    class MainViewModel : Notifier
    {
        #region UI
        private Visibility connectIsVisible;
        public Visibility ConnectIsVisible
        {
            set
            {
                connectIsVisible = value;
                Notify();
            }
            get => connectIsVisible;
        }

        private Visibility warningVisibility;
        public Visibility WarningVisibility
        {
            set
            {
                warningVisibility = value;
                Notify();
            }
            get => warningVisibility;
        }

        private bool sendIsEnable;
        public bool SendIsEnable
        {
            set
            {
                sendIsEnable = value;
                Notify();
            }
            get => sendIsEnable;
        }

        private Visibility usernameTakenLabelIsEnable;
        public Visibility UsernameTakenLabelIsEnable
        {
            set
            {
                usernameTakenLabelIsEnable = value;
                Notify();
            }
            get => usernameTakenLabelIsEnable;
        }

        private string textMessage;
        public string TextMessage
        {
            set
            {
                textMessage = value;
                Notify();
            }
            get => textMessage;
        }

        private string receiver;
        public string Receiver
        {
            set
            {
                receiver = value;
                Notify();
            }
            get => receiver;
        }

        #endregion

        #region Commands
        public ICommand ConnectCommand { set; get; }
        public ICommand SendCommand { set; get; }
        public ICommand SendEmailCommand { set; get; }
        #endregion

        #region Fields
        private User user;
        private IPEndPoint ep;
        private NetworkStream nwStream;
        private const int block = 1024;
        private string selectedUser;
        private string username;
        Kuznechik Crypt = new Kuznechik();
        #endregion

        #region Properties
        public ObservableCollection<MessageUI> MessagessItems { set; get; }

        public string SelectedUser
        {
            set
            {
                selectedUser = value;
                if (value == Users.ElementAt(0))
                    Receiver = Users.ElementAt(0).ToLower();
                else
                    Receiver = $"only {value}";

                Notify();
            }
            get => selectedUser;
        }

        public string Username
        {
            set
            {
                username = value;
                Notify();
            }
            get => username;
        }

        public ObservableCollection<string> Users { set; get; }
        public string EmailAddress { set; get; }
        #endregion

        public MainViewModel(string addres, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(addres), port);
            user = new User();

            Users = new ObservableCollection<string>();
            Users.Add("Everyone");
            SelectedUser = Users.ElementAt(0);

            ConnectIsVisible = Visibility.Visible;
            WarningVisibility = UsernameTakenLabelIsEnable = Visibility.Hidden;
            MessagessItems = new ObservableCollection<MessageUI>();
            InitCommands();

            Application.Current.MainWindow.Closing += new CancelEventHandler(MainWindow_Closing);
        }

        private void InitCommands()
        {
            ConnectCommand = new RelayCommand(x => Task.Run(() =>
            {
                if (!Connect())
                {
                    UsernameTakenLabelIsEnable = Visibility.Visible;
                    return;
                }

                ConnectIsVisible = UsernameTakenLabelIsEnable = Visibility.Hidden;
                WarningVisibility = Visibility.Visible;
                SendIsEnable = true;
                user.Login = Username;

                GetData();
            }));

            SendCommand = new RelayCommand(x => Task.Run(() =>
            {
                Message message = new Message()
                {
                    ServerMessage = ServerMessage.Message,
                    messege = TextMessage,
                    Reciever = user.TcpClient.GetStream(),
                    UserSend = username
            };

                if (Users.ElementAt(0) == SelectedUser || SelectedUser == null)
                {
                    message.ServerMessage = ServerMessage.Broadcast;
                   
                }
                else
                {
                    message.ServerMessage = ServerMessage.Message;
                    message.UserResiv = SelectedUser;
                }
                message.SendMessage(Crypt,message);
                TextMessage = string.Empty;
            }
            ));
        }


        public bool Connect()
        {
            user.TcpClient = new TcpClient();
            user.TcpClient.Connect(ep);
            nwStream = user.TcpClient.GetStream();

            Username = Username ?? "Unknown";
            ///////////////
            Message mess = new Message()      // ServerMessage -> Sender -> Reciever -> Messege -> List -> |
            {
                ServerMessage = ServerMessage.Conect,
                UserSend = Username,
                UserResiv = "",
                Reciever = nwStream
            };
            mess.SendMessage(Crypt,mess);
            ////////////
            Message messCl = new Message();
            messCl = messCl.RessiveMessege(Crypt,user.TcpClient); // Получаем имя клиента и добавляем его в список
            if (messCl.ServerMessage == ServerMessage.WrongUsername)
            {
                user.TcpClient.Client.Shutdown(SocketShutdown.Both);
                user.TcpClient.Close();
                return false;
            }
            App.Current.Dispatcher.Invoke(new Action(() =>
            {
                foreach (var i in messCl.Users)
                    Users.Add(i);
            }));

            return true;
        }

        public void GetData()
        {

            while (true)
            {
                try
                {
                    //Message message = (Message)bf.Deserialize(nwStream);
                    Message message = new Message() ;
                    message = message.RessiveMessege(Crypt,user.TcpClient); // Получаем имя клиента и добавляем его в список
                     
                    if (message.ServerMessage == ServerMessage.Message)
                    {
                        App.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            if (message.UserSend == Username)
                                MessagessItems.Add(new MessageUI() { Sender = $"me: ", Message = message.messege, Color = "#000000", FontStyle = FontStyles.Normal, Align = "Right" });
                            else
                                MessagessItems.Add(new MessageUI() { Sender = $"{message.UserSend}: ", Message = message.messege, Color = "#000000", FontStyle = FontStyles.Normal, Align = "Left" });
                        }));
                    }
                    if (message.ServerMessage == ServerMessage.AddUser &&  message.UserSend != Username)
                    {
                        App.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            MessagessItems.Add(new MessageUI() { Sender = message.UserSend, Message = " joined the chat", Color = "#40698c", FontStyle = FontStyles.Oblique, Align = "Left" });
                            if (!Users.Contains(message.UserSend))
                                Users.Add($"{message.UserSend}");
                        }));
                    }

                    else if (message.ServerMessage == ServerMessage.RemoveUser)
                    {
                        App.Current.Dispatcher.Invoke(new Action(() =>
                        {
                            MessagessItems.Add(new MessageUI() { Sender = message.UserSend, Message = " has left the chat", Color = "#40698c", FontStyle = FontStyles.Oblique, Align = "Left" });
                            Users.Remove(Users.Where(x => x == message.UserSend).First());
                        }));
                    }
                    
                }

                catch (Exception e) { Console.WriteLine(e.Message); }

            }
        }

        public void SendData(Message message)
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(nwStream, message);
        }

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                Message Mess = new Message() { Sender = user.TcpClient, ServerMessage = ServerMessage.RemoveUser, UserSend = username };
                Mess.SendMessage(Crypt,Mess);
            }
            catch { }
        }
    }
}
