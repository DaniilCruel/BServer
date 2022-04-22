using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data
{
    [Serializable]
    public class Message2
    {
        public User Sender { set; get; }
        public User Reciever { set; get; }
        public string MessageString { set; get; }
        public ServerMessage ServerMessage { set; get; } = ServerMessage.None;
        public ObservableCollection<User> Users { set; get; } = new ObservableCollection<User>();


    }
}
