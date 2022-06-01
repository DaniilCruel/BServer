using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;
using System.IO;
using System.Text;
using System.Threading;
using Data;
using GOST_34_12_2015;
using System.Windows;
using System.Windows.Forms;

namespace Server.ViewModels
{
    /// <summary>
    /// Модель представления, связывающая MainView и ServerTCP
    /// </summary>
    public class ServerVM : BaseVM
    {
        private Mutex clientMutex; // Для синхронизации 
        private Mutex fileMutex; // Для синхронизации 
        private Mutex keyMutex; // Для синхронизации 
        private Dispatcher dispatcher; // Для обновления свойств, на которые есть Binding
        private ServerTCP server; // Сервер
        private byte[] textBytes = null;
        private System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        private System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        private System.Windows.Forms.OpenFileDialog openFileDialog2 = new System.Windows.Forms.OpenFileDialog();

        public ObservableCollection<User> ClientList { get; set; } // Список клиентов для ServerWindow
        public ObservableCollection<FileInfo> FileList { get; set;  }         // Список файлов для ServerWindow
        public string Log { get; set; } // Лог для ServerWindow
        public string Key { get; set; } // Ключ для ServerWindow
        /// <summary>
        /// Инициализация необходимых для MainView данных
        /// </summary>
        public ServerVM(string ipAddress, int port, Kuznechik Crypt)
        {
            File.Delete("Log.txt");
            dispatcher = Dispatcher.CurrentDispatcher;
            clientMutex = new Mutex();
            fileMutex = new Mutex();
            keyMutex = new Mutex();
            server = new ServerTCP(ipAddress, port, Crypt);
            server.ClientList.CollectionChanged += ClientList_CollectionChanged;
            server.FileList.CollectionChanged += FileList_CollectionChanged;
            server.AddToLog += LogList_AddData;
            server.KeyChanged += Key_Changed;
            server.Start();
            
        }

        #region События

        /// <summary>
        /// Обновление свойства-коллекции ClientList => обновление данных в MainView
        /// </summary>
        private void ClientList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            clientMutex.WaitOne();
            ClientList = server.ClientList;
            OnPropertyChanged("ClientList");
            clientMutex.ReleaseMutex();
        }

        /// <summary>
        /// Обновление свойства Key => обновление данных в MainView
        /// </summary>
        private void Key_Changed(object sender, string str)
        {
            keyMutex.WaitOne();
            dispatcher.BeginInvoke(new Action(() => Key = str));
            OnPropertyChanged("Key");
            keyMutex.ReleaseMutex();
        }

        /// <summary>
        /// Обновление свойства-коллекции FileList => обновление данных в MainView
        /// </summary>
        private void FileList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            fileMutex.WaitOne();
            FileList = server.FileList;
            OnPropertyChanged("FileList");
            fileMutex.ReleaseMutex();
        }

        /// <summary>
        /// Обновление свойства Log => обновление данных в MainView
        /// </summary>
        private void LogList_AddData(object sender, string str)
        {
            clientMutex.WaitOne();
            string text = $"{DateTime.Now.ToLongTimeString()} {str}";
            using (StreamWriter sw = new StreamWriter("Log.txt", true, Encoding.Default))
            {
                sw.WriteLine(text);
            }
            dispatcher.BeginInvoke(new Action(() => Log += $"{text}\n"));
            OnPropertyChanged("Log");
            clientMutex.ReleaseMutex();
        }

        private void buttonGenerateKey_Click(object sender, RoutedEventArgs e)
        {

            Random r = new Random();
            byte[] randomKey = new byte[256];
            for (int i = 0; i < randomKey.Length; i++)
            {
                randomKey[i] = (byte)(r.Next(byte.MinValue, byte.MaxValue) ^ server.Crypt.magicString[i]);
            }
            if (writeInFile(randomKey))
            {
                server.Crypt.masterKey = randomKey;


            }
        }
        public void writeInFile()
        {
            string text = Encoding.GetEncoding(1251).GetString(textBytes);
            File.WriteAllText(openFileDialog1.FileName, text, Encoding.GetEncoding(1251));

        }

        public bool writeInFile(byte[] value)
        {

            Stream mystr = null;
            if (saveFileDialog1.ShowDialog().ToString() == "OK")
            {
                if ((mystr = saveFileDialog1.OpenFile()) != null)
                {
                    StreamWriter mywriter = new StreamWriter(mystr, Encoding.GetEncoding(1251));
                    try
                    {
                        mywriter.Write(Encoding.GetEncoding(1251).GetString(value));
                    }
                    catch (Exception ex)
                    {
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        return false;
                    }
                    finally
                    {
                        mywriter.Close();
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Загрузка ключа из файла
        /// </summary>
        private void buttonLoadKey_Click(object sender, RoutedEventArgs e)
        {
            byte[] encryptedKey = new byte[256];
            if (readFromFile(ref encryptedKey, ref openFileDialog2))
            {
                try
                {

                    server.Crypt.masterKey = encryptedKey;
                   
                    //fileContents("Мастер-ключ: ", masterKey, masterKey.Length);

                }
                catch (Exception ex)
                {
                
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }

            }
        }

        private bool readFromFile(ref byte[] value, ref OpenFileDialog opf)
        {
            Stream mystr = null;
            if (opf.ShowDialog().ToString() == "OK")
            {
                if ((mystr = opf.OpenFile()) != null)
                {
                    StreamReader myread = new StreamReader(mystr, Encoding.GetEncoding(1251));
                    try
                    {
                        value = Encoding.GetEncoding(1251).GetBytes(myread.ReadToEnd());
                    }
                    catch (Exception ex)
                    {
                        
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        return false;
                    }
                    finally
                    {
                        
                        myread.Close();
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            writeInFile(server.Crypt.masterKey);
        }


        #endregion События

        #region Команды

        private BaseCommand startServerCommand;
        public BaseCommand StartServerCommand => startServerCommand ??
                    (startServerCommand = new BaseCommand(obj => server.Start()));

        #endregion Команды
    }

}
