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

namespace MembershipManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
    } 
}
