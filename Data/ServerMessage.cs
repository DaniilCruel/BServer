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
        AddUser,                // добовление пользователя
        UsersCollection,        // передача листа пользователей
        Conect,                 // запрос на соединение
        RemoveUser,             // удаление пользователя
        Message,                // сообщение
        BreakConnection,        // разрыв соединения на строне пользователя
        Broadcast,              // сообщение всем
        WrongUsername,          // имя занято
        File                    // передача файла                  
    }
}
