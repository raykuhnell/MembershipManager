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
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MembershipManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<MembershipConnection> Connections;

        public ConnectWindow()
        {
            InitializeComponent();

            Connections = new ObservableCollection<MembershipConnection>();
            var connectionStrings = (StringCollection)Properties.Settings.Default["Connections"];

            if (connectionStrings != null)
            {
                foreach (string cs in connectionStrings)
                {
                    Connections.Add(MembershipConnection.GetFromString(cs));
                }
            }

            DataContext = this;
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            MembershipConnection.SetCurrent(txtServer.Text, txtDatabase.Text, txtUsername.Text, txtPassword.Text, txtApplicationName.Text);
            if (MembershipConnection.GetCurrent().Test())
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("MembershipManager could not connect using the specified information.  Please try again.");
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var connectionStrings = (StringCollection)Properties.Settings.Default["Connections"];
            if (connectionStrings == null)
            {
                connectionStrings = new StringCollection();
            }

            var mc = new MembershipConnection
            {
                Name = txtApplicationName.Text,
                Server = txtServer.Text,
                Database = txtDatabase.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Text,
                ApplicationName = txtApplicationName.Text
            };

            connectionStrings.Add(mc.ToString());

            Properties.Settings.Default["Connections"] = connectionStrings;
            Properties.Settings.Default.Save();

            Connections.Add(mc);
            RaisePropertyChanged("Connections");
        }

        void RaisePropertyChanged(string prop)
        {
            if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
