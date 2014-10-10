using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MembershipManager.Common;
using System.Web.Security;
using System.Configuration;

namespace MembershipManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        public ConnectWindow()
        {
            InitializeComponent();

            // Load saved connections.
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            // TO-DO: Test connection.
            MembershipConnection.SetCurrent(txtServer.Text, txtDatabase.Text, txtUsername.Text, txtPassword.Text, txtApplicationName.Text);
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
