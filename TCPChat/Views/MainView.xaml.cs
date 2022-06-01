using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TCPChat.Infrastructure;
using GOST_34_12_2015;
using TCPChat.ViewModels;
using TCPChat.Views;

namespace TCPChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            AppConfigManager.CreateConfigParameters("ip_address", "port");

            if (Authorization(out string address, out int port, out Kuznechik Crypt))
            {
                DataContext = new MainViewModel(address, port);
                InitializeComponent();
            }
            else
            {
                Application.Current.Shutdown();
            }
            
        }

        public bool Authorization(out string address, out int port, out Kuznechik Crypt)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            if (authorizationWindow.ShowDialog() == true)
            {
                address = authorizationWindow.Address;
                port = int.Parse(authorizationWindow.Port);
                Crypt = null;
                return true;
            }
            else
            {
                address = "";
                port = -1;
                Crypt = null;
                return false;
            }
        }
    }


}
