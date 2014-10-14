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
using System.Windows.Shapes;
using System.Web.Security;
using MembershipManager.Providers;
using MembershipManager.Common;

namespace MembershipManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            grdUsers.ItemsSource = Membership.GetAllUsers();

            grdRoles.ItemsSource = Roles.GetAllRoles();
        }

        private void User_DoubleClick(object sender, RoutedEventArgs e)
        {
            var row = (DataGridRow)sender;

            var user = (MembershipUser)row.Item;

            var userWindow = new UserWindow(user);
            userWindow.ShowDialog();
        }

        private void Role_DoubleClick(object sender, RoutedEventArgs e)
        {
            var row = (DataGridRow)sender;

            string role = (string)row.Item;

            var roleWindow = new RoleWindow(role);
            roleWindow.ShowDialog();
        }

        private void DisconnectItem_Click(object sender, RoutedEventArgs e)
        {
            MembershipConnection.Clear();
            var connectWindow = new ConnectWindow();
            connectWindow.Show();
            this.Close();
        }

        private void ExitItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void NewUserItem_Click(object sender, RoutedEventArgs e)
        {
            var userWindow = new UserWindow();
            userWindow.ShowDialog();
        }

        private void NewRoleItem_Click(object sender, RoutedEventArgs e)
        {
            var roleWindow = new RoleWindow();
            roleWindow.ShowDialog();
        } 
    } 
}
