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
    public partial class ConnectWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<MembershipConnection> Connections { get; set; }

        public MembershipConnection _selectedConnection;

        public MembershipConnection SelectedConnection
        {
            get
            {
                return _selectedConnection;
            }
            set
            {
                _selectedConnection = value;
                OnPropertyChanged("SelectedConnection");
            }
        }

        public ConnectWindow()
        {
            InitializeComponent();

            Connections = new ObservableCollection<MembershipConnection>();
            Connections.Add(null);

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
            SetFieldsEnabled(false);

            if (_selectedConnection == null)
            {
                MembershipConnection.SetCurrent(txtServer.Text, txtDatabase.Text, txtUsername.Text, txtPassword.Password, txtApplicationName.Text);
            }
            else
            {
                MembershipConnection.SetCurrent(_selectedConnection);
            }

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

            SetFieldsEnabled(true);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var mc = new MembershipConnection
            {
                Name = txtApplicationName.Text,
                Server = txtServer.Text,
                Database = txtDatabase.Text,
                Username = txtUsername.Text,
                Password = txtPassword.Password,
                ApplicationName = txtApplicationName.Text
            };

            Connections.Add(mc);

            UpdateConnectionSettings();

            OnPropertyChanged("Connections");
        }

        private void cboConnections_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (_selectedConnection != null)
                {
                    Connections.Remove(_selectedConnection);
                    _selectedConnection = null;
                    UpdateConnectionSettings();
                }
            }
        }

        private void UpdateConnectionSettings()
        {
            var connectionStrings = new StringCollection();

            foreach (var mc in Connections)
            {
                if (mc != null)
                {
                    connectionStrings.Add(mc.ToString());
                }
            }

            Properties.Settings.Default["Connections"] = connectionStrings;
            Properties.Settings.Default.Save();
        }

        private void SetFieldsEnabled(bool enabled)
        {
            cboConnections.IsEnabled = enabled;
            txtServer.IsEnabled = enabled;
            txtDatabase.IsEnabled = enabled;
            txtUsername.IsEnabled = enabled;
            txtPassword.IsEnabled = enabled;
            txtApplicationName.IsEnabled = enabled;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }       
        }    
    }
}
