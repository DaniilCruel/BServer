using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public enum ServerMessage   // ServerMessage -> Sender -> Reciever -> Messege -> List -> |
    {
        None,
        AddUser,                //
        UsersCollection,        // передача листа пользователей
        Conect,                  // запрос на соединение
        RemoveUser,             // удаление пользователя
        Message,                // сообщение
        BroadcastMessage,       // сообщение всем
        BreakConnection,        // новый пользователь
        Broadcast,              // 
        WrongUsername           // имя занято
    }
}
