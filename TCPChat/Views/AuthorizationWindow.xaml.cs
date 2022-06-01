using System.Windows;
using TCPChat.Infrastructure;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System;
using GOST_34_12_2015;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace TCPChat.Views
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
            Port = tmpParameter != "" ? tmpParameter : "5000";
            InitializeComponent();
  
            DataContext = this;
        }

       
            /// <summary>
            /// Запуск сервера с сохранением введенных параметров
            /// </summary>
            private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidIPAddress(Address) && IsValidPort(Port) )
            {
                Address = addressTextBox.Text;
                Port = portTextBox.Text;
               
                AppConfigManager.UpdateConfigParameter("ip_address", Address.ToString());
                AppConfigManager.UpdateConfigParameter("port", Port.ToString());
                DialogResult = true;
            }
        }



    }
}
