using System.Windows;
using Server.Other;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System;
using GOST_34_12_2015;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace Server.Views
{
    /// <summary>
    /// Логика окна авторизации
    /// </summary>
    public partial class AuthorizationWindow : Window , IDataErrorInfo
    {
        public string Address { get; set; } // IP адресс
        public string Port { get; set; }    // Порт
        public string Error => throw new System.NotImplementedException();
        private bool loadKey = false;
        private byte[] textBytes;
        private System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        private System.Windows.Forms.SaveFileDialog saveFileDialog1 =  new System.Windows.Forms.SaveFileDialog();
        private System.Windows.Forms.OpenFileDialog openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
        public System.Windows.Forms.RichTextBox richTextBoxOutput;
        public Kuznechik Crypt = new Kuznechik();

        /// <summary>
        /// Корректность ввода данных
        /// </summary>
        public string this[string columnName] 
        {
            get
            {
                string error = string.Empty;
                switch (columnName)
                {
                    case "Address":
                        if (!IsValidIPAddress(Address))
                        {
                            error = "Invalid IP address";
                        }
                        break;
                    case "Port":
                        if (!IsValidPort(Port))
                        {
                            error = "Invalid port number";
                        }
                        break;
                }
                return error;
            }
        }

        /// <summary>
        /// Проверяет, соответствует ли адрес маске IPv4
        /// </summary>
        /// <param name="address">Адрес</param>
        /// <returns></returns>
        public bool IsValidIPAddress(string address) => new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b").IsMatch(address);

        /// <summary>
        /// Проверяет, является ли порт допустимым двухбайтовым числом (1-65535)
        /// </summary>
        /// <param name="port">Порт</param>
        /// <returns></returns>
        public bool IsValidPort(string port) => int.TryParse(Port, out var res) && res > 0 && res < 65536;

        /// <summary>
        /// Инициализирует адрес и порт значениями из app.config или по-умолчанию
        /// </summary>
        public AuthorizationWindow()
        {
            string tmpParameter = AppConfigManager.ReadConfigParameter("ip_address");
            Address = tmpParameter != "" ? tmpParameter : "127.0.0.1";
            tmpParameter = AppConfigManager.ReadConfigParameter("port");
            Port = tmpParameter != "" ? tmpParameter : "50000";
            InitializeComponent();
  
            DataContext = this;
        }

       
            /// <summary>
            /// Запуск сервера с сохранением введенных параметров
            /// </summary>
            private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidIPAddress(Address) && IsValidPort(Port) &&  loadKey)
            {
                Address = addressTextBox.Text;
                Port = portTextBox.Text;
                AppConfigManager.UpdateConfigParameter("ip_address", Address.ToString());
                AppConfigManager.UpdateConfigParameter("port", Port.ToString());
                DialogResult = true;
            }
        }


        /// <summary>
        /// Создание ключа
        /// </summary>
        private void buttonGenerateKey_Click(object sender, RoutedEventArgs e)
        {
            
            Random r = new Random();
            byte[] randomKey = new byte[256];
            for (int i = 0; i < randomKey.Length; i++)
            {
                randomKey[i] = (byte)(r.Next(byte.MinValue, byte.MaxValue) ^ Crypt.magicString[i]);
            }
            if (writeInFile(randomKey))
            {
                Crypt.masterKey = randomKey;
                loadKey = true;

            }
        }
        public void writeInFile()
        {
            string text = Encoding.GetEncoding(1251).GetString(textBytes);
            File.WriteAllText(openFileDialog1.FileName, text, Encoding.GetEncoding(1251));
            
        }
                    
        public  bool writeInFile(byte[] value)
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
            if (readFromFile(ref encryptedKey, ref openFileDialog2, ref loadKey))
            {
                try
                {

                    Crypt.masterKey = encryptedKey;
                    loadKey = true;
                    //fileContents("Мастер-ключ: ", masterKey, masterKey.Length);

                }
                catch (Exception ex)
                {
                    loadKey = false;
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }
              
            }
        }

        private bool readFromFile(ref byte[] value, ref OpenFileDialog opf, ref bool check)
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
                        check = false;
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        return false;
                    }
                    finally
                    {
                        check = true;
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

    }
}
